using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSat : MonoBehaviour
{
	public Mesh satMesh;
	public Material[] satMats;
	
	public Material collided;
	
	public GameObject earth;
	
	static readonly float launchSpeed = 45; // Degrees per second
	static readonly float orbitSpeed = 9; // Degrees per second
	static readonly float turnSpeed = 90; // Degrees per second
	
	float launchRotationTotal = 0;
	
	Collider satCol;
	Renderer satRen;
	MeshFilter satFil;
	
	Transform launcher;
	
	bool orbit = false;
	bool playing = true;
	
    // Start is called before the first frame update
    void Start()
    {		
		satCol = GetComponent<Collider>();
		satRen = GetComponent<Renderer>();
		satFil = GetComponent<MeshFilter>();
		
		launcher = transform.Find("LaunchRotationPoint");
    }

    // Update is called once per frame
    void Update()
    {
		if(playing) {
			float diff = Time.deltaTime * launchSpeed;
			float orbitalDiffLeft = 0;
			if(launchRotationTotal + diff >= 90) {
				orbitalDiffLeft = launchRotationTotal + diff - 90;
				orbitalDiffLeft *= (orbitSpeed / launchSpeed);
				diff = 90 - launchRotationTotal;
				if(diff > 0) {
					// Reached orbit
					orbit = true;
					satCol.enabled = true;
					satRen.materials = satMats;
					satFil.mesh = satMesh;
				}
			}
			launchRotationTotal += diff;
			transform.RotateAround(launcher.position, launcher.up, diff);
			transform.RotateAround(Vector3.zero, launcher.up, orbitalDiffLeft);
			
			if(orbit) {
				float rotate = 0;
				if(Input.GetKey("left") || Input.GetKey("a")) {
					rotate = -turnSpeed;
				} else if (Input.GetKey("right") || Input.GetKey("d")) {
					rotate = turnSpeed;
				}
				transform.RotateAround(transform.position, transform.right, rotate * Time.deltaTime);
			}
		}
	}
	
	void OnTriggerEnter(Collider c)
	{
		satRen.material = collided;
		earth.SendMessage("GameOver");
	}
	
	public void Move(bool move) {
		playing = move;
	}
}
