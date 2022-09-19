using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject GameOverHUD;
	public GameObject NoCoffeeHUD;
	public GameObject VictoryHUD;
	public GameObject BeansHUD;
	public GameObject SpecialBeans;

	private void Start () {
		Time.timeScale = 1;
	}
	
	public void GameOver() {
		GameOverHUD.SetActive (true);
		Time.timeScale = 0;
	}
	public void NoCoffee() {
		NoCoffeeHUD.SetActive (true);
		Time.timeScale = 0;
	}
	public void Victory() {
		GameObject SB = GameObject.Find("SpecialBean");
		if (SB) {
			
		} else {
			VictoryHUD.SetActive (true);
			Time.timeScale = 0;
		}

	}
	public void ChangeBeans(float N) {

		float RealW = (N / 100) * 320;

		BeansHUD.GetComponent<RectTransform> ().SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, RealW);

	}
}
