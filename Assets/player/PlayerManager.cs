using System;
using System.Collections.Generic;
using ChessClub;
using UnityEngine;
using utils;

namespace player
{
    internal enum PlayerStatus
    {
        DELAY,
        IDLE,
        ROLLING,
        SHOOT,
        HURT
    }

    internal enum PlayerFocus
    {
        FOCUS,
        NOT_FOCUS
    }

    public class PlayerManager : LoveGameObject
    {
        public GameObject message;
        public PlayerData playerData;

        private Counter _attackCounter;

        private Camera _camera;
        private Vector2 _cameraVelocity;

        private bool _canMove;
        private Counter _delay;

        private Counter _hurtTimer;
        private GameObject _messageContainer;
        private float _messageSpeed;
        private playerAnimation _playerAnimation;
        private PlayerFocus _playerFocus;
        private PlayerStatus _playerStatus;
        private Rigidbody2D _rigidbody2D;
        private Counter _rollCd;
        private Dictionary<Vector2, Counter> _inputDirs;

        private Vector2 _rollDir;
        private Counter _rollTime;

        private SpriteRenderer _spriteRenderer;

        // private bool _canHurt;
        private void Start()
        {
            _camera = Camera.main;
            _playerAnimation = transform.GetComponentInChildren<playerAnimation>();
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
            _inputDirs = new Dictionary<Vector2, Counter>();
            _inputDirs.Add(Vector2.up, new Counter(playerData.dashDirKeyTime));
            _inputDirs.Add(Vector2.down,new Counter(playerData.dashDirKeyTime));
            _inputDirs.Add(Vector2.left,new Counter(playerData.dashDirKeyTime));
            _inputDirs.Add(Vector2.right,new Counter(playerData.dashDirKeyTime));
        }

        private void FixedUpdate()
        {
            _attackCounter.Update();
            _rollCd.Update();
            foreach (var k in _inputDirs)
            {
                k.Value.Update();
            }
            UpdateFaceDir();

            MoveLogic();
            MessageShootLogic();
            UpdateStateMachine();
            MoveCamera();
        }

        private void MoveCamera()
        {
            var position = _camera.transform.position;
            var cameraNewPosition =
                Vector2.SmoothDamp(
                    position,
                    transform.position,
                    ref _cameraVelocity,
                    playerData.cameraFollowTime);
            position = new Vector3(cameraNewPosition.x, cameraNewPosition.y, position.z);
            _camera.transform.position = position;
        }

        private void UpdateFaceDir()
        {
            if (Input.GetKey(KeyCode.A))
            {
                _inputDirs[Vector2.left].Reset();
                _inputDirs[Vector2.right].Reset(playerData.dashDirKeyTime,playerData.dashDirKeyTime);
            }

            if (Input.GetKey(KeyCode.D))
            {
                _inputDirs[Vector2.right].Reset();
                _inputDirs[Vector2.left].Reset(playerData.dashDirKeyTime,playerData.dashDirKeyTime);
            }

            if (Input.GetKey(KeyCode.W))
            {
                _inputDirs[Vector2.up].Reset();
                _inputDirs[Vector2.down].Reset(playerData.dashDirKeyTime,playerData.dashDirKeyTime);
            }

            if (Input.GetKey(KeyCode.S))
            {
                _inputDirs[Vector2.down].Reset();
                _inputDirs[Vector2.up].Reset(playerData.dashDirKeyTime,playerData.dashDirKeyTime);
            }
        }

        private void UpdateStateMachine()
        {
            if (Input.GetKey(KeyCode.Mouse1))
                _playerFocus = PlayerFocus.FOCUS;
            else
                _playerFocus = PlayerFocus.NOT_FOCUS;
            float trash = 0;
            switch (_playerFocus)
            {
                case PlayerFocus.NOT_FOCUS:
                    _camera.orthographicSize =
                        Mathf.SmoothDamp(_camera.orthographicSize,
                            playerData.noFocusCameraSize, ref trash, playerData.scaleCameraTime);
                    Debug.Log("nofocus");
                    break;
                case PlayerFocus.FOCUS:
                    _camera.orthographicSize =
                        Mathf.SmoothDamp(_camera.orthographicSize,
                            playerData.focusCameraSize, ref trash, playerData.scaleCameraTime);
                    Debug.Log("focus");
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

                    if (_rigidbody2D.velocity != Vector2.zero)
                        _playerAnimation.PlayWalk();
                    else
                        _playerAnimation.PlayIdle();

                    break;
                case PlayerStatus.SHOOT:
                    _spriteRenderer.color = Color.blue;
                    //shoot message
                    //delay some time and go back idle
                    break;
                case PlayerStatus.ROLLING: //i think it will be a block state but can move 
                    //rolling  some time

                    _playerAnimation.PlayRoll();
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
            }
        }

        private void RollingLogic()
        {
            if (Input.GetKey(KeyCode.Space) && _rollCd.IsTrigger())
            {
                _rollTime.Reset(playerData.rollTime);
                
                
                Vector2 rollDir = Vector2.zero;
                foreach (var k in _inputDirs)
                {
                    if (!k.Value.IsTrigger())
                    {
                        rollDir += k.Key;
                    }
                }
                _rollDir = rollDir.normalized;
                _playerStatus = PlayerStatus.ROLLING;
            }
        }

        private void MessageShootLogic()
        {
            if (Input.GetMouseButton(0) &&
                _attackCounter.IsTrigger())
            {
                _attackCounter.Reset(playerData.attackTime);
                var newMessage = Instantiate(message, transform.position, transform.rotation,
                    _messageContainer.transform);
                newMessage.GetComponent<LoveMessage>()
                    .SetHpManager(GetHpManager())
                    .SetPlayerData(playerData)
                    .SetExistTime(playerData.messageExistTime);
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