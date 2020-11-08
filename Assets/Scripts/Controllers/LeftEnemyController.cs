using System.Collections;
using UnityEngine;

public class LeftEnemyController : EnemyController
{
    private Coroutine speedIncreaseCoroutine;
    
    protected override void OnAwake()
    {
        speedIncreaseCoroutine = StartCoroutine(IncreaseSpeed());
    }

    protected override void OnDestroyed()
    {
        StopCoroutine(speedIncreaseCoroutine);
    }

    private IEnumerator IncreaseSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            speed += acceleration;
        }
    }
}
