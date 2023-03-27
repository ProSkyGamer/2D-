using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour
{
    [SerializeField] public string[] keys;
    [SerializeField] private bool[] toggleKeys;

    public static Keys InstanceKeys { get; set; }

    private void Awake()
    {
        if (InstanceKeys != null)
            Destroy(this.gameObject);
        else
            InstanceKeys = this;
    }

    public void AddKey(string keyName)
    {
        foreach (string key in keys)
        {
            if (keyName == key)
            {
                PlayerPrefs.SetInt(key, 1);
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
