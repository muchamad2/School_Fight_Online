using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace FighterAcademy
{
    public class PlayerAttack : MonoBehaviour,IPunObservable
    {
        CharacterAnimation playerAnimation;
        public GameObject attackPoint;
        private PlayerShield shield;
        private CharacterSoundFX soundFX;

        public bool isAttack;

        public JoystickButton joyButton;
        // Start is called before the first frame update
        void Awake()
        {
            playerAnimation = GetComponent<CharacterAnimation>();
            shield = GetComponent<PlayerShield>();
            soundFX = GetComponentInChildren<CharacterSoundFX>();
            
        }
        private void Start()
        {
            joyButton = FindObjectOfType<JoystickButton>();
            isAttack = false;
        }
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                playerAnimation.Defend(true);
                shield.ActiveShield(true);
            }
            if (Input.GetKeyUp(KeyCode.J))
            {
                playerAnimation.UnFreezeAnimation();
                playerAnimation.Defend(false);

                shield.ActiveShield(false);
            }
            if (Input.GetKeyDown(KeyCode.K) || joyButton.isPressed && isAttack == false)
            {
                if (Random.Range(0, 2) > 0)
                {
                    playerAnimation.Attack_1();
                    isAttack = true;
                    soundFX.Attack_1();
                }
                else
                {
                    playerAnimation.Attack_2();
                    isAttack = true;
                    soundFX.Attack_2();
                }
            }
            else
            {
                isAttack = false;
            }
        }

        void ActivateAttack()
        {
            attackPoint.SetActive(true);
        }
        void DeactivateAttack()
        {
            if (attackPoint.activeInHierarchy)
            {
                attackPoint.SetActive(false);
                
            }

        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(isAttack);
                stream.SendNext(attackPoint.activeSelf);
            }
            else
            {
                isAttack = (bool)stream.ReceiveNext();
                attackPoint.SetActive((bool)stream.ReceiveNext());
            }
        }
    }
}

