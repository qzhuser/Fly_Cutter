using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class CompanyManager : MonoBehaviour
{
    public GameObject buju1;
    public GameObject buju2;
    public GameObject com;
    public Transform gameparent;
    public GameObject error;
    /// <summary>
    /// 出现的游戏
    /// </summary>
    public GameObject[] game;
    
    public Image logo;
    public Text desc;
    public Text day;
    public Text zheko;
    public string dizhi;
    int i = 0;
    int j = 0;
    FileInfo[] files1;
    FileInfo[] files2;
    public static CompanyManager cm;
    // 储存获取到的图片  
    List<Texture2D> allTex2d = new List<Texture2D>();
    /// <summary>
    /// 用于键盘移动的二维数组
    /// </summary>
    GameObject[,] Companys1 = new GameObject[2,20];
    GameObject[,] Companys2 = new GameObject[2, 20];
    /// <summary>
    /// 游戏
    /// </summary>
    public GameObject GameName;
    public GameObject panel;
    /// <summary>
    /// 是否启用键盘
    /// </summary>
    public bool qiyongjianpan = true;
    //路径  
    string fullPath1,fullPath2;  //路径
    bool isArrayChange = true;
    private void Awake()
    {
        cm = this;

        fullPath1 = Application.dataPath + "//image1" + "//";
        fullPath2 = Application.dataPath + "//image2" + "//";
        //获取指定路径下面的所有资源文件  
            DirectoryInfo direction1 = new DirectoryInfo(fullPath1);
            files1 = direction1.GetFiles("*", SearchOption.AllDirectories);
            DirectoryInfo direction2 = new DirectoryInfo(fullPath2);
            files2 = direction2.GetFiles("*", SearchOption.AllDirectories);
    }
    void Start()
    {
         getFiles(files1,buju1.transform);
         getFiles(files2, buju2.transform);

         GetCompanys(Companys1,buju1);
         GetCompanys(Companys2, buju2);


        //for (int i = 0; i < Companys1.GetLength(0); i++)
        //{
        //    for (int j = 0; j < Companys1.GetLength(1); j++)
        //    {
        //        print(Companys1[i,j]+"   ("+i+","+j+")");
        //    }
        //}
    }
    void getFiles(FileInfo[] f,Transform tran) {
        for (int i = 0; i < f.Length; i++)
        {
            if (f[i].Name.Split('.')[f[i].Name.Split('.').Length - 1] != "meta")
            {
                GameObject go = Instantiate(com, tran);
                //获取资源名称
                go.name = f[i].Name.Split('.')[0];
                //创建Texture2d图片
                Texture2D t2d = new Texture2D(300, 372);
                //把字节转入jinTexture2D
                t2d.LoadImage(getImageByte(f[i].ToString()));
                //传换成Sprite图片
                Sprite sp = Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height), new Vector2(0.5f, 0.5f));
                go.transform.GetChild(0).GetComponent<Image>().sprite = sp;
            }
        }
    }
    void GetCompanys(GameObject [,] cs,GameObject buju) {
        int a = buju.transform.childCount;
        if (a % 2 == 0)
        {
            for (int i = 0; i < a / 2; i++)
            {
                cs[0, i] = buju.transform.GetChild(i).gameObject;
                cs[1, i] = buju.transform.GetChild(a/2+i).gameObject;
            }
        }
        else {
            for (int i = 0; i < a/2+1; i++)
            {
                cs[0, i] = buju.transform.GetChild(i).gameObject;
                if (i < a / 2)
                {
                    cs[1, i] = buju.transform.GetChild(a / 2+1+i).gameObject;
                }
            }
        }
    }
    void keyController1() {
        Companys1[i, j].transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        //Companys[i, j].transform.GetChild(0).GetComponent<Image>().color = Color.red;
        //上键
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (i != 0)
            {
                if (Companys1[i - 1, j] != null)
                {
                    huanyuan(buju1);
                    i -= 1;
                    Companys1[i, j].transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                    //Companys[i, j].transform.GetChild(0).GetComponent<Image>().color = Color.red;
                }
            }
            print(Companys1[i, j]);
        }
        //下键
        else if (Input.GetKeyDown(KeyCode.S))
        {

            if (i != 1)
            {
                if (Companys1[i + 1, j] != null)
                {
                    huanyuan(buju1);
                    i += 1;
                    Companys1[i, j].transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                    //Companys[i, j].transform.GetChild(0).GetComponent<Image>().color = Color.red;
                }
            }
            else {
                if (Companys2[0, j] != null) {
                    huanyuan(buju1);
                    isArrayChange = false;
                    i = 0;
                }
                //j = 0;
            }
            print(Companys1[i, j]);
        }
        //左键
        else if (Input.GetKeyDown(KeyCode.D))
        {
            
            if (j != 0) {
                if (Companys1[i, j - 1]!=null) {
                    huanyuan(buju1);
                    j -= 1;
                    Companys1[i, j].transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                    //Companys[i, j].transform.GetChild(0).GetComponent<Image>().color = Color.red;
                    if (Companys1[i, j].transform.GetComponent<RectTransform>().position.x < (panel.transform.GetComponent<RectTransform>().position.x - panel.transform.GetComponent<RectTransform>().rect.width / 2))
                    {
                       buju1.transform.GetComponent<RectTransform>().position = new Vector3(buju1.transform.GetComponent<RectTransform>().position.x + 130, buju1.transform.GetComponent<RectTransform>().position.y, buju1.transform.GetComponent<RectTransform>().position.z);
                    }
                }
            }
            
            print(Companys1[i, j]);
        }
        //右键
        else if (Input.GetKeyDown(KeyCode.F))
        {
            print(i+"  "+j);
            if (j != 9) {
                print(Companys1[i, j + 1]);
                if (Companys1[i, j + 1] != null) {
                    
                    huanyuan(buju1);
                    j += 1;
                    Companys1[i, j].transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                    //Companys[i, j].transform.GetChild(0).GetComponent<Image>().color = Color.red;
                    if (Companys1[i, j].transform.GetComponent<RectTransform>().position.x > (panel.transform.GetComponent<RectTransform>().position.x + panel.transform.GetComponent<RectTransform>().rect.width / 2))
                    {
                        buju1.transform.GetComponent<RectTransform>().position = new Vector3(buju1.transform.GetComponent<RectTransform>().position.x - 130, buju1.transform.GetComponent<RectTransform>().position.y, buju1.transform.GetComponent<RectTransform>().position.z);
                    }
                }
            }
            print(Companys1[i, j]);
        }//确定键
        else if (Input.GetKeyDown(KeyCode.W))
        {
            huanyuan(buju1);
            Companys1[i, j].GetComponent<ComClick>().click();
        }
    }
    void keyController2()
    {
        Companys2[i, j].transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        //Companys[i, j].transform.GetChild(0).GetComponent<Image>().color = Color.red;
        //上键
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (i != 0)
            {
                if (Companys1[i - 1, j] != null)
                {
                    huanyuan(buju2);
                    i -= 1;
                    Companys2[i, j].transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                    //Companys[i, j].transform.GetChild(0).GetComponent<Image>().color = Color.red;
                }
            }
            else {
                if (Companys1[1, j] != null)
                {
                    huanyuan(buju2);
                    i = 1;
                    //j = 0;
                    isArrayChange = true;
                }
            }
        }
        //下键
        else if (Input.GetKeyDown(KeyCode.S))
        {

            if (i != 1)
            {
                if (Companys2[i + 1, j] != null)
                {
                    huanyuan(buju2);
                    i += 1;
                    Companys2[i, j].transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                    //Companys[i, j].transform.GetChild(0).GetComponent<Image>().color = Color.red;
                }
            }
            print(Companys2[i, j]);
        }
        //左键
        else if (Input.GetKeyDown(KeyCode.D))
        {

            if (j != 0)
            {
                if (Companys2[i, j - 1] != null)
                {
                    huanyuan(buju2);
                    j -= 1;
                    Companys2[i, j].transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                    //Companys[i, j].transform.GetChild(0).GetComponent<Image>().color = Color.red;
                    if (Companys2[i, j].transform.GetComponent<RectTransform>().position.x < (panel.transform.GetComponent<RectTransform>().position.x - panel.transform.GetComponent<RectTransform>().rect.width / 2))
                    {
                        buju2.transform.GetComponent<RectTransform>().position = new Vector3(buju2.transform.GetComponent<RectTransform>().position.x + 130, buju2.transform.GetComponent<RectTransform>().position.y, buju2.transform.GetComponent<RectTransform>().position.z);
                    }
                }
            }

            print(Companys1[i, j]);
        }
        //右键
        else if (Input.GetKeyDown(KeyCode.F))
        {

            if (j != 9)
            {
                if (Companys2[i, j + 1] != null)
                {
                    huanyuan(buju2);
                    j += 1;
                    Companys2[i, j].transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                    //Companys[i, j].transform.GetChild(0).GetComponent<Image>().color = Color.red;
                    if (Companys2[i, j].transform.GetComponent<RectTransform>().position.x > (panel.transform.GetComponent<RectTransform>().position.x + panel.transform.GetComponent<RectTransform>().rect.width / 2))
                    {
                        buju2.transform.GetComponent<RectTransform>().position = new Vector3(buju2.transform.GetComponent<RectTransform>().position.x - 130, buju2.transform.GetComponent<RectTransform>().position.y, buju2.transform.GetComponent<RectTransform>().position.z);
                    }
                }
            }

            print(Companys2[i, j]);
        }//确定键
        else if (Input.GetKeyDown(KeyCode.W))
        {
            huanyuan(buju2);
            Companys2[i, j].GetComponent<ComClick>().click();
        }
    }
    public void huanyuan(GameObject buju) {
        for (int i = 0; i < buju.transform.childCount; i++)
        {
            //transform.GetChild(i).GetChild(0).GetComponent<Image>().color = Color.white;
            buju.transform.GetChild(i).localScale = new Vector3(2,2,2);
        }
    }
    private void Update()
    {
        if (ttlss.ts.PrintZT=="SOS")
        {
            error.SetActive(true);
            Time.timeScale = 0;
        }
        else {
            error.SetActive(false);
            Time.timeScale = 1;
        }
        if (qiyongjianpan)
        {
            if (isArrayChange)
            {
                keyController1();
            }
            else {
                keyController2();
            }
        }
        else {
            if (Input.GetKeyDown(KeyCode.W))
            {
                print("发射");
                if (GameName.name == "飞刀(Clone)")
                {
                    GameName.GetComponent<CircleRota>().fashefeidao();
                }
            }
        }
    }
    

    /// <summary>  
    /// 根据图片路径返回图片的字节流byte[]  
    /// </summary>  
    /// <param name="imagePath">图片路径</param>  
    /// <returns>返回的字节流</returns>  
    private static byte[] getImageByte(string imagePath)
    {
        FileStream files = new FileStream(imagePath, FileMode.Open);
        byte[] imgByte = new byte[files.Length];
        files.Read(imgByte, 0, imgByte.Length);
        files.Close();
        return imgByte;
    }
}
