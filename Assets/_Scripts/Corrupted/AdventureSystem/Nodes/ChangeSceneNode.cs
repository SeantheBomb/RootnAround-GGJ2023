using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XNode;
using Corrupted;



[NodeWidth(200), NodeTint(0, 70, 130), CreateNodeMenu("Scene/Change Scene")]

public class ChangeSceneNode : GraphNode
{

    public string sceneName;

    public bool openEyesOnCompleted = false;

    public bool reloadScene = true;



    protected override void Init()
    {
        base.Init();
        //hasPlayed = false;
    }

    public override IEnumerator PlayNode(SequenceSystemManager view)
    {
        
        //SceneManager.LoadScene(sceneName);
        if(SceneManager.GetActiveScene().name == sceneName && reloadScene == false)
        {
            Debug.Log("Sequence: Don't load this scene because we are already here!!");
            PlayNextInPath(view);
            yield break;
        }

        //SceneManager.sc

        //Blink.CloseEyes();


        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);



        yield return new WaitUntil(() => ao.isDone);//=> SceneManager.GetActiveScene().name == sceneName);//coroutine == null);
        yield return new WaitForSeconds(1f);

        //if(openEyesOnCompleted)
        //    Blink.OpenEyes();

        PlayNextInPath(view);
    }
}
