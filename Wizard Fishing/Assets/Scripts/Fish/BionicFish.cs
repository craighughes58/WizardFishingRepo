using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BionicFish : BaseFish
{
    [SerializeField] float warningDelay;
    [SerializeField] float minAttackRecharge, maxAttackRecharge;
    [SerializeField] float attackRadius;
    [SerializeField] int damageDealt;
    [SerializeField] LayerMask playerLayer;
    GameObject warning, shockZone;

    protected override void Awake()
    {
        base.Awake();
        warning = transform.GetChild(1).gameObject;
        shockZone = transform.GetChild(2).gameObject;
        StartCoroutine(AttackLoop());
    }
    IEnumerator AttackLoop()
    {
        while (true)
        {
            
            warning.SetActive(true);
            yield return new WaitForSeconds(warningDelay);
            warning.SetActive(false);
            shockZone.SetActive(true);
            Attack();
            yield return new WaitForSeconds(.5f);
            shockZone.SetActive(false);
            yield return new WaitForSeconds(Random.Range(minAttackRecharge, maxAttackRecharge));
        }
    }

    void Attack()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, attackRadius, Vector2.zero,0, playerLayer);
        if (hit.transform == null) return;
        PlayerDamageable damageable = hit.transform.GetComponent<PlayerDamageable>();
        if (damageable == null) return;
        damageable.TakeDamage(damageDealt);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
