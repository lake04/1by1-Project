using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DroppableUI : MonoBehaviour,IPointerEnterHandler,IDropHandler,IPointerExitHandler
{
    private Image image;
    private RectTransform rect;
    public ItempType invetroyType;
  

    void Awake()
    {
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnDrop(PointerEventData eventData)
    {
        var draggable = eventData.pointerDrag.GetComponent<DraggablUI>();
        if (draggable == null || draggable.itemData == null) return;

        if (draggable.itemData.type == invetroyType)
        {
            draggable.transform.SetParent(transform);
            draggable.GetComponent<RectTransform>().position = rect.position;
        }
        else
        {
            Debug.Log("올바른 인벤토리가 아닙니다.");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Color.blue;
    }
}
