using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour {
	public AudioSource Ambience; 
	bool isFullScreen = false;
	void Start () {
		Ambience.Play ();
	}

	public void ExitPressed()
	{
		Application.Quit();
	}
	public void FullScreenToggle()
	{
		isFullScreen = !isFullScreen;
		Screen.fullScreen = isFullScreen;
	}
	public void StartNewLevel(int N)
	{
		if (N == 1) {
			SceneManager.LoadScene ("FirstScene");
		}
		if (N == 2) {
			SceneManager.LoadScene ("SecondScene");
		}
		if (N == 3) {
			SceneManager.LoadScene ("ThirdScene");
		}
		if (N == 4) {
			SceneManager.LoadScene ("FourthScene");
		}
		if (N == 5) {
			SceneManager.LoadScene ("FifthScene");
		}
		if (N == 6) {
			SceneManager.LoadScene ("SixthScene");
		}
		if (N == 7) {
			SceneManager.LoadScene ("SevenScene");
		}
		if (N == 8) {
			SceneManager.LoadScene ("EightScene");
		}
		if (N == 9) {
			SceneManager.LoadScene ("NineScene");
		}
		if (N == 10) {
			SceneManager.LoadScene ("TenScene");
		}
		if (N == 11) {
			SceneManager.LoadScene ("StartScene");
		}
	}
	public void RestartThisScene() {
	
//		SceneManager.LoadScene (SceneManager.GetActiveScene ());
	}
	public void GoToMenu()
	{
		
			SceneManager.LoadScene ("StartScreen");
		

	}
}
	