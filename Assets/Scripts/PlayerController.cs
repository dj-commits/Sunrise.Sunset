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

    private bool isFalling;

    // Start is called before the first frame update
    void Start()
    {
        isFalling = false;
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


}