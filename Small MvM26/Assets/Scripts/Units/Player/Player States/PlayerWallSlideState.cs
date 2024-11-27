using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmallGame.Unit;
namespace SmallGame.Player
{
    public class PlayerWallSlideState : UnitState
    {
        PlayerStateMachine _sm;
        PlayerAnimationSystem _animationSystem;
        PlayerSoundSystem _soundSystem;

        public override string DebugStateName => "wall slide state";
        static readonly PlayerAnimationState _animationstate = PlayerAnimationState.WallSlide;

        public override Unit.AnimationState AnimationState => _animationstate;

        public PlayerWallSlideState(UnitStateMachine stateMachine,
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
            _sm.PlayStateAnimation(_animationstate);
            
            var clip = _animationSystem.Anim.GetCurrentAnimatorClipInfo(0)[0].clip;
            // Debug.Log($"{clip.name} with a length of {clip.length} is currently playing in {StateName}");


            
        }
        public override void ExitState()
        {

        }

        public override void UpdateState()
        {

        }

        PlayerAnimatorVariables _frame;
        public override void FixedUpdateState()
        {
            _frame = _animationSystem.AnimationVariables;

            UnitState anyState = _sm.HandleAnyStates(_frame);

            if (anyState != null && anyState != _sm.WallSlideState)
            {
                _sm.ChangeState(anyState);
                return;
            }

            if (!_frame.IsWallSliding)
            {
                _sm.ChangeState(_sm.JumpState);
            }

        }



    }
}
