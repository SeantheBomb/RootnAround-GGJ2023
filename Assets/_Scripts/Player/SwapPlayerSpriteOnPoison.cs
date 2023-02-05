using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BirdPoisonEffector))]
public class SwapPlayerSpriteOnPoison : MonoBehaviour
{

    

    public GameObject healthy, sick;


    BirdPoisonEffector bird;


    // Start is called before the first frame update
    void Start()
    {
        bird = GetComponent<BirdPoisonEffector>();
        SetHealthy();
        bird.OnPoisonStart += SetPoison;
        bird.OnPoisonStop += SetHealthy;
    }

    private void OnDestroy()
    {
        bird.OnPoisonStart -= SetPoison;
        bird.OnPoisonStop -= SetHealthy;
    }


    public void SetPoison()
    {
        SetIsPoisoned(true);
    }

    public void SetHealthy()
    {
        SetIsPoisoned(false);
    }

    public void SetIsPoisoned(bool value)
    {
        healthy.SetActive(value == false);
        sick.SetActive(value);
    }
}
