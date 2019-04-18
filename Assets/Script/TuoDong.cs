using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuoDong : MonoBehaviour {
    Vector3 benlai;
    // Use this for initialization
    void Start () {
        benlai = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //鼠标拖动
    public void drag()
    {
        print("拖动中");
        //if (transform.childCount > 6) { 
        if (transform.localPosition.x > benlai.x)
        {
            transform.localPosition = benlai;
        }
        else
        {
            transform.localPosition = new Vector3(transform.localPosition.x + Input.GetAxis("Mouse X") * 10f, transform.localPosition.y, transform.localPosition.z);
        }
        // }
    }
}
