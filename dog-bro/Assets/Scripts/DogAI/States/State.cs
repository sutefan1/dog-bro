using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Based on "Unity 3D - Make a Basic AI State Machine" by Joey The Lantern
// Link: https://www.youtube.com/watch?v=PaLD1t-kIwM

namespace StateNameSpace 
{
    public abstract class State<T> 
    {
        public abstract void EnterState(T owner);
        public abstract void UpdateState(T owner);
        public abstract void ExitState(T owner);
    }


}