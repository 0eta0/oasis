using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ScrollController : MonoBehaviour {

	[SerializeField]
	RectTransform prefab = null;

	private IEnumerator UpdateRanking() {
		List<string> ranking = new List<string>();

		yield return StartCoroutine(WebRequest.PostRequest("http://127.0.0.1:8000/data/get", "{\"type\":\"standard\"}", (result) =>
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

		var item2 = GameObject.Find("Score");
		var text2 = item2.GetComponentsInChildren<Text>();
		text2[1].text = TitleTransition.GetName();
		text2[2].text = Score.GetScore().ToString();
	}

	void Start() {
		StartCoroutine("UpdateRanking");
	}

	void Update() {
		if(Input.GetKeyDown("return")) {
			SceneManager.LoadScene("title");
		}
	}
}
