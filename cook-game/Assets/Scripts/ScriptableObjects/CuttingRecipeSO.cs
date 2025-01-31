using UnityEngine;

[CreateAssetMenu(fileName = "CuttingRecipe", menuName = "ScriptableObjects/CuttingRecipeSO")]
public class CuttingRecipeSO : ScriptableObject
{
    public KitchenObjectSO kitchenObjectSO;
    public KitchenObjectSO resultKitchenObjectSO;
    public int cuttingProgressMax;
}
