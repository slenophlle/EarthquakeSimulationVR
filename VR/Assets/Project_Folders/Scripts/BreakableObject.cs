using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public string objectID; // �rne�in: "Bardak_Masa1"
    public GameObject brokenPrefab; // K�r�k versiyonu

    public void Break()
    {
        if (brokenPrefab != null)
        {
            Instantiate(brokenPrefab, transform.position, transform.rotation);
            Debug.Log(objectID + " k�r�ld�!");
            Destroy(gameObject); // Eski nesneyi yok et
        }
        else
        {
            Debug.LogWarning(objectID + " i�in k�r�k versiyon atanmad�!");
        }
    }
}
