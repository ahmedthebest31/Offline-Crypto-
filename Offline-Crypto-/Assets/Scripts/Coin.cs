using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Datum
{
    public override string ToString()
    {
        return "(" + Symbol.ToString() + ") " + FullName;   
    }
}