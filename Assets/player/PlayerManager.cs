using UnityEngine;
using utils;

namespace player
{
    internal enum PlayerStatus
    {
        IDLE,
        ROLLING,
        WALKING,
        SHOOT,
        HURT
    }

    internal enum PlayerFocus
    {
        FOCUS,
        NOT_FOCUS
    }

    public class PlayerManager : MonoBehaviour
    {
        public float moveSpeed;
        public GameObject message;
        public PlayerData playerData;
        private Counter _attackCounter;
        private bool _canMove;
        private GameObject _messageContainer;
        private float _messageSpeed;
        private PlayerFocus _playerFocus;
        private PlayerStatus _playerStatus;
        private Rigidbody2D _rigidbody2D;

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
            moveSpeed = playerData.moveSpeed;
            _canMove = true;
        }

        private void FixedUpdate()
        {
            _attackCounter.Update();
            MoveLogic();
            MessageShootLogic();
            UpdateStateMachine();
        }

        private void UpdateStateMachine()
        {
            switch (_playerFocus)
            {
                case PlayerFocus.NOT_FOCUS:
                    switch (_playerStatus)
                    {
                    }

                    break;
                case PlayerFocus.FOCUS:
                    switch (_playerStatus)
                    {
                    }

                    break;
            }

            switch (_playerStatus)
            {
                case PlayerStatus.HURT:
                    break;
                case PlayerStatus.IDLE:
                    break;
                case PlayerStatus.SHOOT:
                    break;
                case PlayerStatus.ROLLING:
                    break;
                case PlayerStatus.WALKING:
                    break;
            }
        }

        private void MessageShootLogic()
        {
            if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) &&
                _attackCounter.IsTrigger())
            {
                var newMessage = Instantiate(message, transform.position, transform.rotation,
                    _messageContainer.transform);
                var dir = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
                newMessage.GetComponent<Rigidbody2D>().velocity = dir.normalized * _messageSpeed;
            }
        }

        private void MoveLogic()
        {
            if (!_canMove)
            {
                _rigidbody2D.MovePosition(Vector2.zero);
                return;
            }

            var deltaMove = Vector2.zero;
            ;
            if (Input.GetKey(KeyCode.A)) deltaMove.x -= moveSpeed;
            if (Input.GetKey(KeyCode.D)) deltaMove.x += moveSpeed;
            if (Input.GetKey(KeyCode.W)) deltaMove.y += moveSpeed;
            if (Input.GetKey(KeyCode.S)) deltaMove.y -= moveSpeed;

            _rigidbody2D.velocity = deltaMove;
        }
    }
}