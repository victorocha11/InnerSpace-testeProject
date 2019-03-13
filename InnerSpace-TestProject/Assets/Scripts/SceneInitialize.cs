using Models;
using Newtonsoft.Json;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneInitialize : MonoBehaviour
{
    private string jsonPath = "Json/tips";
    private Slider loadingSlider;
    private Text tipText;
    private Text progressText;
    private GameObject canvas;
    private float timeFollowCamera;
    private float timeChangeTip;
    private TipModel tipList;

    private void Awake()
    {
        LoadTipInfo();
        
    }

    void Start()
    {
        LoadObjects();

        ChangeTip();
    }

    private void Update()
    {
        UpdateMenuPosition();
        if (timeChangeTip + 3f < Time.time)
        {
            ChangeTip();
            timeChangeTip = Time.time;
        }
    }

    private void UpdateMenuPosition()
    {
        if (timeFollowCamera + 0.5f < Time.time)
        {
            var camPos = Camera.main.transform.position + Camera.main.transform.forward * 9;
            canvas.transform.position = camPos;
            timeFollowCamera = Time.time;
        }
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
            progressText.text = progress * 100f + "%";
            loadingSlider.value = progress;
            Debug.Log(progress);

            yield return null;
        }

    }

    private void LoadObjects()
    {
        loadingSlider = GameObject.Find("LoadingSlider").GetComponent<Slider>();
        tipText = GameObject.Find("TipText").GetComponent<Text>();
        progressText = GameObject.Find("ProgressText").GetComponent<Text>();
        canvas = GameObject.Find("MainCanvas");
        timeChangeTip = timeFollowCamera = Time.time;
    }

    private void LoadTipInfo()
    {
        var json = Resources.Load<TextAsset>(jsonPath);
        var t = System.Text.Encoding.ASCII.GetString(json.bytes);
        tipList = JsonConvert.DeserializeObject<TipModel>(t);
    }

    private void ChangeTip()
    {
        tipText.text = tipList.Tips[UnityEngine.Random.Range(0, tipList.Tips.Count - 1)];
    }

}
