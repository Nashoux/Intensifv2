using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    [SerializeField] bool statutUpdated = false;
    public string cardName = "Card1";
    [SerializeField] Renderer myRender;

    [SerializeField] Material activ_mat;

    [SerializeField] ButtonRoom button;
    FMOD.Studio.EventInstance scanSound;

    private void Start() {
        scanSound = FMODUnity.RuntimeManager.CreateInstance ("event:/Scanned");
    }
    

    void OnTriggerEnter(Collider other) {
        if(other.tag == cardName && !statutUpdated){
            myRender.material = activ_mat;
            statutUpdated = true;
            scanSound.start();
            button.Activate(this.gameObject);
        }
    }

}
