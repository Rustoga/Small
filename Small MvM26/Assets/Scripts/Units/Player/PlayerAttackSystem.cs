using System.Collections;
using UnityEngine;

namespace SmallGame.Player
{
    public class PlayerAttackSystem : MonoBehaviour
    {
        Input.InputHandler _inputHandler;
        [SerializeField] GameObject _bulletPrefab;
        float _speed;
        Vector2 _direction;
        bool _fire;
        float _attackCooldown;
        bool _attackReady = true;
        PlayerController _playerController;
        [SerializeField] Transform _bulletSpawnPoint;
        bool _aimingUp;
        void Awake()
        {
            _playerController = GetComponent<PlayerController>();
        }
        void Start()
        {
            _inputHandler = Input.InputHandler.Instance;
            _inputHandler.OnAttackStart.AddListener(RecieveAttack);
            _inputHandler.OnMovement.AddListener(RecieveMovement);
        }
        void RecieveAttack()
        {
            _fire = true;
        }
        void RecieveMovement(Vector2 dir)
        {
            _aimingUp = dir.y > 0;
        }
        void Update()
        {

        }
        void FixedUpdate()
        {
            HandleAttack();
            _fire = false;
        }
        void HandleAttack()
        {
            if (!_attackReady || !_fire) return;

            StartCoroutine(AttackCooldown());
            Attack();

        }
        Vector2 GetDirection()
        {
            if (_aimingUp)
            {
                return Vector2.up;
            }
            else
            {
                return _playerController.GetFacingDirection();

            }

        }
        void Attack()
        {
            var bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, Quaternion.identity);
            var bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.EnableBullet(GetDirection());

        }

        IEnumerator AttackCooldown()
        {
            _attackReady = false;
            yield return new WaitForSeconds(_attackCooldown);
            _attackReady = true;
        }


    }
}
