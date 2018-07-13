using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This is for the physical coin object, used in the canvas
public class CoinUI : MonoBehaviour
{
    public string Symbol { set; get; }
    public Datum Datum { set; get; }

    private Text[] texts;

    public void Init(string symbol)
    {
        Symbol = symbol;
        texts = GetComponentsInChildren<Text>();
    //    RequestImage();
        UpdateText();
    }

    private void RequestImage()
    {
        Texture2D t = new Texture2D(128, 128);
        string url = CryptoCompare.endpoint1 + Datum.ImageUrl;
        if (url == null) return;
        gameObject.GetComponentInChildren<RawImage>().texture = t;
        CoinPriceManager.Instance.RequestImage(url, t);
    }

    public void UpdateText()
    {
        Datum d = CoinPriceManager.allCoins.Find(x => x.Symbol == Symbol);
        texts[0].text = d.Symbol;
        if (d.Price != null)
        {
            texts[1].text = d.Price.BTC.ToString() + " BTC";
            texts[2].text = d.Price.USD.ToString() + " USD";
        }
    }
}
