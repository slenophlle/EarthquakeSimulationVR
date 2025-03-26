using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticaleSystem : MonoBehaviour
{
    [Header("Referanslar")]
    [Tooltip("EarthquakeManager nesnesini Inspector'dan atayýn.")]
    public EarthquakeManager earthquakeManager;

    [Tooltip("Sadece bir kez oynatmak istediðiniz Particle System veya AudioSource vb. efekti atayýn.")]
    public ParticleSystem[] stageOneParticle; // Ýsterseniz AudioSource da olabilir

    // Efektin sadece bir kez tetiklenmesini kontrol etmek için
    private bool hasPlayed = false;

    void Update()
    {
        if (!hasPlayed
            && earthquakeManager != null
            && earthquakeManager.CurrentStage == 2 || earthquakeManager.CurrentStage == 3 )
        {
            // Efekti çalýþtýr
            if (stageOneParticle != null)
            {
                foreach (ParticleSystem stageOneParticle in stageOneParticle)
                {
                    stageOneParticle.Play();
                }
                
            }

            // Tekrar tetiklenmemesi için iþaret koy
            hasPlayed = true;

        }
    }
}
