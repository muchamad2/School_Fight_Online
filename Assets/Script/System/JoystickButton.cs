using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickButton : MonoBehaviour,IPointerUpHandler,IPointerDownHandler
{
    private bool pressed;

    public bool isPressed { get => pressed; set => pressed = value; }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }

}
