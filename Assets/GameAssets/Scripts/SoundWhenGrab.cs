using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWhenGrab : MonoBehaviour
{
    FMOD.Studio.EventInstance GrabSound;
    OVRGrabbable grabbable;
    bool grabLastFrame = false;

    [SerializeField] string eventName = "CollisionPlastic";
    // Start is called before the first frame update
    void Start()
    {
        grabbable = GetComponent<OVRGrabbable>();
        GrabSound = FMODUnity.RuntimeManager.CreateInstance ("event:/"+eventName);
    }

    // Update is called once per frame
    void Update()
    {
        if(grabLastFrame){
            if(!grabbable.isGrabbed){
                grabLastFrame = false;
            }
        }else{
            if(grabbable.isGrabbed){
                grabLastFrame = true;
                GrabSound.start();
            }
        }
    }
}
