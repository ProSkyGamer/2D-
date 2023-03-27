using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestOpen : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    [SerializeField] private string keyName;

    [SerializeField] private GameObject _canvas;

    public void OnChestOpen()
    {
        Keys.InstanceKeys.AddKey(keyName);
        Destroy(gameObject);
    }

    private void Update()
    {
        float interactableDistance = 0.7f;
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, interactableDistance, layer);
        if (collider.Length > 0)
        {
            _canvas.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                OnChestOpen();
            }
        }
        else
        {
            _canvas.SetActive(false);
        }
    }

    /*private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }*/
}
