using UnityEngine;

namespace SmallGame.Parallax
{
    [ExecuteInEditMode]
    public class ParallaxLayer : MonoBehaviour
    {
        public float parallaxFactor;

        public void Move(float delta){
            var newPos = transform.localPosition;
            newPos.x -= delta * parallaxFactor;

            transform.localPosition = newPos;
        }
    }
}
