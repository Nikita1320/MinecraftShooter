using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class FireButton : MonoBehaviour, IPointerDownHandler,IPointerUpHandler
{
    [SerializeField] private UnityEvent holdEvent;
    [SerializeField] private bool isHold = false;
    private void Update()
    {
        if (isHold)
        {
            holdEvent?.Invoke();
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isHold = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isHold = false;
    }
}
