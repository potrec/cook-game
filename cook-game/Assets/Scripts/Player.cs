using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotationSpeed = 20f;
    private void Start()
    {
        
    }
    
    private void Update()
    {
        Vector2 inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = 1;
        }
        inputVector = inputVector.normalized;
        
        Vector3 moveVector = new Vector3(inputVector.x, 0f, inputVector.y);
        moveVector *= Time.deltaTime * moveSpeed;
        
        transform.position += moveVector;
        transform.forward = Vector3.Slerp(transform.forward,moveVector,Time.deltaTime*rotationSpeed);
    }
}
