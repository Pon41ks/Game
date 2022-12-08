using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    [SerializeField] private int performeDamage = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {                     
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 10f, ForceMode2D.Impulse);
            collision.GetComponent<cont>().RecountHp(performeDamage);
        }
       
    }
    public IEnumerator Death()
    {
        Debug.Log("s");
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Collider2D>().enabled = false;
        transform.GetChild(0).GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        Debug.Log("rr");
    }

    public void startDeath()
    {
        Debug.Log("q");
        StartCoroutine(Death());
    }

   
}