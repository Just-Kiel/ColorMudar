using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;

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


    [SerializeField] private AudioSource playerAudioSource;
    public Sound[] sounds;

    private Vector3 lastPosition;

    [SerializeField] bool getFly;
    [SerializeField] private GameObject cape = null;

    private void Start()
    {
        perso = GameObject.Find(MenuStart.currentPlayer);

        animator = perso.GetComponent<Animator>();

        perso.transform.localScale = new Vector3(-perso.transform.localScale.x, perso.transform.localScale.y, perso.transform.localScale.z); //les sprites regardent vers la gauche donc on les fait regarder vers la droite dès le début

        

        lastPosition = transform.localPosition;
    }

    private void Update()
    {
        playerAudioSource = AudioManager.instance.soundStream;

        foreach (Sound s in sounds)
        {
            playerAudioSource.volume = s.volume;
        }
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
            toDisable.GetComponent<Collider2D>().enabled = false; //plus de collision avec l'objet à désactiver
        }

        if (GameObject.Find("Discussion") == null || discuss.DiscussionBox.activeSelf == false) //si il n'y a pas de boite de diaogues ou elle n'est pas activée
        {
            //setup des diverses directions
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

            //calcul de la variable de déplacement latéral
            horizontalMove = Input.GetAxis("Horizontal") * speedMovement * Time.deltaTime;

            if (Input.GetButtonDown("Jump") && Walking == true) //si bouton de saut activé et on touche le sol
            {
                animator.SetTrigger("PrepaJump"); //déclenchement de l'animation de préparation du saut
                rb2DPlayer.velocity = Vector2.up * speedJump; //déplacement du joueur vers le haut
            }

            //DASH OROR
            if (getDash == true && isDashing == false && Input.GetButtonDown("Dash")) //si on a débloqué le dash, que le joueur ne dash pas, et que le bouton du dash est activé
            {
                rb2DPlayer.gravityScale = 0;
                animator.SetTrigger("PrepaDash"); //déclenchement de l'animation de préparation du dash

                //en fonction de la direction, on dash pas dans la meme direction
                if (direction == 1)
                {
                    rb2DPlayer.velocity = Vector2.right * speedDash * 15;
                }
                else if (direction == 2)
                {
                    rb2DPlayer.velocity = Vector2.left * speedDash * 15;
                }
                else if (direction == 3)
                {
                    rb2DPlayer.velocity = Vector2.up * speedDash;
                }
                isDashing = true; //impossible de re dash
                animator.SetTrigger("EndDash");//déclenchement de l'animation de fin du dash
                rb2DPlayer.gravityScale = 15;
            }
        }

        if (animator.GetBool("isRunning"))
        {
            Debug.Log("ptdr je cours");
            PlaySound("Walk");
        } else if (animator.GetBool("isJumping"))
        {
            Debug.Log("début saut");
            PlaySound("PrepaJump");
        } else if (!animator.GetBool("isJumping"))
        {
            Debug.Log("fin saut");
            PlaySound("EndJump");
        } else
        {
            playerAudioSource.Stop();
        }

        //StartCoroutine(Falling(cape));
    }

    void FixedUpdate()
    {
        //calcul du booléen de détection du sol
        Walking = Physics2D.OverlapCircle(groundCheck.position, groundCheckRay, collisionLayer);

        if (wallCheck != null) //si il y a un objet qui peut détecter les murs
        {
            //calcul du booléen de détection des murs
            Climbing = Physics2D.OverlapCircle(wallCheck.position, wallCheckRay, collisionLayer);
        }

        if (getFly == true && cape != null)
        {
            if (Input.GetButton("Fly") || Input.GetAxis("Fly") > 0f)
            {
                animator.SetTrigger("PrepaFly");
                //Debug.Log("koukou");
                rb2DPlayer.gravityScale = 1;
                cape.SetActive(true);
                Vector3 targetSpeed = new Vector2(horizontalMove / 3, rb2DPlayer.velocity.y);
                animator.SetBool("isFlying", true);
                rb2DPlayer.velocity = Vector3.SmoothDamp(rb2DPlayer.velocity, targetSpeed, ref velocity, 0.05f);
            }
            else
            {
                animator.SetBool("isFlying", false);
                MovePlayer(horizontalMove); //appel de la fonction Move Player
                cape.SetActive(false);
            }
        }
        if (cape == null || getFly == false)
        {
            MovePlayer(horizontalMove); //appel de la fonction Move Player
        }

        if (parallax != null) //si il y a une parallaxe active
        {
            parallax.UpdateParallax(transform.localPosition.x - lastPosition.x); //mouvement de la parallaxe
        }



        lastPosition = transform.localPosition;
    }

    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetSpeed = new Vector2(_horizontalMovement, rb2DPlayer.velocity.y); //calcul de la vitesse en fonction du déplacement courant
        rb2DPlayer.velocity = Vector3.SmoothDamp(rb2DPlayer.velocity, targetSpeed, ref velocity, 0.05f); //déplacement du personnage de manière fluide
        rb2DPlayer.gravityScale = 15; //gravité par défaut du personnage
        

        //si le personnage est en mouvement latéral, on active l'animation de course
        if (horizontalMove != 0 && Walking == true)
        {
            animator.SetBool("isRunning", true);
        } else
        {
            animator.SetBool("isRunning", false);
        }

        //en fonction de la direction, le personnage se tourne dans le bon sens pour avancer
        if (Input.GetAxis("Horizontal") > 0f && perso.transform.localScale.x > 0)
        {
            perso.transform.localScale = new Vector3(-perso.transform.localScale.x, perso.transform.localScale.y, perso.transform.localScale.z);
        }
        else if (Input.GetAxis("Horizontal") < 0f && perso.transform.localScale.x < 0)
        {
            perso.transform.localScale = new Vector3(-perso.transform.localScale.x, perso.transform.localScale.y, perso.transform.localScale.z);
        }


        if (Walking == true) //si le joueur est en contact avec le sol
        {
            isDashing = false; //le joueur n'est pas en train de dash
            animator.SetBool("isJumping", false); //le personnage ne saute pas
            
            //Debug.Log("Saut, dash et vol non actifs");
        }
        else
        {
            animator.SetBool("isJumping", true);
            targetSpeed = new Vector2(_horizontalMovement / 5, rb2DPlayer.velocity.y);
            rb2DPlayer.velocity = Vector3.SmoothDamp(rb2DPlayer.velocity, targetSpeed, ref velocity, 0.05f);
            //Debug.Log("Saut actif");
        }


        if (Climbing == true && getClimb == true) //si l'escalade est débloquée et détection d'un mur
        {
            //Debug.Log("Détection d'un mur");
            if (Input.GetButtonDown("Climb") || Input.GetAxis("Climb") > 0f) //si bouton activé ou gachette enclenchée pour l'escalade
            {
                animator.SetBool("isJumping", false); //le personnage ne saute pas
                //Debug.Log("Escalade en cours");
                rb2DPlayer.velocity = Vector2.zero; //mouvement immobile par défaut
                animator.SetBool("isWaiting", true);

                //en fonction de la direction verticale active du joueur, montée ou descente le long du mur
                if (Input.GetAxis("Vertical") > 0f)
                {
                    animator.SetBool("isWaiting", false);
                    animator.SetBool("isClimbing", true);
                    rb2DPlayer.velocity = Vector2.up * speedClimb * Time.deltaTime;
                }
                else if (Input.GetAxis("Vertical") < 0f)
                {
                    animator.SetBool("isWaiting", false);
                    animator.SetBool("isClimbing", true);
                    rb2DPlayer.velocity = Vector2.down * speedClimb * Time.deltaTime;
                }
            }
            //animator.SetBool("isClimbing", false);
        }

        if(Climbing == false)
        {
            animator.SetBool("isClimbing", false);
            animator.SetBool("isWaiting", false);
        }

        
    }

    //détection des collisions
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

        if(collision.gameObject.tag == "Fly")
        {
            Debug.Log("j'ai la cape pour voler");
            getFly = true;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Geyser" && cape.activeSelf == true)
        {
            Debug.Log("flute");
            rb2DPlayer.velocity = Vector2.up * 80;
        }
    }

    //fonction pour lancer le bon son
    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        playerAudioSource.clip = s.clip;
        playerAudioSource.Play();
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
