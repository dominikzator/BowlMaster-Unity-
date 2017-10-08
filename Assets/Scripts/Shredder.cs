using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour {

	void OnTriggerExit(Collider col)
	{
		GameObject thingLeft = col.gameObject;
		if (thingLeft.GetComponent<Pin>()) {
			//Destroy (thingLeft);
			thingLeft.GetComponent<Rigidbody>().useGravity = false;
			//thingLeft.GetComponent<Rigidbody> ().isKinematic = true;
			thingLeft.GetComponent<Animator>().SetTrigger("pinDisappearTrigger");
			//Destroy (thingLeft);
		}
	}
}
