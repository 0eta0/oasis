using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class sendRate : MonoBehaviour
{
  public void sendScore(string name, int rate, string type) {
    //var dt = DateTime.Now;
    //var unixTime = ToUnixTime(dt);
    var baseDt = new DateTimeOffset(1970, 1, 1, 0, 0,0, TimeSpan.Zero);
    var unixtime = (DateTimeOffset.Now - baseDt).Ticks / 10000000;
    Debug.Log("{\"rate\":" + rate.ToString() + ", \"name\":" + name + ", \"type\":\"" + type + "\", \"date\":" + unixtime.ToString() + "}");
    StartCoroutine(PostRequest("http://127.0.0.1:8000/data/insert", "{\"rate\":" + rate.ToString() + ", \"name\":\"" + name + "\", \"type\":\"" + type + "\", \"date\":" + unixtime.ToString() + "}"));
  }
  //private static long getNowUnixTime()
  //{
  //  DateTime dt = DateTime.Now;
  //  dt = dt.ToUniversalTime();
  //  return (long)dt.Subtract(dtUnixEpoch).TotalSeconds;
  //}
    public IEnumerator PostRequest(string url, string content)
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
          Debug.Log("Succesful posting " + resultContent);
        }
        else
        {
          Debug.Log("POST request unsuccesful");
        }
    }
}