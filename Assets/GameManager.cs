using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float rootSpeed = 5f;
    public Root root = null;
    public bool moveRoot = false;
    public int currentSeed = 0;
    public List<GameObject> plantPrefabs;
    public bool plantSeed = false;
    public float rootGrowWait = 3f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveRoot();
        PlantSeed();
    }

    void MoveRoot()
    {
        if (root != null)
        {
            if (moveRoot)
            {
                float right = Input.GetAxisRaw("Horizontal");
                float up = Input.GetAxisRaw("Vertical");

                if (right != 0 || up != 0)
                {
                    root.SetDirection(right, up);
                }
            }
            else
            {
                root.SetDirection(0, 0);
            }
        }
    }

    public void SelectSeed(int seedId)
    {
        currentSeed = seedId;
        plantSeed = true;
    }

    void PlantSeed()
    {
        if (plantSeed)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                GameObject plant = GameObject.Instantiate(plantPrefabs[currentSeed], mousePosition, Quaternion.identity);
                root = plant.GetComponentInChildren<Root>();
                plantSeed = false;
                StartCoroutine(StartRootGrow());
            }
        }
    }

    IEnumerator StartRootGrow()
    {
        yield return new WaitForSeconds(rootGrowWait);
        moveRoot = true;
    }
}
