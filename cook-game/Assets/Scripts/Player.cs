using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    
    public static Player Instance { get; private set; }
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }
    
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotationSpeed = 20f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    
    private KitchenObject kitchenObject;
    private bool isWalking;
    private Vector3 lastInteractionDirection;
    private ClearCounter selectedCounter;
    
    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Another instance of Player already exists");
        }
        Instance = this;
    }
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }
    
    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
       if(selectedCounter != null)
       {
           selectedCounter.Interact(this);
       }
    }
    
    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }
    
    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleMovement()
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
                moveVector = canMove ? moveVectorZ : Vector3.zero;
            }
        }
        if (canMove)
        {
            transform.position += moveVector * moveDistance;
        }
        
        isWalking = moveVector != Vector3.zero;
        
        transform.forward = Vector3.Slerp(transform.forward,moveVector,Time.deltaTime*rotationSpeed);
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveVector = new Vector3(inputVector.x, 0f, inputVector.y);
        float interactDistance = 2f;
        
        if(moveVector != Vector3.zero)
        {
            lastInteractionDirection = moveVector;
        }
        
        var raycast = Physics.Raycast(transform.position, lastInteractionDirection, out RaycastHit raycastHit, interactDistance, countersLayerMask);
        
        if (raycast)
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                if(clearCounter != selectedCounter)
                {
                    OnSelectedCounter(clearCounter);
                }
            }
            else
            {
                OnSelectedCounter(null);
            }
        }
        else
        {
            OnSelectedCounter(null);
        }
    }
    
    private void OnSelectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs{selectedCounter = selectedCounter});
    }


    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
    
    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }
    
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }
}
