using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {

	public float standingTreshold = 20f;
	public float distanceToRaise = 100f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//print (name + IsStanding ());
	}

	public bool IsStanding()
	{
		Vector3 rotationInEuler = (transform.rotation.eulerAngles);

		float tiltInX = Mathf.Abs(270 - rotationInEuler.x);
		float tiltInZ = Mathf.Abs(rotationInEuler.z);

		if (tiltInX < standingTreshold && tiltInZ < standingTreshold) {
			return true;
		} 
		else {
			return false;
		}
	}

	public void RaiseIfStanding()
	{
		if (IsStanding ()) {
			GetComponent<Rigidbody> ().useGravity = false;
			transform.Translate( new Vector3 (0, distanceToRaise, 0), Space.World);
			transform.rotation = Quaternion.Euler (270f,0,0);
		}
	}

	public void Lower()
	{
			transform.Translate( new Vector3 (0, -distanceToRaise, 0), Space.World);
			GetComponent<Rigidbody> ().useGravity = true;
	}

	public void DestroyPin()
	{
		Destroy (gameObject);
	}

}
