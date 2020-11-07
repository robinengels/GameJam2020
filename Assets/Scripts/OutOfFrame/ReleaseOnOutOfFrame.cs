using Pooling;

public class ReleaseOnOutOfFrame : OutOfFrameChecker
{
    private protected override void IsOutOfFrame()
    {
        gameObject.TryRelease();
    }
}