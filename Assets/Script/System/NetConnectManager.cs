using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using UnityEngine.SceneManagement;

namespace FighterAcademy
{
    public class NetConnectManager : MonoBehaviourPunCallbacks
    {
        public Button BtnConnectMaster;
        public Button BtnConnectRoom;

        private bool TrisToConnectToMaster;
        private bool TrisToConnectToRoom;
        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(gameObject);
            TrisToConnectToMaster = false;
            TrisToConnectToRoom = false;
        }

        // Update is called once per frame
        void Update()
        {
            if(BtnConnectMaster != null)
                BtnConnectMaster.gameObject.SetActive(!PhotonNetwork.IsConnected && !TrisToConnectToMaster);

            if(BtnConnectRoom != null)
                BtnConnectRoom.gameObject.SetActive(PhotonNetwork.IsConnected && !TrisToConnectToRoom && !TrisToConnectToMaster);
        }

        public void OnClickConnectToMaster()
        {
            PhotonNetwork.OfflineMode = false;
            PhotonNetwork.NickName = "PlayerName";
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.GameVersion = "v1";

            TrisToConnectToMaster = true;
            PhotonNetwork.ConnectUsingSettings();
        }

        public void OnClickConnectToRoom()
        {
            if (!PhotonNetwork.IsConnected)
                return;

            TrisToConnectToRoom = true;
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);
            TrisToConnectToMaster = false;
            TrisToConnectToRoom = false;
            Debug.Log(cause);
        }
        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            TrisToConnectToMaster = false;
            Debug.Log("Connected to master");
            
        }
        //dipanggil ketika memasuki room
        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            TrisToConnectToRoom = false;
            Debug.Log("Master: " + PhotonNetwork.IsMasterClient + " | Players in room : " + PhotonNetwork.CurrentRoom.PlayerCount + 
                "Room name : " + PhotonNetwork.CurrentRoom.Name);

            if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                PhotonNetwork.LoadLevel("SinglePlayer");
            }
            
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            Debug.LogFormat("OnPlayerEnterdRoom {0}", newPlayer.NickName);

            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerEnterdRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient);

                loadLevel();
            }
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);
            Debug.LogFormat("OnPlayerLeftRoom {0}", otherPlayer.NickName);

            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerLeftRoom isMasterClient {0}", PhotonNetwork.IsMasterClient);

                loadLevel();
            }
        }

        //membuat room baru
        public override void OnCreatedRoom()
        {
            base.OnCreatedRoom();
        }
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            base.OnJoinRandomFailed(returnCode, message);
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 3 });
        }
        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            base.OnCreateRoomFailed(returnCode, message);
            Debug.Log(message);
            TrisToConnectToRoom = false;
            
        }

        void loadLevel()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("Photonnetwork: trying to load a level but we are not master client");
            }
            PhotonNetwork.LoadLevel("Multiplayer");
        }
    }
}

