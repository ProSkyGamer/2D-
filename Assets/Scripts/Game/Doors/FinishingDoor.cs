using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.UI;

public class FinishingDoor : Door
{

    public void OnDoorOpen()
    {
        if (isKeyRequired)
        {
            GameObject.FindObjectOfType<Movement>().menuOpened = true;
            _canvasMenu.SetActive(true);
            _canvasMenu.GetComponentInChildren<Text>().text = "Вы уверены что хотите использовать " + requiredKey + ", чтобы открыть дверь?" +
                "<p> У вас есть " + PlayerPrefs.GetInt(requiredKey) + "/1 нужных ключей";
            if (PlayerPrefs.GetInt(requiredKey) == 0)
                _canvasMenu.GetComponentInChildren<Button>().interactable = false;
        }
        else
        {
            int levelNumber = System.Convert.ToInt32(EditorSceneManager.GetActiveScene().name.Replace("Level", ""));
            if (levelNumber >= PlayerPrefs.GetInt("levelReached"))
            {
                PlayerPrefs.SetInt("levelReached", levelNumber + 1);
            }
            EditorSceneManager.LoadScene("Menu");
        }
    }

    public void OpenDoor()
    {
        if (PlayerPrefs.GetInt(requiredKey) == 1)
        {
            PlayerPrefs.SetInt(requiredKey, 0);
            int levelNumber = System.Convert.ToInt32(EditorSceneManager.GetActiveScene().name.Replace("Level", ""));
            if (levelNumber >= PlayerPrefs.GetInt("levelReached"))
            {
                PlayerPrefs.SetInt("levelReached", levelNumber + 1);
            }
            EditorSceneManager.LoadScene("Menu");
        }
        else
            print("Необходим ключ!");
    }

    private void Update()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.7f, layer);
        if (collider.Length > 0)
        {
            _canvasButton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                OnDoorOpen();
            }
        }
        else
        {
            _canvasButton.SetActive(false);
        }
    }
}
