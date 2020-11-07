public class DestroyOnOutOfFrame : OutOfFrameChecker
{
    private protected override void IsOutOfFrame()
    {
        Destroy(gameObject);
    }
}
