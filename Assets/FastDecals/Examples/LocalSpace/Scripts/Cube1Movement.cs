using UnityEngine;
using System.Collections;

public class Cube1Movement : MonoBehaviour {
	void FixedUpdate () {
		transform.position = new Vector3(Mathf.Sin (Time.time) * 5.0f, transform.position.y, transform.position.z);
		transform.Rotate(new Vector3(0.5f, 0.2f, 0.0f));
	}
}
