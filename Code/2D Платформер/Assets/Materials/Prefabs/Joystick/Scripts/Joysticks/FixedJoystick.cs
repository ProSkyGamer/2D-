using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FixedJoystick : Joystick
{
    protected override void Start()
    {
        base.Start();
        background.GetComponent<Image>().color = new Color(background.GetComponent<Image>().color.r, background.GetComponent<Image>().color.g, background.GetComponent<Image>().color.b, 0.3f);
        handle.GetComponent<Image>().color = new Color(handle.GetComponent<Image>().color.r, handle.GetComponent<Image>().color.g, handle.GetComponent<Image>().color.b, 0.3f);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        background.GetComponent<Image>().color = new Color(background.GetComponent<Image>().color.r, background.GetComponent<Image>().color.g, background.GetComponent<Image>().color.b, 1);
        handle.GetComponent<Image>().color = new Color(handle.GetComponent<Image>().color.r, handle.GetComponent<Image>().color.g, handle.GetComponent<Image>().color.b, 1);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        background.GetComponent<Image>().color = new Color(background.GetComponent<Image>().color.r, background.GetComponent<Image>().color.g, background.GetComponent<Image>().color.b, 0.3f);
        handle.GetComponent<Image>().color = new Color(handle.GetComponent<Image>().color.r, handle.GetComponent<Image>().color.g, handle.GetComponent<Image>().color.b, 0.3f);
        base.OnPointerUp(eventData);
    }

}