using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ttlss : MonoBehaviour
{
    /// <summary>
    /// 串口端口
    /// </summary>
    public static ttlss ts;
    public GameObject set;
    public GameObject comko;
    public string COMM ="COM3";
    string huoqu = "";
    /// <summary>
    /// 打印机状态
    /// </summary>
    /// </summary>
    public string PrintZT="SOS";

    private void Awake()
    {
        for (int i = 0; i < SerialPort.GetPortNames().Length; i++)
        {
            GameObject go = Instantiate(comko,set.transform);
            go.transform.GetChild(0).GetComponent<Text>().text = SerialPort.GetPortNames()[i];
        }
        huoqu = PlayerPrefs.GetString("COM");
        if (huoqu != "") {
            COMM = PlayerPrefs.GetString("COM");
        }
        ts = this;
    }
    public void showset() {
        if (set.activeSelf) {
            set.SetActive(false);
        }
        else
        {
            set.SetActive(true);
        }
    }
    /// <summary>
    /// 打印文字
    /// </summary>
    /// <param name="Name">店名</param>
    /// <param name="ZK">折扣价</param>
    /// <param name="Dizhi">店铺地址</param>
    public void PrintData(string Name, string ZK, string Dizhi)
    {
        WenzhiShuru(Name + "h" + ZK + "h" + Dizhi + "h" + System.DateTime.Now + "h");
    }


    public static SerialPort port = new SerialPort();
    public static Thread tPort;
    // Use this for initialization
    void Start()
    {
        // print(SerialPort.GetPortNames().Length);
        port = new SerialPort(COMM, 9600);//(SerialPort.GetPortNames()[0], 9600);
        port.ReadTimeout = 500;
        port.WriteTimeout = 500;
        port.Open();

        tPort = new Thread(new ThreadStart(MenHomeTCP));
        tPort.IsBackground = true;
        tPort.Start();
        print("打开成功");

    }

    void Update()
    {
        GarbageCollection();//在不断接收数据的过程中可能清理垃圾

    }
    ASCIIEncoding ASCIIss = new ASCIIEncoding();
    public static string ss;
    void MenHomeTCP()
    {
        print("tcp");
        //  ss = "";
        while (port != null && port.IsOpen)
        {


            Thread.Sleep(1);

            try
            {
                print("进入高阻状态");
                byte[] st = new byte[1024];

                PrintZT = port.ReadLine();
                port.DiscardInBuffer();


                port.DiscardInBuffer();
                print(PrintZT);


            }
            catch (Exception)
            {

                print("yichang");
            }

        }
    }


    /// <summary>
    /// 垃圾回收
    /// </summary>
    private void GarbageCollection()
    {
        if (Time.frameCount % 120 == 0) System.GC.Collect();
    }


    private static byte[] HexStrTobyte(string hexString)
    {
        hexString = hexString.Replace(" ", "");
        if ((hexString.Length % 2) != 0)
            hexString += " ";
        byte[] returnBytes = new byte[hexString.Length / 2];
        for (int i = 0; i < returnBytes.Length; i++)
            returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2).Trim(), 16);
        return returnBytes;
    }



    /// <summary>
    /// 写入命令
    /// </summary>
    /// <param name="Hex">16进制的string</param>
    public void WrtiMingLing(string Hex)
    {
        byte[] st = HexStrTobyte(Hex);
        port.Write(st, 0, st.Length);
    }



    public void WenzhiShuru(string input)
    {
        Encoding utf8 = new UTF8Encoding();
        ASCIIEncoding ss = new ASCIIEncoding();
        UnicodeEncoding sst = new UnicodeEncoding();

        // port.WriteLine("___________________");
        // port.WriteLine("___________________");
        // byte[] ss = Encoding.UTF8.GetBytes("你好");
        //  port.Write(utf8.GetString(utf8.GetBytes(input)));

        port.Write(utf8.GetBytes(input), 0, utf8.GetBytes(input).Length);

        // port.WriteLine(ss.GetString(ss.GetBytes(input)));
        // port.Write(ss.GetBytes(input), 0,ss.GetBytes(input).Length);
        // port.Write(sst.GetBytes(input), 0, sst.GetBytes(input).Length);
        //  port.WriteLine(ss.GetString(ss.GetBytes(input)));
        //port.WriteLine("___________________");
        // port.WriteLine("___________________");
        // port.WriteLine(Encoding.UTF8.GetString(ss));
        print("打印成功");




    }



    private void OnDestroy()
    {
        port.Close();
        tPort.Abort();
        print("串口关闭");
    }
}


