using SmallGame.Player;
using UnityEngine;
using SmallGame.Physics;

namespace SmallGame
{
    public class PlatformMover : MonoBehaviour
    {
        public float speed = 2.0f; // Speed of the platform movement
        public Vector3 pointOffset = new Vector3(5, 0, 0); // Offset for the second point

        private Vector3 pointA;    // First point
        private Vector3 pointB;    // Second point
        private Vector3 target;    // Target point

        private Rigidbody2D rb;
        Vector2 direction => (target - transform.position).normalized;
        void Start()
        {

            // Set the initial points based on the platform's starting position
            pointA = transform.position;
            pointB = transform.position + pointOffset;

            // Set the initial target to pointB
            target = pointB;
            rb = GetComponent<Rigidbody2D>();


            _force = new("platform force", direction * speed);
        }


        void FixedUpdate()
        {
            rb.linearVelocity = direction * speed;

            if (Vector3.Distance(transform.position, target) < 0.1f)
            {
                // Switch target to the other point
                target = target == pointA ? pointB : pointA;
                UpdateForce(direction);
            }

        }

        void UpdateForce(Vector2 direction)
        {
            _force.Force = direction * speed;
        }

        void OnDrawGizmos()
        {
            // Draw lines in the Scene view to indicate the path of the platform
            if (pointA != null && pointB != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(pointA, pointB);
            }
        }

        PlayerController _playerController;
        ExternalForce _force;
        void OnCollisionEnter2D(Collision2D other)
        {

            var obj = other.collider.gameObject;
            if (obj.layer == LayerMask.NameToLayer("Player"))
            {
                _playerController = obj.GetComponent<PlayerController>();
                _playerController.ApplyExternalForces(_force);
                
            }
        }

        void OnCollisionExit2D(Collision2D other)
        {
            var obj = other.collider.gameObject;
            if (obj.layer == LayerMask.NameToLayer("Player"))
            {
                _playerController.RemoveExternalForce(_force);
                _playerController = null;
            }
        }

    }

}