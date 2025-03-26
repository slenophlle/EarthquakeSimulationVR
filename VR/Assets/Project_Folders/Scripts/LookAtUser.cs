using UnityEngine;

public class LookAtUser : MonoBehaviour
{
    public Transform xrCamera; // XR Kamerasýný (Headset) buraya atayýn
    public float followSpeed = 5f; // UI'nin dönüþ hýzýný belirler
    public float distanceFromUser = 2f; // UI'nin kullanýcýdan uzaklýðý

    void Update()
    {
        if (xrCamera == null)
        {
            Debug.LogWarning("XR Camera atanmamýþ!");
            return;
        }

        // Kullanýcýnýn bakýþ yönünü belirle
        Vector3 targetPosition = xrCamera.position + xrCamera.forward * distanceFromUser;
        targetPosition.y = xrCamera.position.y; // UI'nin dikeyde hizalanmasýný saðla

        // UI'nin konumunu güncelle
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);

        // Doðru yöne bakmasý için 180 derece döndür
        Quaternion lookRotation = Quaternion.LookRotation(transform.position - xrCamera.position);
        transform.rotation = lookRotation * Quaternion.Euler(0, 0, 0);  // Buradaki 180 UI'yi düzeltiyor
    }

}