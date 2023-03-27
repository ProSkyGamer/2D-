using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform followingObject;
    private Vector3 followingObjectPosition;
    private bool isCameraFollowing = true;

    public static CameraController CameraControl { get; set; }

    private void Awake()
    {
        if (!followingObject)
            followingObject = FindObjectOfType<Movement>().transform;
        TogglePlayerPosition();
        transform.position = followingObjectPosition;
    }

    private void Update()
    {
        if (isCameraFollowing)
        {
            TogglePlayerPosition();
            Vector3 newPosition = Vector3.Lerp(new Vector3(transform.position.x,
                (transform.position.y + followingObjectPosition.y) / 2,
                transform.position.z), followingObjectPosition, Time.deltaTime);
            transform.position = newPosition;
        }
    }

    public void StopCameraFollowing()
    {
        isCameraFollowing = false;
    }

    private void TogglePlayerPosition()
    {
        if (followingObject)
        {
            followingObjectPosition = followingObject.position;
            followingObjectPosition.z = -10f;
            followingObjectPosition.y += 2f;
        }
    }
}
