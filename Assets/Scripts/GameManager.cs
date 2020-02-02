using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> attachedPanels;
    int maxPanels;
    public int losePanels = 50;

    List<GameObject> destroyedPanels = new List<GameObject>();

    public static GameManager instance;

    public float gameTime;

    public GameObject player1;
    public GameObject player2;

    public GameObject endScreenPrefab;

    public PostProcessVolume postProcessing;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else if (instance != this)
        {
            Debug.Log("Multiple Game Objects", gameObject);
            Destroy(this);
        }


        maxPanels = attachedPanels.Count;

        StartCoroutine(TimeDestroy());
    }

    public void DestroyPanel()
    {
        //Randomly find a panel to destroy
        GameObject panelToDestroy = attachedPanels[Random.Range(0, (int)attachedPanels.Count)];
        attachedPanels.Remove(panelToDestroy);
        destroyedPanels.Add(panelToDestroy);
        panelToDestroy.GetComponent<Panel>().DestroyPanel();
        //panelToDestroy.GetComponent<Panel>().destroyParticle.Play();
    }

    public void RepairPanel(GameObject panel)
    {
        if (destroyedPanels.Contains(panel))
        {
            destroyedPanels.Remove(panel);
            attachedPanels.Add(panel);
            panel.GetComponent<MeshRenderer>().enabled = true;
            panel.GetComponent<Panel>().destroyed = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DestroyPanel();
        }
        if (Input.GetMouseButtonDown(0))
        {
            //RepairPanel(destroyedPanels[0]);
        }
    }
    
    IEnumerator TimeDestroy()
    {
        while (attachedPanels.Count > losePanels)
        {
            Debug.Log("Panels");
            if (Time.timeSinceLevelLoad > 1)
            {
                DestroyPanel();
                // yield return new WaitForSeconds((Mathf.Pow(((Time.timeSinceLevelLoad + 5) / 90) * 0.9f, -1) * 5));
                yield return new WaitForSeconds(Mathf.Max(10 - (Time.timeSinceLevelLoad / 8), 0.5f));
            }
            else
            {
                yield return 0;
            }
        }
        Debug.Log("You lose");
        StartCoroutine(GameOver());
        
    }

    IEnumerator GameOver()
    {
        //Disable the players
        player1.GetComponent<PlayerController>().alive = false;
        player2.GetComponent<PlayerController>().alive = false;

        Vignette vignetteLayer;

        postProcessing.profile.TryGetSettings(out vignetteLayer);

        float startingIntensity = vignetteLayer.intensity;

        //fade all cameras to black
        float elapsedTime = 0;
        float maxTime = 1f;

        while (elapsedTime < 1)
        {
            vignetteLayer.intensity.value = Mathf.Lerp(startingIntensity, 2 , (elapsedTime / maxTime));
            elapsedTime += Time.deltaTime;
            yield return 0;
        }
        
        //Spawn in the game over screen
        Instantiate(endScreenPrefab);
        
        while (elapsedTime > 0)
        {
            vignetteLayer.intensity.value = Mathf.Lerp(startingIntensity, 2, (elapsedTime / maxTime));
            elapsedTime -= Time.deltaTime;
            yield return 0;
        }

        for (int i = 0; i < 10; i++)
        {
            DestroyPanel();
        }

        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.25f);
            DestroyPanel();
        }

        yield return new WaitForSeconds(5);

        SceneManager.LoadScene(0);

    }


}
