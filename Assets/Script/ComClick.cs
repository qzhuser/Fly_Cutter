using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComClick : MonoBehaviour {
    int month;
    int year;
    int day;
	// Use this for initialization
	void Start () {
        transform.GetComponent<Button>().onClick.AddListener(delegate() { click(); });
	}
    public void click() {
        CompanyManager.cm.huanyuan(transform.parent.gameObject);
        Destroy(GameObject.FindGameObjectWithTag("game"));
        transform.GetComponent<AudioSource>().Play();
        CompanyManager.cm.logo.sprite = transform.GetChild(0).GetComponent<Image>().sprite;
        CompanyManager.cm.desc.text = transform.name.Split('，')[0];
        CompanyManager.cm.dizhi= transform.name.Split('，')[1];
        CompanyManager.cm.day.text = DateTime.Now.AddDays(7).Year + " 年 " + DateTime.Now.AddDays(7).Month + " 月 " + DateTime.Now.AddDays(7).Day+" 日";
        Instantiate(CompanyManager.cm.game[UnityEngine.Random.Range(0, CompanyManager.cm.game.Length - 1)],CompanyManager.cm.gameparent);
        CompanyManager.cm.qiyongjianpan = false;
        CompanyManager.cm.GameName = GameObject.FindGameObjectWithTag("game");
    }
	// Update is called once per frame
	void Update () {
		
	}
}
