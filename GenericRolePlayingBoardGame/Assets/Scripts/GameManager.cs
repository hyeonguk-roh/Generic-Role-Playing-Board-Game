using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform board;               //get the board transform
    [SerializeField] private List<Transform> playerList;    //create a List<T> of players to determine order of positions
    [SerializeField] private GameObject[] playerListArray;  //create an array of player gameobjects

    //Sets up the board by placing player entities at the start
    void Update()
    {
        playerListArray = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < playerListArray.Length; i++) {
            playerListArray[i].name = "Player " + i;
        }
    }
}
