using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class black_enemy_move : MonoBehaviour {

	public float appear_interval = 0.9f;
	public float hide_interval = 0.1f;
	public float time;

	// Use this for initialization
	void Start () {
		time = 0;
		StartCoroutine ("Flash");
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0, 0, 5));
	}

	private IEnumerator Flash() {
		while ( true ) {
			time += Time.deltaTime;
			Debug.Log(time);
			if(time < 0.2){
				yield return new WaitForSeconds(hide_interval);
				yield return new WaitForSeconds(appear_interval);
			}
			else if(time < 0.3){
				var renderComponent = GetComponent<Renderer>();
				renderComponent.enabled = !renderComponent.enabled;
				yield return new WaitForSeconds(hide_interval);
				renderComponent.enabled = !renderComponent.enabled;
				yield return new WaitForSeconds(appear_interval);
			}
			else if(time < 0.4){
				var renderComponent = GetComponent<Renderer>();
				renderComponent.enabled = !renderComponent.enabled;
				yield return new WaitForSeconds(hide_interval/2);
				renderComponent.enabled = !renderComponent.enabled;
				yield return new WaitForSeconds(appear_interval/2);
			}
			else{
				var renderComponent = GetComponent<Renderer>();
				renderComponent.enabled = !renderComponent.enabled;
				yield break;
			}
		} 
	}
}
