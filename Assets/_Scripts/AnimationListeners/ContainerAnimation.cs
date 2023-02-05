using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ContainerAnimation : MonoBehaviour
{

    public RootContainer container;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        BirdRooting.OnStartRooting += StartRooting;
        BirdRooting.OnEndRooting += EndRooting;
        container.OnDepleted += OnDepleted;
        if (container.depleted)
        {
            OnDepleted();
        }
    }

    private void OnDestroy()
    {
        BirdRooting.OnStartRooting -= StartRooting;
        BirdRooting.OnEndRooting -= EndRooting;
        container.OnDepleted -= OnDepleted;
    }

    private void StartRooting(BirdRooting bird, RootContainer con)
    {
        if (con != container)
            return;
        anim.SetBool("IsRooting", true);
    }

    private void EndRooting(BirdRooting bird, RootContainer con)
    {
        if (con != container)
            return;
        anim.SetBool("IsRooting", false);
    }

    void OnDepleted()
    {
        anim.SetBool("IsDepleted", true);
    }


}
