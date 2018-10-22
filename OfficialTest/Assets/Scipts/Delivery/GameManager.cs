using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Little GameManager that i've created to test my code, but that if we want to can be completed with other methods
public class GameManager : MonoBehaviour {

    public static GameManager instance { get; private set; }
    public GameObject player;

    //List of player that are inside the game
    private List<GameObject> players = new List<GameObject>();

	// Use of Singleton Pattern
	private void Awake () {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
	}

    //Take the first player that is instantiated to know exactly where it is. That it is usefull to do a perfect doubling the player in a correct way
    private void Start()
    {
      players.Add(Instantiate(player, new Vector3(0f, 0f, 0f), Quaternion.identity));
    }

    //New player instance that with the use of the list take the last player position for the new player' position
    public void IstantiateNewPlayer()
    {
        Vector3 mom = player.transform.position;
        players.Add(Instantiate(player, new Vector3(players[players.Count -1 ].transform.position.x + 1f, players[players.Count-1].transform.position.y, players[players.Count-1].transform.position.z ), players[players.Count-1].transform.rotation ));
        
    }
}
