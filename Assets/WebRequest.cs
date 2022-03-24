using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
 
public class WebRequest: MonoBehaviour {
    void Start() {
        StartCoroutine(Upload());
    }
 
    public IEnumerator Upload() {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("type", "game_launch"));

        UnityWebRequest www = UnityWebRequest.Post("https://3gidp6rat9.execute-api.us-west-2.amazonaws.com/game-e2e-demo/events", formData);
        {
        www.SetRequestHeader("Authorization", "a99527741ba1faf5ba7818d6de3d53f4");
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Accept","application/json");
        www.SetRequestHeader("X-Api-Version", "1.1.0");
        yield return www.SendWebRequest();
 
        if(www.result == UnityWebRequest.Result.ConnectionError) {
            Debug.Log(www.error);
        }
        else {
            Debug.Log("Form upload complete.");
        }
        }

    }
}