using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    EventSystem eventSystem;

    // Start is called before the first frame update
    void OnEnable()
    {
        //Fetch the current EventSystem. Make sure your Scene has one.
        eventSystem = EventSystem.current;
    }

    // Update is called once per frame
    void Update()
    {
        if(eventSystem.lastSelectedGameObject == null)
        {
            GameObject gameObject = GameObject.Find("BackBtn");
        }
        
        eventSystem.SetSelectedGameObject(gameObject);
    }
}
