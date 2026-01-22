using UnityEngine;

public class Pause : MonoBehaviour
{
 public void PauseGame()
    {
     GameManager.Instance.PauseGame();
    }
}
