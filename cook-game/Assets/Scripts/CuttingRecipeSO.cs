using UnityEngine;

[CreateAssetMenu(fileName = "CuttingRecipySO", menuName = "CuttingRecipy")]
public class CuttingRecipySO : ScriptableObject
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private KitchenObjectSO resultKitchenObjectSO;
    
    public KitchenObjectSO KitchenObjectSO => kitchenObjectSO;
    public KitchenObjectSO ResultKitchenObjectSO => resultKitchenObjectSO;
}
