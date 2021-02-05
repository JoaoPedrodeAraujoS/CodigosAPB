using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public float velproj;
    public Transform armaParado;
    public GameObject ProjetilPrefab;
    private Rigidbody2D rig;
    public bool Pular;
    private Animator anim;
    public bool direita = true;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Atirar();
    }

    public void Move(){
        //ector3 moviment = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        //transform.position += moviment * Time.deltaTime * Speed;

        float mov = Input.GetAxis("Horizontal");
        rig.velocity = new Vector2(mov * Speed, rig.velocity.y);

        if(mov > 0f){
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f,0f,0f);
            direita = true;
        }
        if(mov < 0f){
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f,180f,0f);
            direita = false;
        }
        if(mov == 0f){
            anim.SetBool("walk", false);
        }
    }

    public void Jump(){
        if(Input.GetButtonDown("Jump") && !Pular){
            rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            anim.SetBool("jump", true);
        }
    }

    public void Atirar()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Transform shotPoint;
            shotPoint = armaParado;
            GameObject projectile = Instantiate(ProjetilPrefab, shotPoint.position, transform.rotation);

            if(direita)
            {
                projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(velproj, 0);
            }else{
                projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(-velproj, 0); 
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D c){
        if(c.gameObject.layer == 8){
            Pular = false;
            anim.SetBool("jump", false);
        }
       if(c.gameObject.tag == "Spike"){
           GameController.instance.ShowGameOver();
           Destroy(gameObject); 
        }
    }

    public void OnCollisionExit2D(Collision2D c){
        if(c.gameObject.layer == 8){
            Pular = true;
        }
    }
}