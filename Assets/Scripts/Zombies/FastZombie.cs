using UnityEngine;

public class FastEnemy : BaseEnemyLogic
{
    protected override void Start()
    {
        base.Start();

        health = 60;
        speed = 1.5f;

        ApplyToAgent();
    }
}
