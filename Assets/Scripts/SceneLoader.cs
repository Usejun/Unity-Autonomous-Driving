using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    public void Load(string sceneName, float delay = 1f)
    {
        StartCoroutine(DelayedSceneLoad(sceneName, delay));
    }

    IEnumerator DelayedSceneLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
