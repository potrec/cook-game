using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipySO[] cuttingRecipySOArray;
    private int cuttingProgress;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipyWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
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
        if (HasKitchenObject() && HasRecipyWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            CuttingRecipySO cuttingRecipySO = GetCuttingRecipySOWithInput(GetKitchenObject().GetKitchenObjectSO());
            
            cuttingProgress++;
            if (cuttingProgress >= cuttingRecipySO.cuttingProgressMax)
            {
                KitchenObjectSO resultKitchenObjectSO =
                    GetResultKitchenObjectSO(GetKitchenObject().GetKitchenObjectSO());

                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(resultKitchenObjectSO, this);
            }
        }
    }
    
    private bool HasRecipyWithInput(KitchenObjectSO kitchenObjectSO)
    {
        CuttingRecipySO cuttingRecipySO = GetCuttingRecipySOWithInput(kitchenObjectSO);
        
        return cuttingRecipySO != null;
    }
    
    private KitchenObjectSO GetResultKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
    {
        CuttingRecipySO cuttingRecipySO = GetCuttingRecipySOWithInput(kitchenObjectSO);

        if (cuttingRecipySO != null)
        {
            return cuttingRecipySO.resultKitchenObjectSO;
        }
        
        return null;
    }
    
    private CuttingRecipySO GetCuttingRecipySOWithInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (CuttingRecipySO cuttingRecipySO in cuttingRecipySOArray)
        {
            if (cuttingRecipySO.kitchenObjectSO == kitchenObjectSO)
            {
                return cuttingRecipySO;
            }
        }
        
        return null;
    }
    
}
