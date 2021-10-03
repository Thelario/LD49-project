using UnityEngine;

public class FireBall : MonoBehaviour
{
    public Rigidbody2D rb;
    public float jumpForce;

    private void Start()
    {
        rb.velocity = Vector2.up * jumpForce;
    }
}
