using UnityEngine;
using utils;

namespace player
{
    internal enum PlayerStatus
    {
        DELAY,
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
        private Counter _Delay;
        private GameObject _messageContainer;
        private float _messageSpeed;
        private PlayerFocus _playerFocus;
        private PlayerStatus _playerStatus;
        private Rigidbody2D _rigidbody2D;
        private Counter _rollCd;
        private Counter _rollTime;
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _messageContainer = new GameObject
            {
                transform =
                {
                    name = "messageContainer"
                }
            };
            _attackCounter = new Counter(playerData.attackTime);
            _rollCd = new Counter(playerData.rollCd);
            _messageSpeed = playerData.messageSpeed;
            moveSpeed = playerData.moveSpeed;
            _rollTime = new Counter(playerData.rollTime);
            _Delay = new Counter(playerData.rollDelay);
            _canMove = true;
        }

        private void FixedUpdate()
        {
            _attackCounter.Update();
            _rollCd.Update();

            MoveLogic();
            MessageShootLogic();
            UpdateStateMachine();
        }

        private void UpdateStateMachine()
        {
            if (Input.GetKey(KeyCode.Mouse1))
                _playerFocus = PlayerFocus.FOCUS;
            else
                _playerFocus = PlayerFocus.NOT_FOCUS;

            switch (_playerFocus)
            {
                case PlayerFocus.NOT_FOCUS:
                    break;
                case PlayerFocus.FOCUS:
                    break;
            }


            switch (_playerStatus)
            {
                case PlayerStatus.DELAY:
                    _canMove = false;
                    _Delay.Update();
                    _spriteRenderer.color = Color.yellow;
                    if (_Delay.IsTrigger()) _playerStatus = PlayerStatus.IDLE;
                    break;

                case PlayerStatus.HURT:
                    _canMove = true;
                    _spriteRenderer.color = Color.red;
                    //for animation
                    break;
                case PlayerStatus.IDLE:
                    //for animation
                    _spriteRenderer.color = Color.white;
                    _canMove = true;

                    RollingLogic();
                    MoveLogic();
                    break;
                case PlayerStatus.SHOOT:
                    _spriteRenderer.color = Color.blue;
                    //shoot message
                    //delay some time and go back idle
                    break;
                case PlayerStatus.ROLLING: //i think it will be a block state but can move 
                    //rolling  some time

                    _canMove = true;
                    MoveLogic();
                    _spriteRenderer.color = Color.green;
                    _rollTime.Update();
                    if (_rollTime.IsTrigger())
                    {
                        _Delay = new Counter(playerData.rollDelay);
                        _playerStatus = PlayerStatus.DELAY;
                    }

                    //move with some direction
                    //delay some time and go back idle
                    break;
                case PlayerStatus.WALKING:
                    _spriteRenderer.color = Color.grey;
                    _canMove = true;
                    RollingLogic();
                    //for animation
                    break;
            }
        }

        private void RollingLogic()
        {
            // _canHurt = true;
            if (Input.GetKey(KeyCode.Space) && _rollCd.IsTrigger())
            {
                _rollTime.Reset();
                _playerStatus = PlayerStatus.ROLLING;
            }
            // _canHurt = false;
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
            _rigidbody2D.velocity = Vector2.zero;
                return;
            }

            var deltaMove = Vector2.zero;
            if (Input.GetKey(KeyCode.A)) deltaMove.x -= moveSpeed;
            if (Input.GetKey(KeyCode.D)) deltaMove.x += moveSpeed;
            if (Input.GetKey(KeyCode.W)) deltaMove.y += moveSpeed;
            if (Input.GetKey(KeyCode.S)) deltaMove.y -= moveSpeed;

            _rigidbody2D.velocity = deltaMove;
        }
    }
}