using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Data
{
    public int rate;
    public string name;
    public int unixtime;
}

[Serializable]
public class ListData
{
    public List<Data> items;
}
