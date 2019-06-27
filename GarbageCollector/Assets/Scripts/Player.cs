using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{

    //rotation stuff
    public Transform lookingTarget;

    //Movement stuff
    public Camera cam;
    public NavMeshAgent agent;
    public GameObject playerSprite;
    private float speed = 3.5f;

    //Animation stuff
    public List<Sprite> animSprites;
    bool animDir = false;

    //Garbage stuff
    private SpawnManager spnmgr;
    float garDist;
    public float pickupDist;
    public Garbage nearestGar;
    public Garbage equipedGar;
    public GameObject destPoint;

    // Start is called before the first frame update
    void Start()
    {
        spnmgr = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        GameManager.instance.score = 0;
    }

    private void Update()
    {
        if (GameManager.instance.gameMenuActive == false)
        {
            PickUp();
            GarbageConnector();
            PlayerAnims();
            CheckGameOver();
        }
    }

    void FixedUpdate()
    {
        if (GameManager.instance.gameMenuActive == false)
        {
            playerSprite.transform.position = this.transform.position + new Vector3(0, 0.7f, -0.5f);
            Looking();
            Moving();
        }
        else
        {
            agent.SetDestination(this.transform.position);
        }
    }


    void Looking()
    {
        transform.LookAt(lookingTarget);
    }

    void Moving()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButton(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.speed = speed;
                agent.SetDestination(hit.point);               
            }
        }
    }

    private void PlayerAnims()
    {
        if(agent.destination.z > this.transform.position.z)
        {
            playerSprite.GetComponent<SpriteRenderer>().sprite = animSprites[3];
            animDir = true;
        }
        else if(agent.destination.z < this.transform.position.z)
        {
            playerSprite.GetComponent<SpriteRenderer>().sprite = animSprites[2];
            animDir = false;
        }
        else
        {
            if(animDir == true)
            {
                playerSprite.GetComponent<SpriteRenderer>().sprite = animSprites[0];
            }
            else
            {
                playerSprite.GetComponent<SpriteRenderer>().sprite = animSprites[1];
            }

        }
    }

    private void PickUp()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(equipedGar == null)
            {
                foreach (Garbage g in spnmgr.activeGarbage)
                {
                    if (Vector3.Distance(transform.position, g.go.transform.position) < pickupDist)
                    {
                        garDist = Vector3.Distance(transform.position, g.go.transform.position);
                    }

                    if (garDist > Vector3.Distance(transform.position, g.go.transform.position) || garDist == Vector3.Distance(transform.position, g.go.transform.position))
                    {
                        nearestGar = g;
                    }
                }

                if(nearestGar != null)
                {
                    if (Vector3.Distance(transform.position, nearestGar.go.transform.position) < pickupDist)
                    {
                        equipedGar = nearestGar;
                    }
                    else
                    {
                        equipedGar = null;
                    }
                }
            }
        }
    }

    private void GarbageConnector()
    {
        if(equipedGar != null)
        {
            equipedGar.SetPosition(destPoint);
        }

    }

    private void CheckGameOver()
    {
        if(GameManager.instance.countDownTimer < 1)
        {
            GameManager.instance.countDownTimer = 0;
            GameManager.instance.gameMenuActive = true;
            GameManager.instance.gameOver = true;
        }
    }
}
