using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequest : MonoBehaviour
{
	public static IEnumerator PostRequest(string url, string content, Action<List<string>> callback)
	{
		Debug.Log(url);
		UnityWebRequest www = new UnityWebRequest(url, "POST");

		//--- for upload handler
		UploadHandlerRaw uH = new UploadHandlerRaw(new System.Text.UTF8Encoding().GetBytes(content));
		uH.contentType = "application/json";
		www.uploadHandler = uH;

		//--- for downloadhandler
		DownloadHandlerBuffer dH = new DownloadHandlerBuffer();
		www.downloadHandler = dH;

		yield return www.SendWebRequest();

		if (!www.isNetworkError)
		{
				string resultContent = www.downloadHandler.text;
				// Debug.Log("Succesful posting " + resultContent);
				resultContent = "{\"items\":" + resultContent + "}";
				ListData rankingData = JsonUtility.FromJson<ListData>(resultContent);
				List<string> ranking = new List<string>();
				foreach (Data element in rankingData.items)
				{
					ranking.Add(element.name);
					ranking.Add(element.rate.ToString());
				}
				callback(ranking);
		}
		else
		{
				Debug.Log("POST request unsuccesful");
		}
	}
}
