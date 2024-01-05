using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceSpikeBehaviour : MonoBehaviour
{
    #region Private Variables

    #region Serialized Variables

    [Tooltip("How long the projectile lives after not colliding")]
    [SerializeField] private float _lifeTime;
    [Tooltip("How fast the projectile moves")]
    [SerializeField] private float _speed;
    [Tooltip("How much damage the force spike inflicts")]
    [SerializeField] private int _damage;
    #endregion
    #endregion

    /// <summary>
    /// Start is called before the first frame update
    /// delayed destroys the projectile so that it doesn't linger after use
    /// </summary>
    void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    /// <summary>
    /// Called every fixed frame rate frame
    /// moves the projectile towards the right of the screen at a set speed 
    /// </summary>
    private void FixedUpdate()
    {
        transform.Translate(Vector2.down * Time.deltaTime * _speed);
    }


    #region Collisions and Triggers
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.gameObject.tag.Equals("Anchor") && !collision.gameObject.tag.Equals("ForceField") && !collision.gameObject.tag.Equals("Player"))
        {
            IDamageable target = collision.transform.GetComponent<IDamageable>();
            if (target != null)
            {
                target.TakeDamage(_damage);
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.tag.Equals("Anchor") && !collision.gameObject.tag.Equals("ForceField") && !collision.gameObject.tag.Equals("Player"))
        {
            IDamageable target = collision.transform.GetComponent<IDamageable>();
            if (target != null)
            {
                target.TakeDamage(_damage);
            }
            Destroy(gameObject);
        }
    }
    #endregion


    #region
    public int GetDamage()
    {
        return _damage;
    }
    #endregion
}
