using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public delegate void PlayerDie();
    public static event PlayerDie OnPlayerDie;

    public delegate void PlayerExplosion();
    public static event PlayerExplosion OnPlayerExplote;

    private Rigidbody2D rb;
    private bool isGround;
    private bool lookRight; //Bool para mirar a los lados(esta mirando a la derecha)

    private bool hasUmbrella;
    private bool isJumping;
    private float jumpTimeCounter;
    public int jumpValue;

    public float speed;
    public float jumpForce;
    public float moveInput;
    public float jumpTime;

    public GameObject umbrella;
    public GameObject umbrellaBroken;
    public Transform feet;
    public float feetRadio;
    public LayerMask floorLayer;

    public Transform sprite;
    public Text deadText;
    public Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lookRight = true;
    }

    void Update()
    {
        if(isGround && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
            SoundManager.Instance.PlaySound(SoundType.PlayerJump, GameManager.Instance.soundVolume);
        }
        if(Input.GetKey(KeyCode.Space) && isJumping)
        {
            if(jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        Animate();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(feet.position, feetRadio, floorLayer);
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if(!lookRight && moveInput > 0)
        {
            ChangeDirectionSprite();
        }
        else if(lookRight && moveInput < 0)
        {
            ChangeDirectionSprite();
        }
    }

    void ChangeDirectionSprite()
    {
        lookRight = !lookRight;
        Vector3 localScale = sprite.localScale;
        localScale.x *= -1;
        sprite.localScale = localScale;
    }

    private void Animate()
    {
        if (Mathf.Abs(moveInput) > 0f)
            animator.SetBool("Walking", true);
        else
            animator.SetBool("Walking", false);

        animator.SetBool("Jump", isJumping);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Lava"))
        {

            deadText.text = getPhrase();
            OnPlayerDie();
        }
        else if (collision.CompareTag("Damage"))
        {
            if (!hasUmbrella)
            {
                deadText.text = getPhrase();
                OnPlayerDie();
            }
            else
            {
                Destroy(Instantiate(GameManager.Instance.explosionParticles, collision.transform.position, Quaternion.identity), 1f);
                Destroy(Instantiate(umbrellaBroken, transform.position, Quaternion.identity), 4f);
                SoundManager.Instance.PlaySound(SoundType.RockSmash, 1f);
                Destroy(collision.gameObject);
                hasUmbrella = false;
                umbrella.SetActive(false);
            }
       
        }
        else if (collision.CompareTag("Umbrella"))
        {
            GameManager.Instance.GetPowerUp();
            hasUmbrella = true;
            umbrella.SetActive(true);
            Destroy(Instantiate(GameManager.Instance.coinParticles, collision.transform.position, Quaternion.identity), 1f);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Coin"))
        {
            GameManager.Instance.UpdateScore();
            Destroy(Instantiate(GameManager.Instance.coinParticles, collision.transform.position, Quaternion.identity), 1f);
            Destroy(collision.gameObject);
        }
    }

    string getPhrase()
    {
        int rnd = Random.Range(1, 7);

        return rnd switch
        {
            1 => "Whatever its happening here\n no God can stop it",
            2 => "You have been burn,\n please try again",
            3 => "Sorry, you are dead",
            4 => "Next time, don't walk\n near a volcano",
            5 => "Good job, however,\n the princess is in another volcano",
            6 => "Your mom will not \n save you this time.",
            _ => "You died inside the volcano",
        };
    }
}
