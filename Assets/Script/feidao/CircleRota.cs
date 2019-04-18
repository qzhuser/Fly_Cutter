using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleRota:MonoBehaviour {
    public static CircleRota cr;
    /// <summary>
    /// 倒计时
    /// </summary>
    public Text daojishi;
    public GameObject circle1,circle2;
    /// <summary>
    /// 子弹预制体
    /// </summary>
    public GameObject zhen;
    public Button fashe;
    /// <summary>
    ///  弹药存储
    /// </summary>
    public GameObject danyaocunchu;
    /// <summary>
    /// 两个子弹位置
    /// </summary>
    Vector3 zidan1, zidan2;
    /// <summary>
    /// 游戏是否结束
    /// </summary>
    public bool isGameOver = false;
    /// <summary>
    /// 是否点击发射
    /// </summary>
    public bool isFashe = false;
    /// <summary>
    /// 计分
    /// </summary>
    public Text fenshu;
    /// <summary>
    /// 要发射的子弹
    /// </summary>
    GameObject FaDan;
    /// <summary>
    /// 结算
    /// </summary>
    public GameObject jiesuan;
    /// <summary>
    /// 拿到的折扣
    /// </summary>
    public float zheko;
    /// <summary>
    /// 
    /// </summary>
    public bool isdayin = false;
    public Image comname;
    float a = 0;
    int tim = 30;
    // Use this for initialization
    private void Awake()
    {
        cr = this;
    }
    void Start () {
        zidan1 = danyaocunchu.transform.GetChild(0).localPosition;
        zidan2 = danyaocunchu.transform.GetChild(1).position;
        fashe.onClick.AddListener(delegate() { fashefeidao(); });
        comname.sprite = CompanyManager.cm.logo.sprite;
	}
   public void fashefeidao() {
        if (!isGameOver)
        {
            fashe.GetComponent<AudioSource>().Play();
            FaDan = danyaocunchu.transform.GetChild(0).gameObject;
            danyaocunchu.transform.GetChild(1).localPosition = zidan1;
            Instantiate(zhen, zidan2, Quaternion.identity, danyaocunchu.transform);
            isFashe = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!isdayin)
        {
            if (!isGameOver)
            {
                fenshu.text = circle1.transform.childCount.ToString();
                circle1.transform.Rotate(0, 0, 1);
                circle2.transform.Rotate(0, 0, 1);
            }
            else
            {
                jiesuan.transform.GetChild(0).GetComponent<Image>().sprite = CompanyManager.cm.logo.sprite;
                jiesuan.transform.GetChild(1).GetComponent<Text>().text = CompanyManager.cm.desc.text;
                jiesuan.transform.GetChild(2).GetComponent<Text>().text = "得分：" + fenshu.text;
                if (int.Parse(fenshu.text) > 20)
                {
                    zheko = 9;
                }
                else if (int.Parse(fenshu.text) > 15)
                {
                    zheko = 9.5f;
                }
                else
                {
                    zheko = 0;
                }
                if (zheko == 0)
                {
                    jiesuan.transform.GetChild(3).GetComponent<Text>().text = "没有拿到折扣卷！";
                }
                else
                {
                    jiesuan.transform.GetChild(3).GetComponent<Text>().text = zheko + "折折扣卷";
                }
                jiesuan.SetActive(true);
                CompanyManager.cm.zheko.text = zheko.ToString();
                if (zheko != 0)
                {
                    ttlss.ts.PrintData(CompanyManager.cm.desc.text, zheko.ToString(), CompanyManager.cm.dizhi);
                }
                //PrintPhoto.Index.StartScreenShoot();
                isdayin = true;
            }
        }
        else {
            //if (ttlss.ss == "OK")
            //{
            //    Invoke("des", 2f);
            //}
            Invoke("des",5f);
        }
	}
    void des() {
        CompanyManager.cm.qiyongjianpan = true;
        Destroy(transform.gameObject);
    }
    void FixedUpdate() {
        
        if (isFashe)
        {
            FaDan.transform.position = Vector3.MoveTowards(FaDan.transform.position, circle1.transform.position, 15f);
        }
        if ((a += Time.fixedDeltaTime) >= 1 && !isGameOver) {
            a = 0;
            tim -= 1;
            daojishi.text = tim.ToString();
            if (tim <= 0) {
                isGameOver = true;
                //Time.fixedDeltaTime = 0;
            }
        }
    }
}
