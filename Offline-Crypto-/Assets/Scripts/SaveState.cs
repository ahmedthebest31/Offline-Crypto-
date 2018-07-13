using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveState : MonoSingleton<SaveState>
{
	public List<string> coinList = new List<string>();

    private void Awake()
    {
        coinList = LoadCoinList();
    }

    public void AddCoinToPortfolio(string symbol)
	{
        if (coinList.Find(x => x == symbol) == null)
        {
            coinList.Add(symbol);
            SaveCoin();
        }
	}
    public void RemoveCoinFromPortfolio(string symbol)
    {
        if (coinList.Contains(symbol))
        {
            coinList.Remove(symbol);
            SaveCoin();
        }
    }

    #region save&load
    private List<string> LoadCoinList()
    {
        List<string> r = new List<string>();
        string[] data = PlayerPrefs.GetString("coinList").Split('|');
        foreach (string d in data)
        {
            r.Add(d);
        }

        return r;
    }
    private void SaveCoin()
    {
        string savedCoin = "";
        foreach(string d in coinList)
        {
            savedCoin += d + '|';
        }

        savedCoin = savedCoin.Remove(savedCoin.Length - 1, 1);
        PlayerPrefs.SetString("coinList", savedCoin);
    }
    #endregion
}
