using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float jumpForce;
    [SerializeField] float moveSpeed;
    [SerializeField] float vertSpeed;
    [SerializeField] float vertSpeedMultiplier;
    [SerializeField] float gravityScale;
    [SerializeField] float fallingGravityScale;
    [SerializeField] Sprite batSprite;
    [SerializeField] float _batFormTimer;
    [SerializeField] float _canBatTimer;
    private SpriteRenderer spriteRenderer;
    private Sprite vampForm;
    private Rigidbody2D playerRB;
    private bool canBat;
    private bool isBat;

    // Start is called before the first frame update
    void Start()
    {

        canBat = true;
        isBat = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerRB = GetComponent<Rigidbody2D>();

        vampForm = this.GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        // Jumping: gamedevbeginner.com/how-to-jump-in-unity-with-or-without-physics/#jump_without_pyhsics_unity
        if (isBat)
        {
            playerRB.velocity = Vector2.zero;
            playerRB.isKinematic = true;
            vertSpeed = Input.GetAxis("Vertical") * vertSpeedMultiplier;

        }
        else if (playerRB.velocity.y >= 0)
            vertSpeed = 0;
            playerRB.gravityScale = gravityScale;

            if (Input.GetKeyDown(KeyCode.Space))
            {
            playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

        else 
        {
            playerRB.gravityScale = fallingGravityScale;
        }



        // Check for Bat form
        if (Input.GetKeyDown(KeyCode.LeftShift) && canBat)
        {
            isBat = true;
            canBat = false;

            StartCoroutine(BatFormTimer());
            StartCoroutine(CanBatTimer());

        }

        // Movement

        //transform.Translate(new Vector3(moveSpeed, vertSpeed, 0) * Time.deltaTime);

        //this.transform.position += this.transform.right * moveSpeed * Time.deltaTime;
        transform.position += new Vector3(moveSpeed, vertSpeed, 0) * Time.deltaTime;
    }

    /*private bool GroundCheck(float distanceToCheck)
    {
        bool isGrounded = false;
        RaycastHit2D[] _allHits;
        _allHits = Physics2D.RaycastAll(transform.position, Vector2.down, distanceToCheck);
        foreach (var hit in _allHits)
        {
            if (hit.transform.tag.Equals("Ground"))
            {
                isGrounded = true;
            }
        }

        return isGrounded;
    }*/

    IEnumerator BatFormTimer()
    {
        spriteRenderer.sprite = batSprite;
        Debug.Log("In BatFormTimer");
        yield return new WaitForSeconds(_batFormTimer);
        isBat = false;
        spriteRenderer.sprite = vampForm;
        playerRB.isKinematic = false;
    }

    IEnumerator CanBatTimer()
    {
        Debug.Log("In CanBatTimer");
        yield return new WaitForSeconds(_canBatTimer);
        canBat = true;

    }


}