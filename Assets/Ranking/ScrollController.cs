using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScrollController : MonoBehaviour {

	[SerializeField]
	RectTransform prefab = null;

	private IEnumerator UpdateRanking() {
		List<string> ranking = new List<string>();

		yield return StartCoroutine(WebRequest.PostRequest("http://127.0.0.1:8000/data/get", "{\"type\":\"hard\"}", (result) =>
			ranking = result
		));

		for(int i = 0; i < ranking.Count/2; i+=1)
		{
			var item = GameObject.Instantiate(prefab) as RectTransform;
			item.SetParent(transform, false);

			var text = item.GetComponentsInChildren<Text>();
			text[0].text = (i+1).ToString();
			text[1].text = (ranking[i*2]).ToString();
			text[2].text = (ranking[i*2+1]).ToString();
		}

		var item = GameObject.Find("Score");
		var text = item.GetComponentsInChildren<Text>();
		text[0].text = TitleTranstion.GetName();
		text[1].text = Score.GetScore().ToString();
	}

	void Start() {
		StartCoroutine("UpdateRanking");
	}

	void Update() {
		if(Input.GetKeyDown("return")) {
			// SceneManager.LoadScene("SampleScene");
		}
	}
}
