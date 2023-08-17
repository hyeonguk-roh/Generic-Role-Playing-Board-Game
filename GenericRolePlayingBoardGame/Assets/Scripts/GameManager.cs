using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { START, P1, P2, P3, P4, GAMEOVER }

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform board;
    [SerializeField] private GameState state;

    private int numPlayers = 1;
    private GameObject[] playerList;

    void Start()
    {
        //numPlayers = PlayerPrefs.GetInt("Players");
        state = GameState.START;
        SetupBoard();
    }

    void Update()
    {
    }

    void SetupBoard()
    {
        playerList = new GameObject[numPlayers];
        for (int i = 0; i < numPlayers; i++)
        {
            playerList[i] = this.transform.GetChild(i).gameObject;
            playerList[i].SetActive(true);
            this.transform.GetChild(i).transform.position = board.GetChild(0).GetChild(0).GetChild(i).position;
        }
    }
}
