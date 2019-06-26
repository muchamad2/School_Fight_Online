using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FighterAcademy
{
    public class PlayerAttack : MonoBehaviour
    {
        CharacterAnimation playerAnimation;
        public GameObject attackPoint;
        private PlayerShield shield;
        private CharacterSoundFX soundFX;

        protected JoystickButton joyButton;
        // Start is called before the first frame update
        void Awake()
        {
            playerAnimation = GetComponent<CharacterAnimation>();
            shield = GetComponent<PlayerShield>();
            soundFX = GetComponentInChildren<CharacterSoundFX>();
            joyButton = FindObjectOfType<JoystickButton>();
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
            if (Input.GetKeyDown(KeyCode.K) || joyButton.isPressed)
            {
                if (Random.Range(0, 2) > 0)
                {
                    playerAnimation.Attack_1();
                    soundFX.Attack_1();
                }
                else
                {
                    playerAnimation.Attack_2();
                    soundFX.Attack_2();
                }
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
    }
}

