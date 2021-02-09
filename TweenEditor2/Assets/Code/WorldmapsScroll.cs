using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WorldmapsScroll : ScrollRect
{
    public ScrollRect ScrollView;
    public RectTransform Content;
    public RectTransform InnerContent;
    //private RectTransform m_rectTransform = null;
    private float offsetX;
    private float offsetY;

    private Transform parent;
    protected Transform Parent
    {
        get
        {
            if (parent == null)
            {
                GameObject go = GameObject.Find("Canvas");
                if (go != null) parent = go.transform;
            }
            return parent;
        }
    }


    protected override void Start()
    {
        base.Start();
        offsetX = (Content.sizeDelta.x - GetCanvasWidth()) * 0.5f + (InnerContent.sizeDelta.x - Content.sizeDelta.x) * 0.5f;
        offsetY = (Content.sizeDelta.y - GetCanvasHeight()) * 0.5f + (InnerContent.sizeDelta.y - Content.sizeDelta.y) * 0.5f;
    }

    public float GetCanvasWidth()
    {
        return (Parent as RectTransform).rect.width;
    }

    public float GetCanvasHeight()
    {
        return (Parent as RectTransform).rect.height;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        Vector3 curPos = Content.anchoredPosition;
        Content.anchoredPosition = new Vector3(Mathf.Clamp(curPos.x, -offsetX, offsetX), Mathf.Clamp(curPos.y, -offsetY, offsetY), 0);
    }
}
