using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpSpeed = 12f;
    public float moveSpeed = 5f;

    private Rigidbody2D myRigidbody;

    private bool isBlockedByWall;

    private Vector2 currentMoveDirection;

    [Header("-- Private references --")]
    [SerializeField] private int coinsCollected = 0;
    [SerializeField] private GameObject winningFlag;

    // Start is called before the first frame update
    public void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        isBlockedByWall = false;

        currentMoveDirection = Vector2.zero;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentMoveDirection = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentMoveDirection = Vector2.right;
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentMoveDirection = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentMoveDirection = Vector2.down;
        }
        // You might want to add a way to stop movement, for example by pressing a specific key
        else if (Input.GetKeyDown(KeyCode.Space)) // Example: Stop movement with spacebar
        {
            currentMoveDirection = Vector2.zero;
        }

        // Apply movement if there's a direction and not blocked by a wall
        if (currentMoveDirection != Vector2.zero && !isBlockedByWall)
        {
            transform.Translate(currentMoveDirection * moveSpeed * Time.deltaTime);
        }
    }
    public void SetBlockedByWall(bool blocked)
    {
        isBlockedByWall = blocked;
        if (blocked)
        {
            currentMoveDirection = Vector2.zero; // Optionally stop movement on collision
        }
    }

    
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag.Equals("Coin"))
        {
            coinsCollected += 1;
            Destroy(coll.gameObject);
        }

        if (coinsCollected == 3)
        {
            Instantiate(winningFlag);
        }

        if (coll.gameObject.tag.Equals("Wall"))
        {
            SetBlockedByWall(true);
            SetBlockedByWall(false);
        }
    }

    // OnCollisionEnter2D is called once, when two GameObjects with Collider2Ds hit each other
    // - One GameObject must have a Rigidbody2D as well
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag.Equals("Finish"))
        {
            print("You have won the game!");
        }
    }

    // private void OnTriggerExit2D(Collider2D coll)
    // {
    //     if (coll.gameObject.tag.Equals("Wall"))
    //     {
            
    //     }
    // }
}
