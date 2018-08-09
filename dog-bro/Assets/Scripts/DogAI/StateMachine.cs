using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateNameSpace
{
    public class StateMachine<T> 
    {
        public State<T> currentState { get; private set;}
        public T Owner;

        private State<T> initialState;

        public StateMachine(T owner, State<T> initalState) {
            this.Owner = owner;
            this.initialState = initalState;
            this.currentState = initalState;
        }

        public void ChangeState(State<T> nextState) {

            currentState.ExitState(Owner);
            currentState = nextState;
            currentState.EnterState(Owner);
        }

        public void Update() {
            currentState.UpdateState(Owner);
            Debug.Log("Current State: " + currentState.ToString());
        }

        // Call me only when the character got reset
        public void Reset() {
            currentState = initialState;
        }
    }
}