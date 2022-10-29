using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float jumpForce;
    [SerializeField] float jumpLength;
    [SerializeField] float moveSpeed;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float gravityScale = 5;
    [SerializeField] float velocity;
    [SerializeField] float distanceToCheck;
    [SerializeField] Sprite batSprite;
    [SerializeField] float _batFormTimer;
    [SerializeField] float _canBatTimer;
    private SpriteRenderer spriteRenderer;
    private Sprite vampForm; 

    private bool isFalling;
    private bool canBat;
    private bool isBat;

    // Start is called before the first frame update
    void Start()
    {
        isFalling = false;
        canBat = true;
        isBat = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        vampForm = this.GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        // Jumping: gamedevbeginner.com/how-to-jump-in-unity-with-or-without-physics/#jump_without_pyhsics_unity

        if (isFalling || !GroundCheck(distanceToCheck)) 
        {
            velocity += gravity * gravityScale * Time.deltaTime;
        } 
        
        if (GroundCheck(distanceToCheck))
        {
            velocity = 0;
            isFalling = false;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocity += jumpForce;
                isFalling = true;
            }
        }

        // Check for Bat form
        if (Input.GetKeyDown(KeyCode.LeftShift) && canBat)
        {
            canBat = false;
            BatForm();
            StartCoroutine(BatFormTimer());
            StartCoroutine(CanBatTimer());

        }
        
        // Movement

        transform.Translate(new Vector3(moveSpeed, velocity, 0) * Time.deltaTime);

    }

    private bool GroundCheck(float distanceToCheck)
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
    }

    void BatForm()
    {
        spriteRenderer.sprite = batSprite;
        isBat = true;
        velocity = 0;

    }

    IEnumerator BatFormTimer()
    {
        Debug.Log("In BatFormTimer");
        yield return new WaitForSeconds(_batFormTimer);
        isBat = false;
        spriteRenderer.sprite = vampForm;
    }

    IEnumerator CanBatTimer()
    {
        Debug.Log("In CanBatTimer");
        yield return new WaitForSeconds(_canBatTimer);
        canBat = true;

    }


}