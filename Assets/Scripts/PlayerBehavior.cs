using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2DPlayer;

    [SerializeField] private float speedMovement;
    [SerializeField] private float speedJump;
    //[SerializeField] private float climbJump = 200f;

    [SerializeField] private float horizontalMove;

    private bool Jumping;
    //[SerializeField] private bool Climbing = false;
    private bool Walking;
    public MenuStart menuStart;

    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRay;
    [SerializeField] private LayerMask collisionLayer;

    private void Update()
    {
        if (menuStart.Pausing == true)
        {
            Time.timeScale = 0;
        } 
        else if (menuStart.Pausing == false)
        {
            Time.timeScale = 1;
        }

        horizontalMove = Input.GetAxis("Horizontal") * speedMovement * Time.deltaTime;

        if (Input.GetButtonDown("Jump") && Walking == true)
        {
            Jumping = true;
        }
    }

    void FixedUpdate()
    {
        Walking = Physics2D.OverlapCircle(groundCheck.position, groundCheckRay, collisionLayer);
        MovePlayer(horizontalMove);        
    }

    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetSpeed = new Vector2(_horizontalMovement, rb2DPlayer.velocity.y);
        rb2DPlayer.velocity = Vector3.SmoothDamp(rb2DPlayer.velocity, targetSpeed, ref velocity, 0.05f);

        if (Jumping == true)
        {
            rb2DPlayer.MovePosition(Vector2.up * speedJump);
            //rb2DPlayer.AddForce(new Vector2(100f, speedJump)); //aucune idée de pourquoi ça marche pas hein mais bon
            Jumping = false;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRay);
    }
}
