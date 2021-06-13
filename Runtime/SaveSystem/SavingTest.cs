using Kellojo.SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingTest : MonoBehaviour, ISaveable
{
    public InternalState internalState;

    public object CaptureState() {
        return internalState;
    }

    public void RestoreState(object state) {
        internalState = (InternalState)state;
    }

    [System.Serializable]
    public struct InternalState {
        public string someValue;
    }
    
}
