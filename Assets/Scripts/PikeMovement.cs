using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikeMovement : MonoBehaviour {
	private Rigidbody2D rb;
	private SpriteRenderer sr;
	public string CurrentDirection = "Right";
	public string Axis = "Vertical";
	public float CurrentOffset = 0;
	public float MaxOffset = 30;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (Axis == "Vertical") {
			if (CurrentDirection == "Right") {
				CurrentOffset = CurrentOffset + 1 * Time.deltaTime;
				transform.position += new Vector3(0, 1 * Time.deltaTime, 0);
				if (CurrentOffset >= MaxOffset) {
					CurrentDirection = "Left";
				}

			} else {
				CurrentOffset = CurrentOffset - 1 * Time.deltaTime;
				transform.position -= new Vector3(0, 1 * Time.deltaTime, 0);
				if (CurrentOffset <= -MaxOffset) {
					CurrentDirection = "Right";
				}
			}
		} else {
			if (CurrentDirection == "Right") {
				CurrentOffset = CurrentOffset + 1 * Time.deltaTime;
				transform.position += new Vector3(1 * Time.deltaTime, 0, 0);
				if (CurrentOffset >= MaxOffset) {
					CurrentDirection = "Left";
				}

			} else {
				CurrentOffset = CurrentOffset - 1 * Time.deltaTime;
				transform.position -= new Vector3(1 * Time.deltaTime, 0, 0);
				if (CurrentOffset <= -MaxOffset) {
					CurrentDirection = "Right";
				}
			}
		}



	}
}
