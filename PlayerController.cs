using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {
    // audio Source
    public AudioSource audioSource;
    public AudioClip bgSound;
    // Inventory Controller
    public InventoryController invCtrlObj;
    // Game Manager Object
    GameObject gm;
    // Player Animator
    public Animator playerAnim;
    // Jump Button Object
    public GameObject jumpBtn;
    // Attack Button Object
    public GameObject attackBtn;
    // Left Button Object
    public GameObject leftBtn;
    // Right Button Object
    public GameObject rightBtn;
    // Main Camera Object
    public GameObject mainCamObj;
    // Speed used in button presses
    public float btnSpeed;
    // health of the player
    public float health;
    // sweep speed for the sweep button
    public float sweepSpeed;
    // Coin pickup score
    public int coinScore = 0;
    // mana stones pickups score
    public int manaScore = 0;
    // is the player looking left
    [HideInInspector]
    public bool bIsLookingRight = false;
    // is the player looking right
    [HideInInspector]
    public bool bIsLookingLeft = false;
    bool bIsInContactWithGoblin = false;
    public float jumpForce;
    public bool isCollidingFloor = false;
    public float jumpTimer = 0.2f;
    public bool bCanJump = false;
    public GameObject rightHand;
    public SwordItem basicSword;
    public List<string> weapons;
    public bool bIsBeingAttack = false;
    public bool bIsDead = false;
    public float playerPower = 1.0f;

    private void Start()
    {
        // Assigning the Main Camera Object
        mainCamObj = GameObject.FindWithTag("MainCamera");
        // Assigning the Game Manager Object
        gm = GameObject.FindWithTag("GameManager");
        // Initial Health of the player
        health = 0.5f;
        // Initial coins
        coinScore = 0;
        // Mana Stones Score
        manaScore = 0;
        // Idle Animation
        playerAnim = this.GetComponentInChildren<Animator>();
        if (playerAnim)
        {
          // Idle Animation
          playerAnim.SetFloat("Speed", 0.0f);
        }
        // BG Sound
        audioSource.clip = bgSound;
        audioSource.Play();
    }

    void RegenerateHealth()
    {
      if (health >= 1)
      {
        health = 1;
      }
      else{
        health += Time.deltaTime;
      }
    }


    void Update()
    {
      if (Time.frameCount%30==0 && bIsInContactWithGoblin)
      {
        health -= Time.deltaTime;
      }
      if (Time.frameCount%300==0 && !(bIsInContactWithGoblin))
      {
        RegenerateHealth();
      }

      // Jump Cool down
      if (jumpTimer <= 0)
      {
        bCanJump = true;
        jumpTimer = 0.2f;
      }
      else{
        bCanJump = false;
        jumpTimer -= Time.deltaTime;
      }

      if (health <= 0)
      {
        bIsDead = true;
      }

      // Input Animation
      if (leftBtn && rightBtn && jumpBtn && attackBtn)
      {
        if (rightBtn.GetComponent<RightBtnHandler>().bIsRightBtnPressed && !(jumpBtn.GetComponent<JumpBtnHandler>().bIsClicked))
        {
          // Run Animation 
          playerAnim.SetFloat("Speed", 0.2f);
          if (bIsDead)
          {
            // Death Animation 
            playerAnim.SetFloat("Speed", 1.0f);
            Debug.Log("Player Died :) ");
            // Pause Game
            Time.timeScale = 0;
          }
        }
        else if (leftBtn.GetComponent<LeftButtonHandler>().bIsLeftBtnPressed && !(jumpBtn.GetComponent<JumpBtnHandler>().bIsClicked))
        {
          // Run Animation 
          playerAnim.SetFloat("Speed", 0.2f);
          if (bIsDead)
          {
            // Death Animation 
            playerAnim.SetFloat("Speed", 1.0f);
            Debug.Log("Player Died :) ");
            // Pause Game
            Time.timeScale = 0;
          }
        }
        else if (jumpBtn.GetComponent<JumpBtnHandler>().bIsClicked && leftBtn.GetComponent<LeftButtonHandler>().bIsLeftBtnPressed)
        {
          jumpBtn.GetComponent<JumpBtnHandler>().Jump();
          if (bIsDead)
          {
            // Death Animation 
            playerAnim.SetFloat("Speed", 1.0f);
            Debug.Log("Player Died :) ");
            // Pause Game 
            Time.timeScale = 0;
          }
        }
        else if (jumpBtn.GetComponent<JumpBtnHandler>().bIsClicked && rightBtn.GetComponent<RightBtnHandler>().bIsRightBtnPressed)
        {
          jumpBtn.GetComponent<JumpBtnHandler>().Jump();
          if (bIsDead)
          {
            // Death Animation 
            playerAnim.SetFloat("Speed", 1.0f);
            Debug.Log("Player Died :) ");
            // Pause Game
            Time.timeScale = 0;
          }
        }
        else if (jumpBtn.GetComponent<JumpBtnHandler>().bIsClicked)
        {
          jumpBtn.GetComponent<JumpBtnHandler>().Jump();
          if (bIsDead)
          {
            // Death Animation 
            playerAnim.SetFloat("Speed", 1.0f);
            Debug.Log("Player Died :) ");
            // Pause Game
            Time.timeScale = 0;
          }
        }
        else if (rightBtn.GetComponent<RightBtnHandler>().bIsRightBtnPressed && !(attackBtn.GetComponent<AttackBtnHandler>().bIsAttacking))
        {
          // Run Animation
          playerAnim.SetFloat("Speed", 0.2f);
          if (bIsDead)
          {
            // Death Animation 
            playerAnim.SetFloat("Speed", 1.0f);
            Debug.Log("Player Died :) ");
            // Pause Game
            Time.timeScale = 0;
          }
        }
        else if (leftBtn.GetComponent<LeftButtonHandler>().bIsLeftBtnPressed && !(attackBtn.GetComponent<AttackBtnHandler>().bIsAttacking))
        {
          // Run Animation 
          playerAnim.SetFloat("Speed", 0.2f);
          if (bIsDead)
          {
            // Death Animation 
            playerAnim.SetFloat("Speed", 1.0f);
            Debug.Log("Player Died :) ");
            // Pause Game
            Time.timeScale = 0;
          }
        }
        else if (attackBtn.GetComponent<AttackBtnHandler>().bIsAttacking && leftBtn.GetComponent<LeftButtonHandler>().bIsLeftBtnPressed)
        {
          // Player Attack Animation 
          playerAnim.SetFloat("Speed", 0.6f);
          if (bIsBeingAttack)
          {
            // Player Attacked Animation 
            playerAnim.SetFloat("Speed", 0.4f);
          }
          if (bIsDead)
          {
            // Death Animation 
            playerAnim.SetFloat("Speed", 1.0f);
            Debug.Log("Player Died :) ");
            // Pause Game
            Time.timeScale = 0;
          }
        }
        else if (attackBtn.GetComponent<AttackBtnHandler>().bIsAttacking && rightBtn.GetComponent<RightBtnHandler>().bIsRightBtnPressed)
        {
          // Player Attack Animation 
          playerAnim.SetFloat("Speed", 0.6f);
          if (bIsBeingAttack)
          {
            // Player Attacked Animation 
            playerAnim.SetFloat("Speed", 0.4f);
          }
          if (bIsDead)
          {
            // Death Animation 
            playerAnim.SetFloat("Speed", 1.0f);
            Debug.Log("Player Died :) ");
            // Pause Game
            Time.timeScale = 0;
          }
        }
        else if (attackBtn.GetComponent<AttackBtnHandler>().bIsAttacking)
        {
          // Player Attack Animation 
          playerAnim.SetFloat("Speed", 0.6f);
          if (bIsDead)
          {
            // Death Animation 
            playerAnim.SetFloat("Speed", 1.0f);
            Debug.Log("Player Died :) ");
            // Pause Game
            Time.timeScale = 0;
          }
        }
        else if (bIsBeingAttack)
        {
          // Player Attacked Animation 
          playerAnim.SetFloat("Speed", 0.4f);
          if (bIsDead)
          {
            // Death Animation 
            playerAnim.SetFloat("Speed", 1.0f);
            Debug.Log("Player Died :) ");
            // Pause Game
            Time.timeScale = 0;
          }
        }
        else if (bIsDead)
        {
          // Death Animation 
          playerAnim.SetFloat("Speed", 1.0f);
          Debug.Log("Player Died :) ");
          // Pause Game
          Time.timeScale = 0;
        }
        else{
          // int n = Random.Range(1,3);
          // if (n == 1)
          // {
          //   playerAnim.SetFloat("Speed", 0.0f);
          // }
          // else if (n == 2)
          // {
          //   playerAnim.SetFloat("Speed", 0.6f);
          // }
          // else if (n == 3)
          // {
          //   playerAnim.SetFloat("Speed", 0.8f);
          // }
          // Idle Animation 
          playerAnim.SetFloat("Speed", 0.0f);
        }
      }
      // float hor = Input.GetAxis("Horizontal");
      // float ver = Input.GetAxis("Vertical");
      //
      // if (Input.GetButtonDown("Jump") && this.GetComponent<Rigidbody>().velocity.y == 0)
      // {
      //   this.GetComponent<Rigidbody>().AddForce(Vector3.up*700f);
      // }
      //
      // if (Mathf.Abs(hor) > 0 && this.GetComponent<Rigidbody>().velocity.y == 0)
      // {
      //   playerAnim.SetBool("isRunning", true);
      // }
      // else{
      //   playerAnim.SetBool("isRunning", false);
      // }
      //
      // if (this.GetComponent<Rigidbody>().velocity.y > 0)
      // {
      //   playerAnim.SetBool("isJumping", true);
      // }
      // if (this.GetComponent<Rigidbody>().velocity.y < 0)
      // {
      //   playerAnim.SetBool("isJumping", false);
      //   playerAnim.SetBool("isFalling", true);
      // }
      //
      // if (this.GetComponent<Rigidbody>().velocity.y == 0)
      // {
      //   playerAnim.SetBool("isFalling", false);
      // }

    }

    public void PlayJumpAnim()
    {
      // Jump Animation 
      playerAnim.SetFloat("Speed", 0.8f);
    }

    void FixedUpdate () {
        // if the left button is pressed then the player is looking left and not right
        if (leftBtn.GetComponent<LeftButtonHandler> ().bIsLeftBtnPressed) {
            bIsLookingLeft = true;
            bIsLookingRight = false;
            // anim.SetFloat ("Speed", 0.5f);
        // if the right button is pressed then the player is looking right not left
        } else if (rightBtn.GetComponent<RightBtnHandler> ().bIsRightBtnPressed) {
            bIsLookingLeft = false;
            bIsLookingRight = true;
            // anim.SetFloat ("Speed", 0.5f);
        }

        // Redirecting Main Camera according to the player orientation
        if (this.bIsLookingRight) {
            mainCamObj.GetComponent<CamController> ().xOffset = 6.0f;
        } else if (this.bIsLookingLeft) {
            mainCamObj.GetComponent<CamController> ().xOffset = -6.0f;
        }


        // if (jumpBtn)
        // {
        //   if (jumpBtn.GetComponent<JumpBtnHandler>().bIsInAir)
        //   {
        //     playerAnim.SetFloat("Speed", 1.0f);
        //   }
        //   else{
        //     playerAnim.SetFloat("Speed", 0.0f);
        //   }
        // }
    }

    // void LateUpdate()
    // {
    //   if (!(isCollidingFloor))
    //   {
    //     playerAnim.SetFloat("Speed", 1.0f);
    //   }
    // }

    void OnCollisionEnter(Collision collision)
    {
      if (collision.gameObject.tag == "Goblin")
      {
        // decrease the health as long as the Goblin is in contact with the player
        bIsInContactWithGoblin = true;
      }
      else if (collision.gameObject.tag == "Ground")
      {
        isCollidingFloor = true;
        jumpBtn.GetComponent<JumpBtnHandler>().bIsClicked = false;
      }
      if (collision.gameObject.tag == "ManaStone")
      {
        // Increase mana score and set the text for the UI then destroy the pickup
        if (!(invCtrlObj.CheckInventory()))
        {
          manaScore++;
          gm.GetComponent<GM>().manaTxt.text = manaScore.ToString();
          invCtrlObj.AddToInv(collision.gameObject.GetComponent<ManaStoneController>().manaItem);
          GameObject.Destroy(collision.gameObject);
        }
        else{
          Debug.Log("Inventory is Full !!! ");
        }
      }
      else if (collision.gameObject.tag == "Coin")
      {
        // Increase coin score and set the text for the UI then destroy the pickup
        if (!(invCtrlObj.CheckInventory()))
        {
          coinScore++;
          gm.GetComponent<GM>().scoreTxt.text = coinScore.ToString();
          invCtrlObj.AddToInv(collision.gameObject.GetComponent<CoinController>().coinItem);
          GameObject.Destroy(collision.gameObject);
        }
        else{
          Debug.Log("Inventory is Full !!! ");
        }
      }
      if (collision.gameObject.tag == "BasicSword")
      {
        AddWeapon();
        // Destroy Object
        GameObject.Destroy(collision.gameObject);
      }
    }

    void AddWeapon()
    {
      if (basicSword)
      {
        if (rightHand)
        {
          GameObject weapon = GameObject.Instantiate<GameObject>(basicSword.Model);
          weapon.transform.position = new Vector3(rightHand.transform.position.x, rightHand.transform.position.y, rightHand.transform.position.z);
          weapon.transform.rotation = Quaternion.Euler(rightHand.transform.rotation.x, rightHand.transform.rotation.y, rightHand.transform.rotation.z);
          weapon.transform.SetParent(rightHand.transform);
          weapons.Add(basicSword.name);
        }
      }
    }

    void OnCollisionStay(Collision collision)
    {
      if (collision.gameObject.tag == "Ground")
      {
        isCollidingFloor = true;
      }
    }

    void OnCollisionExit(Collision collision)
    {
      if (collision.gameObject.tag == "Goblin")
      {
        // decrease the health as long as the Goblin is in contact with the player
        bIsInContactWithGoblin = false;
      }
      else if (collision.gameObject.tag == "Ground")
      {
        isCollidingFloor = false;
      }
    }
}
