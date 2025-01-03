using UnityEngine;

public interface IKitchenObjectParent
{
    public void ClearKitchenObject();
    public void SetKitchenObject(KitchenObject kitchenObject);
    public bool HasKitchenObject();
    public KitchenObject GetKitchenObject();
    public Transform GetKitchenObjectFollowTransform();

}
