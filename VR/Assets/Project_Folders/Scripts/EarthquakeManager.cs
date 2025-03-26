using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class EarthquakeManager : MonoBehaviour
{
    public ShakeBuilding shakeEffect;
    public CameraShake cameraShake;

    [Header("Quake Values")]
    public float[] quakeIntensities = { 0.01f, 0.02f, 0.03f };
    public float[] quakeFrequencies = { 3f, 3f, 3f };
    public float[] quakeDurations = { 12f, 13f, 6f };
    public Vector2 earthquakeIntervalRange = new Vector2(15f, 25f);

    private float earthquakeInterval;
    private int currentStage = 0;
    public int CurrentStage => currentStage;
    private float timer = 0f;
    public static bool canActive = false;

    [Header("Audio Sources")]
    public AudioSource[] soundStages;

    [Header("UI Elements")]
    public GameObject endPanel;
    public GameObject restartBTN;
    public TMP_Text countdownText;
    public TMP_Text RestartText;

    [Header("Objects to Change")]
    public GameObject[] solidObjects;  // Saðlam objeler (normal bina)
    public GameObject[] brokenObjects; // Kýrýk objeler (hasarlý bina)

    void Start()
    {     
        earthquakeInterval = Random.Range(earthquakeIntervalRange.x, earthquakeIntervalRange.y);
    }

    void Update()
    {
        if (!canActive) return;

        timer += Time.deltaTime;

        if (timer >= earthquakeInterval && currentStage < quakeIntensities.Length)
        {
            timer = 0f;
            earthquakeInterval = Random.Range(earthquakeIntervalRange.x, earthquakeIntervalRange.y);

            float intensity = quakeIntensities[currentStage];
            float frequency = quakeFrequencies[currentStage];
            float duration = quakeDurations[currentStage];

            shakeEffect.StartShake(intensity, frequency, duration);
            cameraShake.StartShake(duration);

            if (currentStage < soundStages.Length)
            {
                soundStages[currentStage].Play();
            }

            Debug.Log($"Deprem Aþamasý {currentStage + 1} baþladý! Þiddet: {intensity}, Frekans: {frequency}, Süre: {duration}, Bekleme Süresi: {earthquakeInterval} saniye");
            
            // Depremin ikinci aþamasýnda binalarý kýr
            if (currentStage == 1)
            {
                ObjectManager.Instance.BreakObject("KýrýkBardak"); 
                ObjectManager.Instance.BreakObject("KýrýkÞiþe"); 

                for (int k = 0;  k<solidObjects.Length; k++)
                {
                    solidObjects[k].SetActive(false);
                }
               for(int j = 0; j< brokenObjects.Length; j++)
                {
                    brokenObjects[j].SetActive(true);
                }

            }

            currentStage++;
            StartCoroutine(StartNextStageAfterDelay(duration));
        }

        if (currentStage >= quakeIntensities.Length && !endPanel.activeSelf)
        {
            EndEarthquake();
        }
    }

    void EndEarthquake()
    {
        Debug.Log("Deprem sona erdi!");
        canActive = false;
        endPanel.SetActive(true);
    }

    public void RestartEarthquake()
    {
        restartBTN.SetActive(false);
        countdownText.gameObject.SetActive(true);
        RestartText.gameObject.SetActive(false);
        StartCoroutine(RestartAfterCountdown(5f));
    }

    private IEnumerator RestartAfterCountdown(float countdown)
    {
        Debug.Log("Geri sayým baþladý...");
        endPanel.SetActive(true);

        while (countdown > 0)
        {
            countdownText.text = $"{countdown} saniye sonra deprem baþlayacak!";
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        countdownText.text = "Deprem baþlýyor!";
        yield return new WaitForSeconds(1f);

        Debug.Log("Sahne yeniden baþlatýlýyor...");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private IEnumerator StartNextStageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        timer = 0f;
    }
}