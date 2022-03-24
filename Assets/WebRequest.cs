using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Text;
 
public class WebRequest: MonoBehaviour 
{
    void Start() 
    {
        StartCoroutine(Upload());
    }
 
    public IEnumerator Upload() 
    {
        // Request Body
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("type", "game_launch"));

        // Setting web request endpoint and body data
        UnityWebRequest www = UnityWebRequest.Post("https://3gidp6rat9.execute-api.us-west-2.amazonaws.com/game-e2e-demo/events", formData);
        
        // Setting request headers and sending request
        
        www.SetRequestHeader("Authorization", "a99527741ba1faf5ba7818d6de3d53f4");
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Accept","application/json");
        www.SetRequestHeader("X-Api-Version", "1.1.0");
        yield return www.SendWebRequest();
 
        // Error handling
        if(www.result == UnityWebRequest.Result.ConnectionError) 
        {
            Debug.Log(www.error);
        }
        else 
        {
            Debug.Log("POST request complete.");
            StringBuilder sb = new StringBuilder();
            foreach (System.Collections.Generic.KeyValuePair<string, string> dict in www.GetResponseHeaders())
            {
                sb.Append(dict.Key).Append(": \t[").Append(dict.Value).Append("]\n");
            }

            // Print Headers
            Debug.Log(sb.ToString());

            // Print Body
            Debug.Log(www.downloadHandler.text);
        }
        
    }
}