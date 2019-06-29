using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace FighterAcademy
{
    public class PlayerOnNetwork : MonoBehaviourPun, IPunObservable
    {
        
        #region PlayerStats Controller
        public HealthScript healthCondition;
        private PlayerMove moveControll;
        [SerializeField]
        private PlayerAttack attackControll;
        #endregion

        public static GameObject localPlayer;

        public GameObject playerUiPrefb;
        // Start is called before the first frame update
        private void Awake()
        {
            if (photonView.IsMine)
            {
                PlayerOnNetwork.localPlayer = this.gameObject;
            }
            DontDestroyOnLoad(this.gameObject);
        }
        void Start()
        {
            healthCondition = GetComponent<HealthScript>();
            moveControll = GetComponent<PlayerMove>();
            attackControll = GetComponent<PlayerAttack>();
            if (!photonView.IsMine && GetComponent<PlayerMove>() != null && GetComponent<PlayerAttack>() != null)
            {
                Destroy(GetComponent<PlayerMove>());
                Destroy(GetComponent<PlayerAttack>());
                GetComponentInChildren<Camera>().gameObject.SetActive(false);
                gameObject.layer = 10;
                gameObject.tag = Tags.ENEMY_TAG;
                if (healthCondition != null)
                    healthCondition.isPlayer = false;
            }




            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;

            if(playerUiPrefb != null)
            {
                GameObject _ui = Instantiate(playerUiPrefb);
                _ui.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
                //_ui.GetComponent<PlayerUI>().SetTarget(this);
            }
            else
            {
                Debug.LogWarning("<Color=Red><a>Missing</a></Color> PlayerUiPrefab reference on player Prefab.", this);
            }
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                if(healthCondition != null)
                {
                    stream.SendNext(healthCondition.health);
                }
                if(moveControll != null)
                {
                    stream.SendNext(moveControll.moveDirection.x);
                    stream.SendNext(moveControll.moveDirection.y);
                    stream.SendNext(moveControll.moveDirection.z);
                    stream.SendNext(moveControll.rotation_direction.x);
                    stream.SendNext(moveControll.rotation_direction.y);
                    stream.SendNext(moveControll.rotation_direction.z);

                }
                if (attackControll != null)
                {
                    stream.SendNext(attackControll.isAttack);
                    stream.SendNext(attackControll.attackPoint.activeSelf);
                }

            }
            else
            {
                if(healthCondition != null)
                {
                    healthCondition.health = (float)stream.ReceiveNext();
                }

                if(moveControll != null)
                {
                    moveControll.moveDirection.x = (float)stream.ReceiveNext();
                    moveControll.moveDirection.y = (float)stream.ReceiveNext();
                    moveControll.moveDirection.z = (float)stream.ReceiveNext();
                    moveControll.rotation_direction.x = (float)stream.ReceiveNext();
                    moveControll.rotation_direction.y = (float)stream.ReceiveNext();
                    moveControll.rotation_direction.z = (float)stream.ReceiveNext();
                }

                if(attackControll != null)
                {
                    attackControll.isAttack = (bool)stream.ReceiveNext();
                    attackControll.attackPoint.SetActive((bool)stream.ReceiveNext());
                }

            }
        }


        void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadingMode)
        {
            this.CalledOnLevelWasLoaded(scene.buildIndex);
        }

        void CalledOnLevelWasLoaded(int level)
        {
            if (photonView.IsMine)
            {
                if (moveControll.joystick == null)
                {
                    moveControll.joystick = FindObjectOfType<Joystick>();
                }

                if (attackControll.joyButton == null)
                {
                    attackControll.joyButton = FindObjectOfType<JoystickButton>();
                }

                if (playerUiPrefb != null)
                {
                    GameObject _ui = Instantiate(playerUiPrefb);
                    _ui.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
                    //_ui.GetComponent<PlayerUI>().SetTarget(this);
                }
            }
            else
            {
                if (playerUiPrefb != null)
                {
                    GameObject _ui = Instantiate(playerUiPrefb);
                    _ui.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
                    //_ui.GetComponent<PlayerUI>().SetTarget(this);
                }
            }
        }

        public void OnHit(float Damage)
        {
            photonView.RPC("ApplyDamage", RpcTarget.All, Damage);
        }

        [PunRPC]
        void ApplyDamage(float Damage,PhotonMessageInfo info)
        {
            healthCondition.ApplyDamage(Damage);
            Debug.Log(info);
        }
    }

}
