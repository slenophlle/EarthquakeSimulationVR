using UnityEngine;

public class LookAtUser : MonoBehaviour
{
    public Transform xrCamera; // XR Kameras�n� (Headset) buraya atay�n
    public float followSpeed = 5f; // UI'nin d�n�� h�z�n� belirler
    public float distanceFromUser = 2f; // UI'nin kullan�c�dan uzakl���

    void Update()
    {
        if (xrCamera == null)
        {
            Debug.LogWarning("XR Camera atanmam��!");
            return;
        }

        // Kullan�c�n�n bak�� y�n�n� belirle
        Vector3 targetPosition = xrCamera.position + xrCamera.forward * distanceFromUser;
        targetPosition.y = xrCamera.position.y; // UI'nin dikeyde hizalanmas�n� sa�la

        // UI'nin konumunu g�ncelle
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);

        // Do�ru y�ne bakmas� i�in 180 derece d�nd�r
        Quaternion lookRotation = Quaternion.LookRotation(transform.position - xrCamera.position);
        transform.rotation = lookRotation * Quaternion.Euler(0, 0, 0);  // Buradaki 180 UI'yi d�zeltiyor
    }

}