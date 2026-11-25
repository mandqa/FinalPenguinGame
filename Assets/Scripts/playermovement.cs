using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //public float speed;
    public float groundDist;
    
    private bool canMove = true;

    public LayerMask terrainLayer;
    private Rigidbody rb;
    public SpriteRenderer sr;

    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;

    [SerializeField] private Transform cam;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        //if no cam assigned, use main
        if (cam == null)
        {
            cam = Camera.main.transform;
        }
    }

    void Update()
    {
        if (!canMove)
            return;
        /*RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1;
        if (Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer))
        {
            if (hit.collider != null)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDist;
                transform.position = movePos;
            }
        }
        */
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        //checks if left shift being held
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        //choose between walk and sprint
        float currentSpeed = isSprinting ? sprintSpeed : walkSpeed;

        //camera directions 
        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;
        
        //camera directions
        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();
        //Debug.Log("horizontal" + x + "vertical" + z);
        Vector3 moveDir = (camForward * z) + (camRight * x); 
        moveDir = Vector3.ClampMagnitude(moveDir, 1f);
        //applied movement
        rb.linearVelocity = new Vector3(moveDir.x * currentSpeed, rb.linearVelocity.y, moveDir.z * currentSpeed);

        //flip sprite based on camera relative left/right movement
        float horizontalDirection = Vector3.Dot(moveDir, camRight);
        
        //flip sprite depending on direction
        if (horizontalDirection < -0.01f)
        {
            sr.flipX = true;
        }
        else if (horizontalDirection > 0.01f)
        {
            sr.flipX = false;
        }
    }

    public void FreezePlayer()
    {
        canMove = false;
        rb.linearVelocity = Vector3.zero;
    }

    public void UnfreezePlayer()
    {
        canMove = true;
    }
}