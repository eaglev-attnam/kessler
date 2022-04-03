using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sattelite : MonoBehaviour
{
	public Mesh satMesh;
	public Material[] satMats;
	public Mesh broken;
	public Mesh[] chunk;
	
	static readonly float launchSpeed = 45; // Degrees per second
	static readonly float orbitSpeed = 9; // Degrees per second
	static readonly float collisionDelay = 2;
	static readonly float maxCollisions = 3;	
	
	float launchRotationTotal = 0;
	float lastCollision = 0;
	int collisions = 0;
	
	BoxCollider satCol;
	Renderer satRen;
	MeshFilter satFil;
	
	Transform launcher;
	
	bool playing = true;
	
    // Start is called before the first frame update
    void Start()
    {		
		satCol = GetComponent<BoxCollider>();
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
					satCol.enabled = true;
					satFil.mesh = satMesh;
					satRen.materials = satMats;
				}
			}
			launchRotationTotal += diff;
			transform.RotateAround(launcher.position, launcher.up, diff);
			transform.RotateAround(Vector3.zero, launcher.up, orbitalDiffLeft);
		}
	}
	
	void setCloneCollisionTime(float time)
	{
		lastCollision = time;
		launchRotationTotal = 90; // No need to launch anymore
	}
	
	void setCloneCollisions(int collisions)
	{
		this.collisions = collisions;
	}
	
	void OnTriggerEnter(Collider c)
	{
		if(playing) {
			if(collisions == 0) {
				// First collision
				resolveCollision(broken, broken, new Vector3(0, -1.2f, 0),  new Vector3(2, 5, 2));
				satCol.center = new Vector3(0, -1.2f, 0);
				satCol.size = new Vector3(2, 5, 2);
			} else if(lastCollision + collisionDelay < Time.time) {
				if(collisions == maxCollisions) {
					Destroy(gameObject);
				} else {
					resolveCollision(chunk[0], chunk[1],  new Vector3(0, 0, 0), new Vector3(2, 2, 2));
					satCol.center = new Vector3(0, 0, 0);
					satCol.size = new Vector3(2, 2, 2);
				}
			}			
		}
	}
	
	void resolveCollision(Mesh selfMesh, Mesh otherMesh, Vector3 colliderCenter, Vector3 colliderSize) {
			GameObject other = Instantiate(gameObject);
			satCol.center = colliderCenter;
			satCol.size = colliderSize;
			other.GetComponent<BoxCollider>().center = colliderCenter;
			other.GetComponent<BoxCollider>().size = colliderSize;
			satFil.mesh = selfMesh;
			other.GetComponent<MeshFilter>().mesh = otherMesh;
			float rotation = Random.Range(-60,60);
			other.transform.RotateAround(transform.position, transform.right, rotation);
			transform.RotateAround(transform.position, transform.right, -rotation);
			lastCollision = Time.time;
			other.SendMessage("setCloneCollisionTime", lastCollision);
			collisions++;
			other.SendMessage("setCloneCollisions", collisions);
	}
	
	public void Move(bool move) {
		playing = move;
	}
}
