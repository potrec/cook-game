using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipySO[] cuttingRecipySOArray;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipyWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
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
            KitchenObjectSO resultKitchenObjectSO = GetResultKitchenObjectSO(GetKitchenObject().GetKitchenObjectSO());
            
            GetKitchenObject().DestroySelf();
            
            KitchenObject.SpawnKitchenObject(resultKitchenObjectSO, this);
        }
    }
    
    private bool HasRecipyWithInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (CuttingRecipySO cuttingRecipySO in cuttingRecipySOArray)
        {
            if (cuttingRecipySO.KitchenObjectSO == kitchenObjectSO)
            {
                return true;
            }
        }
        
        return false;
    }
    
    private KitchenObjectSO GetResultKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
    {
        foreach (CuttingRecipySO cuttingRecipySO in cuttingRecipySOArray)
        {
            if (cuttingRecipySO.KitchenObjectSO == kitchenObjectSO)
            {
                return cuttingRecipySO.ResultKitchenObjectSO;
            }
        }
        
        return null;
    }
}
