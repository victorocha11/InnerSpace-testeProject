using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneInitialize : MonoBehaviour
{
    private string prefabPath = "Corridors/";
    private string jsonPath = "Json/";
    // Start is called before the first frame update
    void Start()
    {
        //GameObject[] a = Resources.LoadAll<GameObject>(prefabPath) as GameObject[];
        //foreach (var aux in a)
        //{
        //    Instantiate(aux);
        //}

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadSceneAsync(sceneIndex));
    }

    IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            Debug.Log(operation.progress);

            yield return null;
        }
    }
}
