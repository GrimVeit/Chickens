using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotValue : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private int index;
    [SerializeField] private RectTransform transformSlotValue;

    public int SlotID => id;
    public int Index => index;
    public RectTransform TransformSlotValue => transformSlotValue;
}
