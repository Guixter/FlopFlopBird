﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The DataManager is a Singleton.
 * It contains all the data that needs to be saved between several scenes.
 */
public class DataManager : MonoBehaviour {

	// The DataManager instance
	public static DataManager INSTANCE;

	// Properties
	public Generation currentGeneration { get; set; }
	public int generationNb { get; set; }
	public float timeSpeed { get; set; }
	public bool playing { get; set; }

	////////////////////////////////////////////////////////////////

	// Awake method
	void Awake () {
		if (INSTANCE != null) {
			Destroy (gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);
		INSTANCE = this;

		currentGeneration = new Generation ();
		currentGeneration.RandomGeneration ();
		timeSpeed = 1;
		playing = true;
		generationNb = 1;
	}
}
