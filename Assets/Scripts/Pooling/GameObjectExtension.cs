using UnityEngine;

namespace Pooling
{
    public static class GameObjectExtension
    {
        public static bool TryAcquire(this GameObject that, out GameObject toAcquire)
        {
            return ObjectPool.GetPool(that).TryAcquire(out toAcquire);
        }

        public static bool TryRelease(this GameObject that)
        {
            var pooled = that.GetComponent<Pooled>();
            if (pooled?.TryRelease() == true)
                return true;
            Object.Destroy(that);
            return false;
        }
        
    }
}