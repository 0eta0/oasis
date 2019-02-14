using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class purple_enemy_move : MonoBehaviour {

	public float interval = 0.5f;
	public int x = 2;
	public int y = 2;
	public int Flag = 0;

	void Start () {
		StartCoroutine ("Move");
	}

	IEnumerator Move() {
		while (true){
			Vector2 pos = this.gameObject.transform.position;
			if(Flag==0){
				this.gameObject.transform.position = new Vector2(pos.x-x, pos.y-y);
				Flag = 1;
			}else if(Flag==1){
				this.gameObject.transform.position = new Vector2(pos.x-x, pos.y+y);
				Flag = 2;
			}else if(Flag==2){
				this.gameObject.transform.position = new Vector2(pos.x+x, pos.y+y);
				Flag = 3;
			}else if(Flag==3){
				this.gameObject.transform.position = new Vector2(pos.x+x, pos.y-y);
				Flag = 0;
			}else{
				Debug.Log("Error");
			}

			// var renderComponent = GetComponent<Renderer>();
			// renderComponent.enabled = !renderComponent.enabled;
			yield return new WaitForSeconds(interval);
		}
	}
}