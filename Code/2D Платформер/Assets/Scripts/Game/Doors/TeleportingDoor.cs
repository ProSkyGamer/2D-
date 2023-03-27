using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportingDoor : Door
{
    private RawImage hiddenSceen;

    [SerializeField] private Transform teleportPosition;

    private bool startShowing;
    private bool startHiding;


    new private void Start()
    {
        hiddenSceen = gameObject.GetComponentInChildren<RawImage>();
        hiddenSceen.gameObject.SetActive(false);
        base.Start();
    }
    public void OnDoorOpen()
    {
        if (isKeyRequired)
        {
            GameObject.FindObjectOfType<Movement>().menuOpened = true;
            _canvasMenu.SetActive(true);
            _canvasMenu.GetComponentInChildren<Text>().text = $"Вы уверены что хотите использовать {requiredKey}, чтобы открыть дверь?" +
                $"\n У вас есть {PlayerPrefs.GetInt(requiredKey)} / 1 нужных ключей";
            if (PlayerPrefs.GetInt(requiredKey) == 0)
                _canvasMenu.GetComponentInChildren<Button>().interactable = false;
        }
        else
        {
            startShowing = true;
            GameObject.FindObjectOfType<Movement>().menuOpened = true;
        }
    }

    public void OpenDoor()
    {
        if (PlayerPrefs.GetInt(requiredKey) == 1)
        {
            startShowing = true;
            PlayerPrefs.SetInt(requiredKey, 0);
            if (gameObject.TryGetComponent(out ChangeSomeStats changeSomeStats))
                changeSomeStats.ChangeStats();
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

    private void FixedUpdate()
    {
        if (startShowing)
        {
            hiddenSceen.gameObject.SetActive(true);
            hiddenSceen.color = new Color(hiddenSceen.color.r, hiddenSceen.color.g,
                hiddenSceen.color.b, hiddenSceen.color.a + 0.05f);
            if (hiddenSceen.color.a >= 0.99)
            {
                startShowing = false;
                Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.7f, layer);
                collider[0].transform.position = teleportPosition.position;
                StartCoroutine(DelayShowing());
            }
        }
        else if (startHiding)
        {
            hiddenSceen.color = new Color(hiddenSceen.color.r, hiddenSceen.color.g,
                hiddenSceen.color.b, hiddenSceen.color.a - 0.05f);
            if (hiddenSceen.color.a <= 0)
            {
                hiddenSceen.gameObject.SetActive(false);
                startHiding = false;
                GameObject.FindObjectOfType<Movement>().menuOpened = false;
            }
        }

    }

    private IEnumerator DelayShowing()
    {
        yield return new WaitForSeconds(0.5f);
        startHiding = true;
        if (gameObject.TryGetComponent(out ChangeSomeStats changeSomeStats))
            changeSomeStats.ChangeStats();
    }
}
