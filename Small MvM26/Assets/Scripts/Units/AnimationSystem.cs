using UnityEngine;

namespace SmallGame.Unit
{
    public abstract class AnimationSystem : MonoBehaviour
    {
        public virtual Animator Anim { get; set; }
        protected virtual AnimationState CurrentAnimation { get; private set; }
        //put Events Here

        public virtual void HandleAwake(UnitStateMachine stateMachine)
        {

        }


        public virtual void ChangeAnimation(AnimationState newAnimation)
        {
            // Debug.Log(newAnimation.Name);
            Anim.Play(newAnimation.Name);
            CurrentAnimation = newAnimation;
            Anim.Update(0);
        }

        public void EnterStartState(AnimationState startingState) => ChangeAnimation(startingState);

    }
}
