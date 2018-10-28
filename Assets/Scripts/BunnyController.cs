using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BunnyController : MonoBehaviour {

    private Rigidbody2D myRigidBody;
    private Transform myTransform;
    private Animator myAnim;
    private BoxCollider2D myCollider;
    private float startTime;

    public float bunnyJumpForce = 500f;
    private float bunnyHurtTime = -1;
    public Text scoreText;


	// Use this for initialization
	void Start () {
        myTransform = GetComponent<Transform>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myCollider = GetComponent<BoxCollider2D>();
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (bunnyHurtTime == -1)    
        {
            if (Input.GetKeyDown(KeyCode.Space))//if (Input.GetKey("Jump"))
            {
                if (myTransform.position.y <= -2.69)
                {
                    myRigidBody.AddForce(transform.up * bunnyJumpForce);
                }
            }
            myAnim.SetFloat("vVelocity", myRigidBody.velocity.y); //setting the vVelocity parameter to trigger jumping animation.
            scoreText.text = (Time.time - startTime).ToString("0.0");

        }
        else
        {
            if (Time.time > bunnyHurtTime + 2)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            foreach (PrefabSpawner spawner in FindObjectsOfType<PrefabSpawner>())
            {
                spawner.enabled = false;
            }

            foreach (MoveLeft moveLefter in FindObjectsOfType<MoveLeft>())
            {
                moveLefter.enabled = false;
            }

            bunnyHurtTime = Time.time;

            myAnim.SetBool("bunnyHurt", true); //setting the parameter True to trigger death animation

            myRigidBody.velocity = Vector2.zero;
            myRigidBody.AddForce(transform.up * bunnyJumpForce);

            myCollider.enabled = false;
            
        }
        
    }
}


