
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float LifeTime;

    public GameObject DestroyEffect;

    private void Start()
    {
        Invoke("destroySelf", LifeTime);
    }

    private void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
    }

    void destroySelf()
    {
        //instatntiate an effect
        Destroy(gameObject);
    }
}
