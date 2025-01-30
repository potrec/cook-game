using UnityEngine;

[CreateAssetMenu(fileName = "CuttingRecipe", menuName = "ScriptableObjects/CuttingRecipe")]
public class CuttingRecipeSO : ScriptableObject
{
    public KitchenObjectSO kitchenObjectSO;
    public KitchenObjectSO resultKitchenObjectSO;
    public int cuttingProgressMax;
}
