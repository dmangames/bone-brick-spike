using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blob : MonoBehaviour
{

    public Animator anim;
    public Slider s;
    public Rigidbody2D rb;

    private void OnEnable()
    {
        GameManager.Instance.gameStartEvent += SetHealthToFull;
        GameManager.Instance.gameOverEvent += StopRunning;
    }

    private void OnDisable()
    {
        GameManager.Instance.gameStartEvent -= SetHealthToFull;
        GameManager.Instance.gameOverEvent -= StopRunning;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("jump", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isPlaying)
        {
            s.value -= Time.deltaTime * 5f;
            if (s.value <= 0)
                GameManager.Instance.PlayerDied();
        }
        
    }

    void SetHealthToFull()
    {
        s.value = 100;
        anim.SetBool("jump", true);
    }

    void StopRunning()
    {
        anim.SetBool("jump", false);
    }

    public void Jump()
    {
        rb.AddForce(Vector2.up * 700);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "spike")
        {
            s.value -= 49;
            Destroy(collision.gameObject);
            if (s.value <= 0)
            {
                //game is over
                GameManager.Instance.PlayerDied();
            }
        }
        if(collision.gameObject.tag == "bomb")
        {
            s.value -= 9;
            Jump();
            Destroy(collision.gameObject);
            if (s.value <= 0)
            {
                //game is over
                GameManager.Instance.PlayerDied();
            }
        }
        if(collision.gameObject.tag == "treat")
        {
            s.value += 25;
            Destroy(collision.gameObject);
        }
    }
}
