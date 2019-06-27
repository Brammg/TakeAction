using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<Garbage> activeGarbage = new List<Garbage>();
    public Player player;

    public GameObject lopendeBand;

    public float speed;
    public float bandDist;

    public bool isSpawning = false;
    bool isLooping = false;

    private int random;
    private float spawnTimer;
    public float timer;
    public float nextSpawn;
    public float timeIncrease;

    private Vector3 spawnPoint;

    public GameObject garbGameObject;
    public GameObject garbSprite;
    public Sprite[] sprites = new Sprite[3];
    private Sprite selectedSprite;

    private List<Garbage> targetLists = new List<Garbage>();
    private List<ToDoListClass> toDoList = new List<ToDoListClass>();
    public List<List<Garbage>> globalList = new List<List<Garbage>>(); 

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        spawnPoint = new Vector3(10.54f, 0, 6.06f);
        nextSpawn = 5;

        foreach (Transform t in GameObject.Find("UsableObstacles").transform)
        {
            GameManager.instance.interactableObst.Add(t.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameMenuActive == false)
        {
            SetGarbageValues();
        }

        if(isSpawning == true)
        {
            SpawnGarbage();
        }

        if(Time.time > 3 && Time.time > nextSpawn)
        {
            nextSpawn = Time.time + timeIncrease;

            SpawnNewGarbageList();

            if(timeIncrease > 1.5f)
            {
                timeIncrease -= 0.05f;
            }
        }

    }

    public void SpawnNewGarbageList()
    {
        globalList.Add(targetLists);
        
        for (int i = 0; i < 1; i++)
        {
            float time = i;

            random = Random.Range(0, 3);
            spawnTimer = Random.Range(time, time + 1);

            toDoList.Add(new ToDoListClass(random, spawnTimer));
            SetNewGarbage(random, spawnTimer);
        }
    }

    public Sprite SetNewGarbage(int rdm, float timer)
    {
        if (rdm == 0)
        {
            selectedSprite = sprites[Random.Range(8, 14)];
            isSpawning = true;
            return selectedSprite;
        }
        else if (rdm == 1)
        {
            selectedSprite = sprites[Random.Range(5, 7)];
            isSpawning = true;
            return selectedSprite;
        }
        else if (rdm == 2)
        {
            selectedSprite = sprites[Random.Range(0, 4)];
            isSpawning = true;
            return selectedSprite;
        }
        else
        {
            return selectedSprite;
        }
    }

    private void SpawnGarbage()
    {
        Garbage temp;
        for (int i = 0; i < toDoList.Count; i++)
        {
            if (isLooping == false)
            {
                timer = Time.deltaTime + toDoList[i].spawnTimer;
                isLooping = true;
            }

            if (Time.time > timer)
            {
                if (toDoList[i].random == 0)
                {
                    temp = new GarbageTypes.Plastic(selectedSprite, garbSprite, garbGameObject);
                    activeGarbage.Add(temp);
                    globalList[0].Add(temp);
                }
                else if (toDoList[i].random == 1)
                {
                    temp = new GarbageTypes.Ball(selectedSprite, garbSprite, garbGameObject);
                    activeGarbage.Add(temp);
                    globalList[0].Add(temp);
                }
                else if (toDoList[i].random == 2)
                {
                    temp = new GarbageTypes.Apple(selectedSprite, garbSprite, garbGameObject);
                    activeGarbage.Add(temp);
                    globalList[0].Add(temp);
                }
                isLooping = false;
                isSpawning = false;
                toDoList.RemoveAt(i);
            }
        }
    }

    public void SetGarbageValues()
    {
        for (int i = 0; i < activeGarbage.Count; i++)
        {
            if (activeGarbage[i].go != null)
            {
                activeGarbage[i].go.GetComponentInChildren<SpriteRenderer>().sprite = activeGarbage[i].sprite;

                if (Vector3.Distance(lopendeBand.transform.position, activeGarbage[i].sp.transform.position) < bandDist) //5.7, 6.3
                {
                    if (activeGarbage[i].go.transform.position.z >= 5.7f && activeGarbage[i].go.transform.position.z <= 6.3f)
                    {
                        activeGarbage[i].go.transform.rotation = new Quaternion(0, 0, 0, 0);
                        activeGarbage[i].go.transform.Translate(Vector2.left * speed * Time.deltaTime);
                    }
                }

                if (activeGarbage[i].go.transform.position.x > -5.8f && activeGarbage[i].go.transform.position.x < -5.38f && activeGarbage[i].go.transform.position.z >= 4f && activeGarbage[i].go.transform.position.z <= 6.3f)
                {
                    activeGarbage[i].go.transform.Translate(Vector3.back * speed * 2 * Time.deltaTime);

                    if (activeGarbage[i].go.transform.position.z > 3.9f && activeGarbage[i].go.transform.position.z < 4.1f)
                    {
                        GameManager.instance.countDownTimer -= 10;
                        Recycled(activeGarbage[i].go, activeGarbage[i].garbType);
                        activeGarbage.Remove(activeGarbage[i]);
                    }
                }
            }
        }
    }

    public void Recycled(GameObject go, string type)
    {
        for (int i = 0; i < activeGarbage.Count; i++)
        {
            if(activeGarbage[i].go.transform.position == go.transform.position)
            {
                activeGarbage[i] = null;
            }
        }
        player.nearestGar = null;
        Destroy(go);
    }
}
