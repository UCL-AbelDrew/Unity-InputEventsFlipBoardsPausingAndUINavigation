using UnityEngine;

public class ChangeActionMap : MonoBehaviour
{
    public string actionMapName;
    // Call the gameManager to change the action map
    public void ChangeMap(string map)
    {
        //if map is empty, use the one from the inspector
        if (string.IsNullOrEmpty(map))
        {
            map = actionMapName;
        }
        GameManager.Instance.SwitchActionMap(map);
    }
}
