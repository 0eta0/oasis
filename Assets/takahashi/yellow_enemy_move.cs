using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yellow_enemy_move : MonoBehaviour {

	private Rigidbody2D rb2d;
	private float x = 1f;

	// Use this for initialization
	void Start () {
		Debug.Log(this.transform.position);
		Debug.Log(this.transform.right);
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		// 入力後の移動量を求める
		// Vector3 pos = this.transform.position + this.transform.right * x;

		// プレイヤーの位置が画面内に収まるように制限をかける
		//pos.x = Mathf.Clamp(pos.x, min.x, max.x);
		//pos.y = Mathf.Clamp(pos.y, min.y, max.y);

		// 速度に反映
		// rb2d.velocity = pos - this.transform.position;
		rb2d.velocity = this.transform.right * x;
	}
}