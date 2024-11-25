using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public RectTransform contentTransform;
    public List<RectTransform> buttonTransforms;
    public List<Vector2> vectorRelatives;
    
    void Awake()
    {
        Vector2 contentSize = contentTransform.rect.size;
        //Vector2 absolutePosition = new Vector2(vectorRelative.x * contentSize.x, vectorRelative.y * contentSize.y);

        //float distanceFromCenterX = Mathf.Abs(vectorRelative.x - 0.5f);
        //float distanceFromCenterY = Mathf.Abs(vectorRelative.y - 0.5f);

        //float adjustedPositionX = absolutePosition.x + (distanceFromCenterX * contentSize.x);
        //float adjustedPositionY = absolutePosition.y + (distanceFromCenterY * contentSize.y);

        //buttonTransform.anchoredPosition = new Vector2(adjustedPositionX, adjustedPositionY);

        for (int i = 0; i < buttonTransforms.Count; i++)
        {
            float normilizedX = vectorRelatives[i].x;
            float normilizedY = vectorRelatives[i].y;

            Vector2 absolutePosition = new Vector2(normilizedX * contentSize.x, normilizedY * contentSize.y);

            buttonTransforms[i].localPosition = absolutePosition;
        }
    }
}
