using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform followingObject;
    [SerializeField] private float cameraYOffest = 2f;
    private const float CAMERA_Z_OFFSET = -10f;
    private Vector3 followingObjectPosition;
    private bool isCameraFollowing = true;

    private void Awake()
    {
        //Если объект не установлен, находим его по классу
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
            followingObjectPosition.z = CAMERA_Z_OFFSET;
            followingObjectPosition.y += cameraYOffest;
        }
    }
}
