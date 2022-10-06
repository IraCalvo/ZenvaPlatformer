using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int pointValue;
    private Rigidbody2D body;
    [SerializeField]
    private float speed;
    private Vector2 movementDirection;

    // Awake is done a lil before start, ensures that certain things start before the start
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        //movementDirection = Vector2.right;
    }

    // Update is called once per frame
    void Update()
    {
        Move(movementDirection);
    }

    public void Move(Vector2 direction)
    {
        movementDirection = direction;
        body.velocity = new Vector2(movementDirection.x * speed, body.velocity.y);
    }

    private void OnCollision2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            movementDirection *= -1f;
        }
    }

    

}
