using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeftButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    GameObject player;
    [HideInInspector]
    public bool bIsLeftBtnPressed = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        bIsLeftBtnPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        bIsLeftBtnPressed = false;
    }

    public void MoveLeft()
    {
        if (player)
        {
            player.transform.rotation = Quaternion.Euler(0,180,0);
            player.transform.position = new Vector3(
                player.transform.position.x-player.GetComponent<PlayerController>().btnSpeed*Time.deltaTime,
                player.transform.position.y,
                player.transform.position.z
            );
        }
    }

    void FixedUpdate()
    {
        if (bIsLeftBtnPressed)
        {
            MoveLeft();
        }
    }
}
