using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Transform player;
    [SerializeField] float threshold;
    [SerializeField] float dampTime;
    [SerializeField] Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPos = (player.position + mousePos)/2f;

        targetPos.x = Mathf.Clamp(targetPos.x, -threshold+player.position.x, threshold+player.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, -threshold + player.position.y, threshold + player.position.y);

        this.transform.position = Vector3.SmoothDamp(this.transform.position,targetPos, ref velocity,dampTime);
        
        



    }
}
