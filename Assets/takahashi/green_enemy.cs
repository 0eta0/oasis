using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class green_enemy : MonoBehaviour {

	// 敵オブジェクトのRigidbody2Dを入れる変数
	private Rigidbody2D rb2d;

	// ENEMYのx座標の変更値
	private float x = 0;
	// ENEMYのy座標の変更値
	private float y = 0.1f;
	// +か-か
	private float Sign = 1;

	// Use this for initialization
	void Start () {
		// 敵オブジェクト自身のRigidbody2Dを取得
		rb2d = GetComponent<Rigidbody2D> ();

	}
	
	// Update is called once per frame
	void Update () {
		// 移動関数の呼び出し
		EnemyMove ();
	}

	// ENEMYの移動関数1フレーム毎にUpdate関数から呼び出される
	void EnemyMove () {
		// ENEMYのx座標
		float rb2d_velocity_y = rb2d.velocity.y;
		if (System.Math.Abs(rb2d_velocity_y) > 4) {
			Sign = Sign * -1;
		}
		// 移動を計算させるための２次元のベクトルを作る
		Vector2 direction = new Vector2 (x, rb2d_velocity_y - Sign*y);
			// ENEMYのRigidbody2Dに移動速度を指定する
		rb2d.velocity = direction;
	}
}
