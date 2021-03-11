using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using System.Collections;
using Pathfinding;

public class PlayerBehavior : MonoBehaviour
{
    public string[] levelName;
    public int currentLevel = 0;

    private Vector3 lastPosition;
    [SerializeField] private Parallaxe parallax;

    [SerializeField] private Transform start;
    /*[SerializeField] private Path path;
    Seeker seeker;*/
    /*[SerializeField] private int currentWayPoint = 0;
    [SerializeField] private bool reachedEnd = false;
    [SerializeField] private float nextPoint = 3f;*/

    [SerializeField] private Rigidbody2D rb2DPlayer;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private DiscussionManager discuss;

    [SerializeField] private float speedMovement;
    [SerializeField] private float speedJump;
    //[SerializeField] private float climbJump = 200f;

    [SerializeField] private float horizontalMove;

    //private bool Jumping;
    //[SerializeField] private bool Climbing = false;
    private bool Walking;
    public MenuStart menuStart;

    //[SerializeField] private GameObject DiscussionBox;

    public AledText aledText;

    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRay;
    [SerializeField] private LayerMask collisionLayer;

    private void Start()
    {
        spriteRenderer.enabled = true;
        lastPosition = transform.localPosition;
        /*seeker = GetComponent<Seeker>();
        seeker.StartPath(rb2DPlayer.position, start.position, onPathComplete);
        //PlayerPrefs.DeleteAll();
        InvokeRepeating("UpdatePath", 0f, .5f);*/
    }

    /*IEnumerator Respawn()
    {
        Debug.Log("ého");
        *//*if (path == null)
        {
            return null;
        }*//*
        while (Vector2.Distance(start.position, rb2DPlayer.position) > 1)
        {
            if (currentWayPoint >= path.vectorPath.Count)
            {
                Debug.Log("test");
                reachedEnd = true;
            }
            else
            {
                Debug.Log("echec");
                reachedEnd = false;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb2DPlayer.position).normalized;
            Vector2 force = direction * speedMovement * Time.deltaTime;

            float distance = Vector2.Distance(rb2DPlayer.position, path.vectorPath[currentWayPoint]);

            if (distance < nextPoint)
            {
                Debug.Log("JE MARCHE PAS ET JE SOULE OROR");
                currentWayPoint++;
            }
            yield return null;
        }
    }*/

    /*void onPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb2DPlayer.position, start.position, onPathComplete);
        }
    }*/
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

        /*if (speedMovement == 0 && speedJump == 0)
        {
            Debug.Log("test");

            *//*while (Vector2.Distance(start.position, rb2DPlayer.position) > 1)
            {
                if (currentWayPoint >= path.vectorPath.Count)
                {
                    Debug.Log("test");
                    reachedEnd = true;
                }
                else
                {
                    Debug.Log("echec");
                    reachedEnd = false;
                }

                Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb2DPlayer.position).normalized;
                Vector2 force = direction * speedMovement * Time.deltaTime;

                float distance = Vector2.Distance(rb2DPlayer.position, path.vectorPath[currentWayPoint]);

                if (distance < nextPoint)
                {
                    Debug.Log("JE MARCHE PAS ET JE SOULE OROR");
                    currentWayPoint++;
                }
            }*//*
        }*/

        if (GameObject.Find("Discussion") == null || discuss.DiscussionBox.activeSelf == false)
        {
            if (Input.GetButton("Horizontal"))
            {
                animator.SetBool("isRunning", true);
                if (Input.GetAxis("Horizontal") > 0f && spriteRenderer.flipX == false)
                {
                    spriteRenderer.flipX = true;
                }
                else if (Input.GetAxis("Horizontal") < 0f && spriteRenderer.flipX == true)
                {
                    spriteRenderer.flipX = false;
                }
            }
            else
            {
                animator.SetBool("isRunning", false);
            }

            horizontalMove = Input.GetAxis("Horizontal") * speedMovement * Time.deltaTime;

            if (Input.GetButtonDown("Jump") && Walking == true)
            {
                animator.SetTrigger("PrepaJump");
                rb2DPlayer.velocity = Vector2.up * speedJump;
                //Jumping = true;
            }
        }
        
    }


    void FixedUpdate()
    {
        Walking = Physics2D.OverlapCircle(groundCheck.position, groundCheckRay, collisionLayer);
        MovePlayer(horizontalMove);

        parallax.UpdateParallax(transform.localPosition.x - lastPosition.x);
        lastPosition = transform.localPosition;
    }

    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetSpeed = new Vector2(_horizontalMovement, rb2DPlayer.velocity.y);
        rb2DPlayer.velocity = Vector3.SmoothDamp(rb2DPlayer.velocity, targetSpeed, ref velocity, 0.05f);

        if (Walking == true)
        {
            animator.SetBool("isJumping", false);
        } else
        {
            animator.SetBool("isJumping", true);
            
        }

        /*if (Jumping == true)
        {
            //animator.SetTrigger("Jump");
            rb2DPlayer.velocity = Vector2.up * speedJump;
            //rb2DPlayer.MovePosition(Vector2.up * speedJump);
            //rb2DPlayer.AddForce(new Vector2(100f, speedJump)); //aucune idée de pourquoi ça marche pas hein mais bon
            Jumping = false;
        }*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "End")
        {
            currentLevel = PlayerPrefs.GetInt("nextLevel");
            int m_nextLevel = currentLevel + 1;
            Debug.Log(m_nextLevel);
            SceneManager.LoadScene(levelName[m_nextLevel]);
            PlayerPrefs.SetInt("nextLevel", m_nextLevel);
            Debug.Log(m_nextLevel);
        }

        if(collision.gameObject.tag == "Aled")
        {
            if (SceneManager.GetActiveScene().name == "Level1_3")
            {
                animator.SetBool("isRunning", false);
                horizontalMove = 0;
                discuss.DiscussionBox.SetActive(true);
                
            }
            Destroy(collision.gameObject);
            AledText.score ++;

            
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRay);
    }
    void OnBecameInvisible()
    {
        Debug.Log("JE SUIS PAS VISIBLE LALALALA");
        rb2DPlayer.transform.position = start.position; //retour à la case départ

        //StartCoroutine("Respawn");
    }

    
}
