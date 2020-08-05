using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpBtnHandler : MonoBehaviour, IPointerDownHandler
{
    public bool bIsClicked = false;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
      player = GameObject.FindWithTag("Player");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
      bIsClicked = true;
    }

    public void Jump()
    {
      if (player)
      {
        if (player.GetComponent<PlayerController>().bCanJump && player.GetComponent<PlayerController>().isCollidingFloor)
        {
          player.GetComponent<Rigidbody>().AddForce(Vector3.up*player.GetComponent<PlayerController>().jumpForce);
          player.GetComponent<PlayerController>().PlayJumpAnim();
        }
      }
    }
}
