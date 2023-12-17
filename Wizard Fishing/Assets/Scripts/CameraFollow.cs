using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float yOffset;
    [SerializeField] Transform player;

    void Update()
    {
        transform.position = new Vector3(0, player.position.y + yOffset, -10);
    }
}
