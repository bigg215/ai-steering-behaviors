using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAimpoint : MonoBehaviour
{

    public Transform attachmentPoint;
    private PlayerController _player;

    private void Awake()
    {
        _player = transform.root.GetComponent<PlayerController>();
    }

    void Update()
    {
        
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(attachmentPoint.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        
        if (!_player.IsFlipped())
        {
            if (angle > 90f)
            {
                angle = 90f;
            }
            if (angle < -90f)
            {
               angle = -90f;
            }
        } else
        {
            if(angle < 90f && angle > 0)
            {
                angle = 90f;
            }
            if(angle > -90f && angle < 0)
            {
                angle = -90f;
            }
        }

        attachmentPoint.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }
}
