using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour, IDataPersistence
{
    private Rigidbody2D player;
    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray;
    private Animator anim;
    private float dirX;

    // Start is called before the first frame update
    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        player.velocity = new Vector2(dirX * 7f, player.velocity.y);

        //Jump this is checked for every frame
        if (Input.GetButtonDown("Jump"))
        { //Adjust the strenght of the jump
            player.velocity = new Vector2(player.velocity.x, 14);
        }

        //Up and down movement
        if (player.velocity.y < 0)
        {
            spriteRenderer.sprite = spriteArray[2];
        }
        else
        {  //Left and right movement
            if (player.velocity.x >= 0)
            {
                spriteRenderer.sprite = spriteArray[0];
            }
            if (player.velocity.x < 0)
            {
                spriteRenderer.sprite = spriteArray[1];
            }
        }

        UpdateAnimation();

    }

    private void UpdateAnimation()
    {
        //running and idle animations
        if (dirX > 0f) //right
        {
            anim.SetBool("running", true);
            spriteRenderer.flipX = false;
        }
        else if (dirX < 0f) //left
        {
            anim.SetBool("running", true);
            spriteRenderer.flipX = true;
        }
        else //idle
        {
            anim.SetBool("running", false);

        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "VOID")
        {
            // Starting position of player character
            player.position = new Vector2(-13, -2);
        }

        // Once the friends are free, door will be set with
        // an active tag "Exit" and this will trigger the next scene
        if (other.tag == "Exit")
        {
            SceneManager.LoadScene(2);
        }
    }

    // Load player position from saved game data
    public void LoadData(GameData data)
    {
        // Move the player to the last saved location
        this.transform.position = data.position;
    }

    // Save player position to game data
    public void SaveData(ref GameData data)
    {
        // save the player's current location
        data.position = this.transform.position;
    }
}

