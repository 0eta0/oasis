using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleTransition : MonoBehaviour {
	public InputField inputField;
  public Text text;
	// Use this for initialization

	private static string name;
	   public static string GetName()
	   {
	       return name;
	   }
	   public static void SetName(string InName)
	   {
	       name = InName;
	   }

	void Start () {
		inputField = GameObject.Find ("InputField").GetComponent<InputField> ();

	}

	// Update is called once per frame
	void Update () {
		string inputValue = inputField.text;
		if(Input.GetKeyDown("return")) {
			if(inputValue != ""){
				SetName(inputValue);
				Debug.Log(GetName());
				SceneManager.LoadScene ("MainGame");
			}
	}



	}
}
