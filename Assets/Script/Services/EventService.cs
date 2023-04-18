using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class EventService : MonoBehaviour
{
    [SerializeField] private string serverUrl;
    [SerializeField] private float cooldownBeforeSend;
    private EventsDataLoader _loader;
    private RequestDTO _requestToSend;
    private RequestDTO _requestToAccumulate;

    void Start()
    {
        _loader = new EventsDataLoader();
        _requestToSend = _loader.GetSavedRequest();
        _requestToAccumulate = new RequestDTO();
        StartCoroutine(SendRequest());
    }

    void OnDestroy()
    {
        RequestDTO requestToSave = new RequestDTO();
        requestToSave.events.AddRange(_requestToSend.events);
        requestToSave.events.AddRange(_requestToAccumulate.events);
        _loader.SaveRequest(requestToSave);
    }

    public void TrackEvent(string type, string data)
    {
        _requestToAccumulate.events.Add(new EventDTO(){ type=type, data=data});
    }

    private IEnumerator SendRequest()
    {
        yield return new WaitForSeconds(cooldownBeforeSend);
        _requestToSend.events.AddRange(_requestToAccumulate.events);
        _requestToAccumulate.events.Clear();
        if(_requestToSend.events.Count > 0)
        {
            using(UnityWebRequest request = UnityWebRequest.Post(serverUrl, JsonUtility.ToJson(_requestToSend)))
            {
                yield return request.SendWebRequest();

                if(request.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.LogError("network problem: " + request.error);
                }
                else if(request.responseCode != (long)System.Net.HttpStatusCode.OK)
                {
                    Debug.LogError("response error: " + request.responseCode);
                }
                else
                {
                    _requestToSend.events.Clear();
                }
            }
        }
        StartCoroutine(SendRequest());
    }
}
