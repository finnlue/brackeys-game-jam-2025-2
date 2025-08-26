using UnityEngine;

public class DefaultZombie : BaseEnemyLogic
{
    protected override void Start()
    {
        base.Start();

        health = 100;
        speed = 1.2f;

        ApplyToAgent();
    }
}
