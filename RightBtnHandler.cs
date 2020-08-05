using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightBtnHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    GameObject player;
    [HideInInspector]
    public bool bIsRightBtnPressed = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        bIsRightBtnPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        bIsRightBtnPressed = false;
    }

    public void MoveRight()
    {
        if (player)
        {
            player.transform.rotation = Quaternion.Euler(0,0,0);
            player.transform.position = new Vector3(
                player.transform.position.x+player.GetComponent<PlayerController>().btnSpeed*Time.deltaTime,
                player.transform.position.y,
                player.transform.position.z
            );
        }
    }

    void FixedUpdate()
    {
        if (bIsRightBtnPressed)
        {
            MoveRight();
        }
    }
}
