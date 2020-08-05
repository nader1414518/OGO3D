using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HealingHandler : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {
    // Start is called before the first frame update
    GameObject player;
    public GameObject outerCircle;
    public GameObject healingBar;
    bool bIsHealing = false;
    int cyclesCount = 0;
    // Start is called before the first frame update
    void Start () {
        player = GameObject.FindWithTag ("Player");
        if (healingBar) {
            healingBar.SetActive (false);
        }
    }

    public void OnPointerDown (PointerEventData eventData) {
        bIsHealing = true;
    }

    public void OnPointerUp (PointerEventData eventData) {
        bIsHealing = false;
    }

    // Update is called once per frame
    void Update () {
        if (player.GetComponent<PlayerController> ().health < 1.0f) {
            if (bIsHealing) {
                cyclesCount++;
                if (healingBar) {
                    healingBar.SetActive (true);
                    healingBar.transform.position = Input.mousePosition;
                    // healingBar.GetComponentInChildren<Image> ().fillAmount = cyclesCount / 120.0f;
                    if (outerCircle) {
                        outerCircle.GetComponent<Image> ().fillAmount = cyclesCount / 120.0f;
                    }
                }
            } else {
                cyclesCount = 0;
                if (healingBar) {
                    // healingBar.GetComponentInChildren<Image> ().fillAmount = cyclesCount / 120.0f;
                    if (outerCircle) {
                        outerCircle.GetComponent<Image> ().fillAmount = cyclesCount / 120.0f;
                    }
                    healingBar.SetActive (false);
                }
            }
            if (cyclesCount == 120) {
                player.GetComponent<PlayerController> ().health = 1;
                cyclesCount = 0;
                if (healingBar) {
                    // healingBar.GetComponentInChildren<Image> ().fillAmount = cyclesCount / 120.0f;
                    if (outerCircle) {
                        outerCircle.GetComponent<Image> ().fillAmount = cyclesCount / 120.0f;
                    }
                    healingBar.SetActive (false);
                }
            }
        }
    }
}