using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class NavigationScript : MonoBehaviour
{
	GameObject wordGO;
	GameObject wordGO2;
	GameObject navigationGO;
	GameObject signGO1;
	GameObject signGO2;	
	Animator wordAnimator;
	Animator wordAnimator2;
	Animator signAnimator1;
	Animator signAnimator2;
	List<string> WordList;
	List<Tuple<string,string>> SignList;
	List<string> NavigationList;
	List<Tuple<string,GameObject>> WordObjects;
	List<Tuple<string,GameObject>> NavigationObjects;
	[SerializeField] private Material defaultMaterial;
	bool waitFade;
    // Start is called before the first frame update
    void Start()
    {

    	WordList = new List<string>(){"DK_word", "MKP1_word"};
    	WordObjects = new List<Tuple<string,GameObject>>{};
        foreach(string word in WordList){
        	wordGO = GameObject.Find(word);
  			WordObjects.Add(new Tuple<string,GameObject>(word,wordGO));
        	wordGO.SetActive(false);
        }
        SignList = new List<Tuple<string,string>>(){};
        SignList.Add(new Tuple<string,string>("Male_Toilet_1_Ground_sign","Female_Toilet_1_Ground_sign"));
    	NavigationList = new List<string>(){"DK_navigation", "Toilet_1_Ground_navigation", "MKP1_navigation"};
    	NavigationObjects = new List<Tuple<string,GameObject>>{};
    	foreach(string navigation in NavigationList){
    		Debug.Log("finding navigation object: "+navigation);
        	navigationGO = GameObject.Find(navigation);
  			NavigationObjects.Add(new Tuple<string,GameObject>(navigation,navigationGO));
        	navigationGO.SetActive(false);
        }
        waitFade = false;
        Debug.Log("wordAnimator is null = "+wordAnimator==null);
    }

    // Update is called once per frame
    void Update()
    {
    	if(waitFade){
        	if(wordAnimator2.GetCurrentAnimatorStateInfo(0).IsName("Fade")){
                    wordGO2.SetActive(false);
                    waitFade = false;
        	}
    	}
    }

    public void buttonClicked(GameObject door){
    	if(door.tag.Contains("Toilet")){
    		foreach(var sign in SignList){
    			if(sign.Item1.Contains(door.tag)){
    				signGO1 = GameObject.Find(sign.Item1);
    				signGO2 = GameObject.Find(sign.Item2);
    			}			
    		}
    		signAnimator1 = signGO1.GetComponent<Animator>();
    		signAnimator2 = signGO2.GetComponent<Animator>();
    		signAnimator1.SetBool("Fade", false);
    		signAnimator2.SetBool("Fade", false);
    	}else{
    		foreach(string word in WordList){
    			if(word.Contains(door.tag)){
    				Debug.Log(word);
    				foreach(var Wobject in WordObjects){
    					if(string.Equals(Wobject.Item1,word)){
    						wordGO = Wobject.Item2;
    						Debug.Log("word Object: "+ wordGO.ToString());
    					}
    				}
    				break;
    			}
    		}
    		wordGO.SetActive(true);
    		Debug.Log("word chosen: "+ wordGO.ToString());
			wordAnimator = wordGO.GetComponent<Animator>();
    		wordAnimator.SetBool("Fade",false);
    	}
    	foreach(string navigation in NavigationList){
    		Debug.Log("door.tag = " + door.tag);
    		if(navigation.Contains(door.tag)){
    			Debug.Log(navigation);
    			foreach(var Nobject in NavigationObjects){
    				if(string.Equals(Nobject.Item1,navigation)){
    					navigationGO = Nobject.Item2;
    					Debug.Log("navigation Object: "+navigationGO.ToString());
    				}
    			}
    			break;
    		}
    	}
    	door.GetComponent<Renderer>().material.color = Color.green;
    	navigationGO.SetActive(true);
    	
    }

    public void Deselect(GameObject door){
    	if(door.tag.Contains("Toilet")){
    		foreach(var sign in SignList){
    			if(sign.Item1.Contains(door.tag)){
    				signGO1 = GameObject.Find(sign.Item1);
    				signGO2 = GameObject.Find(sign.Item2);
    			}			
    		}
    		signAnimator1 = signGO1.GetComponent<Animator>();
    		signAnimator2 = signGO2.GetComponent<Animator>();
    		signAnimator1.SetBool("Fade", true);
    		signAnimator2.SetBool("Fade", true);
    	}else{
    		foreach(string word in WordList){
    			if(word.Contains(door.tag)){
    				Debug.Log("deselected word chosen: "+ word);
    				wordGO2=GameObject.Find(word);
    				Debug.Log("deselected word object: "+wordGO2.ToString());
    				break;
    			}
    		}
    		wordAnimator2 = wordGO2.GetComponent<Animator>();
    		wordAnimator2.SetBool("Fade",true);
    		waitFade = true;
    	}
    	foreach(string navigation in NavigationList){
    		if(navigation.Contains(door.tag)){
    			navigationGO=GameObject.Find(navigation);
    			break;
    		}
    	}
    	door.GetComponent<Renderer>().material = defaultMaterial;
    	navigationGO.SetActive(false);    	
    }

    
}
