using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerBehavior : MonoBehaviour
{
    public string[] levelName; //tableau de noms de toutes les scenes de niveau
    public int currentLevel = 0; //numéro du niveau courant

    [SerializeField] private Transform start; //point de départ
    [SerializeField] private Parallaxe parallax; //appel du script de Parallaxe
    [SerializeField] private DiscussionManager discuss; //appel du script de gestion d'affichage des boites de dialogue
    public MenuStart menuStart; //appel du script Menu start (gestion des menus, des boutons)
    public AledText aledText; //appel du script Aled Text, qui permet d'attribuer la valeur à la bonne zone de texte

    [SerializeField] private Rigidbody2D rb2DPlayer; //RIGIDBODY du joueur
    [SerializeField] public Animator animator; //élément Animator du joueur (permet de gérer les animations)
    [SerializeField] private GameObject perso; //objet du joueur
    [SerializeField] private int direction; //variable où chaque valeur correspond à une direction

    [SerializeField] private float speedMovement; //vitesse de mouvement
    [SerializeField] public float horizontalMove; //variable de mouvement en latéral
    private Vector3 velocity = Vector3.zero; //référence du mouvement nul
    private bool Walking; //booléen de détection de si le joueur touche le sol

    [SerializeField] private Transform groundCheck; //objet pour la détection du sol
    [SerializeField] private float groundCheckRay; //variable du rayon de la zone de détection du sol
    [SerializeField] private LayerMask collisionLayer; //calque pour détecter les bonnes collisions

    [SerializeField] private float speedJump; //vitesse de saut

    [SerializeField] bool getDash; //booléen de déblocage du dash
    [SerializeField] private bool isDashing; //booléen de détection du dash
    [SerializeField] private float speedDash; //vitesse du dash

    [SerializeField] bool getClimb; //booléen de déblocage de l'escalade
    private bool Climbing; //booléen de détection de si le joueur touche un mur
    [SerializeField] private Transform wallCheck; //objet pour la détection de mur
    [SerializeField] private float wallCheckRay; //variable du rayon de la zone de détection du mur
    [SerializeField] private float speedClimb; //vitesse d'escalade

    [SerializeField] public GameObject toDisable = null; //objet "à désactiver"



    private Vector3 lastPosition;

    private void Start()
    {
        perso.transform.localScale = new Vector3(-perso.transform.localScale.x, perso.transform.localScale.y, perso.transform.localScale.z); //les sprites regardent vers la gauche donc on les fait regarder vers la droite dès le début
        
        

        lastPosition = transform.localPosition;
    }

    private void Update()
    {
        //gestion de pause du jeu
        if (menuStart.Pausing == true)
        {
            Time.timeScale = 0;
        }
        else if (menuStart.Pausing == false)
        {
            Time.timeScale = 1;
        }

        
        if (toDisable && discuss.DiscussionBox.activeSelf == false) //si objet à désactiver assigné et les boites de dialogue sont désactivées
        {
            toDisable.GetComponent<CapsuleCollider2D>().enabled = false; //plus de collision avec l'objet à désactiver
        }

        if (GameObject.Find("Discussion") == null || discuss.DiscussionBox.activeSelf == false)
        {
            if (Input.GetAxis("Horizontal") > 0f)
            {
                direction = 1;
            }
            else if (Input.GetAxis("Horizontal") < 0f)
            {
                direction = 2;
            }
            else if (Input.GetAxis("Vertical") > 0f)
            {
                direction = 3;
            }
            else
            {
                direction = 0;
            }

            horizontalMove = Input.GetAxis("Horizontal") * speedMovement * Time.deltaTime;

            if (Input.GetButtonDown("Jump") && Walking == true)
            {
                animator.SetTrigger("PrepaJump");
                rb2DPlayer.velocity = Vector2.up * speedJump;
            }



            //DASH OROR
            if (getDash == true && isDashing == false && Input.GetButtonDown("Dash"))
            {
                animator.SetTrigger("PrepaDash");
                if (direction == 1)
                    {
                        rb2DPlayer.velocity = Vector2.right * speedDash * 5;
                    }
                    else if (direction == 2)
                    {
                        rb2DPlayer.velocity = Vector2.left * speedDash * 5;
                    }
                    else if (direction == 3)
                    {
                        rb2DPlayer.velocity = Vector2.up * speedDash;
                    }
                isDashing = true;
                animator.SetTrigger("EndDash");
            }
        }
        
    }

    void FixedUpdate()
    {
        Walking = Physics2D.OverlapCircle(groundCheck.position, groundCheckRay, collisionLayer);

        if (wallCheck != null)
        {
            Climbing = Physics2D.OverlapCircle(wallCheck.position, wallCheckRay, collisionLayer);
        }

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

            if (Input.GetAxis("Horizontal") > 0f && perso.transform.localScale.x > 0)
            {
                perso.transform.localScale = new Vector3(-perso.transform.localScale.x, perso.transform.localScale.y, perso.transform.localScale.z);
            }
            else if (Input.GetAxis("Horizontal") < 0f && perso.transform.localScale.x < 0)
            {
                perso.transform.localScale = new Vector3(-perso.transform.localScale.x, perso.transform.localScale.y, perso.transform.localScale.z);
            }

            if (Walking == true)
            {
                isDashing = false;
                animator.SetBool("isJumping", false);
                //Debug.Log("je veu pa soté");
            }
            else
            {
                animator.SetBool("isJumping", true);
                //Debug.Log("je saute llalalalalalala");
            }

            if (Climbing == true && getClimb == true)
            {
                //Debug.Log("je suis contre un mur");

                if (Input.GetButtonDown("Climb") || Input.GetAxis("Climb") > 0f)
                {
                    Debug.Log("je grimpe");
                rb2DPlayer.velocity = Vector2.zero;

                if (Input.GetAxis("Vertical") > 0f)
                {
                    rb2DPlayer.velocity = Vector2.up * speedClimb * Time.deltaTime;
                } else if (Input.GetAxis("Vertical") < 0f)
                {
                    rb2DPlayer.velocity = Vector2.down * speedClimb * Time.deltaTime;
                }

                }
            }
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

        if(collision.gameObject.tag == "Climb")
        {
            Debug.Log("je peux enfin climb");
            getClimb = true;
            animator.SetBool("isRunning", false);
            horizontalMove = 0;
            discuss.DiscussionBox.SetActive(true);
            toDisable = collision.gameObject;
        }

        if (collision.gameObject.tag == "Discussion")
        {
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
        Gizmos.color = Color.magenta;
        if (wallCheck != null)
        {
            Gizmos.DrawWireSphere(wallCheck.position, wallCheckRay);
        }
    }
}
