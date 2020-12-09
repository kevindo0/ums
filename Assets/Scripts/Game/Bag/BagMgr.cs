using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagData {
    public string title;
    public string img;
    public int money;
    public bool isBuy;
}

public class BagMgr : SingletonBase<BagMgr>
{
    // 获取数据
    public List<BagData> InitItemsInfo()
    {
        JsonFileMgr<List<BagData>> f = new JsonFileMgr<List<BagData>>("bag.json");
        List<BagData>  datas = f.Decode();
        return datas;
    }

    public List<DecorateData> InitDecorateInfo()
    {
        JsonFileMgr<List<DecorateData>> f = new JsonFileMgr<List<DecorateData>>("Config/decorate.json");
        List<DecorateData> datas = f.Decode();
        return datas;
    }
}
