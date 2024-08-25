using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class PostMethod : MonoBehaviour
{
    private TMP_InputField _outputArea;
    [SerializeField] private Button _postButton;
    
    void Start()
    {
        _outputArea = GameObject.Find("OutputArea").GetComponent<TMP_InputField>();
        
        if (_outputArea == null)
            Debug.LogError("output area is null");
        
        _postButton.onClick.AddListener(PostData);
    }

    void PostData() => StartCoroutine(PostData_Coroutine());
    
    IEnumerator PostData_Coroutine()
    {
        _outputArea.text = "Loading...";
        string uri = "https://jsonplaceholder.typicode.com/posts";
        WWWForm form = new WWWForm();
        form.AddField("title", "test data");
        using (UnityWebRequest request = UnityWebRequest.Post(uri, form))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
                _outputArea.text = request.error;
            else
                _outputArea.text = request.downloadHandler.text;
        }
    }
}
