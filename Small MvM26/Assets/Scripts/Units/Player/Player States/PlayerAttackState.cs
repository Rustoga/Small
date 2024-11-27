using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmallGame.Unit;

namespace SmallGame.Player
{
    public class PlayerAttackState : UnitState
    {
        PlayerStateMachine _sm;
        PlayerAnimationSystem _animationSystem;
        PlayerSoundSystem _soundSystem;

        public override string DebugStateName => "attack state";
        static readonly PlayerAnimationState _animationstate = PlayerAnimationState.Attack;

        public override Unit.AnimationState AnimationState => _animationstate;

        public PlayerAttackState(UnitStateMachine stateMachine,
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
            _soundSystem.PlaySound(_soundSystem.AttackSound, false);
            _sm.PlayStateAnimation(_animationstate);
            var multi = _stateInfo.speed * _stateInfo.speedMultiplier;
            SetStateLengthToClip(_clipInfo.clip, multi);

            _timer = 0;


        }

        public override void ExitState()
        {
            _animationSystem.AnimationVariables.IsAttacking = false;
            _soundSystem.StopSound();
            _timer = 0;

        }
        float _timer;
        public override void UpdateState()
        {
            _timer += Time.deltaTime;
            if (_timer >= AnimationLength)
            {
                _sm.ChangeState(_sm.IdleState);
            }
        }

        PlayerAnimatorVariables _frame;
        public override void FixedUpdateState()
        {
            // _frame = _animationSystem.AnimationVariables;

            // UnitState anyState = _sm.HandleAnyStates(_frame);
            // if (anyState != null)
            // {
            //     _sm.ChangeState(anyState);
            //     return;
            // }

        }

    }
}
