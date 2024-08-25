using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class API_Call : MonoBehaviour
{
    public class Fact
    {
        public string fact { get; set; }
        public int length { get; set; }
    }

    [SerializeField] private TextMeshProUGUI _text;
    
    void Start()
    {
        RefreshRequest();
    }

    public void RefreshRequest()
    {
        StartCoroutine(GetRequest("https://catfact.ninja/fact"));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(string.Format("Something went wrong: {0}", webRequest.error)); 
                    break;
                case UnityWebRequest.Result.Success:
                    Fact fact = JsonConvert.DeserializeObject<Fact>(webRequest.downloadHandler.text);
                    _text.text = fact.fact;
                    break;
            }
        }
    }
}
