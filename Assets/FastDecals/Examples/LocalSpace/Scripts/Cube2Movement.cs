using UnityEngine;
using System.Collections;

public class Cube2Movement : MonoBehaviour {
	void FixedUpdate () {
		transform.position = new Vector3(transform.position.x, Mathf.Sin (Time.time) * 5.0f, transform.position.z);
		transform.Rotate(new Vector3(0, 0.5f, 0.2f));
	}
}
