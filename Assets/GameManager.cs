using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float rootSpeed = 5f;
    public Root root = null;
    public bool moveRoot = false;
    public int currentSeed = 0;
    public List<GameObject> plantPrefabs;
    public bool plantSeed = false;
    public float rootGrowWait = 3f;
    public GameObject nutrientPrefab;
    public float nutrientSpawnHeight = 4f;
    public float nutrientSpawnWidth = 8f;
    public GameObject nutrient;
    public GameObject seedPrefab;
    public List<Sprite> seedSprites;
    public GameObject seed;
    public int[] seedCount;
    public List<TMP_Text> seedText;
    public List<Button> seedButtons;
    public GameObject ghostPlant;
    public GameObject growStartText;
    public int rootNum = 0;
    public List<AudioClip> sounds;

    // Start is called before the first frame update
    void Start()
    {
        UpdateSeedCountText();
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
        if (seedId != 0)
        {
            if (seedCount[seedId - 1] != 0)
            {
                seedCount[seedId - 1]--;
                UpdateSeedCountText();
            }
            else
            {
                return;
            }
        }
        currentSeed = seedId;
        plantSeed = true;
        EnableGhostPlant(true);
        PlaySound(0);
    }

    void PlantSeed()
    {
        if (plantSeed)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            UpdateGhostPlantPosition(mousePosition.x);

            if (Input.GetMouseButtonDown(0))
            {
                GameObject plant = GameObject.Instantiate(plantPrefabs[currentSeed], ghostPlant.transform.position, Quaternion.identity);
                root = plant.GetComponentInChildren<Root>();
                rootNum++;
                root.GetComponentInChildren<TrailRenderer>().sortingOrder = rootNum;
                plantSeed = false;
                EnableGhostPlant(false);
                StartCoroutine(StartRootGrow());
                PlaySound(5);
            }
        }
    }

    IEnumerator StartRootGrow()
    {
        SpawnNutrient();
        UpdateStartGrowText(true, "3");
        yield return new WaitForSeconds(rootGrowWait);
        UpdateStartGrowText(true, "2");
        yield return new WaitForSeconds(rootGrowWait);
        UpdateStartGrowText(true, "1");
        yield return new WaitForSeconds(rootGrowWait);
        UpdateStartGrowText(true, "GROW!");
        yield return new WaitForSeconds(0.5f);
        UpdateStartGrowText(false, "not active");
        moveRoot = true;
        root.SetDirection(0, -1);
        GetComponent<AudioSource>().Play();
    }

    void UpdateStartGrowText(bool enable, string text)
    {
        growStartText.SetActive(enable);
        growStartText.GetComponent<TMP_Text>().SetText(text);
    }

    public void SpawnNutrient()
    {
        if (nutrient != null)
        {
            DestroyNutrient();
        }
        float x = Random.Range(-nutrientSpawnWidth, nutrientSpawnWidth);
        float y = Random.Range(-nutrientSpawnHeight, 0);

        nutrient = GameObject.Instantiate(nutrientPrefab, new Vector3(x, y, 0), Quaternion.identity);
    }

    public void SpawnSeed(int seedId)
    {
        if (seed != null)
        {
            DestroySeed();
        }
        float x = Random.Range(-nutrientSpawnWidth, nutrientSpawnWidth);
        float y = Random.Range(-nutrientSpawnHeight, 0);

        seed = GameObject.Instantiate(seedPrefab, new Vector3(x, y, 0), Quaternion.identity);
        seed.GetComponentInChildren<SpriteRenderer>().sprite = seedSprites[seedId];
    }

    public void DestroySeed()
    {
        Destroy(seed);
        seed = null;
    }

    public void DestroyNutrient()
    {
        Destroy(nutrient);
        nutrient = null;
    }

    public void CollectSeed()
    {
        DestroySeed();
        seedCount[root.GetComponentInParent<Plant>().seedDropped]++;
        UpdateSeedCountText();
        PlaySound(4);
    }

    public void UpdateSeedCountText()
    {
        seedText[0].SetText(seedCount[0].ToString());
        seedText[1].SetText(seedCount[1].ToString());
        seedText[2].SetText(seedCount[2].ToString());
        seedText[3].SetText(seedCount[3].ToString());

        if (seedCount[0] == 0)
        {
            seedButtons[0].interactable = false;
        }
        else
        {
            seedButtons[0].interactable = true;
        }
        if (seedCount[1] == 0)
        {
            seedButtons[1].interactable = false;
        }
        else
        {
            seedButtons[1].interactable = true;
        }
        if (seedCount[2] == 0)
        {
            seedButtons[2].interactable = false;
        }
        else
        {
            seedButtons[2].interactable = true;
        }
        if (seedCount[3] == 0)
        {
            seedButtons[3].interactable = false;
        }
        else
        {
            seedButtons[3].interactable = true;
        }
    }

    void EnableGhostPlant(bool enable)
    {
        ghostPlant.SetActive(enable);
    }

    void UpdateGhostPlantPosition(float x)
    {
        ghostPlant.transform.position = new Vector3(x, ghostPlant.transform.position.y, ghostPlant.transform.position.z);
    }

    public void ShakeCamera()
    {
        Camera.main.gameObject.GetComponent<Animation>().Play();
    }

    public void PlaySound(int soundId)
    {
        AudioSource sound = GetComponent<AudioSource>();
        if (soundId == 3)
        {
            sound.PlayOneShot(sounds[soundId], 0.3f);
        }
        else
        {
            sound.PlayOneShot(sounds[soundId]);
        }
    }
}
