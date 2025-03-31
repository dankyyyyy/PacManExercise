using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Player ActivePlayer;
    [Header("-- Private references --")]
    [SerializeField] private int coinsCollected = 0;
    [SerializeField] private GameObject winningFlag;

    void Start()
    {
        ActivePlayer = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag.Equals("Coin"))
        {
            coinsCollected += 1;
            Destroy(coll.gameObject);
        }

    // void OnTriggerEnter2D(Collider2D coll)
    // {
    //     if (coll.gameObject.tag.Equals("Coin"))
    //     {
    //         coinsCollected += 1;
    //         Destroy(coll.gameObject);
    //     }

    //     if (coinsCollected == 3)
    //     {
    //         Instantiate(winningFlag);
    //     }
    // }

    // // OnCollisionEnter2D is called once, when two GameObjects with Collider2Ds hit each other
    // // - One GameObject must have a Rigidbody2D as well
    // void OnCollisionEnter2D(Collision2D coll)
    // {
    //     if (coll.gameObject.tag.Equals("Finish"))
    //     {
    //         print("You have won the game!");
    //     }
    // }
        if (coinsCollected == 3)
        {
            Instantiate(winningFlag);
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
}
