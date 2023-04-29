using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

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

    [SerializeField] float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
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
        //Vector3 newPos = new Vector3(-25, 0.5f, 0);
        // set start position based on something...
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

        transform.Translate(Vector3.forward * Time.deltaTime * speed * Random.value);
    }

    // Command means this is being called from the client to the server
    [Command]
    public void CmdfinishRace(float id)
    {
        // RPC means the server is sending messages to the clients
        RpcFinish(id);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!isLocalPlayer) return;

        // If this is the server send a message to other clients letting them know someone finshed
        if (other.gameObject.name == "Finish")
        {
            CmdfinishRace(player_name_val);
            speed = 0;
            anim.SetBool("isDancing", true);
        }

        if (!isLocalPlayer)
        {
            return;
        }

        isOnGround = true;
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "Ground")
        {
            anim.SetBool("isJumping", false);
        }

        /*
        else if (other.gameObject.name == "Finish")
        {
            
        }
        */

        /*
        if (other.gameObject.name == "Player_Y_Bot")
        {
            Debug.Log("Touching Player");
        }
        */
    }

    [ClientRpc]
    public void RpcFinish(float playerID)
    {
        Debug.Log(playerID);
    }
}
