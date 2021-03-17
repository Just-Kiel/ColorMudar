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
    [SerializeField] public Animator animator;
    [SerializeField] private SpriteRenderer[] spriteRenderer;
    [SerializeField] private GameObject perso;

    [SerializeField] private DiscussionManager discuss;

    [SerializeField] private float speedMovement;
    [SerializeField] private float speedJump;
    //[SerializeField] private float climbJump = 200f;

    [SerializeField] public float horizontalMove;

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

    [SerializeField] public GameObject toDisable = null;
    [SerializeField] bool getDash;
    [SerializeField] private bool isDashing;
    [SerializeField] private float speedDash;

    private void Start()
    {
        /*for (int i = 0; i < spriteRenderer.Length; i++)
        {
            spriteRenderer[i].enabled = true;
        }*/
        perso.transform.localScale = new Vector3(-perso.transform.localScale.x, perso.transform.localScale.y, perso.transform.localScale.z);
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

        if (toDisable && discuss.DiscussionBox.activeSelf == false)
        {

            toDisable.GetComponent<CapsuleCollider2D>().enabled = false;
        }

        if (GameObject.Find("Discussion") == null || discuss.DiscussionBox.activeSelf == false)
        {

                
                /*if (Input.GetAxis("Horizontal") > 0f && spriteRenderer.flipX == false)
                {
                    spriteRenderer.flipX = true;
                    animator.SetBool("isRunning", true);
                }
                else if (Input.GetAxis("Horizontal") < 0f && spriteRenderer.flipX == true)
                {
                    spriteRenderer.flipX = false;
                }
            else
            {
                animator.SetBool("isRunning", false);
            }*/

            horizontalMove = Input.GetAxis("Horizontal") * speedMovement * Time.deltaTime;

            if (Input.GetButtonDown("Jump") && Walking == true)
            {
                animator.SetTrigger("PrepaJump");
                rb2DPlayer.velocity = Vector2.up * speedJump;
                //Walking = false;
                //Jumping = true;
            }

            if(getDash == true && isDashing == false && Input.GetButtonDown("Dash"))
            {
                //petit souci à régler mais dans l'idée ça marche comme ça
                if (horizontalMove !=0)
                {
                    //Debug.Log(Input.GetAxis("Horizontal"));
                    rb2DPlayer.velocity = new Vector2(rb2DPlayer.velocity.x * Time.deltaTime * speedMovement * Input.GetAxis("Horizontal"), rb2DPlayer.velocity.y);
                    //rb2DPlayer.MovePosition(Input.GetAxis("Horizontal") * Vector2.right * speedJump);
                } else if (Input.GetAxis("Vertical") != 0)
                {
                    rb2DPlayer.velocity = new Vector2(rb2DPlayer.velocity.x, rb2DPlayer.velocity.y + Time.deltaTime * speedMovement * speedDash);
                    //rb2DPlayer.MovePosition(Input.GetAxis("Vertical") * Vector2.up * speedJump);
                } else
                {
                    rb2DPlayer.velocity = new Vector2(Time.deltaTime * speedMovement, rb2DPlayer.velocity.y);
                    //rb2DPlayer.MovePosition(Vector2.right * speedJump);
                }
                //rb2DPlayer.MovePosition(horizontalMove * Vector2.one * speedJump);
                Debug.Log("dash");
                isDashing = true;
            }
        }
        
    }


    void FixedUpdate()
    {
        Walking = Physics2D.OverlapCircle(groundCheck.position, groundCheckRay, collisionLayer);
        MovePlayer(horizontalMove);

        if (parallax != null)
        {
            parallax.UpdateParallax(transform.localPosition.x - lastPosition.x);
        }
        lastPosition = transform.localPosition;
    }

    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetSpeed = new Vector2(_horizontalMovement, rb2DPlayer.velocity.y);
        rb2DPlayer.velocity = Vector3.SmoothDamp(rb2DPlayer.velocity, targetSpeed, ref velocity, 0.05f);
        if(horizontalMove != 0)
        {
            animator.SetBool("isRunning", true);
        } else
        {
            animator.SetBool("isRunning", false);

        }

        /*for (int i = 0; i < spriteRenderer.Length; i++)
        {*/
            if (Input.GetAxis("Horizontal") > 0f && perso.transform.localScale.x > 0 /*&& spriteRenderer[0].flipX == false*/)
            {
                perso.transform.localScale = new Vector3(-perso.transform.localScale.x, perso.transform.localScale.y, perso.transform.localScale.z);
                //spriteRenderer[i].flipX = true;
                //gameObject.transform.localScale.x = -spriteRenderer[0].transform.localScale.x;

        }
            else if (Input.GetAxis("Horizontal") < 0f && perso.transform.localScale.x < 0 /*&& spriteRenderer[0].flipX == true*/)
            {
                perso.transform.localScale = new Vector3(-perso.transform.localScale.x, perso.transform.localScale.y, perso.transform.localScale.z);
                //spriteRenderer[0].flipX = false;
            }

            if (Walking == true)
            {
                isDashing = false;
                animator.SetBool("isJumping", false);
                Debug.Log("je veu pa soté");

        }
        else
            {
                animator.SetBool("isJumping", true);
            //Walking = true;
                Debug.Log("je saute llalalalalalala");
            }
        //}

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

        if (collision.gameObject.tag == "Dash")
        {
            Debug.Log("j'ai le dash");
            getDash = true;
            animator.SetBool("isRunning", false);
            horizontalMove = 0;
            discuss.DiscussionBox.SetActive(true);
            toDisable = collision.gameObject;
        }

        if (collision.gameObject.tag == "Reset")
        {
            rb2DPlayer.position = start.position; //retour à la case départ
            rb2DPlayer.velocity = new Vector2(0 * rb2DPlayer.velocity.x, 0 * rb2DPlayer.velocity.y);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRay);
    }
    /*void OnBecameInvisible()
    {
        if (rb2DPlayer)
        {
            Debug.Log("JE SUIS PAS VISIBLE LALALALA");
            rb2DPlayer.position = start.position; //retour à la case départ
            rb2DPlayer.velocity = new Vector2(0 * rb2DPlayer.velocity.x, 0 * rb2DPlayer.velocity.y);
        }

        //StartCoroutine("Respawn");
    }*/

    
}
