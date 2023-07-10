using System;
using UnityEngine;

namespace player
{
    public class PlayerManager : MonoBehaviour
    {
        public float moveSpeed;
        private Rigidbody2D _rigidbody2D;
        public  GameObject message;
        public  GameObject messageContainer;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            messageContainer = new GameObject();
            messageContainer.transform.name = "messageContainer";
        }

        private void FixedUpdate()
        {
            MoveLogic();
            MessageShootLogic();
        }

        private void MessageShootLogic()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                var newMwssage = Instantiate(message, transform.position, transform.rotation,messageContainer.transform);
            }
        }

        void MoveLogic()
        {
            Vector2 deltaMove = Vector2.zero;
            ;
            if (Input.GetKey(KeyCode.A))
            {
                deltaMove.x -= moveSpeed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                deltaMove.x += moveSpeed;
            }
            if (Input.GetKey(KeyCode.W))
            {
                deltaMove.y += moveSpeed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                deltaMove.y -= moveSpeed;
            }

            var position = new Vector2(transform.position.x, transform.position.y);
            _rigidbody2D.MovePosition( position + deltaMove);
        }
        
    }
}