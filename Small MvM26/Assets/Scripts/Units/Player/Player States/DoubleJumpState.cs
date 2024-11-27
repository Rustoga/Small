using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmallGame.Unit;
using Unity.VisualScripting;

namespace SmallGame.Player
{
    public class PlayerDoubleJumpState : UnitState
    {
        PlayerStateMachine _sm;
        PlayerAnimationSystem _animationSystem;
        PlayerSoundSystem _soundSystem;

        public override string DebugStateName => "double jump state";
        static readonly PlayerAnimationState _animationstate = PlayerAnimationState.DoubleJump;

        public override Unit.AnimationState AnimationState => _animationstate;

        public PlayerDoubleJumpState(UnitStateMachine stateMachine,
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
            _soundSystem.PlaySound(_soundSystem.HitSound, false);
            _sm.PlayStateAnimation(_animationstate);
            var multi = _stateInfo.speed * _stateInfo.speedMultiplier;
            SetStateLengthToClip(_clipInfo.clip, multi);
            _timer = 0;
        }
        public override void ExitState()
        {
            _animationSystem.AnimationVariables.IsDoubleJumping = false;
            _soundSystem.StopSound();
            _timer = 0;
        }
        float _timer;
        public override void UpdateState()
        {
            _timer += Time.deltaTime;
            if (_timer >= AnimationLength)
            {
                _sm.ChangeState(_sm.JumpState);

            }
        }

        PlayerAnimatorVariables _frame;
        public override void FixedUpdateState()
        {
            _frame = _animationSystem.AnimationVariables;
            if (_frame.IsDoubleJumping) return;

            UnitState anyState = _sm.HandleAnyStates(_frame);
            if (anyState != null && anyState != _sm.JumpState)
            {
                _sm.ChangeState(anyState);
                return;
            }
            if (_frame.Speed < 0.01)
            {
                // _sm.ChangeState(_sm.IdleState);
                _sm.ChangeState(_sm.LandState);
                return;
            }
            if (_frame.Speed > 0.01)
            {
                _sm.ChangeState(_sm.RunState);
                
            }
        }


    }
}
