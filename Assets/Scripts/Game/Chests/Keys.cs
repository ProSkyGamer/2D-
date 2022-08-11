using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour
{
    [SerializeField] public string[] keys;
    [SerializeField] private bool[] toggleKeys;

    public static Keys InstanceKeys { get; set; }

    private void Start()
    {
        /*for (int i = 0; i <= keys.Length; i++)
        {
            toggleKeys = PlayerPrefs.GetInt(keys[i]) == 1 ? true : false;
        }*/
    }

    public void AddKey(string keyName)
    {
        foreach(string key in keys)
        {
            if(keyName == key)
            {
                PlayerPrefs.SetInt(key, 1);
                print(PlayerPrefs.GetInt(key));
                break;
            }
        }
    }

    public void ResetAllKeys()
    {
        foreach (string key in keys)
        {
            PlayerPrefs.SetInt(key, 0);
        }
    }
}
