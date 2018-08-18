using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoHorizontal : MonoBehaviour {

    private bool colide = false;

    private float move = -2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody2D>().velocity = new Vector2(move, GetComponent<Rigidbody2D>().velocity.y);
        if (colide)
        {
            flip();
        }
	}

    private void flip()
    {
        move *= -1;
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        colide = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Plataformas"))
        {
            colide = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Plataformas"))
        {
            colide = false;
        }
    }
}
