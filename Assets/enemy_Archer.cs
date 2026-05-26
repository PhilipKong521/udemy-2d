using UnityEngine;

public class enemy_Archer : Enemy
{
    protected override void Attack()
    {
        Debug.Log(enemyName + "shoots an arrow!!");
    }
}
