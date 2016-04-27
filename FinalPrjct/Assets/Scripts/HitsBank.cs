using UnityEngine;
using System.Collections;

public class HitsBank : MonoBehaviour 
{
    public static HitsBank S;
    public AudioClip[] sHitBank;

    public void Awake()
    {
        S = this;
    }
}
