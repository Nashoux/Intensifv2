using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRoom : MonoBehaviour, Activated
{
    
    public bool isOpening = false;

    [SerializeField] Door door;

    FMOD.Studio.EventInstance pushSound;
    public Animator an;

    void Start()
    {
        pushSound = FMODUnity.RuntimeManager.CreateInstance ("event:/DoorOpen");
    }

    public void Activate(GameObject origin){
        isOpening = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other) {
        if(other.transform.tag == "Hand"){
            pushSound.start();
            an.SetTrigger("Push");

            if(isOpening){
                door.Activate(this.gameObject);
                
            }else{

            }
        }
    }
}
