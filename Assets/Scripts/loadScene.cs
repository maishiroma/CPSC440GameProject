using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class loadScene : MonoBehaviour {

    public string sceneName;

	// Use this for initialization
	public void LoadScene ()
    {
        SceneManager.LoadScene(sceneName);	
	}

}
