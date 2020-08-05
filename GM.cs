using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GM : MonoBehaviour {
    public AudioSource audioSource;
    public AudioClip clickSound;
    public Text scoreTxt;
    public Text manaTxt;
    public Text fpsText;
    public float deltaTime;
    GameObject player;
    public GameObject wheel;
    int wheelRot = 2;
    void Start () {
        player = GameObject.FindWithTag ("Player");
        audioSource = this.GetComponent<AudioSource>();
        Application.targetFrameRate = 300;
    }
    void Update () {
        if (player)
        {
          if (player.transform.position.y < -20.0f) {
              SceneManager.LoadScene ("Game", LoadSceneMode.Single);
          }
        }
        if (wheel)
        {
          wheel.transform.rotation = Quaternion.Euler(wheel.transform.rotation.x+wheelRot, 90, 0);
          wheelRot++;
        }
        if (fpsText)
        {
          deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
          float fps = 1.0f / deltaTime;
          fpsText.text = "FPS: " + Mathf.Ceil (fps).ToString ();
        }
    }
}
