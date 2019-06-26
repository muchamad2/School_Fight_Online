using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FighterAcademy
{
    public class PlayerShield : MonoBehaviour
    {
        HealthScript healthScript;

        // Start is called before the first frame update
        void Awake()
        {
            healthScript = GetComponent<HealthScript>();
        }

        public void ActiveShield(bool shieldActive)
        {
            healthScript.shieldActived = shieldActive;
        }
    }

}
