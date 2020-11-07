using UnityEngine;

namespace Pooling
{
    public class PoolableEnemy : MonoBehaviour
    {
        private Camera _mainCamera;
        private Transform _mainCameraTransform;
        private Transform _transform;
        
        private void Awake()
        {
            _mainCamera = Camera.main;
            _mainCameraTransform = _mainCamera.transform;
            _transform = transform;
        }

        private void Update()
        {
            var xReleaseThreshold = _mainCameraTransform.position.x - (_mainCamera.orthographicSize * 3f);
            if (_transform.position.x <= xReleaseThreshold)
                gameObject.TryRelease();
        }
    }
}