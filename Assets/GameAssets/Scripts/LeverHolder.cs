using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverHolder : MonoBehaviour
{
    public Transform pos;

    GameObject lastEnteredLever;
    public GameObject grabableHandler;

    public GameObject[] handlesToAdd;

    public string tagObj = "Lever";
    int nbLever = 0;

    public bool isDestroyedAfter = false;

    public int nbToAdd = 1;
    public bool isActive = false;

    FMOD.Studio.EventInstance addLeverSound;
    void Start()
    {
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
        if(other.tag == tagObj && lastEnteredLever != null && other.GetComponent<OVRGrabbable>().isGrabbed == false){
            other.GetComponent<OVRGrabbable>().enabled = false;
            other.GetComponent<Rigidbody>().isKinematic = true;
            //other.GetComponent<Collider>().enabled = false;
            //other.gameObject.SetActive(false);
            other.transform.position = new Vector3(-100,0,0);
            if(other.GetComponent<ResetPositionIfOut>()){
                other.GetComponent<ResetPositionIfOut>().enabled = false;
            }
            //SnapToPos mySnap = other.gameObject.AddComponent<SnapToPos>();
            //mySnap.snapToThis = pos;
            other.tag = "Untagged";
            handlesToAdd[nbLever].SetActive(true);
            nbLever ++;
            addLeverSound.start();
            if(nbLever >= nbToAdd && isDestroyedAfter){
                isActive = true;
                grabableHandler.SetActive(true);
                Destroy(this);
            }else if (nbLever >= nbToAdd ){
                isActive = true;
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.tag == tagObj){
            lastEnteredLever = null;
        }
    }


}
