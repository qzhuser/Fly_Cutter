using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setting : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        transform.GetComponent<Button>().onClick.AddListener(delegate () { click(); });
	}
    public void click() {
        PlayerPrefs.SetString("COM",transform.GetChild(0).GetComponent<Text>().text);
        Application.Quit();
    }
	// Update is called once per frame
	void Update () {
		
	}
}
