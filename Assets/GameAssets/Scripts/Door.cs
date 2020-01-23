using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMOD;

public class Door : MonoBehaviour, Activated
{
    bool opened = false;
    FMOD.Studio.EventInstance openSound;
    void Start()
    {
        openSound = FMODUnity.RuntimeManager.CreateInstance ("event:/DoorOpen");
    }

    public void Activate(GameObject origin){
        if(!opened){
            GetComponent<Animator>().SetTrigger("Open");
            openSound.start();
            opened = true;
            GetComponent<BoxCollider>().enabled = false;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
