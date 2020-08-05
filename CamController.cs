using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    GameObject player;
    float smoothTime = 0.3f;
    Vector3 velocity = Vector3.zero;
    [HideInInspector]
    public float xOffset;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void FixedUpdate() {
        this.transform.position = Vector3.SmoothDamp(this.transform.position, new Vector3(player.transform.position.x+xOffset, player.transform.position.y+3, this.transform.position.z), ref velocity,  smoothTime);
    }
}
