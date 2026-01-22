using System;
using UnityEngine;
using UnityEngine.InputSystem;


// Extends Singleton - GameManager to manage game-wide states and behaviors.
// Only one singleton instance can exist and the base class "Awake" will make it persist across scenes.

// If you already have a GameManager in your scene then instantiating this will destroy itself.
// If you have a GameManager prefab you can add it to your initial scene and it will persist across scenes.
// But if you want to test a later scene that doesn't have a GameManager you can add the prefab to that scene so it will work as expected.
public class GameManager : Singleton<GameManager>
{
    // **********Events**********
    public event Action OnPauseGame;
    public event Action OnActionMapChange;

    // *********Variables**********

    // The default action map name to switch to on Awake.
    public string defaultActionMapName = "Player";
    // The current active Input Action Map.
    public InputActionMap currentActionMap;


    //*********Unity Methods**********

    public void Start()
    {
        // Switch to the default action map on start.
        SwitchActionMap(defaultActionMapName);
    }
    // Method to pause the game and invoke the pause event.
    public void PauseGame()
    {
        OnPauseGame?.Invoke();     
        Debug.Log("Game Paused");
    }
    // Method to switch the current Input Action Map.  
    public void SwitchActionMap(string actionMapName)
    {
        var inputActions = InputSystem.actions;
        var newActionMap = inputActions.FindActionMap(actionMapName);
        if (newActionMap != null)
        {
            currentActionMap?.Disable();
            newActionMap.Enable();
            currentActionMap = newActionMap;
            OnActionMapChange?.Invoke();
            
            #if UNITY_EDITOR
            Debug.Log($"Switched to action map: {actionMapName}");
            #endif

        }
        else
        {
            #if UNITY_EDITOR
            Debug.LogWarning($"Action map '{actionMapName}' not found.");
            #endif  
        }
    }
}
