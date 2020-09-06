﻿using UnityEditor;
using UnityEngine;

public class SnapToGround : MonoBehaviour
{
    [MenuItem("Custom/Snap To Ground %g")]
    public static void Ground()
    {
        foreach (var transform in Selection.transforms)
        {
            var hits = Physics.RaycastAll(transform.position + Vector3.up, Vector3.down, 10f);


            foreach (var hit in hits)
            {
                if (hit.collider.gameObject == transform.gameObject)
                    continue;

                Renderer renderer = transform.gameObject.GetComponent<Renderer>();
                Vector3 offset = Vector3.zero;
                if (renderer != null)
                {
                    offset.y = renderer.bounds.size.y / 2;
                }

                transform.position = hit.point + offset;
                break;
            }
        }
    }

}