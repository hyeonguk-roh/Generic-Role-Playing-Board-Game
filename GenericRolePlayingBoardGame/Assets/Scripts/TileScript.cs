using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class TileScript : MonoBehaviour
{
    public int cost;
    public int oneFactory;
    public int twoFactory;
    public int threeFactory;
    public int fourFactory;
    public int fiveFactory;
    public int megaFactory;

    public string tileName = "Empty Plot";
    public Material color;

    public List<Transform> players = new List<Transform>();

    private int numOfPlayers;

    void Start()
    {
        oneFactory = cost / 3;
        twoFactory = oneFactory * 2;
        threeFactory = twoFactory + oneFactory;
        fourFactory = threeFactory + twoFactory;
        fiveFactory = fourFactory + threeFactory;
        megaFactory = fiveFactory * 2;

    }
    void Update()
    {
        if (this.tag == "Tile") {
            this.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Text>().text = tileName;
            this.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TMP_Text>().text = cost.ToString() + " mony";
            this.GetComponent<MeshRenderer>().material = color;
        }

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].GetComponent<Player>().isStopped == true)
            {
                if (this.tag == "Tile") {
                    players[i].position = this.transform.GetChild(i + 1).position;
                } else {
                    players[i].position = this.transform.GetChild(i).position;
                }
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            players.Add(other.gameObject.transform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            players.Remove(other.gameObject.transform);
        }
    }
}
