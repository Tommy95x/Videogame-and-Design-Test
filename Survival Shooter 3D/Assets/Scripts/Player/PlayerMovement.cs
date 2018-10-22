using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player' speed")]
    public float speed;

    //We use this vector to store the movements that we want to apply to the player
    private Vector3 movement;
    private Animator anim;
    private Rigidbody playerRigidbody;
    private int floorMask;
    private float canRayLenght = 100f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        floorMask = LayerMask.GetMask("Floor");
    }

    //This kind of Update function is call every time that we have a physic change inside the scene
    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Turning();
        Animating(h, v);
    }

    private void Move(float h, float v)
    {
        movement.Set(h ,0f ,v);
        
        //We must normalize the diagonal movement, bacause normally diagonally we have a greater movement
        movement = movement.normalized * speed * Time.deltaTime;

        playerRigidbody.MovePosition(transform.position + movement);

    }

    private void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if(Physics.Raycast(camRay, out floorHit, canRayLenght, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }


    private void Animating(float h, float v)
    {
        //We use this bool variable to automatize the set of walking animation
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }
}
