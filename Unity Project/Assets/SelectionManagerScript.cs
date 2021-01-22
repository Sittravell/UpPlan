using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.SceneManagement;

public class SelectionManagerScript : MonoBehaviour
{
	GameObject selected;
	GameObject MainMenu;

    void Awake(){
    	
    }
    void Start()
    {    	
    	selected = null;
    	
        Debug.Log("Selection start");
    }

    // Update is called once per frame
    void Update()
    {
    	if(MenuScript.option != null){
    		var door = GameObject.Find(MenuScript.option);
    		selected = door;
    		var NavigationScript = GameObject.Find("NavigationManager").GetComponent<NavigationScript>();
    		NavigationScript.buttonClicked(selected);
    	}
        if(Input.GetMouseButtonDown(0)){
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, 100.0f)){
                if(hit.transform != null){
                	Debug.Log("object hit");
                  if(selected != null){
                  	Debug.Log("selected: " + selected.ToString());
                  	if(selected.name.Contains("door")){
                  		var NavigationScript = GameObject.Find("NavigationManager").GetComponent<NavigationScript>();
                  		NavigationScript.Deselect(selected);
                  		MenuScript.option = null;
                  		Debug.Log("deselect navigation");
                  	selected = null;
                    }
                  }
                  var selection = hit.transform.gameObject;
                  selected = selection;
                  if(selection.name.Contains("door")){
                  	Debug.Log(selection.ToString());
                  	var NavigationScript = GameObject.Find("NavigationManager").GetComponent<NavigationScript>();
                  	Debug.Log(NavigationScript.ToString());
                  	NavigationScript.buttonClicked(selection);
                  	Debug.Log("navigation button clicked");
                  }else if(selected.name.Contains("level_button")){
                  	var LevelController = GameObject.Find("level_button").GetComponent<LevelController>();
                  	LevelController.buttonClicked();
                  	Debug.Log("level controller button clicked");
                  }
                    // PrintName(hit.transform.gameObject);
                }
            }else{
                	Debug.Log("no object hit");
                	if(selected != null){
                		Debug.Log("selected: " + selected.ToString());
                  		if(selected.name.Contains("door")){
                  			var NavigationScript = GameObject.Find("NavigationManager").GetComponent<NavigationScript>();
                  			NavigationScript.Deselect(selected);
                  			MenuScript.option = null;
                  			Debug.Log("navigation deselect");
                  		}
                  		selected = null;
                    }
                }
        }
    }
    public void back(){
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
