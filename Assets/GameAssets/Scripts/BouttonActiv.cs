using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouttonActiv : MonoBehaviour
{

    public BoutonPannel panneau;
    public Collider myCollider;
    public Animator animator;

    public int numSwitch = 0;
    void Start()
    {
        myCollider = GetComponent<Collider>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Active(){
        if(!panneau.iseReseting){
            panneau.Activate(this.gameObject);
            myCollider.enabled = false;
            animator.SetBool("Active", true);
        }

    }

    private void OnTriggerEnter(Collider other) {
        if(other.transform.tag == "Hand"){
            Active();
        }
    }
}