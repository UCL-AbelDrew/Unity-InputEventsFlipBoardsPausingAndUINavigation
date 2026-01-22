using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[Serializable]
public class InputListenerEvent : UnityEvent<GameObject> {}

// Listens for input actions and triggers corresponding events.
public class InputListener : MonoBehaviour
{
    [Tooltip("The name of the action map containing the input action to listen for.")]
    public string actionMapName = "Player";
    [Tooltip("The name of the input action to listen for.")]
    public string actionName = "Jump";
      
    // Enum to choose how the input should be checked each Update.
    public enum ActionCheckType
    {
        PressedThisFrame,
        ReleasedThisFrame,
        IsPressed,
        Triggered
    }

    [Tooltip("How the input action should be evaluated each Update.")]
    public ActionCheckType checkType = ActionCheckType.PressedThisFrame;
    
    [Tooltip("The GameObject to send with the event when the input action is triggered. If null gameObject sends itself")]
    public GameObject objectToSendWithEvent;
    public InputListenerEvent inputListenerEvent;

    // The input action being listened to.
    private InputAction action;
    // The current active action map from the GameManager.
    private InputActionMap currentActionMap;


    // When enabled, subscribe to input action events.
    public void Awake()
    {
        // If no object is assigned to send with the event, default to this gameObject
        if (!objectToSendWithEvent) { 
        objectToSendWithEvent = this.gameObject; 
        }      
    }
    private void Start()
    {
        //Subscribe to action map changes and get the initial action map
        GameManager.Instance.OnActionMapChange += OnActionMapChange;
        OnActionMapChange();
    }
    // Called when the action map changes in the GameManager and from start to set the initial action map
    private void OnActionMapChange()
    {
        currentActionMap = GameManager.Instance.currentActionMap;
        action = currentActionMap.FindAction(actionName);

        // if the action is null, or if the action is found, but the action map name does not match, then set action to null
        // as we may have found an action with the same name using a different map. We log this for debugging purposes in case the user
        // has misconfigured something. (e.g. misspelled actionmap name)
        if (action == null || action.actionMap.name != currentActionMap.name)
        {
            action = null;
#if UNITY_EDITOR
            Debug.Log($"InputListener on {gameObject.name} did not find action '{actionName}' in current action map '{currentActionMap.name}'.");
            #endif
        }
        else
        {
            #if UNITY_EDITOR
            Debug.Log($"InputListener on {gameObject.name} is now listening to action '{actionName}' in action map '{currentActionMap.name}'.");
            #endif
        }

    }
    private void Update()
    {
        if (action == null)
            return;

        // Check the action according to the selected check type.
        switch (checkType)
        {
            case ActionCheckType.PressedThisFrame:
                if (action.WasPressedThisFrame()) ActivateInputEvent();
                break;
            case ActionCheckType.ReleasedThisFrame:
                if (action.WasReleasedThisFrame()) ActivateInputEvent();
                break;
            case ActionCheckType.IsPressed:
                if (action.IsPressed()) ActivateInputEvent();
                break;
            case ActionCheckType.Triggered:
                if (action.triggered) ActivateInputEvent();
                break;
        }

    }

    // Ivokes the input listener event with the specified GameObject, e.g. does something when the input action is detected.
    public void ActivateInputEvent()
    {       
        if (inputListenerEvent != null)
        {
            inputListenerEvent.Invoke(objectToSendWithEvent);
        }
    }
    

}
