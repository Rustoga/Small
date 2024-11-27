using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmallGame.Unit;
namespace SmallGame.Player
{
    public class PlayerDeathState : UnitState
    {
        PlayerStateMachine _sm;
        PlayerAnimationSystem _animationSystem;
        PlayerSoundSystem _soundSystem;
        public override string DebugStateName => "death state";
        static readonly PlayerAnimationState _animationstate = PlayerAnimationState.Dead;

        public override Unit.AnimationState AnimationState => _animationstate;

        public PlayerDeathState(UnitStateMachine stateMachine,
                              PlayerAnimationSystem animationSystem,
                              PlayerSoundSystem soundSystem)
                              : base(stateMachine)
        {
            if (stateMachine is PlayerStateMachine)
            {
                _sm = stateMachine as PlayerStateMachine;
                _animationSystem = animationSystem as PlayerAnimationSystem;
                _soundSystem = soundSystem;

            }
            else
            {
                Debug.LogError("Invalid State Machine");
            }

        }

        AnimatorStateInfo _stateInfo => _animationSystem.Anim.GetCurrentAnimatorStateInfo(0);
        AnimatorClipInfo _clipInfo => _animationSystem.Anim.GetCurrentAnimatorClipInfo(0)[0];
        public override void EnterState()
        {
            _sm.PlayStateAnimation(_animationstate);
            var multi = _stateInfo.speed * _stateInfo.speedMultiplier;
            SetStateLengthToClip(_clipInfo.clip, multi);

        }
        public override void ExitState()
        {

        }

        public override void UpdateState()
        {

        }

        public override void FixedUpdateState()
        {

        }

    }
}
