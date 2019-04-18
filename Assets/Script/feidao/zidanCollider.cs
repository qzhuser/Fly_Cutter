using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zidanCollider: MonoBehaviour { 
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "cirle")
        {
            
            transform.parent = other.GetComponent<Collider>().transform;
            CircleRota.cr.isFashe = false;
        }
        else
        {
            CircleRota.cr.isGameOver = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        print(other);
        
    }
}
