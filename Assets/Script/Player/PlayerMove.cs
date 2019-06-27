using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace FighterAcademy
{
    public class PlayerMove : MonoBehaviour, IPunObservable
    {
        CharacterController charController;

        [SerializeField]
        CharacterAnimation playerAnimation;

        [SerializeField]
        public Joystick joystick;

        public float movement_speed = 3f;
        public float gravity = 9.8f;
        public float rotation_speed = 0.15f;
        public float rotateDegressPerSecond = 180f;

        public Vector3 moveDirection;
        public Vector3 rotation_direction;
        // Start is called before the first frame update
        void Awake()
        {
            charController = GetComponent<CharacterController>();
            playerAnimation = GetComponent<CharacterAnimation>();
            
        }

        private void Start()
        {
            joystick = FindObjectOfType<Joystick>();
        }

        // Update is called once per frame
        void Update()
        {
            Move();
            AnimatateWalk();
        }
        void Move()
        {
            
            if (Input.GetAxis("Vertical") > 0 || joystick.Vertical > 0)
            {
                moveDirection = transform.forward;
                moveDirection.y -= gravity * Time.deltaTime;

                charController.Move(moveDirection * movement_speed * Time.deltaTime);
            }
            else if (Input.GetAxis("Vertical") < 0 || joystick.Vertical < 0)
            {
                moveDirection = -transform.forward;
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
            rotation_direction = Vector3.zero;

            if (Input.GetAxis("Horizontal") < 0 || joystick.Horizontal < 0)
            {
                rotation_direction = transform.TransformDirection(Vector3.left);
            }
            if (Input.GetAxis("Horizontal") > 0 || joystick.Horizontal > 0)
            {
                rotation_direction = transform.TransformDirection(Vector3.right);
            }

            if (rotation_direction != Vector3.zero)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(rotation_direction),
                    rotateDegressPerSecond * Time.deltaTime);
            }
        }

        void AnimatateWalk()
        {
            if (charController.velocity.sqrMagnitude != 0)
            {
                playerAnimation.Walk(true);
            }
            else
            {
                playerAnimation.Walk(false);
            }
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(moveDirection);
                stream.SendNext(rotation_direction);
            }
            else
            {
                this.moveDirection = (Vector3)stream.ReceiveNext();
                this.rotation_direction = (Vector3)stream.ReceiveNext();
            }
        }
    }

}
