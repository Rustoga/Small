using UnityEngine;

namespace SmallGame.Unit
{
    public abstract class UnitStateMachine
    {
        public UnitState CurrentState { get; protected set; }
        protected AnimationSystem AnimationSystem{get; set;}


        public void HandleUpdate()
        {
            CurrentState.UpdateState();
        }

        public void HandleFixedUpdate()
        {
            CurrentState.FixedUpdateState();
        }

        public void StartMachine(UnitState startSate)
        {
            CurrentState = startSate;
            CurrentState.EnterState();
            Debug.Log("Machine started");
        }
        public void PlayStateAnimation(AnimationState animationState)
        {
            AnimationSystem.ChangeAnimation(animationState);
        }

        public virtual void ChangeState(UnitState newState)
        {
            CurrentState.ExitState();
            CurrentState = newState;
            CurrentState.EnterState();

        }
    }
}
