using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kellojo.GameMode {
    public interface IGameMode {
        IEnumerator OnStart();
        IEnumerator OnEditorStart();
        IEnumerator OnEnd();
    }
}


