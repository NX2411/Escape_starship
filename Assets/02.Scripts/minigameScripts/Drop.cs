using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler
{
    public int id;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if(eventData.pointerDrag != null)
        {
            if(eventData.pointerDrag.GetComponent<DragAndDrop>().id == id)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                eventData.pointerDrag.GetComponent<DragAndDrop>().enabled = false;
                eventData.pointerDrag.GetComponent<DragAndDrop>().canvasGroup.alpha = 1f;
            }
            else
            {
                eventData.pointerDrag.GetComponent<DragAndDrop>().ResetPosition();
            }
        }
    }
}
