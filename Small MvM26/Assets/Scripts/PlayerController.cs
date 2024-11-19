using System.Threading.Tasks;
using UnityEngine;

namespace SmallGame
{
    enum FacingDirection
    {
        Left = -1,
        Right = 1
    }


    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {

        Rigidbody2D _rb;
        [Range(0, 0.3f), SerializeField] float _movementSmoothing = 0.03f;
        [SerializeField] Vector3 _velocity = Vector3.zero;
        [SerializeField] float _dashForce = 4f;
        [SerializeField] Vector2 _jumpForce = new Vector2(0f, 100f);

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Move(float horizontalSpeed)
        {
            Vector3 targetVelocity = new Vector2(horizontalSpeed, _rb.linearVelocityY);
            _rb.linearVelocity = Vector3.SmoothDamp(_rb.linearVelocity, targetVelocity, ref _velocity, _movementSmoothing);

        }

        public void Dash()
        {
            _rb.linearVelocity = new Vector2(transform.localScale.x * _dashForce, 0);
        }
        public void Jump()
        {
            _rb.AddForce(_jumpForce);
            print("jumping");
        }

        void FaceDirection(FacingDirection direction)
        {
            var scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * (int)direction;
            transform.localScale = scale;
        }
    }
}
