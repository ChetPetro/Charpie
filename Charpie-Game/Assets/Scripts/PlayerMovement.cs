using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
 
    public Rigidbody2D Rb;
    private Vector2 moveDirection;
    private Vector2 mousePosition;
    private float shotDelay;
    private PlayerStats playerStats;

    public Weapon weapon;
    private EnemyHit enemyHit;
    private GameObject[] enemyList;

    public Animator animator;

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        shotDelay = playerStats.shotDelayReset;
    }

    // Update is called once per frame
    void Update()
    {
        // Porcess Inputs of player every frame
        ProcessInputs();

        // Update shotDelay with time sinse last frame
        if(shotDelay > 0) { 
            shotDelay -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        // Get horizontal and vertical inputs from player
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Send imputs to the animator for animation of player
        animator.SetFloat("Horizontal", moveX);
        animator.SetFloat("Vertical", moveY);
        animator.SetFloat("Speed", moveDirection.sqrMagnitude);

        // Create vector for movement 
        moveDirection = new Vector2(moveX, moveY);

        // Get the position of the mouse
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && shotDelay <= 0)
        {
            // If the player clicks left click, fire a bullet and reset the shot delay
            weapon.Fire();
            shotDelay = playerStats.shotDelayReset;
        }
    }

    void Move()
    {
        // Change the direct of movement based on movement vector
        Rb.velocity = new Vector2(moveDirection.x * playerStats.playerMovementSpeed, moveDirection.y * playerStats.playerMovementSpeed);

        // Correcly angle the gun depending on the mouse position
        Vector2 aimDirection = mousePosition - Rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        Rb.rotation = aimAngle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Continously damage the player if they are colliding with an enemy
        if (collision.gameObject.tag == "Enemy"){
            enemyList = GameObject.FindGameObjectsWithTag("Enemy");

            for (int i = 0; i < enemyList.Length; i++){
                enemyHit = enemyList[i].GetComponent<EnemyHit>();
              
            }

            InvokeRepeating("DamagePlayer", 0f, enemyHit.hitSpeed);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Stop damaging the player if they are no longer colliding with an enemy
        if (collision.gameObject.tag == "Enemy")
        {
            CancelInvoke("DamagePlayer");
        }
    }

    public void DamagePlayer()
    {
        // Damage the player
        playerStats.playerHeath -= 1;
        FindObjectOfType<AudioManager>().Play("PlayerHurt");
    }

}
