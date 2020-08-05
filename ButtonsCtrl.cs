using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsCtrl : MonoBehaviour {
    public GameObject PausePanel;
    public GameObject MobileCtrlPanel;
    public GameObject pauseBtn;
    public GameObject itemsBtn;
    public GameObject healingPanel;
    public GameObject scorePanel;
    public GameObject craftPanel;
    public GameObject closeCraftBtn;
    public GM gm;
    public GameObject backToGameBtn;
    GameObject player;

    void Start () {
        player = GameObject.FindWithTag("Player");
        if (PausePanel) {
            PausePanel.SetActive (false);
        }
        if (scorePanel)
        {
          scorePanel.SetActive(true);
        }
        
        // Set Player Position to last saved position
        if (player)
        {
          PlayerData data = SaveSystem.LoadPlayer();
          player.GetComponent<PlayerController>().health = data.health;
          
          Vector3 position;
          position.x = data.position[0];
          position.y = data.position[1];
          position.z = data.position[2];
          
          player.transform.position = position;
        } 
    }
    public void StartGame () {
        // Load Progress
        if (player)
        {
          PlayerData data = SaveSystem.LoadPlayer();
          player.GetComponent<PlayerController>().health = data.health;
          
          Vector3 position;
          position.x = data.position[0];
          position.y = data.position[1];
          position.z = data.position[2];
          
          player.transform.position = position;
        } 
        if (gm)
        {
          gm.audioSource.clip = gm.clickSound;
          gm.audioSource.Play();
        }
        SceneManager.LoadScene ("Game", LoadSceneMode.Single);
        Time.timeScale = 1;
    }
    public void QuitGame () {
      if (gm)
      {
        gm.audioSource.clip = gm.clickSound;
        gm.audioSource.Play();
      }
      Application.Quit ();
    }
    public void PauseGame ()
    {
        if (gm)
        {
          gm.audioSource.clip = gm.clickSound;
          gm.audioSource.Play();
        }
        if (PausePanel) {
            PausePanel.SetActive (true);
            Time.timeScale = 0;
        }
        if (MobileCtrlPanel) {
            MobileCtrlPanel.SetActive (false);
        }
        if (pauseBtn) {
            pauseBtn.SetActive (false);
        }
        if (healingPanel) {
            healingPanel.SetActive (false);
        }
        if (itemsBtn)
        {
          itemsBtn.SetActive(false);
        }
        if (scorePanel)
        {
          scorePanel.SetActive(false);
        }
    }
    public void ResumeGame ()
    {
        if (gm)
        {
          gm.audioSource.clip = gm.clickSound;
          gm.audioSource.Play();
        }
        if (PausePanel) {
            PausePanel.SetActive (false);
            Time.timeScale = 1;
        }
        if (MobileCtrlPanel) {
            MobileCtrlPanel.SetActive (true);
        }
        if (pauseBtn) {
            pauseBtn.SetActive (true);
        }
        if (healingPanel) {
            healingPanel.SetActive (true);
        }
        if (itemsBtn)
        {
          itemsBtn.SetActive(true);
        }
        if (scorePanel)
        {
          scorePanel.SetActive(true);
        }
    }
    public void GotoMainMenu () {
        // Save Progress
        if (player)
        {
          SaveSystem.SavePlayer(player.GetComponent<PlayerController>());
        }
        if (gm)
        {
          gm.audioSource.clip = gm.clickSound;
          gm.audioSource.Play();
        }
        SceneManager.LoadScene ("MainMenu", LoadSceneMode.Single);
        Time.timeScale = 1;
    }

    public void SweepPlayer()
    {
      if (gm)
      {
        gm.audioSource.clip = gm.clickSound;
        gm.audioSource.Play();
      }
      if (player.GetComponent<PlayerController>().bIsLookingRight)
      {
        player.transform.position = new Vector3(
          player.transform.position.x+player.GetComponent<PlayerController>().sweepSpeed,
          player.transform.position.y,
          player.transform.position.z
        );
      }
      else if (player.GetComponent<PlayerController>().bIsLookingLeft)
      {
        player.transform.position = new Vector3(
          player.transform.position.x-player.GetComponent<PlayerController>().sweepSpeed,
          player.transform.position.y,
          player.transform.position.z
        );
      }
    }

    public void BackToGame()
    {
      if (backToGameBtn)
      {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
        // Set Player Position to last saved position
        if (player)
        {
          PlayerData data = SaveSystem.LoadPlayer();
          player.GetComponent<PlayerController>().health = data.health;
          
          Vector3 position;
          position.x = data.position[0];
          position.y = data.position[1];
          position.z = data.position[2];
          
          player.transform.position = position;
        } 
      }
    }

    public void GoToAvatarMode()
    {
      // Save Player Location
      if (player)
      {
        SaveSystem.SavePlayer(player.GetComponent<PlayerController>());
      }
      
      SceneManager.LoadScene("AvatarMode", LoadSceneMode.Single);
    }

    // public void CloseCraftingPanel()
    // {
    //   if (craftPanel)
    //   {
    //     craftPanel.SetActive(false);
    //   }
    //   if (closeCraftBtn)
    //   {
    //     closeCraftBtn.SetActive(false);
    //   }
    // }
}
