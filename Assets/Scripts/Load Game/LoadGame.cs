using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadGame : MonoBehaviour
{
    [SerializeField] private string SceneName;
    [SerializeField] private GameObject sceneToLoad;
    [SerializeField] private GameObject CanvasMenu;

    public void LoadSceneAsync()
    {
        StartCoroutine(LoadScreenCoroutine());
    }

    IEnumerator LoadScreenCoroutine()
    {
        var ecran = Instantiate(sceneToLoad);
        CanvasMenu.SetActive(false);
        DontDestroyOnLoad(ecran);

        var chargement = SceneManager.LoadSceneAsync("SCENE");
        chargement.allowSceneActivation = false;

        while (chargement.isDone == false)
        {
            if (chargement.progress >= 0.9f)
            {
                chargement.allowSceneActivation = true;
                //ecran.GetComponent<Animator>().SetTrigger("Disparition");
                Destroy(ecran);
            }

            yield return new WaitForSeconds(4);

        }

    }
}
