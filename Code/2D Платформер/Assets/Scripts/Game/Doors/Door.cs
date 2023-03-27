using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] protected LayerMask layer;

    [SerializeField] protected bool isKeyRequired;
    [SerializeField] protected string requiredKey;

    [SerializeField] protected GameObject _canvasButton;
    [SerializeField] protected GameObject _canvasMenu;

    private const string PLAYER_LAYER_MASK = "Player";

    protected void Start()
    {
        layer = LayerMask.GetMask(PLAYER_LAYER_MASK);
    }
}
