using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public string objectID; // Örneðin: "Bardak_Masa1"
    public GameObject brokenPrefab; // Kýrýk versiyonu

    public void Break()
    {
        if (brokenPrefab != null)
        {
            Instantiate(brokenPrefab, transform.position, transform.rotation);
            Debug.Log(objectID + " kýrýldý!");
            Destroy(gameObject); // Eski nesneyi yok et
        }
        else
        {
            Debug.LogWarning(objectID + " için kýrýk versiyon atanmadý!");
        }
    }
}
