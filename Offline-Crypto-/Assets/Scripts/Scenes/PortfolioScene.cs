using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortfolioScene : MonoBehaviour
{
    private const float UPDATE_RATE = 5.0f;

    public static PortfolioScene instance;
    private void Awake()
    {
        instance = this;
    }

    public List<string> symbols;

    public Transform portfolioCoinContainer;
    public GameObject portfolioCoinPrefab;
    public List<Datum> activeDatum = new List<Datum>();

    // Update
    private float lastUpdate;
    private float updateCooldown = 5.0f;

    private void Start()
    {
        string f = "{ \"ETH\":{ \"BTC\":0.04675,\"USD\":465.51,\"EUR\":389.82},\"DASH\":{ \"BTC\":0.06156,\"USD\":609.12,\"EUR\":519.14} }";
        PriceResponse pr = PriceResponse.FromJson(f);
    }

    public void Init()
    {
        foreach (string s in symbols)
            CreateCoinEntry(s);
    }

    private void Update()
    {
        if (Time.time - lastUpdate > UPDATE_RATE)
        {
            lastUpdate = Time.time;
            UpdateCoinEntry();
        }
    }

    private void CreateCoinEntry(string symbol)
    {
        GameObject go = Instantiate(portfolioCoinPrefab);
        go.transform.SetParent(portfolioCoinContainer);

        Texture2D t = new Texture2D(128,128);
        string url = CryptoCompare.endpoint1 + CoinManager.allCoins.Find(x => x.Symbol == symbol).ImageUrl;
        if (url == null) return;
        go.GetComponentInChildren<RawImage>().texture = t;
        Request.Instance.RequestImage(url, t);
    }
    private void UpdateCoinEntry()
    {
        Request.Instance.RequestCoinUpdate(symbols);
    }
}
