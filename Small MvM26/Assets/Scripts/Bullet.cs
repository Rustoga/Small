using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace SmallGame
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] float _speed = 10f;
        Rigidbody2D _rb;
        [SerializeField] float _lifeDuration = 2f;
        Vector2 _direction;
        bool _isEnabled;
        // LayerMask _ignoreLayers;
        [SerializeField] LayerMask _collisionLayers;
        float _damageAmount = 2f;
        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            // gameObject.SetActive(false);
        }

        public void EnableBullet(Vector2 direction)
        {
            _direction = direction;
            gameObject.SetActive(true);
            _isEnabled = true;
            _rb.linearVelocity = Vector2.zero;
        }

        void FixedUpdate()
        {
            if (!_isEnabled) return;

            _rb.linearVelocity = _direction * _speed;
            _lifeDuration -= Time.deltaTime;

            if (_lifeDuration <= 0)
            {
                Destroy(gameObject);
            }
        }


        void OnTriggerEnter2D(Collider2D other)
        {
            if (_collisionLayers == (_collisionLayers | (1 << other.gameObject.layer)))
            {
                
                if (other.gameObject.TryGetComponent(out IDamageable damageable))
                {
                    damageable.ApplyDamage(_damageAmount, transform);
                }

                Destroy(gameObject);
            }
        }

    }
}
