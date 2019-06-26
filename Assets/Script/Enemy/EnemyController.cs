using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FighterAcademy
{
    public enum EnemyState
    {
        CHASE,
        ATTACK
    }
    public class EnemyController : MonoBehaviour
    {
        CharacterAnimation enemy_anim;
        NavMeshAgent navAgent;

        Transform playerTarget;

        public float move_speed = 3.5f;
        public float attack_Distance = 1f;
        public float chase_player_after_attack_distance = 1f;

        float wait_before_Attack = 3f;
        float attack_timer;

        EnemyState enemy_State;

        public GameObject attackPoint;

        // Start is called before the first frame update
        void Awake()
        {
            enemy_anim = GetComponent<CharacterAnimation>();
            navAgent = GetComponent<NavMeshAgent>();

            playerTarget = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).transform;
        }
        private void Start()
        {
            enemy_State = EnemyState.CHASE;

            attack_timer = wait_before_Attack;
        }
        // Update is called once per frame
        void Update()
        {
            if (enemy_State == EnemyState.CHASE)
            {
                ChasePlayer();
            }
            if (enemy_State == EnemyState.ATTACK)
            {
                AttackPlayer();
            }
        }

        void ChasePlayer()
        {
            navAgent.SetDestination(playerTarget.position);
            navAgent.speed = move_speed;

            if (navAgent.velocity.sqrMagnitude == 0)
            {
                enemy_anim.Walk(false);
            }
            else
            {
                enemy_anim.Walk(true);
            }
            if (Vector3.Distance(transform.position, playerTarget.position) <= attack_Distance)
            {
                enemy_State = EnemyState.ATTACK;
            }
        }
        void AttackPlayer()
        {
            navAgent.velocity = Vector3.zero;
            navAgent.isStopped = true;

            enemy_anim.Walk(false);

            attack_timer += Time.deltaTime;

            if (attack_timer > wait_before_Attack)
            {
                if (Random.Range(0, 2) > 0)
                {
                    enemy_anim.Attack_1();
                }
                else
                {
                    enemy_anim.Attack_2();
                }
                attack_timer = 0f;
            }

            if (Vector3.Distance(transform.position, playerTarget.position) >
                attack_Distance + chase_player_after_attack_distance)
            {
                navAgent.isStopped = false;
                enemy_State = EnemyState.CHASE;
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
