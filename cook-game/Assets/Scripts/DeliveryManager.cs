using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryRecipe
{
    public RecipeSO recipeSO;
    public float timeToDeliver;
    public float timeLeftToDeliver;
}

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;
    public event EventHandler OnRecipeTimeOut;
    
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO recipeListSO;
    private List<DeliveryRecipe> waitingDeliveryRecipeList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;
    private int successfullRecipeCount = 0;
    private int failedRecipeCount = 0;

    private void Awake()
    {
        Instance = this;
        waitingDeliveryRecipeList = new List<DeliveryRecipe>();
    }
    
    private void Start()
    {
        
    }

    private void Update()
    {
        foreach (var deliveryRecipe in waitingDeliveryRecipeList.ToList())
        {
            UpdateRecipeTime(deliveryRecipe, Time.deltaTime);
        }
        
        spawnRecipeTimer -= Time.deltaTime;
        if(spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;
            if(waitingDeliveryRecipeList.Count < waitingRecipeMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingDeliveryRecipeList.Add(new DeliveryRecipe
                {
                    recipeSO = waitingRecipeSO,
                    timeToDeliver = 120f,
                    timeLeftToDeliver = 120f
                });
                
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        foreach (DeliveryRecipe recipe in waitingDeliveryRecipeList)
        {
            RecipeSO waitingRecipeSO = recipe.recipeSO;
            if (plateKitchenObject.GetKitchenObjectSOList().Count == waitingRecipeSO.kitchenObjectSOList.Count)
            {
                bool isRecipeMatch = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        if (recipeKitchenObjectSO == plateKitchenObjectSO)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound)
                    {
                        isRecipeMatch = false;
                        break;
                    }
                }
                if (isRecipeMatch)
                {
                    waitingDeliveryRecipeList.Remove(recipe);
                    
                    successfullRecipeCount++;
                    
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        
        Debug.Log("Recipe not match");
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }
    
    public List<DeliveryRecipe> GetWaitingDeliveryRecipeList()
    {
        return waitingDeliveryRecipeList;
    }
    
    public int GetSuccessfullRecipeCount()
    {
        return successfullRecipeCount;
    }
    
    public void UpdateRecipeTime(DeliveryRecipe deliveryRecipe, float time)
    {
        deliveryRecipe.timeLeftToDeliver -= time;
        if (deliveryRecipe.timeLeftToDeliver <= 0)
        {
            failedRecipeCount++;
            waitingDeliveryRecipeList.Remove(deliveryRecipe);
            OnRecipeTimeOut?.Invoke(this, EventArgs.Empty);
        }
    }
}
