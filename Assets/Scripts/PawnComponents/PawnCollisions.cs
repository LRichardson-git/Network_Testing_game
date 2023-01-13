using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;

public class PawnCollisions : NetworkBehaviour
{

    public AudioClip tookdamage;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            AudioSource.PlayClipAtPoint(tookdamage, transform.position);
            GetComponent<Pawn>().RecieveDamage(20);

            //knockback, dosent really work
            Vector2 enemydir = collision.gameObject.transform.position - transform.position;
            enemydir.Normalize();
            GetComponent<Rigidbody2D>().AddForce(-enemydir * 20);
        }
    }
}
