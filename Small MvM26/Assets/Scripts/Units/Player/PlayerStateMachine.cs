using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmallGame.Unit;


namespace SmallGame.Player
{
    public class PlayerStateMachine : UnitStateMachine
    {
        PlayerController _controller;
        PlayerSoundSystem _soundSystem;


        public Dictionary<string, UnitState> StateLookup { get; private set; }
        public UnitState IdleState;
        public UnitState RunState;
        public UnitState JumpState;
        public UnitState AttackState;
        public UnitState DashState;
        public UnitState DeathState;
        public UnitState DoubleJumpState;
        public UnitState HitState;
        public UnitState WallSlideState;
        public UnitState LandState;


        public override void ChangeState(UnitState newState)
        {
            base.ChangeState(newState);
            _controller.UpdateDisplayState(newState.DebugStateName);
        }

        public PlayerStateMachine(PlayerController controller, PlayerAnimationSystem animationSystem, PlayerSoundSystem soundSystem)
        {
            _controller = controller; 
            AnimationSystem = animationSystem;

            IdleState = new PlayerIdleState(this, animationSystem, soundSystem);
            RunState = new PlayerRunState(this, animationSystem, soundSystem);
            JumpState = new PlayerJumpState(this, animationSystem, soundSystem);
            AttackState = new PlayerAttackState(this, animationSystem, soundSystem);
            DashState = new PlayerDashState(this, animationSystem, soundSystem);
            DeathState = new PlayerDeathState(this, animationSystem, soundSystem);
            DoubleJumpState = new PlayerDoubleJumpState(this, animationSystem, soundSystem);
            HitState = new PlayerHitState(this, animationSystem, soundSystem);
            WallSlideState = new PlayerWallSlideState(this, animationSystem, soundSystem);
            LandState = new PlayerLandingState(this, animationSystem, soundSystem);


            AssignStateLookup();

        }

        void AssignStateLookup()
        {
            StateLookup = new(){
                {IdleState.AnimationState.Name, IdleState},
                {RunState.AnimationState.Name, RunState},
                {JumpState.AnimationState.Name, JumpState},
                {AttackState.AnimationState.Name, AttackState},
                {DashState.AnimationState.Name, DashState},
                {DeathState.AnimationState.Name, DeathState},
                {DoubleJumpState.AnimationState.Name, DoubleJumpState},
                {HitState.AnimationState.Name, HitState},
                {WallSlideState.AnimationState.Name, WallSlideState},
                {LandState.AnimationState.Name, LandState}

            };
            
            Debug.Log("states assigned");

        }


        //commonly used functions across states

        public UnitState HandleAnyStates(PlayerAnimatorVariables frame)
        {
            if (frame.IsDoubleJumping) return DoubleJumpState;

            if (frame.IsDead) return DeathState;
            if (frame.IsDashing) return DashState;
            if (frame.IsJumping && !frame.IsAttacking && !frame.IsDashing && !frame.Hit && !frame.IsWallSliding && !frame.IsDead) return JumpState;
            if (frame.Hit) return HitState;
            if (frame.IsDoubleJumping) return DoubleJumpState;
            if (frame.IsWallSliding && !frame.IsAttacking) return WallSlideState;
            if (frame.IsAttacking) return AttackState;

            return null;
        }

    }


    //create a class that holds a collection of audioclips
    //create an instance of the class and be able to use the inspector to assign audio clips
    //

    // public class UnitSounds : MonoBehaviour
    // {
    //     Dictionary<string, 
    // }
}
