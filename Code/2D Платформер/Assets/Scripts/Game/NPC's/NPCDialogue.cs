using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    [SerializeField] private GameObject npcButton;

    private void Update()
    {
        float interactableDistance = 1.5f;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactableDistance, layer);

        if (colliders.Length > 0)
        {
            npcButton.SetActive(true);
        }
        else
        {
            npcButton.SetActive(false);
        }
    }
}
