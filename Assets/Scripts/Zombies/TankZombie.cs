using UnityEngine;

public class TankZombie : BaseEnemyLogic
{
    protected override void Start()
    {
        base.Start();

        health = 300;
        speed = 0.6f;

        ApplyToAgent();
    }
}
