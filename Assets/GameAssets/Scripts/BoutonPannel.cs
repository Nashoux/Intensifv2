using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutonPannel : MonoBehaviour,Activated
{

    public BouttonActiv[] mySwitchsNormal;
    public Renderer myLightWin;
    public Renderer activLight;
    public Renderer[] mySwitchsLightsSoluce;


    public List<BouttonActiv> mySwitchsActive = new List<BouttonActiv>();
    public List<BouttonActiv> mySwitchsOrder = new List<BouttonActiv>();

    public bool iseReseting;
    float timerReset;

    public Transform boxSoluce;


    public Material activ_Mat;
    public Material unactiv_Mat;
    bool alreadyActiv = false;

    FMOD.Studio.EventInstance winSound;
    FMOD.Studio.EventInstance resetSound;
    FMOD.Studio.EventInstance fusibleSound;
    [SerializeField] Door openDoor;

    public bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        winSound = FMODUnity.RuntimeManager.CreateInstance ("event:/LeverEnd");
        resetSound = FMODUnity.RuntimeManager.CreateInstance ("event:/ResetBouton");
        fusibleSound = FMODUnity.RuntimeManager.CreateInstance ("event:/AddFusible"); 
    }

    // Update is called once per frame
    void Update()
    {
        if(iseReseting){
            timerReset -=Time.deltaTime;
            if(timerReset <= 0){
                iseReseting = false;
            }
        }
        if(isActive && !alreadyActiv){
            alreadyActiv = true;
            activLight.material = activ_Mat;
            fusibleSound.start();
        }
    }

    public void Activate(GameObject origin){
        BouttonActiv added = origin.GetComponent<BouttonActiv>();
        mySwitchsActive.Add(added);
        

        if(isItRight() && isActive){
            
            if(mySwitchsActive.Count >= 5){
                winThis();
            }
        }else{
            StartCoroutine(resetAfter());
            timerReset = 3f;
        }
        iseReseting = true;
        timerReset = 0.2f;
    }

    IEnumerator resetAfter(){
        yield return new WaitForSeconds(0.2f);
        ResetCompteur();
        yield return null;
    }

    void winThis(){
        openDoor.Activate(this.gameObject);
        winSound.start();
    }



    void ResetCompteur(){     
        resetSound.start();   
        for(int i = 0; i < mySwitchsNormal.Length; i ++){
            mySwitchsNormal[i].GetComponent<Animator>().SetBool("Active", false);
            mySwitchsNormal[i].GetComponent<Collider>().enabled = true;
        }
        for(int i = 0; i < mySwitchsLightsSoluce.Length; i++){
            mySwitchsLightsSoluce[i].material = unactiv_Mat;
        }
        mySwitchsActive.Clear();
    }



    bool isItRight(){
        bool result = true;
            if(mySwitchsActive[mySwitchsActive.Count-1].gameObject.name != mySwitchsOrder[mySwitchsActive.Count-1].gameObject.name){
                result = false;
            }
            if(result && isActive){
                mySwitchsLightsSoluce[mySwitchsActive.Count-1].material = activ_Mat;
            }else{
                //son de fail
            }
        return result;
    }
}