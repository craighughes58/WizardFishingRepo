using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBehaviour : MonoBehaviour
{

    #region Private Variables
    //the current transform that the ball is moving towards
    private Transform _target = null;
    #endregion

    #region Serialized Variables

    [Tooltip("How fast the lightning ball moves")]
    [SerializeField] private float _speed;

    [Tooltip("The perception radius of the ball")]
    [SerializeField] private float _radius;

    [Tooltip("How many targets the lightning ball can have before exploding")]
    [SerializeField] private int _maxTargets;

    [Tooltip("The layer the fish are on")]
    [SerializeField] private LayerMask _fishLayer;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        FindNewTarget();
    }

    private void FixedUpdate()
    {
        MoveTowardsTarget();   
    }
    #region Movement

    private void MoveTowardsTarget()
    {
        if(_target == null)
        {
            FindNewTarget();
            return;
        }
        transform.position = Vector2.MoveTowards(transform.position,_target.position,_speed);
    }
    #endregion

    #region Targeting
    private void FindNewTarget()
    {

        if(--_maxTargets < 0)
        {
            DestroyLightningBall();
        }
        Transform Closest;
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _radius,_fishLayer);
        if(hits.Length <= 0)
        {
            DestroyLightningBall();
        }
        Closest = hits[0].gameObject.transform;
        foreach(Collider2D f in hits)
        {
            if(Vector2.Distance(transform.position,Closest.position) > Vector2.Distance(transform.position,f.gameObject.transform.position))
            {
                Closest = f.gameObject.transform;
            }
        }
    }


    #endregion

    #region Collision and Triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    #endregion

    #region Destruction
    private void DestroyLightningBall()
    {
        //sound effect here

        //visual effects

        //
        Destroy(gameObject);
    }
    #endregion

    #region Getters and setters

    #endregion
}
