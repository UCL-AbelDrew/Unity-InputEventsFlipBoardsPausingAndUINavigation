using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class MaterialSwap : MonoBehaviour
{
    public Material materialA;
    public Material materialB;

    private Renderer rend;
    private bool usingA = true;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
      
    }
    public void SwapMaterial()
    {   
        usingA = !usingA;
        rend.material = usingA ? materialA : materialB;
    }
}
