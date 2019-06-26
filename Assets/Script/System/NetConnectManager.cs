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
            PhotonNetwork.NickName = "Player Name";
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
            SceneManager.LoadScene("Arena");
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
    }
}

