using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public TMP_Text giftCountLabel;
    public TMP_Text blinkStatusLabel;
    public List<GameObject> lifes;
    public static HUDManager SharedInstance;

    private void Awake()
    {
        if (SharedInstance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            SharedInstance = this;
            // Keep this object even when we switch scenes
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        if(giftCountLabel == null && GameObject.Find("Score") != null)
        {
            giftCountLabel = GameObject.Find("Score").GetComponent<TMP_Text>();
            UpdateGiftCount();
        }
        if (blinkStatusLabel == null && GameObject.Find("BlinkStatus"))
        {
            blinkStatusLabel = GameObject.Find("BlinkStatus").GetComponent<TMP_Text>();
            UpdateBlinkStatus();
        }

        if (lifes[0] == null && GameObject.Find("Life1") && GameObject.Find("Life2") && GameObject.Find("Life3"))
        {
            lifes = new List<GameObject>();
            lifes.Add(GameObject.Find("Life1"));
            lifes.Add(GameObject.Find("Life2"));
            lifes.Add(GameObject.Find("Life3"));
        }
    }

    public void DeactivateLife(int index)
    {
        lifes[index].gameObject.SetActive(false);
    }

    public void ActivateLife(int index)
    {
        lifes[index].gameObject.SetActive(true);
    }

    public void UpdateGiftCount()
    {
        giftCountLabel.SetText(GameManager.SharedInstance.giftCount.ToString());
    }

    public void UpdateBlinkStatus()
    {
        if(GameManager.SharedInstance.blinkStatus)
        {
            blinkStatusLabel.SetText("<ON>");
        } else
        {
            blinkStatusLabel.SetText("<OFF>");
        }
    }

}
