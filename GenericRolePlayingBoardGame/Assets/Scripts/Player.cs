using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Unity.Netcode;

using TMPro;

public class Player : NetworkBehaviour
{
    [SerializeField] private Transform board;                           //get board transform to find children objects
    [SerializeField] private Transform[] tiles = new Transform[44];     //list of all the tiles in order
    private int currentTile = 0;                                            //the current tile the player is on
    private TileScript tileScript;                                      //access to the tile's script

    [SerializeField] private RawImage dice1;    //dice roller 1
    [SerializeField] private RawImage dice2;    //dice roller 2

    [SerializeField] private Texture oneDice;   //sprite for 1 dot
    [SerializeField] private Texture twoDice;   //sprite for 2 dot
    [SerializeField] private Texture threeDice; //sprite for 3 dot
    [SerializeField] private Texture fourDice;  //sprite for 4 dot
    [SerializeField] private Texture fiveDice;  //sprite for 5 dot
    [SerializeField] private Texture sixDice;   //sprite for 6 dot

    [SerializeField] private Button rollButton; //access to the roll button

    public bool isTurn;             //is it this player's turn?
    public bool isStopped = true;   //has the player stopped moving?
    private int rollNumber1;        //number on first dice
    private int rollNumber2;        //number on second dice
    private int totalRoll;          //total sum of both dice

    [SerializeField] private TMP_Text monyText;     //displays amount of mony
    public int mony = 1500000;                      //amount of mony

    /*
    =====================================================================
    ||                  ON CREATION OF PLAYER ENTITY                   ||
    =====================================================================
    */
    void Awake()
    {
        board = GameObject.Find("Board").transform.GetChild(0);
        for (int i = 0; i < tiles.Length; i++) //fills an array with all tiles in the game starting from index 0
        {
            tiles[i] = board.GetChild(i);
        }
        currentTile = 0; //starting tile

        dice1 = this.transform.parent.GetChild(1).GetChild(0).GetChild(1).gameObject.GetComponent<RawImage>(); 
        dice2 = this.transform.parent.GetChild(1).GetChild(0).GetChild(2).gameObject.GetComponent<RawImage>();
        monyText = this.transform.parent.GetChild(1).GetChild(0).GetChild(3).gameObject.GetComponent<TMP_Text>();

        rollButton = this.transform.parent.GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<Button>();
        rollButton.onClick.AddListener(TakeTurn);

        if (IsLocalPlayer) {
            this.transform.parent.GetChild(1).gameObject.SetActive(false);
        }

        StartCoroutine(BoardSetup());
    }

    IEnumerator BoardSetup()
    {
        yield return new WaitForSeconds(0.1f);
        this.transform.position = tiles[0].position;
        TakeTurn();
    }

    /*
    =====================================================================
    ||                           EVERY FRAME                           ||
    =====================================================================
    */
    void Update()
    {
        monyText.text = mony.ToString() + " mony";
    }

    /*
    =====================================================================
    ||                         TAKE YOUR TURN                          ||
    =====================================================================
    */

    public void TakeTurn()
    {
        StartCoroutine(RollDice());
        StartCoroutine(MoveAcrossBoard());
    }

    IEnumerator RollDice()
    {
        isStopped = false;
        Debug.Log(this.transform.parent.gameObject + " is rolling");
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
        Debug.Log(this.transform.parent.gameObject + " is moving");
        yield return new WaitForSeconds(1);
        for (int i = 0; i < totalRoll; i++)
        {
            yield return new WaitForSeconds(0.3f);
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
        yield return new WaitForSeconds(0.1f);
        isStopped = true;
    }

    /*
    =====================================================================
    ||                  ON ENTERING A TRIGGER COLLIDER                 ||
    =====================================================================
    */
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tile")
        {
            tileScript = other.gameObject.GetComponent<TileScript>();
            Debug.Log("Entered trigger");
        }
        if (other.tag == "Unstable Investment")
        {
        }
        if (other.tag == "Breaking News")
        {
        }
        if (other.tag == "Level Up")
        {
        }
        if (other.tag == "Go 2 Jail")
        {
        }
        if (other.tag == "Gigafactory")
        {
        }
        if (other.tag == "Public Transportation")
        {
        }
    }
}
