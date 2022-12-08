using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class cont : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float jumpHeight;
    public Transform groundCheck;
    bool isGrounded;
    Animator anim;
    public int curHP;
    public int maxHP;
    bool isHit = false;
    public main main;


    // Start is called before the first frame update
    void Start()
    {
        curHP = maxHP;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetAxis("Horizontal") == 0 && (isGrounded))
        {
            anim.SetInteger("State", 1);

        }
        else
        {
            Flip();
            if (isGrounded)
                anim.SetInteger("state", 2);
        }
        CheckGround();
        
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
            rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
        //rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
    }
    void FixedUpdate()
    {

        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
    }

    // переворот во время ходьбы
    void Flip()
    {
        if (Input.GetAxis("Horizontal") > 0)
            transform.localRotation = Quaternion.Euler(0,0,0);
        if (Input.GetAxis("Horizontal") < 0)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
    }
    void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f);
        isGrounded = colliders.Length > 1;
        if (!isGrounded)
            anim.SetInteger("State", 3);
    }
    public void RecountHp(int deltaHP)
    {
        
        curHP = curHP + -deltaHP;
        if (-deltaHP <= 0)
        {
            StartCoroutine(OnHit());
            
            isHit = true;
            StopCoroutine(OnHit());
        }
        if (curHP <= 0)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            Invoke("Lose", 2f);
        }
    }
    IEnumerator OnHit()
    {
        if (isHit)
            GetComponent<SpriteRenderer>().color = new Color(1f, GetComponent<SpriteRenderer>().color.g - 0.04f, GetComponent<SpriteRenderer>().color.b - 0.04f);
        else
            GetComponent<SpriteRenderer>().color = new Color(1f, GetComponent<SpriteRenderer>().color.g + 0.04f, GetComponent<SpriteRenderer>().color.b + 0.04f);

        if (GetComponent<SpriteRenderer>().color.g == 1f)
            StopCoroutine(OnHit());
        
        if (GetComponent<SpriteRenderer>().color.g <= 0)
            isHit = false;
        yield return new WaitForSeconds(0.02f);
        StartCoroutine(OnHit());
    }

    void Lose()
    {
        main.GetComponent<main>().Lose();
    }

    
}
