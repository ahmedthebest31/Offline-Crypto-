using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPriceManager : MonoSingleton<CoinPriceManager>
{
	public static string endpoint1 = "https://www.cryptocompare.com";
	public static string endpoint2 = "https://min-api.cryptocompare.com";

	public static string coinListUrl = "/data/all/coinlist";
	public static string price = "/data";
	public static string priceMulti = "/data/pricemulti";

	// Events
	public event HandleCoinListBuilt CoinListBuilt;
	public delegate void HandleCoinListBuilt();

	public event HandleCoinListUpdate CoinListUpdate;
	public delegate void HandleCoinListUpdate();

	// static members
	public static List<Datum> allCoins = new List<Datum>();
	public static void AddCoin(Datum c)
	{
		allCoins.Add(c);
	}
		
	#region API calls
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
			AddCoin(d);
		}

        CoinListBuilt();
    }

	public void RequestCoinUpdate(List<string> c)
	{
		string fsyms = "";
		foreach (string s in c)
			fsyms += s + ",";
		fsyms = fsyms.Remove(fsyms.Length - 1);

		WWW req = new WWW(CryptoCompare.endpoint2 + CryptoCompare.priceMulti + "?fsyms=" + fsyms + "&tsyms=USD,BTC,ETH");
		StartCoroutine(OnReceiveCoinUpdate(req,c));
	}
	private IEnumerator OnReceiveCoinUpdate(WWW req,List<string> c)
	{
		yield return req;

		// Gotta fix the response text
		string fixedReq = string.Format("{0}{1}{2}", "{\"Prices\":", req.text, "}");

        Debug.Log(fixedReq);
		PriceResponse obj = PriceResponse.FromJson(fixedReq);

        int i = 0;
		foreach(CoinPrice cp in obj.prices.Values) {
            allCoins.Find(x => x.Symbol == c[i]).Price = cp;
            i++;
		}

        CoinListUpdate();
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
	#endregion
}