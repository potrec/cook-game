using UnityEngine;

[CreateAssetMenu(fileName = "FryingRecipe", menuName = "ScriptableObjects/FryingRecipeSO", order = 1)]
public class FryingRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public float fryingTimerMax;
}
