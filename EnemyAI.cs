using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public int energia;
    public float speed;

    public Transform rightCol;
    public Transform leftCol;

    public bool colliding;    
    private Rigidbody2D rig;
    private Animator anim;

    public LayerMask layer;

    public BoxCollider2D boxCollider2D;

    //public bool playerDestroyed;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rig.velocity = new Vector2(speed, rig.velocity.y);
        colliding = Physics2D.Linecast(rightCol.position, leftCol.position, layer);

        if(colliding)
        {
            transform.localScale =  new Vector2(transform.localScale.x * -1f, transform.localScale.y);
            speed *= -1f;
        }

        if(energia <= 0)
        {
            speed = 0;
            anim.SetTrigger("die");
            boxCollider2D.enabled = false;
            rig.bodyType = RigidbodyType2D.Kinematic;
            Destroy(gameObject, .5f);
        }
    }

    public void TakeDamege(int dano)
    {
        energia -= dano;
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if(c.gameObject.tag == "Player")
        {
            GameController.instance.ShowGameOver();
            Destroy(c.gameObject);
        }
    }    
}
