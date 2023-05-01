using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UIElements;

public class PlayerController : NetworkBehaviour
{
    private Rigidbody playerRB;
    public float jumpForce;
    public float gravityModifier;

    public bool isOnGround = true;

    private float player_name_val;

    [SerializeField] Camera mainCam;

    [SerializeField] Vector3 offset;

    [SerializeField] Animator anim;

    [SerializeField] GameObject finishPrefab;

    [SerializeField] Vector3 finishPosition;

    [SerializeField] Quaternion finishRotation;

    [SerializeField] TextMesh playerMsg;

    [SerializeField] float speed = 1.0f;

    VisualElement root;

    GameObject UIObject;

    Label gameMessage;

    //[SyncVar(hook = nameof(SetRaceState))]
    //bool raceState = false;

    [SyncVar] public bool raceState = false;

    // Start is called before the first frame update

    private void Update()
    {
        //Debug.Log("STATE: " + raceState);
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        UIObject = GameObject.FindGameObjectWithTag("UIDoc");
        root = UIObject.GetComponent<UIDocument>().rootVisualElement;
        VisualElement container = root.Q<VisualElement>("container");
        container.visible = false;
        gameMessage = container.Q<Label>("message");
    }

    void Start()
    {
        if (isServer)
        {
            /*
            Debug.Log("Server Started");
            GameObject finishLine = Instantiate(finishPrefab, finishPosition, finishRotation);
            NetworkServer.Spawn(finishLine);
            */
        }

        if (!isLocalPlayer)
        {
            // Disable cam if not local player
            mainCam.gameObject.SetActive(false);
            return;
        }

        player_name_val = Random.value;
        Debug.Log("My Value " + player_name_val);
        playerRB = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    private void LateUpdate()
    {
        if (!isLocalPlayer) return;
        mainCam.transform.position = transform.position + offset;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLocalPlayer) return;

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            anim.SetBool("isJumping", true);
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            Debug.Log("Jump");
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            //resetPosition();
        }

        transform.Translate(Vector3.forward * Time.deltaTime * speed * Random.value);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!isLocalPlayer) return;

        if (other.gameObject.name == "Ground")
        {
            anim.SetBool("isJumping", false);
            isOnGround = true;
        }
        else if (other.gameObject.name == "FinishLine")
        {
            CmdfinishRace(player_name_val);
        }
    }

    [Command]
    public void CmdfinishRace(float id)
    {
        // RPC means the server is sending messages to the clients
        RpcFinish(id);
    }

    [ClientRpc]
    public void RpcFinish(float playerID)
    {
        Debug.Log("Someone Finished " + playerID);
        //playerMsg.text = "Done";
        gameMessage.text = playerID + " Won!";
        root.Q<VisualElement>("container").visible = true;
    }

    public void resetPlayer()
    {
        Debug.Log("reset position");
        speed = 0;
        transform.position = new Vector3(0.0f, 0.5f, 0.0f);
    }


    /*
    [ServerCallback]
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        RpcFinish(player_name_val);
        //raceState = true;
    }
    */


    /*
    [ClientRpc]
    public void RpcFinish(float playerID)
    {
        Debug.Log("Someone Finished " + playerID);
        playerMsg.text = "Done";
        root.Q<VisualElement>("container").visible = true;
    }
    */


    /*
    private void OnCollisionEnter(Collision other)
    {
        ///if (!isLocalPlayer) return;

        isOnGround = true;
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "Ground")
        {
            anim.SetBool("isJumping", false);
        }
        else if (other.gameObject.name == "Finish")
        {
            // If this is the server send a message to other clients letting them know someone finshed
            CmdfinishRace(player_name_val);
            anim.SetBool("isDancing", true);
            speed = 0;
            //raceState = true;
            //StartCoroutine(prepareForNextRound());
        }
    }*/

    /*
    void SetRaceState(bool oldVal, bool newVal)
    {
        Debug.Log(oldVal + " : " + newVal);
        // give user instructions here
    }*/

    // Command means this is being called from the client to the server

    /*


    [Command]
    public void CmdStartCountDown()
    {

    }



    [ClientRpc]
    public void RpcFinish(float playerID)
    {
        Debug.Log(playerID);
    }

    IEnumerator prepareForNextRound()
    {
        yield return new WaitForSeconds(3);
        transform.position = new Vector3(0.0f, 0.5f, 0.0f);
    }
    */
}
