using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public GameObject[] canvasses;
	public Text highScore;
	
	int currentCanvas = 0;
	
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 1; i < canvasses.Length; i++) {
			canvasses[i].SetActive(false);
		}
		GameObject keepOnScene = GameObject.FindWithTag("KeepOnScene");
		keepOnScene.SendMessage("RequestTopScore", gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void SetTopScore(int score) {
		highScore.text = "Your best run lasted " + score + " seconds.";
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
