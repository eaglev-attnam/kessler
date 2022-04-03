using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepOnSceneSwitch : MonoBehaviour
{
	int topScore = 0;
	
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("KeepOnScene");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
	
	void RequestTopScore(GameObject contact) {
		contact.SendMessage("SetTopScore", topScore);
	}
	
	void MaybeSetTopScore(int score) {
		if(score > topScore) {
			topScore = score;
		}
	}
}
