using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class GetMethod : MonoBehaviour
{
    private TMP_InputField _outputArea;
    [SerializeField] private Button _getButton;

    private void Start()
    {
        _outputArea = GameObject.Find("OutputArea").GetComponent<TMP_InputField>();
        
        if (_outputArea == null)
            Debug.LogError("output area is null");
        
        _getButton.onClick.AddListener(GetData);
    }

    void GetData() => StartCoroutine(GetData_Coroutine());

    IEnumerator GetData_Coroutine()
    {
        _outputArea.text = "Loading...";
        string uri = "https://jsonplaceholder.typicode.com/todos/1";
        using (UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
                _outputArea.text = request.error;
            else
                _outputArea.text = request.downloadHandler.text;
        }
    }
}
