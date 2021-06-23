using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoading : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    [SerializeField] private Text progressPercent;


    private void Start()
    {
        //start async operation
        StartCoroutine(LoadAsyncOperation());
    }

    IEnumerator LoadAsyncOperation()
    {
        //create async operation
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(GameManager.instance.LevelToLoad + 1);

        while(asyncLoad.progress < 1)
        {
            //take progress fill bar = async operation progress
            progressBar.fillAmount = asyncLoad.progress;
            progressPercent.text = (asyncLoad.progress * 100) + "%";
            yield return new WaitForEndOfFrame();
        }

        //when finished, load the game scene

    }
}
