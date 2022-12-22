using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    DontDestroy SharedInstance;
    private void Awake()
    {
        if (SharedInstance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            SharedInstance = this;
        }
        // Keep this object even when we switch scenes
        DontDestroyOnLoad(gameObject);
    }
}
