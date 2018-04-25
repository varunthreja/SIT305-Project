using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Splash : MonoBehaviour
{
	void Start ()
    {
        Time.timeScale = 1f;
        Invoke("Next", 4f);
	}
	
    void Next()
    {
        Application.LoadLevel(Application.loadedLevel + 1);
    }
}
