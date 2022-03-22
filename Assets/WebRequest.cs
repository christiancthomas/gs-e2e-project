using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
 
public class WebRequest: MonoBehaviour {
    void Start() {
        StartCoroutine(Upload());
    }
 
    IEnumerator Upload() {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add( new MultipartFormDataSection("type" = "game_launch"));

        UnityWebRequest www = UnityWebRequest.Post("https://3gidp6rat9.execute-api.us-west-2.amazonaws.com/game-e2e-demo/events", formData);
        UnityWebRequest header = UnityWebRequest.SetRequestHeader("Accept","application/json");
        UnityWebRequest header = UnityWebRequest.SetRequestHeader("X-Api-Version", "1.1.0");
        UnityWebRequest header = UnityWebRequest.SetRequestHeader("Authorization", "a99527741ba1faf5ba7818d6de3d53f4");
        UnityWebRequest header = UnityWebRequest.SetRequestHeader("Content-Type", "application/json");
        yield return www.Send();
 
        if(www.isError) {
            Debug.Log(www.error);
        }
        else {
            Debug.Log("Form upload complete.");
        }
    }
}