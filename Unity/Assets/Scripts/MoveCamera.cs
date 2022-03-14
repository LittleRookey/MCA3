using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform player;
    Vector3 offset = new Vector3(0, 1.678f, 0);
    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
