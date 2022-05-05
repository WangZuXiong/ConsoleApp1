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

    public virtual Text GetCountText()
    {
        return transform.Find("TexCount").GetComponent<Text>();
    }

    public virtual void OnBtnClicked()
    {

    }

    public virtual void InitIconImage()
    {
        GetIconImage().sprite = StaticFuncLibrary.GetRewardSprite(id);
    }

    public virtual void InitNameText()
    {
        GetNameText().text = StaticFuncLibrary.GetRewardName(id);
    }

    public virtual void InitCountText()
    {
        GetNameText().text = string.Format("x{0}", UserInfo.GetRewardCount(id).ToString());
    }

    public void SomeBaseFunc()
    {
        InitIconImage();
        InitNameText();
    }

    public void Refresh()
    {
        InitCountText();
    }
}


public class ItemContainer
{
    //private BaseRewardItem[] items;
    private Dictionary<int, BaseRewardItem> itemDict = new Dictionary<int, BaseRewardItem>();


    public ItemContainer()
    {

    }

    public ItemContainer(Transform[] transforms, int[] ids)
    {

    }

    //public ItemContainer(int count)
    //{
    //    items = new BaseRewardItem[count];
    //}

    //public ItemContainer(IEnumerable datas, IEnumerable<BaseRewardItem> items)
    //{
    //    foreach (var item in datas)
    //    {

    //    }
    //}


    public void Refresh(int id)
    {
        if (itemDict.TryGetValue(id, out BaseRewardItem item))
        {
            item.Refresh();
        }
        else
        {
            throw new Exception("some tips");
        }
    }


    public static ItemContainer MakeContainer(Transform[] transforms, int[] ids)
    {
        if (transforms == null || ids == null)
        {
            throw new Exception("some tips");
        }


        if (transforms.Length != ids.Length)
        {
            throw new Exception("some tips");
        }

        int length = transforms.Length;
        ItemContainer result = new ItemContainer();
        for (int i = 0; i < length; i++)
        {
            BaseRewardItem item = new BaseRewardItem(transforms[i], ids[i]);
            item.SomeBaseFunc();
            result.itemDict.Add(ids[i], item);
        }

        return result;
    }

}



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
        GetNameText().text = string.Format("[{0}]", StaticFuncLibrary.GetRewardName(id));
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

public class UserInfo
{
    public static int GetRewardCount(int id)
    {
        throw new NotImplementedException();
    }
}

public static class StaticFuncLibrary
{
    public static string GetRewardName(int id)
    {
        throw new NotImplementedException();
    }

    public static Sprite GetRewardSprite(int id)
    {
        throw new NotImplementedException();
    }

    public static Transform[] GetChildren(this Transform transform)
    {
        Transform[] result = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            result[i] = transform.GetChild(i);
        }
        return result;
    }

    public static Transform CreateGameObject(string path)
    {
        throw new NotImplementedException();
    }

    public static Transform[] CreateGameObject(string path, int count)
    {
        throw new NotImplementedException();
    }
}


public class Example : MonoBehaviour
{
    private ItemContainer container1;

    private void Awake()
    {
        //
        BaseRewardItem item1 = new BaseRewardItem(transform.Find("Item1"), 1001);
        item1.SomeBaseFunc();
        //
        var myData = new object();
        MyRewardItem item2 = new MyRewardItem(transform.Find("Item2"), 1002, myData);
        item2.SomeBaseFunc();
        item2.MyFunc();
        //已经存在的item
        container1 = ItemContainer.MakeContainer(transform.Find("ItemContent").GetChildren(), GetIds());
        //支持动态生成的item
        var itemPath = "";
        var ids = GetIds(); 
        ItemContainer container2 = ItemContainer.MakeContainer(StaticFuncLibrary.CreateGameObject(itemPath, ids.Length), ids);
    }

    private void OnRewardInfoUpdate(int id)
    {
        container1.Refresh(id);
    }


    private int[] GetIds()
    {
        throw new NotImplementedException();
    }
}