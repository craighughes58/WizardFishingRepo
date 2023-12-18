using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fish", menuName = "ScriptableObjects/FishScriptableObject", order = 1)]
public class FishScriptableObject : ScriptableObject
{
    public string fishName;
    public float health;
    public float speed;
    public Sprite sprite;
    public HitBehavior hitBehavior;
    public FishMovement movement;
    public Collider2D collider;
}
