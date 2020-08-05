using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackBtnHandler : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public bool bIsAttacking = false;
    public bool bIsPressed = false;
    float attackTimer = 0.5f;

    public void OnPointerDown(PointerEventData eventData)
    {
      bIsPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
      bIsPressed = false;
    }

    void Update()
    {
      if (attackTimer <= 0.0f && bIsPressed)
      {
        attackTimer = 0.5f;
        bIsAttacking = true;
      }
      else if (attackTimer <= 0.0f && !(bIsPressed))
      {
        attackTimer = 0.5f;
        bIsAttacking = false;
      }
      else{
        attackTimer -= Time.deltaTime;
      }
    }
}
