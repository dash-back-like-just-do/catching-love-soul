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
        public GameObject message;
        public PlayerData playerData;

        private Counter _attackCounter;

        private bool _canMove;
        private Counter _delay;
        private Vector2 _faceDir;

        private Counter _hurtTimer;
        private GameObject _messageContainer;
        private float _messageSpeed;
        private PlayerFocus _playerFocus;
        private PlayerStatus _playerStatus;
        private Rigidbody2D _rigidbody2D;
        private Counter _rollCd;

        private Vector2 _rollDir;
        private Counter _rollTime;

        private SpriteRenderer _spriteRenderer;

        // private bool _canHurt;
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
            _rollTime = new Counter(playerData.rollTime);
            _delay = new Counter(playerData.rollDelay);
            _canMove = true;
            _faceDir = Vector2.zero;
        }

        private void FixedUpdate()
        {
            _attackCounter.Update();
            _rollCd.Update();
            UpdateFaceDir();

            MoveLogic();
            MessageShootLogic();
            UpdateStateMachine();
        }

        private void UpdateFaceDir()
        {
            var tmpDir = Vector2.zero;
            if (Input.GetKey(KeyCode.A)) tmpDir += Vector2.left;
            if (Input.GetKey(KeyCode.D)) tmpDir += Vector2.right;
            if (Input.GetKey(KeyCode.W)) tmpDir += Vector2.up;
            if (Input.GetKey(KeyCode.S)) tmpDir += Vector2.down;
            if (tmpDir != Vector2.zero) _faceDir = tmpDir;
            // [art]sprite change 
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
                    _delay.Update();
                    _spriteRenderer.color = Color.yellow;
                    if (_delay.IsTrigger()) _playerStatus = PlayerStatus.IDLE;
                    break;

                case PlayerStatus.HURT:
                    _canMove = true;
                    _spriteRenderer.color = Color.red;
                    _hurtTimer.Update();
                    if (_hurtTimer.IsTrigger()) _playerStatus = PlayerStatus.IDLE;
                    //for animation
                    break;
                case PlayerStatus.IDLE:
                    //for animation
                    _spriteRenderer.color = Color.white;
                    _canMove = true;

                    RollingLogic();
                    //tmp code
                    if (Input.GetKey(KeyCode.H))
                    {
                        _playerStatus = PlayerStatus.HURT;
                        _hurtTimer = new Counter(1);
                    }

                    break;
                case PlayerStatus.SHOOT:
                    _spriteRenderer.color = Color.blue;
                    //shoot message
                    //delay some time and go back idle
                    break;
                case PlayerStatus.ROLLING: //i think it will be a block state but can move 
                    //rolling  some time

                    _canMove = true;
                    _spriteRenderer.color = Color.green;
                    _rollTime.Update();
                    if (_rollTime.IsTrigger())
                    {
                        _delay = new Counter(playerData.rollDelay);
                        _playerStatus = PlayerStatus.DELAY;
                        _rollCd.Reset(playerData.rollCd);
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
                _rollTime.Reset(playerData.rollTime);
                _rollDir = _faceDir.normalized;
                _playerStatus = PlayerStatus.ROLLING;
            }
            // _canHurt = false;
        }

        private void MessageShootLogic()
        {
            if (Input.GetMouseButton(0) &&
                _attackCounter.IsTrigger())
            {
                _attackCounter.Reset(playerData.attackTime);
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

            if (_playerStatus == PlayerStatus.ROLLING)
            {
                _rigidbody2D.velocity = _rollDir * playerData.rollSpeed;
                return;
            }

            var deltaMove = Vector2.zero;
            var moveSpeed = playerData.moveSpeed;
            if (Input.GetKey(KeyCode.A)) deltaMove.x -= moveSpeed;
            if (Input.GetKey(KeyCode.D)) deltaMove.x += moveSpeed;
            if (Input.GetKey(KeyCode.W)) deltaMove.y += moveSpeed;
            if (Input.GetKey(KeyCode.S)) deltaMove.y -= moveSpeed;

            _rigidbody2D.velocity = deltaMove;
        }
    }
}