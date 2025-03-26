using UnityEngine;

public class RemoveMaterialOnCollision : MonoBehaviour
{
    private Renderer objRenderer;

    void Start()
    {
        objRenderer = GetComponent<Renderer>();

        if (objRenderer == null)
        {
            Debug.LogError("Bu objede Renderer bile�eni yok!");
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (objRenderer != null)
        {
            // �rnek: Triggere girince siyah yapabilirsiniz
            objRenderer.material.SetColor("_BaseColor", Color.black);
        }
    }

}
