using Models;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneInitialize : MonoBehaviour
{
    private string prefabPath = "Corridors/";
    private string jsonPath = "Json/";
    private Slider loadingSlider;
    private Text tipText;

    private void Awake()
    {
        var json = Resources.Load<TextAsset>( jsonPath + "json");
        var tipList = JsonUtility.FromJson<TipModel>(json.text);
    }

    void Start()
    {
        loadingSlider = GameObject.Find("LoadingSlider").GetComponent<Slider>();
        tipText = GameObject.Find("TipText").GetComponent<Text>();
        

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
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingSlider.value = progress;
            Debug.Log(progress);

            yield return null;
        }
    }
}
