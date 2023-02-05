using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextSceneOnStart : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
