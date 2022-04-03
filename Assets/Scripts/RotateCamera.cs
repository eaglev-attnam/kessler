using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
	public GameObject target;
	float distance;
	
    // Start is called before the first frame update
    void Start()
    {
        distance = transform.position.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 targetPos = target.transform.position.normalized * distance;
		transform.position = targetPos;
		transform.LookAt(target.transform);
    }
}
