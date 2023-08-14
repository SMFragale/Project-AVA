using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AVA.AI
{
    public interface State
    {
        public abstract void OnStart();
        public abstract void OnUpdate();
        public abstract void OnExit();
    }
}