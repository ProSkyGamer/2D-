using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Button[] levels;

    private void Start()
    {
        if(PlayerPrefs.GetInt("levelReached")==0)
            PlayerPrefs.SetInt("levelReached", 1);
        int levelReached = PlayerPrefs.GetInt("levelReached");
        for (int i = 0; i < levels.Length; i++)
        {
            if (i + 1 > levelReached)
                levels[i].interactable = false;
        }
    }

    public void Select(int numberInBuild)
    {
        SceneManager.LoadScene("Level" + numberInBuild);
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }

}
