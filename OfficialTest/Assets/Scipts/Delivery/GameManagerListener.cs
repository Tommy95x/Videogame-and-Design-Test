﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Listener that is used to capture the event for a new player' instance
public class GameManagerListener : MonoBehaviour {


    private void OnEnable()
    {
        EventManager.StartListening("NewPlayer", NewPlayer);
    }

    private void NewPlayer()
    {
        //The GameManager must instantiate the first player, in way to know alway where is the player to create a new instance
        GameManager.instance.IstantiateNewPlayer();
    }
}
