using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmallGame.Unit;
namespace SmallGame.Player
{
    public class PlayerHitState : UnitState
    {
        PlayerStateMachine _sm;
        PlayerAnimationSystem _animationSystem;
        PlayerSoundSystem _soundSystem;

        public override string DebugStateName => "hit state";
        static readonly PlayerAnimationState _animationstate = PlayerAnimationState.Hit;

        public override Unit.AnimationState AnimationState => _animationstate;

        float _timer = 0;


        public PlayerHitState(UnitStateMachine stateMachine,
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
            _animationSystem.AnimationVariables.Hit = false;
            _soundSystem.StopSound();
            _timer = 0;
        }

        public override void UpdateState()
        {
            _timer += Time.deltaTime;
            if (_timer >= AnimationLength)
            {
                _sm.ChangeState(_sm.IdleState);
            }
        }
        public override void FixedUpdateState()
        {

        }


    }
}
