using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A simple class with an overloaded method to receive an event and write to the debug.log
/// Use to test or demonstrate events being received by an object.
/// </summary>
public class EventReceiver : MonoBehaviour
{  
    public void Receive()
    {
        Debug.Log("Received Event");
    }
    public void Receive(GameObject receivedObject)
    {
        Debug.Log("Received " + receivedObject.name);
    }
}
