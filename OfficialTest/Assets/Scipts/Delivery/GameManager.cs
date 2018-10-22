using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance { get; private set; }

    public GameObject player;

	// Use this for initialization
	private void Awake () {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
	}

    private void Start()
    {
       player = Instantiate(player, new Vector3(1f, 0f, 0f), Quaternion.identity);
    }

    public void IstantiateNewPlayer()
    {
        Vector3 mom = player.transform.position;
        Instantiate(player, new Vector3(player.transform.position.x + 1f, player.transform.position.y, player.transform.position.z ), Quaternion.identity);
        player.transform.position = mom;
    }
}
