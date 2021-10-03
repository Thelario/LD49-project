using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isGround;
    private bool lookRight; //Bool para mirar a los lados(esta mirando a la derecha)


    private bool isJumping;
    private float jumpTimeCounter;
    public int jumpValue;

    public float speed;
    public float jumpForce;
    public float moveInput;
    public float jumpTime;

    public Transform feet;
    public float feetRadio;
    public LayerMask floorLayer;

    public Transform sprite;

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
        if (collision.CompareTag("Damage"))
        {
            SceneManager.LoadScene(0);
        }
        else if (collision.CompareTag("Coin"))
        {
            GameManager.Instance.UpdateScore();
            Destroy(Instantiate(GameManager.Instance.coinParticles, collision.transform.position, Quaternion.identity), 1f);
            Destroy(collision.gameObject);
        }
    }
}
