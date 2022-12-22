using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyLife : MonoBehaviour
{
    
    private void OnCollisionEnter2D(Collision2D collision)
    {

        bool enemyIsDown = collision.gameObject.transform.position.y - transform.position.y > transform.lossyScale.y;

        if (collision.gameObject.tag == "Player" && enemyIsDown)
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Player" && !enemyIsDown && !GameManager.SharedInstance.isInvulnerable)
        {
            Vector2 direction = (collision.gameObject.transform.position - transform.position).normalized;

            GameManager.SharedInstance.TakeDamage();
            if(GameManager.SharedInstance.playerLifes <= 0)
            {
                GameManager.SharedInstance.GameOver();
            }

            collision.gameObject.GetComponent<PlayerController>().MakeInvulnerable();
        }

    }

}
