using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace SmallGame
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField]float _speed = 10f;
        Rigidbody2D _rb;
        float _lifeDuration = 5f;
        CancellationTokenSource _cts;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _cts = new CancellationTokenSource();
            StartLifeDurationCountdown(_cts.Token);
        }

        public void EnableBullet(Vector2 direction)
        {
            _rb.AddForce(direction * _speed, ForceMode2D.Impulse);
        }
        async void StartLifeDurationCountdown(CancellationToken token)
        {
            await Task.Delay((int)(_lifeDuration * 1000), token);

            if (!token.IsCancellationRequested)
            {
                Destroy(gameObject);
            }


        }

        void OnDestroy()
        {
            _cts.Cancel();
        }


    }
}
