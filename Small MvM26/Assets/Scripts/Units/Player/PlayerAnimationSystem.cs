using UnityEngine;
using SmallGame.Unit;

namespace SmallGame.Player
{
    public class PlayerAnimationSystem : AnimationSystem
    {
        public override Animator Anim
        {
            get { return _animator; }
            set { _animator = value; }
        }

        public PlayerAnimatorVariables AnimationVariables;
        public void OnFall() => AnimationVariables.IsJumping = true;
        public void OnLanding() => AnimationVariables.IsJumping = false;

        PlayerStateMachine _sm;
        [SerializeField] Animator _animator;

        public override void HandleAwake(UnitStateMachine stateMachine)
        {
            if (stateMachine is PlayerStateMachine)
            {
                _sm = stateMachine as PlayerStateMachine;
            }
            else
            {
                Debug.LogError("statemachine is not a type of playerstatemachine");
            }
        }







    }
}
