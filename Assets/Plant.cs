using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public List<Sprite> sprites;
    public SpriteRenderer sprite;
    public int currentGrowthStage = 0;

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
                // TODO: spawn same seed
            }
            if (currentGrowthStage == 2)
            {
                // TODO: spawn bigger seed
            }
            if (currentGrowthStage == 3)
            {
                // TODO: stop moving plant
            }
        }
    }
}
