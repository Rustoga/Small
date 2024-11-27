using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmallGame.Unit;
namespace SmallGame.Player
{
    public class PlayerJumpState : UnitState
    {
        PlayerStateMachine _sm;
        PlayerAnimationSystem _animationSystem;
        PlayerSoundSystem _soundSystem;

        public override string DebugStateName => "jump state";
        static readonly PlayerAnimationState _animationstate = PlayerAnimationState.Jump;

        public override Unit.AnimationState AnimationState => _animationstate;

        public PlayerJumpState(UnitStateMachine stateMachine,
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


        public override void EnterState()
        {
            if (_animationSystem.AnimationVariables.JumpUp)
            {
                _animationSystem.AnimationVariables.JumpUp = false;
                _soundSystem.PlaySound(_soundSystem.JumpUpSound, true);
            }

            _sm.PlayStateAnimation(_animationstate);
        }
        public override void ExitState()
        {
            _soundSystem.StopSound();
        }

        public override void UpdateState()
        {

        }

        PlayerAnimatorVariables _frame;
        public override void FixedUpdateState()
        {
            _frame = _animationSystem.AnimationVariables;

            UnitState anyState = _sm.HandleAnyStates(_frame);
            if (anyState != null && anyState != _sm.JumpState)
            {
                _sm.ChangeState(anyState);
                return;
            }



            if(_frame.IsDoubleJumping)
            {
                _sm.ChangeState(_sm.DoubleJumpState);
            }
            if (_frame.IsJumping) return;

            if (_frame.Speed < 0.01)
            {
                // _sm.ChangeState(_sm.IdleState);
                _sm.ChangeState(_sm.LandState);
            }
            if (_frame.Speed > 0.01)
            {
                _sm.ChangeState(_sm.RunState);
            }
        }

    }
}
