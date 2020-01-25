using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManagefloor : MonoBehaviour, Activated
{

    public CasePuzzle[] cases;
    public string[] nameOrder;

    public float[] orientation;
    
    FMOD.Studio.EventInstance pushSound;

    // Start is called before the first frame update
    void Start()
    {
        pushSound = FMODUnity.RuntimeManager.CreateInstance ("event:/DoorOpen");
    }
    public void Activate(GameObject origin){
        if(isItWin()){
            pushSound.start();
        }
    }

    bool isItWin(){
        bool result = true;
        for(int i = 0; i < cases.Length;i++ ){
            Debug.Log(i);
            if(cases[i].myObject != null){
                if(cases[i].myObject.name == nameOrder[i]){
                    if(nameOrder[i] == "Line"){
                        if( cases[i].oriantationMyObject%180 != 0  ){
                            result = false;
                            Debug.Log("line not align");
                        }
                    }else{
                        if( cases[i].oriantationMyObject != orientation[i] ){
                            result = false;
                            Debug.Log("not align");
                        }
                    }
                }else{
                    result = false;
                    Debug.Log("not good name");
                }
            }else{
                Debug.Log("pas d'objet");
                result = false;
            }
        }
        Debug.Log(result);
        Debug.Log("\n");
        return result;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}