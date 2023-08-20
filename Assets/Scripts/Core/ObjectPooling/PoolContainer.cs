using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AVA.Core
{
    public class PoolContainer : MonoBehaviour
    {
        //Unity singleton 
        private static PoolContainer _instance;
        
        //Instance property
        public static PoolContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject poolContainer = new("PoolContainer");
                    _instance = poolContainer.AddComponent<PoolContainer>();
                }
                return _instance;
            }
        }

        //Dictionary of tags/parents that act as container for different kind of pools
        private Dictionary<PoolContainerType, Transform> _poolContainers = new Dictionary<PoolContainerType, Transform>();

        public Transform GetPoolParent(PoolContainerType type)
        {
            if (_poolContainers.ContainsKey(type))
            {
                return _poolContainers[type];
            }
            else
            {
                Transform newParent = new GameObject(type.ToString()+"Pooling").transform;
                newParent.parent = transform;
                _poolContainers.Add(type, newParent);
                return newParent;
            }
        }
    }

    public enum PoolContainerType
    {
        Projectile,
        Enemy,
        //Sugested by copilot...
        Effect,
        UI
    }
}
