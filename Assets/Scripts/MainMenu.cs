using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public GameObject[] canvasses;
	int currentCanvas = 0;
	
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 1; i < canvasses.Length; i++) {
			canvasses[i].SetActive(false);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void switchCanvas(int i)
	{
		canvasses[currentCanvas].SetActive(false);
		currentCanvas = i;
		currentCanvas %= canvasses.Length;
		canvasses[currentCanvas].SetActive(true);
	}
	
	public void Play()
	{
		SceneManager.LoadScene("Game");
	}
}
