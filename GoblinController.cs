using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinController : MonoBehaviour
{
    GameObject player;
    Animator Gob_Anim;
    float speed = 3.0f;
    void Start()
    {
      player = GameObject.FindWithTag("Player");
      Gob_Anim = this.GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
      this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(player.transform.position.x, this.transform.position.y, this.transform.position.z), speed*Time.deltaTime);
      if (speed > 0.0f)
      {
        Gob_Anim.SetFloat("Speed", 1.0f);
      }
      else{
        Gob_Anim.SetFloat("Speed", 0.0f);
      }
    }

    void OnCollisionEnter(Collision collision)
    {
      if (collision.gameObject.tag == "Player")
      {
        speed = 0.0f;
        // Play Attack here
      }
    }

    void OnCollisionExit(Collision collision)
    {
      if (collision.gameObject.tag == "Player")
      {
        speed = 3.0f;
      }
    }
}
