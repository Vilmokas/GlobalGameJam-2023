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
        // TODO: add sprite change and growth limit
    }
}
