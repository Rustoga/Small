using UnityEngine;

namespace SmallGame.Unit
{
    public abstract class UnitState
    {
        public abstract string DebugStateName { get; }
        // protected AnimationSystem AnimationSystem;
        public abstract AnimationState AnimationState { get; }
        public virtual float AnimationLength{get; set;}
        public UnitState(UnitStateMachine stateMachine)
        {
        }

        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void FixedUpdateState();
        public abstract void ExitState();

        // public void SetAnimationLength(float duration)
        // {
        //     AnimationLength = duration;

        //     // var anim = AnimationSystem.Anim;
        //     // var clips = anim.runtimeAnimatorController.animationClips;
        // }

        public void SetStateLengthToClip(AnimationClip clip, float speedMultiplier)
        {
            AnimationLength = clip.length / speedMultiplier;
            // Debug.Log($"Assigning {clip.name}'s length of {clip.length} to {StateName}");


        }
    }
}
