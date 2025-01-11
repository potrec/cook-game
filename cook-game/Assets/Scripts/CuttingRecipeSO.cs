using UnityEngine;

[CreateAssetMenu(fileName = "CuttingRecipySO", menuName = "CuttingRecipy")]
public class CuttingRecipySO : ScriptableObject
{
    public KitchenObjectSO kitchenObjectSO;
    public KitchenObjectSO resultKitchenObjectSO;
    public int cuttingProgressMax;
}
