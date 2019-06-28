using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FighterAcademy
{
    public class AttackDamage : MonoBehaviour
    {
        public LayerMask layer;
        public float radius = 1f;
        public float damage = 10f;
        // Update is called once per frame
        void Update()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, radius, layer);
            if (hits.Length > 0)
            {
                Debug.Log("Hit damage");
                hits[0].GetComponent<PlayerOnNetwork>().OnHit(damage);
                gameObject.SetActive(false);
            }
        }
    }

}
