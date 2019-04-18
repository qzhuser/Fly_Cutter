using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LCPrinter;
using UnityEngine.UI;
using System;
using System.Diagnostics;

public class PrintPhoto : MonoBehaviour
{
    public Camera cam;
    //public Image logo;
    //public Text company;
    public Texture2D ss;
    public Rect rect;

    public static PrintPhoto Index;
    PrintPhoto()
    {
        Index = this;

    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.UpArrow)) {
        //    StartScreenShoot();
        //}
    }
    public void StartScreenShoot()
    {
        /*
        //System.Diagnostics.Process.Start("IEXPLORE.EXE", "http://www.west163.net");

        Texture2D screenShot = new Texture2D(800, 800, TextureFormat.ARGB32, false);
        screenShot.ReadPixels(rect, 0, 0);
        screenShot.Apply();
        ss = screenShot;
        byte[] bytes = screenShot.EncodeToPNG();
        string filename = Application.dataPath + "/123.png";
        System.IO.File.WriteAllBytes(filename, bytes);
        // Print.PrintTexture(screenShot.EncodeToPNG(), 1, "");
        // Invoke("Num",2f);
        */
        CaptureCamera(cam, rect);
    }

    //public void Test()
    //{
    //    string path = "D:\\123.png,0,0,100,100";//从纸张的0. 0点，将图像调整为750×350点（计算：150mm／28.346 px/cm＝529点，100mm／28.346 pm／cm＝352点） 图片路径
    //    string exepath = Application.streamingAssetsPath + @"\PrintImage.exe";//这个是需要下载的应用直接放到电脑上就行(调用打印机打印图片应用的路径)
    //    ProcessStartInfo info = new ProcessStartInfo(exepath);//指定启动进程时使用的一组值
    //    info.Arguments = path;//获取或设置启动应用程序时要使用的一组命令行自变量
    //    using (Process p = new Process())
    //    {
    //        p.StartInfo = info;
    //        p.Start();
    //    }
    //}

    public string printerName = "";
    public void Num()
    {
        string filename ="D:\\123.png";
        //if (ttlss.ss == "SOS") {
        //    CompanyManager.cm.error.SetActive(true);
        //}
        //else
        //{
        //    CompanyManager.cm.error.SetActive(false);
            Print.PrintTextureByPath(filename.Trim(), 1, printerName);
        //}
    }


    // <summary>  
    /// 对相机截图。   
    /// </summary>  
    /// <returns>The screenshot2.</returns>  
    /// <param name="camera">Camera.要被截屏的相机</param>  
    /// <param name="rect">Rect.截屏的区域</param>  
    Texture2D CaptureCamera(Camera camera, Rect rect)
    {
        // 创建一个RenderTexture对象  
        RenderTexture rt = new RenderTexture(2000, 2000, 0);
        // 临时设置相关相机的targetTexture为rt, 并手动渲染相关相机  
        camera.targetTexture = rt;
        camera.Render();
        //ps: --- 如果这样加上第二个相机，可以实现只截图某几个指定的相机一起看到的图像。  
        //ps: camera2.targetTexture = rt;  
        //ps: camera2.Render();  
        //ps: -------------------------------------------------------------------  

        // 激活这个rt, 并从中中读取像素。  
        RenderTexture.active = rt;
        Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(rect, 0, 0);// 注：这个时候，它是从RenderTexture.active中读取像素  
        screenShot.Apply();

        // 重置相关参数，以使用camera继续在屏幕上显示  
        camera.targetTexture = null;
        //ps: camera2.targetTexture = null;  
        RenderTexture.active = null; // JC: added to avoid errors  
        GameObject.Destroy(rt);
        // 最后将这些纹理数据，成一个png图片文件  
        byte[] bytes = screenShot.EncodeToPNG();
        string filename = "D:\\123.png";
        System.IO.File.WriteAllBytes(filename, bytes);
        print(string.Format("截屏了一张照片: {0}", filename));
        Invoke("Num", 2f);
        return screenShot;
    }
}
