using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEnemyController : MonoBehaviour
{
    GameObject player;
    float speed = 2.0f;
    Animator anim;
    public bool bIsAttacking = false;
    public float damageAmount = 0.1f;
    public float health = 1.0f;
        
    void Start()
    {
      player = GameObject.FindWithTag("Player");
      // targetTransform.position = new Vector3(this.transform.position.x, player.transform.position.y, this.transform.position.z);
      anim = this.GetComponent<Animator>();
    }
    
    void Update()
    {
      if (health <= 0)
      {
        player.GetComponent<PlayerController>().bIsBeingAttack = false;
        GameObject.Destroy(this.gameObject);
      }
    }
    
    void FixedUpdate()
    {
      float distanceFromPlayer = Vector3.Distance(player.transform.position, this.transform.position);
      if (distanceFromPlayer <= 100.0f)
      {
        this.transform.LookAt(player.transform, Vector3.up);
        if (player.GetComponent<PlayerController>().bIsLookingLeft)
        {
          this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(player.transform.position.x-5, player.transform.position.y+1, player.transform.position.z), Time.deltaTime*speed);
        }
        else{
          this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(player.transform.position.x+5, player.transform.position.y+1, player.transform.position.z), Time.deltaTime*speed);
        }
        if (distanceFromPlayer <= 5.0f)
        {
          anim.SetFloat("Speed", 0.5f);
          bIsAttacking = true;
          player.GetComponent<PlayerController>().bIsBeingAttack = true;
          if (player.GetComponent<PlayerController>().attackBtn.GetComponent<AttackBtnHandler>().bIsAttacking)
          {
            if ((int)Time.frameCount % 30 == 0)
            {
              health -= 10*player.GetComponent<PlayerController>().playerPower*Time.deltaTime;
            }
          }
        }
        else{
          anim.SetFloat("Speed", 0.0f);
          bIsAttacking = false;
          player.GetComponent<PlayerController>().bIsBeingAttack = false;
        }
      }
      if (bIsAttacking && (int)Time.frameCount % 30 == 0)
      {
        player.GetComponent<PlayerController>().health -= damageAmount;
      }
    }
}
