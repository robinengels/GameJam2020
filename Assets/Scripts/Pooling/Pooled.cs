using UnityEngine;

namespace Pooling
{
    class Pooled : MonoBehaviour
    {
        public ObjectPool pool;

        public bool TryRelease()
        {
            return pool.TryRelease(gameObject);
        }
    }
}