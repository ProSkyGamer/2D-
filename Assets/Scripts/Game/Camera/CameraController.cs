using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 pos;
    private bool isFollowingCamera = true;

    public static CameraController CameraControl { get; set; }

    private void Awake()
    {
        if (!player)
            player = FindObjectOfType<Movement>().transform;
        TogglePlayerPosition();
        transform.position = pos;
    }

    private void Update()
    {
        if (isFollowingCamera)
        {
            TogglePlayerPosition();
            transform.position = Vector3.Lerp(new Vector3(transform.position.x,(transform.position.y+pos.y)/2,transform.position.z), pos, Time.deltaTime);
        }
    }

    public void StopCameraFollowing()
    {
        isFollowingCamera = false;
    }

    private void TogglePlayerPosition()
    {
        if (player)
        {
            pos = player.position;
            pos.z = -10f;
            pos.y += 2f;
        }
    }
}
