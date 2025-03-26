using UnityEngine;
using System.Collections.Generic;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager Instance; // Singleton yap�s�

    // Her objectID i�in birden fazla nesne tutabilmek ad�na List kulland�k.
    private Dictionary<string, List<BreakableObject>> breakableObjects = new Dictionary<string, List<BreakableObject>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        BreakableObject[] objects = FindObjectsOfType<BreakableObject>();
        foreach (var obj in objects)
        {
            if (breakableObjects.ContainsKey(obj.objectID))
            {
                breakableObjects[obj.objectID].Add(obj);
            }
            else
            {
                breakableObjects[obj.objectID] = new List<BreakableObject> { obj };
            }

        }
    }

    public void BreakObject(string objectID)
    {
        if (breakableObjects.ContainsKey(objectID))
        {
            foreach (var obj in breakableObjects[objectID])
            {
                obj.Break();
            }
            breakableObjects.Remove(objectID);
        }
        else
        {
            Debug.LogWarning(objectID + " bulunamad�!");
        }
    }
}
