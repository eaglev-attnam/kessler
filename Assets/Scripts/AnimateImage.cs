using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateImage : MonoBehaviour
{
	public float cycletime = 1;
	public Sprite[] sprites;
	
	Image img;
	int current = 0;
	float lastUpdate = 0;
	
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
		img.sprite = sprites[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(lastUpdate + cycletime < Time.time) {
			current++;
			current %= sprites.Length;
			img.sprite = sprites[current];
			lastUpdate = Time.time;
		}
    }
}
