using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotationSpeed = 20f;
    [SerializeField] private GameInput gameInput;
    
    private bool isWalking;
    
    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        
        Vector3 moveVector = new Vector3(inputVector.x, 0f, inputVector.y);
        isWalking = moveVector != Vector3.zero;
        moveVector *= Time.deltaTime * moveSpeed;
        
        transform.position += moveVector;
        transform.forward = Vector3.Slerp(transform.forward,moveVector,Time.deltaTime*rotationSpeed);
    }
    
    public bool IsWalking()
    {
        return isWalking;
    }
    
    
}
