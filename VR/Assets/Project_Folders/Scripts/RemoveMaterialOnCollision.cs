using UnityEngine;

public class RemoveMaterialOnCollision : MonoBehaviour
{
    private Renderer objRenderer;

    void Start()
    {
        objRenderer = GetComponent<Renderer>();

        if (objRenderer == null)
        {
            Debug.LogError("Bu objede Renderer bileþeni yok!");
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (objRenderer != null)
        {
            // Örnek: Triggere girince siyah yapabilirsiniz
            objRenderer.material.SetColor("_BaseColor", Color.black);
        }
    }

}
