using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Animator MyAnimationController;
    [SerializeField] private Animator Ground_Controller;
    [SerializeField] private Animator First_Controller;
    [SerializeField] private Animator Second_Controller;
    [SerializeField] private Material defaultMaterial;
    Renderer rend;
    GameObject Ground;
    GameObject First;
    GameObject Second;
    bool waitFade;
    bool waitAppear;
    List<int> levels;
    int plevels;
    void Start()
    {        
        Debug.Log("LevelController start");
        rend = GetComponent<Renderer> ();
        Ground = GameObject.Find("Ground_Model");
        First = GameObject.Find("First_Model");
        Second = GameObject.Find("Second_Model");
        waitFade = false;
        levels = new List<int>(){0,1,2};
        plevels = 0;
        First.SetActive(false);
        Second.SetActive(false);
        Debug.Log("MyAnimationController is null = "+(MyAnimationController==null).ToString());
        Debug.Log("Ground_Controller is null = "+(Ground_Controller==null).ToString());
        Debug.Log("First_Controller is null = "+(First_Controller==null).ToString());
        Debug.Log("Second_Controller is null = "+(Second_Controller==null).ToString());

    }
    // Update is called once per frame
    void Update()
    {
        
        DisableModels();
        AppearModels();     
    }
    void PrintName(GameObject go){
        print(go.name);
    }

    int getPlevels(){
        if(plevels>levels.Count-1){
            plevels = 0;
        }else if(plevels<0){
            plevels = 0;
        }
        return plevels;
    }

    public void buttonClicked(){
        plevels++;
        int level;
        level = getPlevels();
        MyAnimationController.SetBool("isClicked", true);
        rend.material.color = Color.green;
        switch(level){
            case 0:
                Second_Controller.SetBool("Fade", true);
                break;
            case 1:
                Ground_Controller.SetBool("Fade", true);
                break;
            case 2:
                First_Controller.SetBool("Fade", true);
                // Debug.Log("Second_C Fade true");
                break;
        }
        waitFade = true;
    }

    void DisableModels(){
        if(Ground_Controller.GetCurrentAnimatorStateInfo(0).IsName("Fade") && waitFade){
                    Ground.SetActive(false);
                    waitFade = false;
                    waitAppear = true;       
        }else if(First_Controller.GetCurrentAnimatorStateInfo(0).IsName("Fade") && waitFade){
                    First.SetActive(false);
                    waitFade = false;
                    waitAppear = true;
        }else if(Second_Controller.GetCurrentAnimatorStateInfo(0).IsName("Fade") && waitFade){
                    Second.SetActive(false);
                    waitFade = false;
                    waitAppear = true;

        }
    }

    void AppearModels(){
        if(waitAppear){
            switch(plevels){
            case 0:
                Ground.SetActive(true);
                Ground_Controller.SetBool("Fade", false);
                break;
            case 1:
                First.SetActive(true);
                First_Controller.SetBool("Fade", false);
                break;
            case 2:
                Second.SetActive(true);
                Second_Controller.SetBool("Fade", false);
                break;
            }
            waitAppear = false;
            MyAnimationController.SetBool("isClicked", false);
            rend.material = defaultMaterial;
        }
    }
}
