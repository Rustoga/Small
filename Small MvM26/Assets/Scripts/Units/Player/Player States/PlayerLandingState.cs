using SmallGame.Unit;
using UnityEngine;

namespace SmallGame.Player
{
    public class PlayerLandingState : UnitState
    {
        public override string DebugStateName => "landing state";
        public override Unit.AnimationState AnimationState => _animationstate;
        static readonly PlayerAnimationState _animationstate = PlayerAnimationState.Land;

        float _timer;
        PlayerStateMachine _sm;
        PlayerAnimationSystem _animationSystem;
        PlayerSoundSystem _soundSystem;
        private PlayerAnimatorVariables _frame;

        AnimatorStateInfo _stateInfo => _animationSystem.Anim.GetCurrentAnimatorStateInfo(0);
        AnimatorClipInfo _clipInfo => _animationSystem.Anim.GetCurrentAnimatorClipInfo(0)[0];


        public PlayerLandingState(UnitStateMachine stateMachine,
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
            var multi = _stateInfo.speed * _stateInfo.speedMultiplier;
            SetStateLengthToClip(_clipInfo.clip, multi);

            _timer = 0;
        }

        public override void ExitState()
        {
            _animationSystem.AnimationVariables.IsLanding = false;
            _timer = 0;
        }

        public override void UpdateState()
        {
            // UnitState anyState = _sm.HandleAnyStates(_frame);
            // if (anyState != null && anyState != _sm.LandState)
            // {
            //     _sm.ChangeState(anyState);
            //     return;
            // }
            _timer += Time.deltaTime;
            if (_timer >= AnimationLength)
            {
                _sm.ChangeState(_sm.IdleState);
            }
            _frame = _animationSystem.AnimationVariables;


        }

        public override void FixedUpdateState()
        {
            _frame = _animationSystem.AnimationVariables;

            UnitState anyState = _sm.HandleAnyStates(_frame);
            if (anyState != null && anyState != _sm.LandState)
            {
                _sm.ChangeState(anyState);
                return;
            }
            if (_frame.Speed > 0.1f)
            {
                _sm.ChangeState(_sm.RunState);
            }
        }


    }
}
