using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pooling
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject pooledObject;
        [SerializeField] private int maxActiveInstances = 2;
        
        private Queue<GameObject> _pool = new Queue<GameObject>(5);
        private static Dictionary<GameObject, ObjectPool> _pools = new Dictionary<GameObject, ObjectPool>(5);
        private Transform _transform;

        public static ObjectPool GetPool(GameObject pooledObject)
        {
            if (_pools.TryGetValue(pooledObject, out var pool) == false)
            {
                pool = new GameObject(pooledObject.name  + " Pool", typeof(ObjectPool)).GetComponent<ObjectPool>();
                pool.pooledObject = pooledObject;
                _pools[pooledObject] = pool;
            }
            return pool;
        }

        private void Awake()
        {
            _transform = transform;
            if (pooledObject)
                _pools[pooledObject] = this;
        }

        public bool TryAcquire(out GameObject toAcquire)
        {
            if (_pool.Any())
            {
                toAcquire = _pool.Dequeue();
                toAcquire.SetActive(true);
                return true;
            }
            toAcquire = Instantiate(pooledObject, _transform);
            var pooled = toAcquire.GetComponent<Pooled>();
            if (!pooled)
                pooled = toAcquire.AddComponent<Pooled>();
            pooled.pool = this;
            return true;
        }

        public bool TryRelease(GameObject toRelease)
        {
            if (toRelease.GetComponent<Pooled>()?.pool != this)
                return false;
            toRelease.SetActive(false);
            StartCoroutine(Enqueue(toRelease));
            return true;
        }

        private IEnumerator Enqueue(GameObject obj)
        {
            yield return null;
            _pool.Enqueue(obj);
        }
    }
}