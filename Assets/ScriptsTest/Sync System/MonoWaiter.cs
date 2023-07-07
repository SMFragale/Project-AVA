using System.Collections.Generic;
using AVA.Core;
using UnityEngine;
using System.Collections;

// This class is used to wait for dependencies to be ready before executing setup code
namespace AVA.Core
{
    public abstract class MonoWaiter : MonoBehaviour, IReadyCheck
    {
        private bool ready = false;
        protected List<IReadyCheck> dependencies;

        private void Start()
        {
            if (dependencies == null)
                throw new NoDependenciesException();
            StartCoroutine(Init());
        }

        private IEnumerator Init()
        {
            foreach (var dependency in dependencies)
            {
                yield return new WaitUntil(() => dependency.isReady());
            }
            OnDependenciesReady();
            ready = true;
        }

        protected abstract void OnDependenciesReady();

        public bool isReady()
        {
            return ready;
        }
    }

    public class NoDependenciesException : System.Exception
    {
        public NoDependenciesException() : base("No dependencies set, dependencies must be set in Awake()")
        {
        }
    }

}