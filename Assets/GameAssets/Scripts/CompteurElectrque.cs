using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompteurElectrque : LeverHolder , Activated
{

    public SwitchCompteur[] mySwitchsNormal;
    public Renderer[] mySwitchsLights;
    public Renderer[] mySwitchsLightsSoluce;

    public Renderer activLight;

    public List<SwitchCompteur> mySwitchsActive = new List<SwitchCompteur>();
    public List<SwitchCompteur> mySwitchsOrder = new List<SwitchCompteur>();

    public bool iseReseting;
    float timerReset;

    public Transform boxSoluce;

    public GameObject handleToGet;

    public Material activ_Mat;
    public Material unactiv_Mat;
    bool alreadyActiv = false;

    FMOD.Studio.EventInstance openSound;
    FMOD.Studio.EventInstance resetSound;
    FMOD.Studio.EventInstance fusibleSound;

    // Start is called before the first frame update
    void Start()
    {
        openSound = FMODUnity.RuntimeManager.CreateInstance ("event:/VictorySwitch");
        resetSound = FMODUnity.RuntimeManager.CreateInstance ("event:/ResetSwitch");
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
        SwitchCompteur added = origin.GetComponent<SwitchCompteur>();
        mySwitchsActive.Add(added);
        

        if(isItRight() && isActive){
            
            if(mySwitchsActive.Count >= 3){
                winThis();
            }
        }else{
            StartCoroutine(resetAfter());
            timerReset = 3f;
        }
        iseReseting = true;
        timerReset = 0.2f;
        Debug.Log(isItRight());
    }

    IEnumerator resetAfter(){
        yield return new WaitForSeconds(0.2f);
        ResetCompteur();
        yield return null;
    }

    void winThis(){
        StartCoroutine(openWin());
        handleToGet.SetActive(true);
        openSound.start();
    }

    IEnumerator openWin(){
        for(int i = 0; i < 30; i ++){
            boxSoluce.transform.Rotate(new Vector3(-64.659f/30,0,0));
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    void ResetCompteur(){     
        resetSound.start();   
        for(int i = 0; i < mySwitchsNormal.Length; i ++){
            mySwitchsNormal[i].GetComponent<Animator>().SetBool("Active", false);
            mySwitchsNormal[i].GetComponent<Collider>().enabled = true;
            mySwitchsLights[i].material = unactiv_Mat;
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
                mySwitchsLights[mySwitchsActive[mySwitchsActive.Count-1].numSwitch].material = activ_Mat;
            }else{
                mySwitchsLightsSoluce[0].material = unactiv_Mat;
                mySwitchsLightsSoluce[1].material = unactiv_Mat;
                mySwitchsLightsSoluce[2].material = unactiv_Mat;
            }
        return result;
    }
}
