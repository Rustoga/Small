using UnityEngine;

namespace SmallGame
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterController : MonoBehaviour
    {
        Rigidbody2D _rb;
        [Range(0, 0.3f), SerializeField] float _movementSmoothing = 0.03f;
        Vector3 _velocity = Vector3.zero;


        void Awake() {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Move(float horizontalSpeed)
        {
            Vector3 targetVelocity = new Vector2(horizontalSpeed * 10f, _rb.linearVelocityY);
            _rb.linearVelocity = Vector3.SmoothDamp(_rb.linearVelocity, targetVelocity, ref _velocity, _movementSmoothing);
            
        }
    }
}
