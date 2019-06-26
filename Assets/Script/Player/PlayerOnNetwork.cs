using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace FighterAcademy
{
    public class PlayerOnNetwork : MonoBehaviourPun
    {
        // Start is called before the first frame update
        void Start()
        {
            if (!photonView.IsMine && GetComponent<PlayerMove>() != null && GetComponent<PlayerAttack>() != null)
            {
                Destroy(GetComponent<PlayerMove>());
                Destroy(GetComponent<PlayerAttack>());
            }
        }

        public static void RefreshInstance(ref PlayerOnNetwork player, PlayerOnNetwork prefabs)
        {
            var position = Vector3.zero;
            var rotation = Quaternion.identity;
            if(player != null)
            {
                position = player.transform.position;
                rotation = player.transform.rotation;
                PhotonNetwork.Destroy(player.gameObject);
            }

            player = PhotonNetwork.Instantiate(prefabs.gameObject.name, position, rotation).GetComponent<PlayerOnNetwork>();
        }
    }

}
