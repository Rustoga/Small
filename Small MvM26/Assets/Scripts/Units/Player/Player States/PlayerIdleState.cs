using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SmallGame.Unit;

namespace SmallGame.Player
{
    public class PlayerIdleState : UnitState
    {
        PlayerStateMachine _sm;
        PlayerAnimationSystem _animationSystem;
        PlayerSoundSystem _soundSystem;

        public override string DebugStateName => "idle state";
        static readonly PlayerAnimationState _animationstate = PlayerAnimationState.Idle;

        public override Unit.AnimationState AnimationState => _animationstate;


        public PlayerIdleState(UnitStateMachine stateMachine,
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
            _animationSystem.ChangeAnimation(_animationstate);

            _frame = _animationSystem.AnimationVariables;

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
            if (anyState != null)
            {
                _sm.ChangeState(anyState);
                return;
            }

            else if (_frame.Speed > 0.01f)
            {
                _sm.ChangeState(_sm.RunState);
                return;
            }

        }



    }
}
