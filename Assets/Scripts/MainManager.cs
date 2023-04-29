using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MainManager : NetworkBehaviour
{
    public static MainManager Instance;

    void Awake()
    {
        Debug.Log("manager");
        if (!isLocalPlayer)
            return;

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }




    /*
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    */
}
