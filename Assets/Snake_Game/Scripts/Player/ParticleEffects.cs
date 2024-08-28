using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffects : MonoBehaviour
{
    public static ParticleEffects instance;
    public GameObject confetti;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        confetti.SetActive(false);
    }

    public void ConfettiEnable()
    {
        confetti.SetActive(true);
    }
   
}
