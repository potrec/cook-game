using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;
    
    private List<GameObject> plateVisualGameObjects = new List<GameObject>();

    public void Awake()
    {
        plateVisualGameObjects = new List<GameObject>();
    }

    private void Start()
    {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }
    
    private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);
        
        float plateOffsetY = .1f;
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY*plateVisualGameObjects.Count, 0);
        
        plateVisualGameObjects.Add(plateVisualTransform.gameObject);
    }
    
    private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        Destroy(plateVisualGameObjects[plateVisualGameObjects.Count - 1]);
        plateVisualGameObjects.RemoveAt(plateVisualGameObjects.Count - 1);
    }
}
