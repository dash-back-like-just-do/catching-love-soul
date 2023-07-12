using System;
using UnityEngine;
using utils;

namespace player
{
    public class PlayerManager : MonoBehaviour
    {
        public float moveSpeed;
        private Rigidbody2D _rigidbody2D;
        public  GameObject message;
        private  GameObject _messageContainer;
        private Counter _attackCounter;
        public PlayerData playerData;
        private float _messageSpeed;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _messageContainer = new GameObject
            {
                transform =
                {
                    name = "messageContainer"
                }
            };
            _attackCounter = new Counter(playerData.attackTime);
            _messageSpeed = playerData.messageSpeed;
        }

        private void FixedUpdate()
        {
            _attackCounter.Update();
            MoveLogic();
            MessageShootLogic();
        }

        private void MessageShootLogic()
        {
            if (( Input.GetKey(KeyCode.Space) ||Input.GetMouseButton(0))&&
                _attackCounter.IsTrigger())
            {
                var newMessage = Instantiate(message, transform.position, transform.rotation,_messageContainer.transform);
                var dir = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
                newMessage.GetComponent<Rigidbody2D>().velocity = (dir.normalized)*_messageSpeed;
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