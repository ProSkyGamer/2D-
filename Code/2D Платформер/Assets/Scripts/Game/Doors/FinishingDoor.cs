using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishingDoor : Door
{
    public void OnDoorOpen()
    {
        if (isKeyRequired)
        {
            GameObject.FindObjectOfType<Movement>().menuOpened = true;
            _canvasMenu.SetActive(true);
            _canvasMenu.GetComponentInChildren<Text>().text = $"�� ������� ��� ������ ������������ {requiredKey}, ����� ������� �����?" +
                $"\n � ��� ���� {PlayerPrefs.GetInt(requiredKey)} / 1 ������ ������";
            if (PlayerPrefs.GetInt(requiredKey) == 0)
                _canvasMenu.GetComponentInChildren<Button>().interactable = false;
        }
        else
        {
            int levelNumber = System.Convert.ToInt32(SceneManager.GetActiveScene().name.Replace("Level", ""));
            if (levelNumber >= PlayerPrefs.GetInt("levelReached"))
            {
                PlayerPrefs.SetInt("levelReached", levelNumber + 1);
            }
            SceneManager.LoadScene("Menu");
        }
    }

    public void OpenDoor()
    {
        if (PlayerPrefs.GetInt(requiredKey) == 1)
        {
            PlayerPrefs.SetInt(requiredKey, 0);
            int levelNumber = System.Convert.ToInt32(SceneManager.GetActiveScene().name.Replace("Level", ""));
            if (levelNumber >= PlayerPrefs.GetInt("levelReached"))
            {
                PlayerPrefs.SetInt("levelReached", levelNumber + 1);
            }
            SceneManager.LoadScene("Menu");
        }
    }

    private void Update()
    {
        float interactableDistance = 0.7f;
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, interactableDistance, layer);

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
