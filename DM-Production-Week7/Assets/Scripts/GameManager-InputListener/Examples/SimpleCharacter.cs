using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

// uses a rigidbody to move a simple character around the scene with the Unity input system
public class SimpleCharacter : MonoBehaviour
{
    // The name of the action map to use for this Object
    public string actionMapName = "Player";
    private InputActionMap currentActionMap;

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
  
    public float groundedMovementSpeed = 5f;
    public float airborneMovementSpeed = 5f;
    public float groundedDistance = 0.1f;
    public float jumpHeight = 2f;
    public float rotateSpeed = 20f;

    private Rigidbody rb;
    private void Start()
    {
        //Subscribe to action map changes and get the initial action map
        GameManager.Instance.OnActionMapChange += OnActionMapChange;
        OnActionMapChange();
    }
    // Called when the action map changes in the GameManager and from start to set the initial action map
    // This is tightly coupled with the GameManager's currentActionMap so not best practice but
    // it does only update on action map change events rather than calling the value directly.
    private void OnActionMapChange()
    {
        currentActionMap = GameManager.Instance.currentActionMap;
        
        if (currentActionMap == null)
        {
            // no action map, log warning and return
            #if UNITY_EDITOR
            Debug.LogWarning("Current Action Map is null in SimpleCharacter.");
            #endif
            return;
        }
        else {

            if (currentActionMap.name == actionMapName)
            {
                //action map name matches, get actions
                // try to find actions on the current action map and log warning if not found
                try {                     
                    
                    moveAction = currentActionMap.FindAction("Move", true);
                    lookAction = currentActionMap.FindAction("Look", true);
                    jumpAction = currentActionMap.FindAction("Jump", true);
                }
                catch (System.Exception e)
                {
                    #if UNITY_EDITOR
                    Debug.LogWarning($"SimpleCharacter on {gameObject.name} could not find one or more actions in action map '{actionMapName}': {e.Message}");
                    #endif
                }

            }
            else
            {
                //action map does not match, set null so that no actions are processed in update
                currentActionMap = null;
                return;
            }
        }

    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    private void Update()
    {
        if (currentActionMap == null)
        {
            // no action map, do nothing
            return;
        }

        Jump();
        Move();
        Look(); 

    }

    private void Jump()
    {
        // check if grounded and jump action is triggered
       if(IsGrounded()) 
        {
            if (jumpAction.WasPressedThisFrame())
            {
                // Perform jump
                float jumpVelocity = Mathf.Sqrt(2 * jumpHeight * Physics.gravity.magnitude);
                Vector3 currentVelocity = rb.linearVelocity;
                //reset y velocity to 0 before jumping
                currentVelocity.y = 0;
                rb.linearVelocity = currentVelocity;
                // add jump force
                Vector3 jump = new Vector3(0, jumpVelocity, 0);
                rb.AddForce(jump, ForceMode.VelocityChange);
                
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * groundedDistance);
    }

    private void Move()
    {
        // get movement input and move character
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        move = Camera.main.transform.TransformDirection(move);
        move.y = 0;
        rb.MovePosition(rb.position + move * Time.deltaTime * (IsGrounded() ? groundedMovementSpeed : airborneMovementSpeed));
    }
    private void Look()
    {
        // get rotation input and rotate character
        Vector2 lookInput = lookAction.ReadValue<Vector2>();
        Vector3 rotation = new Vector3(0, lookInput.x, 0);
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation * rotateSpeed * Time.deltaTime));
    }

    private bool IsGrounded()
    {
        // check if the character is grounded
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundedDistance))
        {
            return true;
        }
        return false;
    }
}
