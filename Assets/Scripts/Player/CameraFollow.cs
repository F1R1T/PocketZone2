using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float speed;
    public GameObject player;

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - player.transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 targetPosition = player.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.fixedDeltaTime);
    }
}
