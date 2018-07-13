using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class Request : MonoBehaviour
{
#region static Instance
    public static Request Instance { set; get; }
    private void Awake()
    {
        Instance = this;
        RequestCoinList();
    }
    #endregion

    public void RequestCoinList()
    {
        WWW req = new WWW(CryptoCompare.endpoint2 + CryptoCompare.coinListUrl);
        StartCoroutine(OnReceiveCoinList(req));
    }

    private IEnumerator OnReceiveCoinList(WWW req)
    {
        yield return req;

        CoinListResponse resp = CoinListResponse.FromJson(req.text);
        foreach (Datum d in resp.Data.Values)
        {
            CoinManager.AddCoin(d);
        }
        FindObjectOfType<PortfolioScene>().Init();
    }

    public void RequestCoinUpdate(List<string> c)
    {
        string fsyms = "";
        foreach (string s in c)
            fsyms += s + ",";
        fsyms = fsyms.Remove(fsyms.Length - 1);

        WWW req = new WWW(CryptoCompare.endpoint2 + CryptoCompare.priceMulti + "?fsyms=" + fsyms + "&tsyms=USD,BTC,ETH");
        //Debug.Log(CryptoCompare.endpoint2 + CryptoCompare.priceMulti + "?fsyms=" + fsyms + "&tsyms=USD,BTC,ETH");
        StartCoroutine(OnReceiveCoinUpdate(req));
    }

    private IEnumerator OnReceiveCoinUpdate(WWW req)
    {
        yield return req;

        // Gotta fix the response text
        string fixedReq = string.Format("{0}{1}{2}", "{\"Prices\":", req.text, "}");

        PriceResponse obj = PriceResponse.FromJson(fixedReq);

        foreach(CoinPrice cp in obj.prices.Values) {
            Debug.Log("\nUSD:" + cp.USD +
            "\nBTC:" + cp.BTC +
            "\nETH:" + cp.ETH);
        }
 
    }

    public void RequestImage(string url, Texture2D tex)
    {
        WWW req = new WWW(url);
        StartCoroutine(OnReceiveImage(req, tex));
    }
    private IEnumerator OnReceiveImage(WWW req, Texture2D tex)
    {
        yield return req;
        req.LoadImageIntoTexture(tex);
    }
}
