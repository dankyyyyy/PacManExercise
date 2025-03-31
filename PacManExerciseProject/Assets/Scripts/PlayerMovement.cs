using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 nextMoveTarget;
    private Rigidbody2D playerRigidBody;
    private Animator playerAnimator;
    private Tilemap levelTilemap;
    // public TileBase wallTile;

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();

        levelTilemap = Object.FindFirstObjectByType<Tilemap>();
        if (levelTilemap == null)
        {
            Debug.LogError("Tilemap not found in the scene!");
        }
        nextMoveTarget = levelTilemap.WorldToCell(new Vector3(0.5f, 0.5f, 0f));
        transform.position = nextMoveTarget;

        //initiate animator here when feature is implemented
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3Int intendedDirection = Vector3Int.zero;

        if (horizontalInput > 0) intendedDirection = Vector3Int.right;
        if (horizontalInput < 0) intendedDirection = Vector3Int.left;
        if (verticalInput > 0) intendedDirection = Vector3Int.up;
        if (verticalInput < 0) intendedDirection = Vector3Int.down;

        if (Vector3.Distance(transform.position, nextMoveTarget) < 0.01f)
        {
            Vector3Int targetCell = (Vector3Int)levelTilemap.WorldToCell(nextMoveTarget + (Vector3)intendedDirection);
            if (IsCellWalkable(targetCell))
            {
                moveDirection = (Vector3)intendedDirection;
                nextMoveTarget = (Vector3)targetCell + new Vector3(0.5f, 0.5f, 0f);
            }
            else if (IsCellWalkable((Vector3Int)levelTilemap.WorldToCell(nextMoveTarget + moveDirection)))
            {
                nextMoveTarget = (Vector3)levelTilemap.WorldToCell(nextMoveTarget + moveDirection) + new Vector3(0.5f, 0.5f);
            }
            else
            {
                moveDirection = Vector3.zero;
            }
        }

        //animator parameters to be set here, based on direction   
    }

    void FixedUpdate()
    {
        if (moveDirection != Vector3.zero)
        {
            playerRigidBody.MovePosition(Vector3.MoveTowards(transform.position, nextMoveTarget, moveSpeed * Time.fixedDeltaTime));
        }
        else
        {
            playerRigidBody.linearVelocity = Vector3.zero;
        }
    }

    private bool IsCellWalkable(Vector3Int cellPosition)
    {
        if (levelTilemap == null) return false;
        TileBase tile = levelTilemap.GetTile(cellPosition);
        return tile == null;
    }
}
