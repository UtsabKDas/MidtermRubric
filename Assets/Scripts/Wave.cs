using UnityEngine;
using UnityEngine.Events;



public class Wave : MonoBehaviour
{
    public UnityEvent onWaveCleared;

    void Start()
    {
        
    }

    void WaveComplete()
    {
        onWaveCleared.Invoke(); // calls both subscribers
    }
}
