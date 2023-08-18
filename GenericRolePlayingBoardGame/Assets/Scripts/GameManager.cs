using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform board;               //get the board transform
    [SerializeField] private List<Transform> playerList;    //create a List<T> of players to determine order of positions
    [SerializeField] private GameObject[] playerListArray;  //create an array of player gameobjects

    public int playerCount = 0; //number of players

    //Sets up the board by placing player entities at the start
    public void SetupBoard()
    {
        playerList = new();
        playerListArray = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < playerCount; i++)
        {
            playerList.Add(playerListArray[i].transform);
            playerList[i].position = board.GetChild(0).GetChild(0).GetChild(i).position;
            Debug.Log("Set Up the Board");
            Debug.Log(playerList);
        }
    }
}
