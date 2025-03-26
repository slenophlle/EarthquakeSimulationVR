using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    [Header("Shake Settings")]
    [SerializeField] private float intensity = 0.2f;  // Kameranýn sallantý þiddeti
    [SerializeField] private float frequency = 3f;    // Perlin Noise frekansý

    private Vector3 originalPos;
    private bool isShaking = false;

    /// <summary>
    /// Sadece sallantý süresini (duration) parametre olarak alýyoruz.
    /// intensity ve frequency sabit veya Inspector'dan ayarlanabilir.
    /// </summary>
    public void StartShake(float duration)
    {
        if (!isShaking)
        {
            StartCoroutine(Shake(intensity, frequency, duration));
        }
    }

    private IEnumerator Shake(float initialIntensity, float frequency, float duration)
    {
        isShaking = true;
        originalPos = transform.localPosition;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            // elapsed/duration oranýyla intensity deðerini zamanla azaltýyoruz.
            float fadeIntensity = Mathf.Lerp(initialIntensity, 0f, elapsed / duration);
            float x = Mathf.PerlinNoise(Time.time * frequency, 0f) * 2 - 1;
            float y = Mathf.PerlinNoise(0f, Time.time * frequency) * 2 - 1;
            transform.localPosition = originalPos + new Vector3(x, y, 0) * fadeIntensity;

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Sallantý bittiðinde kamerayý eski konumuna döndür
        transform.localPosition = originalPos;
        isShaking = false;

        Debug.Log("Camera Shake Finished!");
    }
}
