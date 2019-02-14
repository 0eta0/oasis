using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleTransition : MonoBehaviour {
	public InputField inputField;
  public Text text;
	// Use this for initialization
	void Start () {
		inputField = GameObject.Find ("InputField").GetComponent<InputField> ();

	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("return")) {
		string inputValue = inputField.text;
		Debug.Log(inputValue);
		SceneManager.LoadScene ("SampleScene");
	}



	}
}
