using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageable : MonoBehaviour, IDamageable
{
    public void Die()
    {
        GameController.instance.AddTime(-999);
    }

    public void TakeDamage(int damage)
    {
        GameController.instance.AddTime(-damage);
    }
}
