using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasePuzzle : MonoBehaviour
{
public Transform pos;

    GameObject lastEnteredLever;

    public PuzzleManagefloor puzzleManagerfloor;


    public string tagObj = "Lever";
    int nbLever = 0;


    public bool isActive = false;

    public OVRGrabbable myObject;

    public float oriantationMyObject;

    FMOD.Studio.EventInstance addLeverSound;
    void Start()
    {
        myObject = null;
        addLeverSound = FMODUnity.RuntimeManager.CreateInstance ("event:/AddLever");
    }


    void OnTriggerEnter(Collider other) {
        if(other.tag == tagObj && other.GetComponent<OVRGrabbable>().isGrabbed == true){
            lastEnteredLever = other.gameObject;
        }
    }

    void OnTriggerStay(Collider other) {
        if(other.tag == tagObj &&  lastEnteredLever == null && other.GetComponent<OVRGrabbable>().isGrabbed == true){
            lastEnteredLever = other.gameObject;
        }
        if(other.tag == tagObj && lastEnteredLever != null && other.GetComponent<OVRGrabbable>().isGrabbed == false && nbLever < 1){            
            if(Vector2.Distance(new Vector2(transform.position.x,transform.position.z ), new Vector2(other.transform.position.x,other.transform.position.z )) < 0.7f ){
                nbLever ++;
                addLeverSound.start();
                other.transform.position = transform.position;
                oriantationMyObject = other.transform.rotation.eulerAngles.y - (other.transform.rotation.eulerAngles.y%90 );
                other.transform.rotation = Quaternion.Euler( new Vector3(0,oriantationMyObject,0) );
                
                myObject = other.GetComponent<OVRGrabbable>();
                puzzleManagerfloor.Activate(this.gameObject);
            }
            
        }
        if(myObject != null && other.gameObject == myObject.gameObject){
            if(myObject.isGrabbed){
                myObject = null;
                nbLever --;
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.tag == tagObj){
            lastEnteredLever = null;
        }
    }
}
