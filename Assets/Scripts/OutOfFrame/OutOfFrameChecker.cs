using UnityEngine;

public abstract class OutOfFrameChecker : MonoBehaviour
{
    [SerializeField] private float orthographicSizeMultiplier = 3f;
    
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
        var camPos = _mainCameraTransform.position;
        var orthographicSize = _mainCamera.orthographicSize;
        var offset = orthographicSize * orthographicSizeMultiplier;
        var xLeftReleaseThreshold = camPos.x - offset;
        var xRightReleaseThreshold = camPos.x + offset;
        var xPos = _transform.position.x;
        if (xPos <= xLeftReleaseThreshold || xPos >= xRightReleaseThreshold)
            IsOutOfFrame();
    }

    private protected abstract void IsOutOfFrame();
}