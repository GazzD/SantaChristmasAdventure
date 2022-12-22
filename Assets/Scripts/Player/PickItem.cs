using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.SharedInstance.giftCount++;
            HUDManager.SharedInstance.UpdateGiftCount();
            Destroy(gameObject);
        }
    }
}
