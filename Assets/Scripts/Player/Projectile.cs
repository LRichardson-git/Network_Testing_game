using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    public int bounces = 3;
    Vector2 dir;
    Rigidbody2D rb;
    public int damage = 50;
    private void Start()
    {
       // dir = transform.right;
        Physics2D.IgnoreLayerCollision(6, 7, true);
        // GetComponent<Rigidbody2D>().constraints.
        Invoke("destroySelf", 2f);
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * 0.05f);


    }

    private void Update()
    {
        dir = rb.velocity;

    }
    // Check for wall collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            // Reflect the projectile
            var speed = dir.magnitude;

            //allowed projtiles to bounce
            /*
            var Direct = Vector2.Reflect(dir.normalized, collision);
             Destroy projectile if reached max bounces
            bounces--;
           rb.velocity = Direct * Mathf.Max(speed, 0f);
            */


            if (collision.gameObject.CompareTag("Enemy"))
                Hit(collision.gameObject);
        }
    }
    
    public static Vector2 ReflectUnclamped(Vector2 inDirection, Vector2 inNormal)
    {
        return -2 * Vector2.Dot(inNormal, inDirection) * inNormal + inDirection;
    }

    
    //hit enemey
    void Hit(GameObject collision)
    {
       // Debug.Log("test");
        collision.GetComponent<Enemy>().RecieveDamage(damage);
        
        Destroy(gameObject);

    }
    


    // Play animation that faces in the opposite direction of the forward vector
    void DeathAnimation()
    {

        transform.rotation = Quaternion.LookRotation(dir * -1); 
        // Play animation
        GetComponent<Animator>().Play("DeathAnimation");
    }

    void destroySelf()
    {
        //instatntiate an effect
        Destroy(gameObject);
    }
}





