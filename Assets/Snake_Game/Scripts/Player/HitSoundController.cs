using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSoundController : MonoBehaviour
{
    public void Hit()
    {
        AudioManager.instance.Hit();
    }
    
}
