using UnityEngine;
using System.Collections;

public class GunsBank : MonoBehaviour 
{
    public static GunsBank S;
    public AudioClip[] sGunBank;

    void Awake()
    {
        S = this;
    }
}
