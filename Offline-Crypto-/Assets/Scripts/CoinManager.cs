using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoinManager
{
    public static List<Datum> allCoins = new List<Datum>();

    public static void AddCoin(Datum c)
    {
        allCoins.Add(c);
     //   Debug.Log("Added \"" + c.ToString() + "\" to the coin list");
    }
}
