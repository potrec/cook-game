using System;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }
    
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
        DeliveryManager.Instance.OnRecipeTimeOut += DeliveryManager_OnRecipeTimeOut;
        
        UpdateVisuals();
    }
    
    private void DeliveryManager_OnRecipeCompleted(object sender, EventArgs e)
    {
        UpdateVisuals();
    }
    
    private void DeliveryManager_OnRecipeSpawned(object sender, EventArgs e)
    {
        UpdateVisuals();
    }
    
    private void DeliveryManager_OnRecipeTimeOut(object sender, EventArgs e)
    {
        UpdateVisuals();
    }
    
    private void UpdateVisuals()
    {
        foreach (Transform child in container)
        {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (DeliveryRecipe recipe in DeliveryManager.Instance.GetWaitingDeliveryRecipeList())
        {
            Debug.Log($"Instantiating {recipe.recipeSO.recipeName} time left: {recipe.timeLeftToDeliver}");
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetDeliveryRecipe(recipe);
        }
    }
}
