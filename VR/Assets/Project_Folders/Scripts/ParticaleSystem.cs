using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticaleSystem : MonoBehaviour
{
    [Header("Referanslar")]
    [Tooltip("EarthquakeManager nesnesini Inspector'dan atay�n.")]
    public EarthquakeManager earthquakeManager;

    [Tooltip("Sadece bir kez oynatmak istedi�iniz Particle System veya AudioSource vb. efekti atay�n.")]
    public ParticleSystem[] stageOneParticle; // �sterseniz AudioSource da olabilir

    // Efektin sadece bir kez tetiklenmesini kontrol etmek i�in
    private bool hasPlayed = false;

    void Update()
    {
        if (!hasPlayed
            && earthquakeManager != null
            && earthquakeManager.CurrentStage == 2 || earthquakeManager.CurrentStage == 3 )
        {
            // Efekti �al��t�r
            if (stageOneParticle != null)
            {
                foreach (ParticleSystem stageOneParticle in stageOneParticle)
                {
                    stageOneParticle.Play();
                }
                
            }

            // Tekrar tetiklenmemesi i�in i�aret koy
            hasPlayed = true;

        }
    }
}
