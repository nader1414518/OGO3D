using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    // public GameObject healthBar;
    GameObject player;
    
    void Start()
    {
      player = GameObject.FindWithTag("Player");
    }
    
    void Update()
    {
        this.GetComponent<Slider>().value = player.GetComponent<PlayerController>().health;
    }
}
