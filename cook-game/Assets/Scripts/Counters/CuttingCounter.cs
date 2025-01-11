using System;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    public event EventHandler<ProgressChangedEventArgs> OnProgressChanged;
    public class ProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }

    public event EventHandler OnCut;
    
    [SerializeField] private CuttingRecipeSO[] cuttingRecipySOArray;
    private int cuttingProgress;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    CuttingRecipeSO fryingRecipeSo = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    OnProgressChanged?.Invoke(this, new ProgressChangedEventArgs
                    {
                        progressNormalized = (float) cuttingProgress / fryingRecipeSo.cuttingProgressMax
                    });
                }
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
    
    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            CuttingRecipeSO fryingRecipeSo = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            
            cuttingProgress++;
            OnCut?.Invoke(this, EventArgs.Empty);
            OnProgressChanged?.Invoke(this, new ProgressChangedEventArgs
            {
                progressNormalized = (float) cuttingProgress / fryingRecipeSo.cuttingProgressMax
            });
            
            if (cuttingProgress >= fryingRecipeSo.cuttingProgressMax)
            {
                KitchenObjectSO resultKitchenObjectSO =
                    GetResultKitchenObjectSO(GetKitchenObject().GetKitchenObjectSO());

                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(resultKitchenObjectSO, this);
            }
        }
    }
    
    private bool HasRecipeWithInput(KitchenObjectSO kitchenObjectSO)
    {
        CuttingRecipeSO fryingRecipeSo = GetCuttingRecipeSOWithInput(kitchenObjectSO);
        
        return fryingRecipeSo != null;
    }
    
    private KitchenObjectSO GetResultKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
    {
        CuttingRecipeSO fryingRecipeSo = GetCuttingRecipeSOWithInput(kitchenObjectSO);

        if (fryingRecipeSo != null)
        {
            return fryingRecipeSo.resultKitchenObjectSO;
        }
        
        return null;
    }
    
    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipySO in cuttingRecipySOArray)
        {
            if (cuttingRecipySO.kitchenObjectSO == kitchenObjectSO)
            {
                return cuttingRecipySO;
            }
        }
        
        return null;
    }
    
}