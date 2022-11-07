using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drop2 : MonoBehaviour, IDropHandler
{
    public int id;

    public PointerEventData eventData1;
    public bool EndGame = false;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            //������ ������
            if (eventData.pointerDrag.GetComponent<DragAndDrop>().Endid == id)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                eventData.pointerDrag.GetComponent<DragAndDrop>().enabled = false;
                eventData.pointerDrag.GetComponent<DragAndDrop>().canvasGroup.alpha = 1f;
                EndGame = true;
            }


            //������ ���� ��
            if (eventData.pointerDrag.GetComponent<DragAndDrop>().id == id || eventData.pointerDrag.GetComponent<DragAndDrop>().Endid==3)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                eventData.pointerDrag.GetComponent<DragAndDrop>().canvasGroup.alpha = 1f;
            }
            else
            {
                eventData.pointerDrag.GetComponent<DragAndDrop>().ResetPosition();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("������");
        collision.gameObject.GetComponent<DragAndDrop>().ResetPosition();
    }

}
