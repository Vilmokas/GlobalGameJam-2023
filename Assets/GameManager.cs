using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float rootSpeed = 5f;
    public List<Root> roots;
    public int currentRoot = 0;
    public bool moveRoot = false;
    public int currentSeed = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveRoot();
    }

    void MoveRoot()
    {
        if (moveRoot)
        {
            float right = Input.GetAxisRaw("Horizontal");
            float up = Input.GetAxisRaw("Vertical");

            if (right != 0 || up != 0)
            {
                roots[currentRoot].SetDirection(right, up);
            }
        }
        else
        {
            roots[currentRoot].SetDirection(0, 0);
        }
    }

    public void SelectSeed(int seedId)
    {
        currentSeed = seedId;
    }
}
