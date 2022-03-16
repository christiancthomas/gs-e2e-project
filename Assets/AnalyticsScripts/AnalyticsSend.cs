using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
import System

public class MyBehavior : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Upload());
    }

    IEnumerator Upload()
    {
        WWWForm form = new WWWForm();
        form.AddField("type", "game_launch");
        form.AddField("user_id", "AnalyticsSessionInfo.userId");
        form.AddField("os", "Debug.Log(SystemInfor.operatingSystem");
        form.AddField("timestamp", "System.DateTime.UTCNow");
        form.AddField("version", "1.00");

        using (UnityWebRequest www = UnityWebRequest.Post("https://3gidp6rat9.execute-api.us-west-2.amazonaws.com/game-e2e-demo/events", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }
}