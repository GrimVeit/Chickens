using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] private bool isVisible = true;
    [SerializeField] private Transform point;
    [SerializeField][Range(0, 1000)] private float size;
    [SerializeField] private Color color;
    [SerializeField] private DrawType drawType;


    private void OnDrawGizmos()
    {
        if(point != null)
        {
            if (isVisible)
            {
                Gizmos.color = color;
                switch (drawType)
                {
                    case DrawType.Sphere:
                        Gizmos.DrawSphere(point.position, size);
                        break;
                    case DrawType.Cube:
                        Gizmos.DrawCube(point.position, new Vector3(size, size, size));
                        break;
                }
            }
        }
    }
}

public enum DrawType
{
    Sphere, Cube
}
