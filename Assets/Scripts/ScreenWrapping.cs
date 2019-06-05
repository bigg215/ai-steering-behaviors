using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapping : MonoBehaviour
{
    private bool isVisible;
    private Camera cam;

    public void Start()
    {
        isVisible = true;
        cam = Camera.main;
    }

    private void OnBecameInvisible()
    {
        if (cam != null)
        {
            Vector3 viewportPosition = cam.WorldToViewportPoint(transform.position);
            Vector3 newPosition = transform.position;

            if (isVisible)
            {

                isVisible = false;

                if (viewportPosition.x > 1 || viewportPosition.x < 0)
                {
                    newPosition.x = -newPosition.x;
                }

                if (viewportPosition.y > 1 || viewportPosition.y < 0)
                {
                    newPosition.y = -newPosition.y;
                }

                transform.position = newPosition;
            }
        }

    }

    private void OnBecameVisible()
    {
        if (cam != null)
        {
            isVisible = true;
        }

    }
}
