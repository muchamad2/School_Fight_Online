using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

namespace FighterAcademy
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        #region PlayerPrefabs LocalPlayer
        [Header("Player Prefabs")]
        public PlayerOnNetwork playerPrefabs;

        [HideInInspector]
        public PlayerOnNetwork localPlayer;
        #endregion



        #region Singeleton
        public static GameManager Instance;

        void Start()
        {
            if(Instance == null)
            {
                Instance = this;
                
            }
            
            if(playerPrefabs != null)
            {
                var position = playerPrefabs.transform.position;
                if(PlayerOnNetwork.localPlayer == null)
                {
                    PhotonNetwork.Instantiate(this.playerPrefabs.name, position, Quaternion.identity, 0);
                }
                
            }

        }
        // Start is called before the first frame update
        #endregion

        void Awake()
        {
            if (!PhotonNetwork.IsConnected)
            {
                SceneManager.LoadScene("Menu");
                return;
            }
        }
        public void Win(long score)
        {
            
            PlayGamesController.Instance.PostLeaderBoards(score);
            LeaveRoom();
        }
        public void Lose()
        {
            LeaveRoom();
        }
        
        public void LeaveRoom()
        {
            PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
            PhotonNetwork.LeaveRoom();
        }
    }

}
