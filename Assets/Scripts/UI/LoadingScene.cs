using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadingScene : MonoBehaviour
{
    public Image progressBar;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadAsincOperation());
    }

    float elapsedTime = 0;

    private void Update()
    {

        if (elapsedTime < 1)
        {
            elapsedTime += Time.deltaTime;
        }
    }

    IEnumerator LoadAsincOperation()
    {
        Debug.Log("Loading scene...");

        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(2);

        while (elapsedTime <= 1 && gameLevel.progress <= 1)
        {
            progressBar.fillAmount = elapsedTime;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(1.0f);
        //yield return new WaitForEndOfFrame();
    }
}
