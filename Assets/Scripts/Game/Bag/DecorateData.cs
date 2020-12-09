using System.Collections;
using System.Collections.Generic;

public class DecorateDataItem
{
    public int id;
    public string objname;
    public string image;
    public bool isBuy;
    public int money;
}

public class DecorateData
{
    public string title;
    public int useId;
    public List<DecorateDataItem> items;
}
