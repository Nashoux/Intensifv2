using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManagefloor : MonoBehaviour, Activated
{

    public CasePuzzle[] cases;
    public string[] nameOrder;

    public float[] orientation;

    [SerializeField] Door doorOpen;
    [SerializeField] BoutonPannel pannelToActiv;
    
    FMOD.Studio.EventInstance pushSound;

    // Start is called before the first frame update
    void Start()
    {
        pushSound = FMODUnity.RuntimeManager.CreateInstance ("event:/powerStart");
    }
    public void Activate(GameObject origin){
        if(isItWin()){
            pushSound.start();
            pannelToActiv.isActive = true;
            doorOpen.Activate(this.gameObject);
        }
    }

    bool isItWin(){
        bool result = true;
        for(int i = 0; i < cases.Length;i++ ){
            if(cases[i].myObject != null){
                if(cases[i].myObject.name == nameOrder[i]){
                    if(nameOrder[i] == "Line"){
                        if( cases[i].oriantationMyObject%180 != 0  ){
                            result = false;
                        }
                    }else{
                        if( cases[i].oriantationMyObject != orientation[i] ){
                            result = false;
                        }
                    }
                }else{
                    result = false;
                }
            }else{
                result = false;
            }
        }
        return result;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}