using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSomeStats : MonoBehaviour
{
    [SerializeField] private bool isNeedToChangeJumpForce;
    [SerializeField] private float newJumpForce;
    [SerializeField] private bool isNeedToChangeSpeed;
    [SerializeField] private float newSpeed;
    [SerializeField] private bool isNeedToChangeHealth;
    [SerializeField] private int newHealth;
    
    private GameObject healthOnSceen;
    private Movement _player;

    private void Start()
    {
        _player = GameObject.FindObjectOfType<Movement>();
        healthOnSceen = GameObject.Find("AllHealth");
    }

    public void ChangeStats()
    {
        if(isNeedToChangeJumpForce)
        {
            _player.jumpForce = newJumpForce;
            PlayerPrefs.SetFloat("prevJumpForce", newJumpForce);
        }
        if(isNeedToChangeSpeed)
        {
            _player.speed = newSpeed;

        }
        if(isNeedToChangeHealth)
        {
            if(newHealth > healthOnSceen.GetComponentsInChildren<Image>().Length)
            {
                int prevHealth = healthOnSceen.GetComponentsInChildren<Image>().Length;
                for (int i = 1; i <= newHealth - prevHealth; i++)
                {
                    GameObject newHeart = Instantiate(healthOnSceen.GetComponentsInChildren<Image>()[healthOnSceen.GetComponentsInChildren<Image>().Length - 1].gameObject,healthOnSceen.transform);
                    _player.hearts.Add(newHeart.GetComponent<Image>());
                   
                }
                _player.ChangeLives();
                _player.health = _player.health + (newHealth - prevHealth);
            }
        }
    }
}
