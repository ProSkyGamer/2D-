using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    [SerializeField] private GameObject npcButton;

    private bool turnOnDialogueBtn;
    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.5f, layer);

        if(colliders.Length>0)
        {
            npcButton.SetActive(true);
        }
        else
        {
            npcButton.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1.5f);
    }
}
