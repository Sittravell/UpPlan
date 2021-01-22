using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
	public static string option;
	void Start(){
		option = null;
	}
	public void play(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}    

	public void navigateToDK(){
		option = "DK_door";
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void navigateToMKP1(){
		option = "MKP1_door";
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void navigateToWashroom(){
		option = "Toilet_1_Ground_door";
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
