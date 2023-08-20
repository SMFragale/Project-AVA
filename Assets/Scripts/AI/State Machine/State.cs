using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AVA.AI
{
    /// <summary>
    /// State interface for the state machine
    /// </summary>
    public interface State
    {
        /// <summary>
        /// Called when the state is entered
        /// </summary>
        public abstract void OnStart();
        /// <summary>
        /// Called every frame while the state is active
        /// </summary>
        public abstract void OnUpdate();
        /// <summary>
        /// Called when the state is exited
        /// </summary>
        public abstract void OnExit();
    }
}