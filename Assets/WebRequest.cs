using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequest : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(MakeRequests());
    }

    private IEnumerator MakeRequests() {
        // POST
        var dataToPost = new PostData(){type = "game_launch", user_id = AnalyticsSessionInfo.userId};
        var postRequest = CreateRequest("https://3gidp6rat9.execute-api.us-west-2.amazonaws.com/game-e2e-demo/events", RequestType.POST, dataToPost);
        AttachHeader(postRequest, "Accept","application/json");
        AttachHeader(postRequest, "Authorization", "a99527741ba1faf5ba7818d6de3d53f4");
        AttachHeader(postRequest, "Content-Type", "application/json");
        yield return postRequest.SendWebRequest();
        var deserializedPostData = JsonUtility.FromJson<Todo>(postRequest.downloadHandler.text);
    }

    private UnityWebRequest CreateRequest(string path, RequestType type = RequestType.POST, object data = dataToPost) {
        var request = new UnityWebRequest(path, type.ToString());

        if (data != null) {
            var bodyRaw = Encoding.UTF8.GetBytes(JsonUtility.ToJson(data));
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        }

        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        return request;
    }

    private void AttachHeader(UnityWebRequest request,string key,string value)
    {
        request.SetRequestHeader(key, value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum RequestType {
    GET = 0,
    POST = 1,
    PUT = 2
}

public class Todo {
    // Ensure no getters / setters
    // Typecase has to match exactly
    public int userId;
    public int id;
    public string title;
    public bool completed;
}

[Serializable]
public class PostData {
    public string type;
    public string user_id;
}

public class PostResult
{
    public string success { get; set; }
}