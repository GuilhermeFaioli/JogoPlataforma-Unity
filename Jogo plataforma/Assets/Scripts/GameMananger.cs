using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMananger : MonoBehaviour {

    public GameObject PainelCompleto;
    public bool isPaused = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void pause()
    {
        if (isPaused)
        {
            PainelCompleto.SetActive(false);
            isPaused = false;
            Time.timeScale = 1;
        }
        else
        {
            PainelCompleto.SetActive(true);
            isPaused = true;
            Time.timeScale = 0;
        }
        
    }

}
