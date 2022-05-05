using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//namespace ConsoleApp1
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.WriteLine("Hello World!");
//        }
//    }
//}




//public interface IRewardItem
//{
//    Text GetNameText();
//    Button GetBtn();
//    Image GetIconImage();

//    void InitIconImage();
//    void InitBtn();
//    void InitNameText();
//}

public class BaseRewardItem //: IRewardItem
{
    public Transform transform;
    public int id;

    public BaseRewardItem(Transform transform, int id)
    {
        this.transform = transform;
        this.id = id;
        GetBtn().onClick.AddListener(OnBtnClicked);
    }

    public virtual Button GetBtn()
    {
        return transform.Find("Btn").GetComponent<Button>();
    }

    public virtual Image GetIconImage()
    {
        return transform.Find("ImgIcon").GetComponent<Image>();
    }

    public virtual Text GetNameText()
    {
        return transform.Find("TexName").GetComponent<Text>();
    }

    public virtual void OnBtnClicked()
    {
        //throw new NotImplementedException();
    }

    public virtual void InitIconImage()
    {
        GetIconImage().sprite = SomeAPI.GetRewardSprite(id);
    }

    public virtual void InitNameText()
    {
        GetNameText().text = SomeAPI.GetRewardName(id);
    }


    public void SomeBaseFunc()
    {
        InitIconImage();
        InitNameText();
    }
}


//public class ItemContainer
//{
//    public ItemContainer(IEnumerator datas, IEnumerator<BaseRewardItem> items)
//    {

//    }
//}



public class MyRewardItem : BaseRewardItem
{
    public object myData;

    public MyRewardItem(Transform transform, int id, object myData) : base(transform, id)
    {
        this.myData = myData;
    }

    public override Image GetIconImage()
    {
        return transform.Find("XXX/MyImg").GetComponent<Image>();
    }

    public override void InitNameText()
    {
        GetNameText().text = string.Format("x {0}", SomeAPI.GetRewardName(id));
    }

    public override void OnBtnClicked()
    {
        //do something
    }

    public void MyFunc()
    {
        var temp = this.myData;
        //do something
    }
}



public class SomeAPI
{
    public static string GetRewardName(int id)
    {
        throw new Exception();
    }

    public static Sprite GetRewardSprite(int id)
    {
        throw new Exception();
    }
}


public class Example : MonoBehaviour
{
    private void Awake()
    {
        var data = new object();
        MyRewardItem item1 = new MyRewardItem(transform.Find("Item1"), 1001, data);
        item1.SomeBaseFunc();
        item1.MyFunc();
    }
}