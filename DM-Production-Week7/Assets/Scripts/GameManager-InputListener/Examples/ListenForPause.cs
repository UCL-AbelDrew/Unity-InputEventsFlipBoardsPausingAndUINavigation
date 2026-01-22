using UnityEngine;

public class ListenForPause : MonoBehaviour
{
    // Subscribe to the GameManager's OnPauseGame event
    // This is done in start so that it allows the awake function of the GameManager to complete first.
    private void Start()
    {
        GameManager.Instance.OnPauseGame += OnPauseGame;
    }
   
    void OnPauseGame()
    {
        Debug.Log("Pause event received in ListenForPause.");
        // Add additional logic to handle the pause event here.
    }
}
