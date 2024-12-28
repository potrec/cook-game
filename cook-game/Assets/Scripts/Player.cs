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
        
        float playerHeight = 2f;
        float playerRadius = 0.7f;
        float moveDistance = moveSpeed * Time.deltaTime;
        
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveVector, moveDistance);
        
        if(!canMove)
        {
            Vector3 moveVectorX = new Vector3(moveVector.x, 0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveVectorX, moveDistance);

            if (canMove)
            {
                moveVector = moveVectorX;
            }
            else
            {
                Vector3 moveVectorZ = new Vector3(0f, 0f, moveVector.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveVectorZ, moveDistance);
                if (canMove)
                {
                    moveVector = moveVectorZ;
                }
                else
                {
                    moveVector = Vector3.zero;
                }
            }
        }
        if (canMove)
        {
            transform.position += moveVector * moveDistance;
        }
        
        isWalking = moveVector != Vector3.zero;
        
        transform.forward = Vector3.Slerp(transform.forward,moveVector,Time.deltaTime*rotationSpeed);
    }
    
    public bool IsWalking()
    {
        return isWalking;
    }
    
    
}
