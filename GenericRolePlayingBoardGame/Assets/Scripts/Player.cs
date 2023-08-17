using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform board;
    private Transform[] tiles = new Transform[44];
    private int currentTile;
    private TileScript tileScript;

    [SerializeField] private RawImage dice1;
    [SerializeField] private RawImage dice2;

    [SerializeField] private Texture oneDice;
    [SerializeField] private Texture twoDice;
    [SerializeField] private Texture threeDice;
    [SerializeField] private Texture fourDice;
    [SerializeField] private Texture fiveDice;
    [SerializeField] private Texture sixDice;

    public bool isTurn;
    public bool isStopped = true;
    private int rollNumber1;
    private int rollNumber2;
    private int totalRoll;

    [SerializeField] private TMP_Text monyText;
    public int mony = 1500000;

    void Start()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = board.GetChild(i);
        }
        currentTile = 0;
    }

    void Update()
    {
        monyText.text = mony.ToString() + " mony";
    }

    public void TakeTurn()
    {
        StartCoroutine(RollDice());
        StartCoroutine(MoveAcrossBoard());
    }

    IEnumerator RollDice()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.1f);
            rollNumber1 = Random.Range(1, 6);
            if (rollNumber1 == 1)
            {
                dice1.texture = oneDice;
            }
            if (rollNumber1 == 2)
            {
                dice1.texture = twoDice;
            }
            if (rollNumber1 == 3)
            {
                dice1.texture = threeDice;
            }
            if (rollNumber1 == 4)
            {
                dice1.texture = fourDice;
            }
            if (rollNumber1 == 5)
            {
                dice1.texture = fiveDice;
            }
            if (rollNumber1 == 6)
            {
                dice1.texture = sixDice;
            }
            rollNumber2 = Random.Range(1, 6);
            if (rollNumber2 == 1)
            {
                dice2.texture = oneDice;
            }
            if (rollNumber2 == 2)
            {
                dice2.texture = twoDice;
            }
            if (rollNumber2 == 3)
            {
                dice2.texture = threeDice;
            }
            if (rollNumber2 == 4)
            {
                dice2.texture = fourDice;
            }
            if (rollNumber2 == 5)
            {
                dice2.texture = fiveDice;
            }
            if (rollNumber2 == 6)
            {
                dice2.texture = sixDice;
            }
        }


        totalRoll = rollNumber1 + rollNumber2;
    }

    IEnumerator MoveAcrossBoard()
    {
        isStopped = false;
        yield return new WaitForSeconds(2);
        for (int i = 0; i < totalRoll; i++)
        {
            yield return new WaitForSeconds(0.5f);
            if (currentTile != 43)
            {
                this.transform.position = tiles[currentTile + 1].position;
                Debug.Log("Moved to" + (currentTile + 1));
                currentTile++;
            }
            else
            {
                this.transform.position = tiles[0].position;
                Debug.Log("Moved to 0");
                currentTile = 0;
            }
        }
        yield return new WaitForSeconds(1);
        isStopped = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tile")
        {
            tileScript = other.gameObject.GetComponent<TileScript>();
            Debug.Log("Entered trigger");
        }
    }
}
