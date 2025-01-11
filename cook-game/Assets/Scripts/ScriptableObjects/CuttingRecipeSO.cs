using UnityEngine;

[CreateAssetMenu(fileName = "CuttingRecipySO", menuName = "ScriptableObjects/CuttingRecipy")]
public class CuttingRecipeSO : ScriptableObject
{
    public KitchenObjectSO kitchenObjectSO;
    public KitchenObjectSO resultKitchenObjectSO;
    public int cuttingProgressMax;
}
