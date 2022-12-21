using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    private int health = 2;
    private RoomEnemies roomEnemies;
    public GameObject coin;

    private void Start()
    {
        roomEnemies = GameObject.FindGameObjectWithTag("Enemies").GetComponent<RoomEnemies>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hit");
        if (collision.gameObject.tag == "Bullet")
        {
            health -= 1;

            if (health < 1)
            {
                roomEnemies.enemyCount -= 1;
                Instantiate(coin, gameObject.transform.position, gameObject.transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
