using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] GameObject mobileButtons;

    private void Start()
    {
        if (PlayerPrefs.GetInt("joystickEnabled") == 1)
        {
            gameObject.GetComponent<Toggle>().isOn = true;
        }
        else
        {
            gameObject.GetComponent<Toggle>().isOn = false;
        }
    }
    public void onChangeJoystick()
    {
        if (gameObject.GetComponent<Toggle>().isOn)
        {
            PlayerPrefs.SetInt("joystickEnabled", 1);
            mobileButtons.SetActive(true); 
        }
        else
        {
            PlayerPrefs.SetInt("joystickEnabled", 0);
            mobileButtons.SetActive(false);
        }
    }
}
