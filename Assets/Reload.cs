using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour
{
    public string SceneName;

    public void OnRestart()
    {
        GameObject mask = GameObject.FindGameObjectWithTag("Mask");
        LeanTween.scale(mask, new Vector3(0, 0, 0), 0.5f);
        StartCoroutine(ReloadLevel(0.6f));
    }

    private IEnumerator ReloadLevel(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneName);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
