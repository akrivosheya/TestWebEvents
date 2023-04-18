using UnityEngine;

public class EventsTracker : MonoBehaviour
{
    [SerializeField] private EventService _service;
    
    public void StartLevel()
    {
        _service.TrackEvent("start_level", System.DateTime.UtcNow.ToString());
    } 
    
    public void GetReward()
    {
        _service.TrackEvent("reward", "10 coins");
    } 
    
    public void SpendCoins()
    {
        _service.TrackEvent("spend", "5 coins");
    } 
}
