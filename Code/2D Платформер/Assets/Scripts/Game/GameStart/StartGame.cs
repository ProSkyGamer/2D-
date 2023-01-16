using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private GameObject mobileButtons;

    private void Start()
    {
        if (PlayerPrefs.GetInt("joystickEnabled") == 1)
        {
            mobileButtons.SetActive(true);
        }
        else
        {
            mobileButtons.SetActive(false);
        }

        Destroy(gameObject);
    }
}
