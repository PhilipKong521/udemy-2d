using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
   private void AttackStarted()
    {
        Debug.Log("Attack Started");
    }

    private void AttackFinished()
    {
        Debug.Log("Attack Finished");
    }
}
