using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleCoin : MonoBehaviour
{
    public string Symbol { set; get; }

    public void OnDeleteClick()
    {
        SaveState.Instance.RemoveCoinFromPortfolio(Symbol);
    }
}
