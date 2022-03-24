using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
 
public class WebRequest: MonoBehaviour 
{
    void Start() 
    {
        StartCoroutine(Post());
    }
 
    public IEnumerator Post(string url = null, string bodyJsonString = null)
    {
        // Creating a custom class and Json encoding
        MyClass myObject = new MyClass();
        myObject.type = "game_launch";
        myObject.user_id = AnalyticsSessionInfo.userId;
        bodyJsonString = JsonUtility.ToJson(myObject);

        url = "https://3gidp6rat9.execute-api.us-west-2.amazonaws.com/game-e2e-demo/events";
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();

        // Request headers
        request.SetRequestHeader("Authorization", "a99527741ba1faf5ba7818d6de3d53f4");
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Accept","application/json");
        request.SetRequestHeader("X-Api-Version", "1.1.0");
        
        // Send request
        yield return request.SendWebRequest();

        // Error handling
        if(request.result == UnityWebRequest.Result.ConnectionError) 
        {
            Debug.Log(request.error);
        }
        else 
        {
            Debug.Log("POST request complete.");
            StringBuilder sb = new StringBuilder();
            foreach (System.Collections.Generic.KeyValuePair<string, string> dict in request.GetResponseHeaders())
            {
                sb.Append(dict.Key).Append(": \t[").Append(dict.Value).Append("]\n");
            }

            // Print Headers
            Debug.Log(sb.ToString());

            // Print Body
            Debug.Log(request.downloadHandler.text);

            Debug.Log("Status Code: " + request.responseCode);
        }
    }
}

// Using JSON serialization to create a structured JSON class
[Serializable]
public class MyClass
{
    public string type;
    public string user_id;
}