using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{
	static readonly float launchSpacing = 0.5f;
	
	public GameObject gameOverCanvas;
	public GameObject pauseCanvas;
	public GameObject baseSat;
	public Text score;
	
	float lastLaunch = -launchSpacing;
	float radius;
	float startTime;
	
	float pauseStart;
	
	bool paused = false;
	bool gameOver = false;
	
    // Start is called before the first frame update
    void Start()
    {
		radius = baseSat.transform.position.magnitude;
		startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
		if(!gameOver) {
			if(Input.GetKeyDown("escape")) {
				paused = !paused;
				pauseCanvas.SetActive(paused);
				if(paused) {
					pauseStart = Time.time;
				} else {
					startTime += Time.time - pauseStart;
				}
				foreach(GameObject sattelite in GameObject.FindGameObjectsWithTag("Sattelite")) {
					sattelite.SendMessage("Move", !paused);
				}
			}
			if(!paused) {
				if (Time.time > lastLaunch + launchSpacing)
				{
					GameObject newSat = Object.Instantiate(baseSat);
					Vector3 rotation = Random.onUnitSphere;
					newSat.transform.position = rotation * radius;
					newSat.transform.up = -rotation;
					newSat.transform.Rotate(Vector3.up, Random.Range(0,360));
					newSat.SetActive(true);
					// newSat.transform.Rotate(rotation);
					lastLaunch = Time.time;
				}
				score.text = "T+" + Mathf.Round(Time.time - startTime);
			}
		}
    }
	
	public void GameOver() {
		gameOver = true;
		paused = false;
		pauseCanvas.SetActive(paused);
		gameOverCanvas.SetActive(gameOver);
		foreach(GameObject sattelite in GameObject.FindGameObjectsWithTag("Sattelite")) {
			sattelite.SendMessage("Move", false);
		}
	}
}