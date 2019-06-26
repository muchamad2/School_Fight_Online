using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    CharacterController charController;
    CharacterAnimation playerAnimation;

    public float movement_speed = 3f;
    public float gravity = 9.8f;
    public float rotation_speed = 0.15f;
    public float rotateDegressPerSecond = 180f;
    // Start is called before the first frame update
    void Awake()
    {
        charController = GetComponent<CharacterController>();
        playerAnimation = GetComponent<CharacterAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        AnimatateWalk();
    }
    void Move()
    {
        if(Input.GetAxis("Vertical") > 0)
        {
            Vector3 moveDirection = transform.forward;
            moveDirection.y -= gravity * Time.deltaTime;

            charController.Move(moveDirection * movement_speed * Time.deltaTime);
        }else if(Input.GetAxis("Vertical") < 0)
        {
            Vector3 moveDirection = -transform.forward;
            moveDirection.y -= gravity * Time.deltaTime;

            charController.Move(moveDirection * movement_speed * Time.deltaTime);

        }
        else
        {
            charController.Move(Vector3.zero);
        }
        rotate();
    }
    void rotate()
    {
        Vector3 rotation_direction = Vector3.zero;

        if(Input.GetAxis("Horizontal") < 0)
        {
            rotation_direction = transform.TransformDirection(Vector3.left);
        }
        if(Input.GetAxis("Horizontal") > 0)
        {
            rotation_direction = transform.TransformDirection(Vector3.right);
        }

        if(rotation_direction != Vector3.zero)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(rotation_direction),
                rotateDegressPerSecond * Time.deltaTime);
        }
    }

    void AnimatateWalk()
    {
        if(charController.velocity.sqrMagnitude != 0)
        {
            playerAnimation.Walk(true);
        }
        else
        {
            playerAnimation.Walk(false);
        }
    }
}
