using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision_enemys : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter2D(Collision2D other) {
		// GameObject other_rb2d;
		// other_rb2d = GameObject.Find(other.gameObject.tag);
		// Vector2 targetPos = other_rb2d.transform.position;
		Vector2 targetPos = other.transform.position;
		// Debug.Log(targetPos.x);
		Debug.Log(other.gameObject.GetComponent<Rigidbody2D>().velocity);
		// Debug.Log(GetComponent<Rigidbody2D> ());
		if (other.gameObject.tag == "Enemy") {
			Destroy(other.gameObject);
		}
	}
}


