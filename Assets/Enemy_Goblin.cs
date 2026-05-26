using UnityEngine;

public class Enemy_Goblin : Enemy
{
    private void Awake()
    {
        moveSpeed = 10;
    }

    protected override void Attack()
    {
        base.Attack();
        StealMoney();
    }

    private void StealMoney()
    {
        Debug.Log(enemyName + "steal's gold from player!");
    }

}
