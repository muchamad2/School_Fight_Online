using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    CharacterAnimation playerAnimation;
    // Start is called before the first frame update
    void Awake()
    {
        playerAnimation = GetComponent<CharacterAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            playerAnimation.Defend(true);
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            playerAnimation.Defend(false);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if(Random.Range(0f,2f) > 0)
            {
                playerAnimation.Attack_1();
            }
            else
            {
                playerAnimation.Attack_2();
            }
        }
    }
}
