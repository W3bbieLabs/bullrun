using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace MainManage
{
    public class MainManager : NetworkBehaviour
    {
        public static MainManager Instance;
        [SyncVar] public int pCount = 0;

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

        public void incrementPlayerCount()
        {
            pCount++;
        }

        public void decrementPlayerCount()
        {
            pCount--;
        }

        public int getPCount()
        {
            return pCount;
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

}
