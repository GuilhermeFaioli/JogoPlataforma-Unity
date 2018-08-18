using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float velocidadeMaxima;
    public float forcaPulo;

    public int vidas;
    public int rings;

    public Text textoVidas;
    public Text textoRings;

    public bool isGrounded;
    public bool canFly;

    public bool inWater;

    public GameObject lastCheckpoint;

    public GameObject Joystick1;
    public bool cliqueiBotao = false;
    public bool cliqueiAtack = false;


    // Use this for initialization
    void Start() {
        textoVidas.text = vidas.ToString();
        textoRings.text = rings.ToString();

    }

    // Update is called once per frame
    void Update() {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        // float movimento = Input.GetAxis("Horizontal");
        float movimento = Joystick1.GetComponent<FixedJoystick>().Horizontal;
        rigidbody.velocity = new Vector2(velocidadeMaxima * movimento, rigidbody.velocity.y);
        if (movimento < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        } else if (movimento > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (movimento > 0 || movimento < 0)
        {
            GetComponent<Animator>().SetBool("walking", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("walking", false);
        }
        if (!inWater) {
            
            //if (Input.GetKeyDown(KeyCode.Space))
            if(cliqueiBotao == true || Input.GetKeyDown(KeyCode.Space))
            {
                if (isGrounded)
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, forcaPulo));
                    GetComponent<AudioSource>().Play();
                    canFly = false;
                }
                else
                {
                    canFly = true;
                }
                cliqueiBotao = false;
            }

            if (canFly && Input.GetKey(KeyCode.Space))
            {
                GetComponent<Animator>().SetBool("flying", true);
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, -0.5f);
            }
            else
            {
                GetComponent<Animator>().SetBool("flying", false);
            }

            if (isGrounded)
            {
                GetComponent<Animator>().SetBool("jumping", false);
            } else
            {
                GetComponent<Animator>().SetBool("jumping", true);
            }
            
        }
        else
        {
            //if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            if(Joystick1.GetComponent<FixedJoystick>().Vertical>0)
            {
                rigidbody.AddForce(new Vector2(0, 6f * Time.deltaTime), ForceMode2D.Impulse);
            }

            //if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            if(Joystick1.GetComponent<FixedJoystick>().Vertical<0)
            {
                rigidbody.AddForce(new Vector2(0, -6f * Time.deltaTime), ForceMode2D.Impulse);
            }

            rigidbody.AddForce(new Vector2(0, 10f * Time.deltaTime), ForceMode2D.Impulse);
        }

        GetComponent<Animator>().SetBool("swimming", inWater);
        if (Input.GetKeyDown(KeyCode.K) || cliqueiAtack)
        {
            GetComponent<Animator>().SetTrigger("hammering");
            Collider2D[] colliders = new Collider2D[3];
            transform.Find("HammerSquare").gameObject.GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D(), colliders);
            for (int i=0; i < colliders.Length; i++)
            {
                if (colliders[i] != null && colliders[i].gameObject.CompareTag("Monstro"))
                {
                    Destroy(colliders[i].gameObject);
                }
            }
            cliqueiAtack = false;
        }
    }

    public void cliqBotao(bool cliq)
    {
        cliqueiBotao = true;
    }

    public void cliqAtack(bool cliq)
    {
        cliqueiAtack = true;
    }

private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Agua"))
        {
            inWater = true;
        }

        if (collision.gameObject.CompareTag("Moedas"))
        {
            Destroy(collision.gameObject);
            rings++;
            textoRings.text = rings.ToString();
        }

        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            lastCheckpoint = collision.gameObject;
        }

        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Agua"))
        {
            inWater = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Plataformas"))
        {
            isGrounded = true;
            canFly = false;
        }

        if (collision.gameObject.CompareTag("Trampolim"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 10f);
        }

        if (collision.gameObject.CompareTag("Monstro"))
        {
            vidas--;
            textoVidas.text = vidas.ToString();
            if (vidas == 0)
            {
                transform.position = lastCheckpoint.transform.position;
                vidas = 1;
                textoVidas.text = vidas.ToString();
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Plataformas"))
        {
            isGrounded = false;
        }
        
    }

}
