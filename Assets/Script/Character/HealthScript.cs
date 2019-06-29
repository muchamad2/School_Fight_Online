using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace FighterAcademy
{
    public class HealthScript : MonoBehaviour
    {
        public float health = 100f;
        public float totalHealth;
        public bool isPlayer;

        float x_Death = -90f;
        float death_smooth = 0.9f;
        float rotate_Time = 0.23f;
        bool playerDead = false;

        [SerializeField]
        private Image healthBar;

        [HideInInspector]
        public bool shieldActived;

        private CharacterSoundFX soundFX;
        // Start is called before the first frame update
        private void Awake()
        {
            soundFX = GetComponentInChildren<CharacterSoundFX>();
            totalHealth = health;
        }
        private void Update()
        {
            if (playerDead)
            {
                RotateAfterDead();
            }
        }
        
        public void ApplyDamage(float damage)
        {
            if (shieldActived)
            {
                return;
            }
            health -= damage;
            if (healthBar != null)
            {
                healthBar.fillAmount = health / 100f;
            }
            if (health <= 0)
            {
                
                GetComponent<Animator>().enabled = false;
                soundFX.Die();

                StartCoroutine(AllowRotate());

                
                if (isPlayer)
                {
                    if(GetComponent<PlayerMove>() != null && GetComponent<PlayerAttack>() != null)
                    {
                        GetComponent<PlayerMove>().enabled = false;
                        GetComponent<PlayerAttack>().enabled = false;

                    }

                    Camera.main.transform.SetParent(null);
                    

                    if (GameObject.FindGameObjectWithTag(Tags.ENEMY_TAG)
                        .GetComponent<EnemyController>() != null)
                    {
                        GameObject.FindGameObjectWithTag(Tags.ENEMY_TAG)
                        .GetComponent<EnemyController>().enabled = false;
                    }
                    GameManager.Instance.Lose();
                }
                else
                {
                    
                    GetComponent<EnemyController>().enabled = false;
                    GetComponent<NavMeshAgent>().enabled = false;
                    GameManager.Instance.Win(10);
                }

                
               
            }
        }
        void RotateAfterDead()
        {
            transform.eulerAngles = new Vector3(
                Mathf.Lerp(transform.eulerAngles.x, x_Death, Time.deltaTime * death_smooth),
                transform.eulerAngles.y, transform.eulerAngles.z);
        }

        IEnumerator AllowRotate()
        {
            playerDead = true;

            yield return new WaitForSeconds(rotate_Time);

            playerDead = false;
        }
    }

}
