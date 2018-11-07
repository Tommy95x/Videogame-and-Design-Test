using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance { get; private set; }
    public GameObject board;
    public GameObject player;
    public GameObject[] enemy = new GameObject[4];

    private int playerLife;
    private GameObject[] food;
    
   

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        InitiateGame();
    }

    private void InitiateGame()
    {
        playerLife = 4;
        for(int i = 0; i < enemy.Length; i++)
        {
            Instantiate(enemy[i]);
        }
        Instantiate(board);
        Instantiate(player);

    }

    
    public void playerDeath()
    {
        if (playerLife <= 0)
        {
            gameOver();
        }
        else
            newLife();
    }

    private void newLife()
    {
        Instantiate(player);
    }

    private void gameOver()
    {
        throw new NotImplementedException();
    }
}
