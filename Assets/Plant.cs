using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public List<Sprite> sprites;
    public SpriteRenderer sprite;
    public int currentGrowthStage = 0;
    public int seedId;
    public int seedDropped;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GrowPlant()
    {
        currentGrowthStage++;
        if (currentGrowthStage < 3)
        {
            sprite.sprite = sprites[currentGrowthStage];
            if (currentGrowthStage == 1)
            {
                if (seedId != 0)
                {
                    seedDropped = seedId - 1;
                    GameObject.Find("GameManager").GetComponent<GameManager>().SpawnSeed(seedDropped);
                }
            }
            if (currentGrowthStage == 2)
            {
                if (seedId != 4)
                {
                    seedDropped = seedId;
                    GameObject.Find("GameManager").GetComponent<GameManager>().SpawnSeed(seedDropped);
                }
            }
            if (currentGrowthStage == 3)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().DestroyNutrient();
                GameObject.Find("GameManager").GetComponent<GameManager>().DestroySeed();
                GameObject.Find("GameManager").GetComponent<GameManager>().moveRoot = false;
            }
        }
    }
}
