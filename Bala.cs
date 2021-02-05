using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float vida;
    public int dano;
    public float distancia;
    public LayerMask layerInimigo;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestruirBala", vida);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.forward, distancia, layerInimigo);

        if(hitInfo.collider != null)
        {
            if(hitInfo.collider.CompareTag("Inimigo"))
            {
                hitInfo.collider.GetComponent<EnemyAI>().TakeDamege(dano);
            }else if(hitInfo.collider.CompareTag("Lula")){
                 hitInfo.collider.GetComponent<LulaAI>().TakeDamege(dano);
            }
            DestruirBala();
        }
    }
    void DestruirBala(){
        Destroy(gameObject);
    }
}
