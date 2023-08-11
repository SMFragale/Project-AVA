using System;
using System.Collections.Generic;
using UnityEngine;

namespace AVA.Core
{
    public class ObjectPool <T> : IPool<T> where T : MonoBehaviour, IPoolable<T>
    {
        private System.Action<T> onReleaseAction;
        private System.Action<T> onPullAction;
        private Stack<T> pooledObjects = new Stack<T>();
        private GameObject prefab;
        private GameObject parent;
        public int pooledCount { get => pooledObjects.Count;}

        public ObjectPool(GameObject prefab, GameObject parent = null, int initialSpawn = 0)
        {
            this.prefab = prefab;
            this.parent = parent;
            Spawn(initialSpawn);
        }
        public ObjectPool(GameObject prefab,  Action<T> onPullAction, Action<T> onReleaseAction, GameObject parent = null, int initialSpawn = 0)
        {
            this.prefab = prefab;
            this.parent = parent;
            this.onPullAction = onPullAction;
            this.onReleaseAction = onReleaseAction;
            Spawn(initialSpawn);
        }


        public T Pull()
        {
            T poolable;
            if(pooledCount > 0)
                poolable = pooledObjects.Pop();
            else {
                poolable = GameObject.Instantiate(prefab).GetComponent<T>();
                if (parent != null)
                    poolable.transform.SetParent(parent.transform);
            }
            poolable.gameObject.SetActive(true);
            poolable.InitializePoolable(Push);
            onPullAction?.Invoke(poolable);
            return poolable;
        }

        public T Pull(Vector3 position)
        {
            T poolable = Pull();
            poolable.transform.position = position;
            return poolable;
        }
        public T Pull(Vector3 position, Quaternion rotation)
        {
            T poolable = Pull();
            poolable.transform.position = position;
            poolable.transform.rotation = rotation;
            return poolable;
        }

        public void Push(T poolable)
        {
            pooledObjects.Push(poolable);
            onReleaseAction?.Invoke(poolable);
            poolable.gameObject.SetActive(false);
        }
        
        public GameObject PullGameObject(Vector3 position)
        {
            GameObject gameObj = Pull().gameObject;
            gameObj.transform.position = position;
            return gameObj;
        }

        public GameObject PullGameObject(Vector3 position, Quaternion rotation)
        {
            GameObject gameObj = Pull().gameObject;
            gameObj.transform.position = position;
            gameObj.transform.rotation = rotation;
            return gameObj;
        }
        public GameObject PullGameObject()
        {
            return Pull().gameObject;
        }

        private void Spawn(int num)
        {
            T poolable;
            for (int i = 0; i < num; i++)
            {
                poolable = GameObject.Instantiate(prefab).GetComponent<T>();
                pooledObjects.Push(poolable);
                poolable.gameObject.SetActive(false);
            }
                
        }

    }
}