using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFish : MonoBehaviour, IDamageable
{
    [SerializeField] int maxHP;
    [SerializeField] FishType fishType;
    int currentHP;

    protected virtual void Awake()
    {
        currentHP = maxHP;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHP -= damage;
        if(currentHP <= 0) 
        {
            Die();
        }
    }

    public virtual void Die()
    {
        GameController.instance.CollectFish(fishType);
        Destroy(gameObject);
    }
}
