using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage
{
    SpawnManager spnm;
    Player player;
    List<GameObject> nearbyObstacles = new List<GameObject>();
    public GameObject go;
    public GameObject sp;

    public Sprite sprite;
    public string garbType;

    public Garbage(GameObject garbage3D, GameObject garbage2D, Sprite spr)
    {
        go = GameObject.Instantiate(garbage3D);
        go.transform.position = new Vector3(10.54f, 0, 6.06f);
        sp = GameObject.Instantiate(garbage2D);
        sp.transform.position = go.transform.position + new Vector3(0, 1.01f, 0);
        sp.transform.parent = go.transform;
        player = GameObject.Find("Player").GetComponent<Player>();
        spnm = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        sprite = spr;
    }

    public void SetPosition(GameObject destPoint)
    {
        go.transform.SetPositionAndRotation(destPoint.transform.position, player.gameObject.transform.rotation);

        if(nearbyObstacles != null)
        {
            nearbyObstacles.Clear();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (player.equipedGar != null)
            {
                foreach (GameObject g in GameManager.instance.interactableObst)
                {
                    if (Vector3.Distance(g.transform.position, go.transform.position) < 1.5)
                    {
                        nearbyObstacles.Add(g);
                        CheckObstacles();
                    }
                }

                go.transform.SetPositionAndRotation(go.transform.position, go.transform.rotation);
                player.equipedGar = null;
            }
        }
    }

    private void CheckObstacles()
    {
        if(nearbyObstacles != null)
        {
            float dist = 1.8f;
            GameObject nearest = null;

            foreach (GameObject g in nearbyObstacles)
            {
                if (Vector3.Distance(g.transform.position, go.transform.position) < dist)
                {
                    dist = Vector3.Distance(g.transform.position, go.transform.position);
                    nearest = g;
                }
            }

            if (nearest.name == "AppleBin")
            {
                if (garbType == "Apple")
                {
                    GameManager.instance.score += 10;
                    GameManager.instance.countDownTimer += 10;
                    spnm.activeGarbage.Remove(this);
                    spnm.Recycled(go, garbType);
                }
            }
            else if (nearest.name == "BallBin")
            {
                if (garbType == "Ball")
                {
                    GameManager.instance.score += 10;
                    GameManager.instance.countDownTimer += 10;
                    spnm.activeGarbage.Remove(this);
                    spnm.Recycled(go, garbType);
                }
            }
            else if (nearest.name == "PlasticBin")
            {
                if (garbType == "Plastic")
                {
                    GameManager.instance.score += 10;
                    GameManager.instance.countDownTimer += 10;
                    spnm.activeGarbage.Remove(this);
                    spnm.Recycled(go, garbType);
                }
            }
        }
    }
}
