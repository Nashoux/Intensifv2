using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLever : MonoBehaviour
{

    public OVRGrabbable handleGrab;
    public Door door;
    public Renderer lightRend;
    public Material activMat;
    FMOD.Studio.EventInstance openSound;

    bool alreadyActivated = false;
    // Start is called before the first frame update
    void Start()
    {
        openSound = FMODUnity.RuntimeManager.CreateInstance ("event:/LeverEnd");
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.rotation.eulerAngles.z < 200 && transform.rotation.eulerAngles.z > 150  && !handleGrab.isGrabbed && !alreadyActivated){
            handleGrab.enabled = false;
            
            alreadyActivated = true;
            door.Activate(this.gameObject);
            lightRend.material = activMat;
            openSound.start();
            
        }
    }
}
