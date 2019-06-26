using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

namespace FighterAcademy
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        [Header("Player Prefabs")]
        public PlayerOnNetwork playerPrefabs;

        [HideInInspector]
        public PlayerOnNetwork localPlayer;
        // Start is called before the first frame update
        void Awake()
        {
            if (!PhotonNetwork.IsConnected)
            {
                SceneManager.LoadScene("Menu");
                return;
            }
        }
        private void Start()
        {
            PlayerOnNetwork.RefreshInstance(ref localPlayer, playerPrefabs);
        }
        // Update is called once per frame
        void Update()
        {

        }
    }

}
