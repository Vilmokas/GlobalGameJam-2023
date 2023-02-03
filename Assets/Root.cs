using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    public float speed = 5f;
    public float up = 0;
    public float right = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (up < 0)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
        if (up > 0)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        if (right < 0)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (right > 0)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }

    public void SetDirection(float r, float u)
    {
        if (right + r != 0)
        {
            right = r;
        }
        if (up + u != 0)
        {
            up = u;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision enter...");
        if (collision.gameObject.CompareTag("obstacle"))
        {
            Debug.Log("...obstacle");
            StopRoot();
        }
    }

    void StopRoot()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().moveRoot = false;
    }
}
