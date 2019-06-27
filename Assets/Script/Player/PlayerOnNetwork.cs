using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace FighterAcademy
{
    public class PlayerOnNetwork : MonoBehaviourPun, IPunObservable
    {
        [SerializeField]
        #region PlayerStats Controller
        private HealthScript healthCondition;
        private PlayerMove moveControll;
        private PlayerAttack attackControll;
        #endregion

        public static GameObject localPlayer;
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
            if (!photonView.IsMine && GetComponent<PlayerMove>() != null && GetComponent<PlayerAttack>() != null)
            {
                Destroy(GetComponent<PlayerMove>());
                Destroy(GetComponent<PlayerAttack>());
                GetComponentInChildren<Camera>().gameObject.SetActive(false);
                gameObject.layer = 10;
            }

            healthCondition = GetComponent<HealthScript>();
            moveControll = GetComponent<PlayerMove>();
            attackControll = GetComponent<PlayerAttack>();

#if UNITY_5_4_OR_NEWER
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
#endif
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(healthCondition.health);
                stream.SendNext(moveControll.moveDirection);
                stream.SendNext(moveControll.rotation_direction);
                stream.SendNext(attackControll.isAttack);
                stream.SendNext(attackControll.attackPoint.activeSelf);
            }
            else
            {
                healthCondition.health = (float)stream.ReceiveNext();
                moveControll.moveDirection = (Vector3)stream.ReceiveNext();
                moveControll.rotation_direction = (Vector3)stream.ReceiveNext();
                attackControll.isAttack = (bool)stream.ReceiveNext();
                attackControll.attackPoint.SetActive((bool)stream.ReceiveNext());
            }
        }

#if UNITY_5_4_OR_NEWER
        void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadingMode)
        {
            this.CalledOnLevelWasLoaded(scene.buildIndex);
        }

        void CalledOnLevelWasLoaded(int level)
        {
            if(moveControll.joystick == null)
            {
                moveControll.joystick = FindObjectOfType<Joystick>();
            }
            if(attackControll.joyButton == null)
            {
                attackControll.joyButton = FindObjectOfType<JoystickButton>();
            }
            
        }
#endif
    }

}
