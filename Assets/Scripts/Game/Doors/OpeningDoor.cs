using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpeningDoor : Door
{

    [SerializeField] private GameObject doorToOpen;


    public void OnDoorOpen()
    {
        
        if (isKeyRequired)
        {
            GameObject.FindObjectOfType<Movement>().menuOpened = true;
            _canvasMenu.SetActive(true);
            _canvasMenu.GetComponentInChildren<Text>().text = "Вы уверены что хотите использовать " + requiredKey + ", чтобы открыть дверь?" +
                "\n У вас есть " + PlayerPrefs.GetInt(requiredKey) + "/1 нужных ключей";
            if (PlayerPrefs.GetInt(requiredKey) == 0)
                _canvasMenu.GetComponentInChildren<Button>().interactable = false;
        }
        else
        {
            Destroy(gameObject);
            Destroy(doorToOpen);
            if(isNeedChangeStats)
                gameObject.GetComponent<ChangeSomeStats>().ChangeStats();

        }
    }

    public void OpenDoor()
    {
        if (PlayerPrefs.GetInt(requiredKey) == 1)
        {
            PlayerPrefs.SetInt(requiredKey, 0);
            Destroy(gameObject);
            Destroy(doorToOpen);
            if (isNeedChangeStats)
                gameObject.GetComponent<ChangeSomeStats>().ChangeStats();
        }
        GameObject.FindObjectOfType<Movement>().menuOpened = false;

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
