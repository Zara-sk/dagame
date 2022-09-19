using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
//using static System.Math;
using System;


public class PlayerHandler : MonoBehaviour {
	public float AnglePerSecond = 30;
	public float Vel = 200;
	private Rigidbody2D rb;
	private SpriteRenderer sr;
	public string LookDirection = "Left";

	public GameObject PlayerCamera;
	public GameObject Background;

	public float CurrentCoffee = 100;

	public GameManager gameManager;

	public AudioSource VictorySong; 
	public AudioSource BeanCollect; 
	public AudioSource DeathSong; 
	public AudioSource Flap; 
	public AudioSource Ambience; 
	WebSocket ws;
	string[] data;

	protected int f1, f2, f3, f4, f5;
	protected float xr, yr, zr;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();

		PlayerCamera = GameObject.Find("Main Camera");
		Background = GameObject.Find("bg");
		Vel = 200;
		CurrentCoffee = 100;
		Ambience.Play();


		ws = new WebSocket("ws://localhost:1122");
		ws.Connect();
		ws.OnMessage += (sender, e) =>
		{ 
			data = e.Data.Split(',');
			f1 = int.Parse(data[0].Trim( new Char[] { ']', '"', '[' } ));
			f2 = int.Parse(data[1].Trim( new Char[] { ']', '"', '[' } ));
			f3 = int.Parse(data[2].Trim( new Char[] { ']', '"', '[' } ));
			f4 = int.Parse(data[3].Trim( new Char[] { ']', '"', '[' } ));
			f5 = int.Parse(data[4].Trim( new Char[] { ']', '"', '[' } ));
			zr = float.Parse(data[5].Trim( new Char[] { ']', '"', '[' } ).Replace('.', ','));
			yr = float.Parse(data[6].Trim( new Char[] { ']', '"', '[' } ).Replace('.', ','));
			xr = float.Parse(data[7].Trim( new Char[] { ']', '"', '[' } ).Replace('.', ','));
			Debug.Log("" + xr + " " + yr + " " + zr);

		};
       
	}

	void FixFlip(){
		if (transform.localEulerAngles.z == 90) {
		} else if (transform.localEulerAngles.z >90 && transform.localEulerAngles.z <= 270) {
			sr.flipY = true; 
			LookDirection = "Right";
			transform.Rotate (0, 0, -Time.deltaTime*AnglePerSecond);
		} 
		else {
			sr.flipY = false;
			LookDirection = "Left";
			transform.Rotate (0, 0, Time.deltaTime*AnglePerSecond);
		}
	}

	void PressedLeft(){
		if (LookDirection == "Right") {
			Flap.Play ();
			float CurrentAngle = transform.localEulerAngles.z;
			float NewAngle = 180 - CurrentAngle;
			transform.Rotate (0, 0, (360-CurrentAngle+NewAngle));


		}
	}
	void PressedRight(){
		if (LookDirection == "Left") {
			Flap.Play ();
			float CurrentAngle = transform.localEulerAngles.z;
			float NewAngle = 180 - CurrentAngle;
			transform.Rotate (0, 0, NewAngle-CurrentAngle);

	
		}
	}
	void PressedDown(){
		if (LookDirection == "Left") {
			Flap.Play ();
			float NewAngle = 45;
			float CurrentAngle = transform.localEulerAngles.z;
			transform.Rotate (0, 0, (360-CurrentAngle+NewAngle));
		} else if (LookDirection == "Right") {
			Flap.Play ();
			float NewAngle = 135;
			float CurrentAngle = transform.localEulerAngles.z;
			transform.Rotate (0, 0, (360-CurrentAngle-(360-NewAngle)));
		}


	}
	void PressedUp(){
		if (LookDirection == "Left") {
			Flap.Play ();
			float NewAngle = -45;
			float CurrentAngle = transform.localEulerAngles.z;
			transform.Rotate (0, 0, (360-CurrentAngle+NewAngle));
		} else if (LookDirection == "Right") {
			Flap.Play ();
			float NewAngle = 225;
			float CurrentAngle = transform.localEulerAngles.z;
			transform.Rotate (0, 0, (360-CurrentAngle-(360-NewAngle)));
		}


	}
	void FixCamera() {

		PlayerCamera.transform.position = new Vector3 (transform.position.x, transform.position.y, -10);
	}
	// Update is called once per frame
	void Update () {
		

		//ChangeAngle ();



		CurrentCoffee = CurrentCoffee - 10*Time.deltaTime;
		if (CurrentCoffee <= 0) {
			NoCoffeeScreen ();
		} else {
			FixFlip ();
			rb.velocity = transform.right * -Vel * Time.deltaTime;
			FixCamera ();
			gameManager.ChangeBeans (CurrentCoffee);
		}

		if (Input.GetKeyDown (KeyCode.A) || f2 >= 50) {
			PressedLeft ();
		} 
		else if (Input.GetKeyDown (KeyCode.D) || f4 >= 50) {
			PressedRight ();
		}
		else if (Input.GetKeyDown (KeyCode.S) || f5 >= 50) {
			PressedDown ();
		}
		else if (Input.GetKeyDown (KeyCode.W) || f3 >= 50) {
			PressedUp ();
		}
		else if (Input.GetKeyDown (KeyCode.Escape)) {

			GameObject HUD = GameObject.Find("HUD");
			HUD.GetComponent<MenuHandler>().GoToMenu();

		}




		}
	void NoCoffeeScreen() {
		AnglePerSecond = 0;
		Vel = 0;
		gameManager.NoCoffee ();
	}
	void DeathScreen() {

		AnglePerSecond = 0;
		Vel = 0;
		gameManager.GameOver ();

	}
	void VictoryScreen() {

		GameObject SB = GameObject.Find("SpecialBean");
		if (SB) {

		} else {
			VictorySong.Play ();
			AnglePerSecond = 0;
			Vel = 0;
			gameManager.Victory ();
		}


	}
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.name.StartsWith ("Wall") || coll.gameObject.name.StartsWith ("Pike") || coll.gameObject.name.StartsWith ("DoorA") || coll.gameObject.name.StartsWith ("DoorB")) {

			DeathSong.Play ();
			DeathScreen ();

		} 
		if (coll.gameObject.name.StartsWith ("Bean")) {
			CurrentCoffee = 100;
			Destroy (coll.gameObject);
			BeanCollect.Play ();
		} 
		if (coll.gameObject.name.StartsWith ("Key")) {
			BeanCollect.Play ();
			Destroy (coll.transform.parent.gameObject);
		} 
		if (coll.gameObject.name.StartsWith ("SpecialBean")) {
			Destroy (coll.gameObject);
			BeanCollect.Play ();
		} 
		if (coll.gameObject.name.StartsWith ("Finish")) {
			
			VictoryScreen();
		} 
		if (coll.gameObject.name.StartsWith ("BounceWallVertical")) {

			if (LookDirection == "Left") {
				PressedRight ();
			} else if (LookDirection == "Right") {
				PressedLeft ();
			}

		} 
		if (coll.gameObject.name.StartsWith ("BounceWallHorizontal")) {

			if (transform.localEulerAngles.z >= 0 && transform.localEulerAngles.z < 180) {
				PressedUp ();
			} else {
				PressedDown ();
			}

		} 
     }
}
