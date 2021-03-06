﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The base class representing a bird.
 */
public abstract class Bird : MonoBehaviour {

	// Private attributes
	private Rigidbody2D rbody;
	private SpriteRenderer render;
	private float startX;
	private float lastFlap;
	private AudioSource audioSource;

	// Parameters
	public float X_speed;
	public float Y_impulse;
	public float AnimationFlapTime;
	public Sprite AnimationIdle, AnimationFlap;
	public AudioClip flap, hit;

	// Properties
	public bool dead { get; set; }
	public float fitness { get; set; }

	////////////////////////////////////////////////////////////////

	// Start the component
	void Start () {
		rbody = GetComponent<Rigidbody2D> ();
		render = GetComponent<SpriteRenderer> ();
		audioSource = GetComponent<AudioSource> ();

		dead = false;
		startX = transform.position.x;
		lastFlap = -1;
		rbody.AddForce (new Vector2(X_speed, 0));

		// Start the child class
		OnStart ();
	}

	// Update the component
	void Update() {
		// Update the fitness
		fitness = transform.position.x - startX;

		// Update the sprite
		if (lastFlap != -1 && Time.time >= lastFlap + AnimationFlapTime) {
			render.sprite = AnimationIdle;
			lastFlap = -1;
		}

		// Update the child class
		OnUpdate ();
	}

	// Called when the bird is started
	protected virtual void OnStart () { }

	// Called when the bird is updated
	protected virtual void OnUpdate () { }

	////////////////////////////////////////////////////////////////

	// Let the bird fly
	public void Fly() {
		if (!dead) {
			//rbody.AddForce (new Vector2 (0, Y_impulse));
			if (lastFlap == -1) {
				Play ("FLAP");
			}
			rbody.velocity = new Vector2(rbody.velocity.x, Y_impulse);
			render.sprite = AnimationFlap;
			lastFlap = Time.time;
		}
	}

	// Called when the bird hits an obstacle
	public virtual void Hit() {
		rbody.simulated = false;
		dead = true;
		Play ("HIT");
	}

	// Handle the collision with an obstacle
	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.CompareTag ("Hit")) {
			Hit ();
		}
	}

	// Play a sound
	public void Play(string sound) {
		switch (sound) {
		case "FLAP":
			audioSource.clip = flap;
			break;
		case "HIT":
			audioSource.clip = hit;
			break;
		default:
			break;
		}

		audioSource.pitch = Random.Range (.95f, 1.05f);
		audioSource.Play ();
	}
}
