using UnityEngine;

namespace SmallGame.Parallax
{
    public class ParallaxCamera : MonoBehaviour
    {
        public delegate void ParallaxCameraDelegate(float deltaMovement);
        public ParallaxCameraDelegate onCameraTranslate;

        float _oldPosition;

        void Start() {
            _oldPosition = transform.position.x;
        }

        void Update() {
            if(transform.position.x != _oldPosition)
            {
                if(onCameraTranslate != null)
                {
                    var delta = _oldPosition - transform.position.x;
                    onCameraTranslate(delta);
                }

                _oldPosition = transform.position.x;
            }
        }
    }
}
