using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmallGame.Unit;

namespace SmallGame.Player
{
    public class PlayerAnimationState : Unit.AnimationState
    {

        public PlayerAnimationState(string name) : base(name)
        {

        }

        public static readonly PlayerAnimationState Idle = new(State.IDLE);
        public static readonly PlayerAnimationState Run = new(State.RUN);
        public static readonly PlayerAnimationState Jump = new(State.JUMP);
        public static readonly PlayerAnimationState Attack = new(State.ATTACKGROUND);
        public static readonly PlayerAnimationState Dead = new(State.DEAD);
        public static readonly PlayerAnimationState DoubleJump = new(State.DOUBLEJUMP);
        public static readonly PlayerAnimationState Hit = new(State.HIT);
        public static readonly PlayerAnimationState WallSlide = new(State.WALLSLIDE);
        public static readonly PlayerAnimationState Dash = new(State.DASH);
        public static readonly PlayerAnimationState Land = new(State.LAND);




    }
}
