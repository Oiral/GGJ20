using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

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
            yield return new WaitForSeconds(5f - (5f * (attachedPanels.Count / (maxPanels / (Time.timeSinceLevelLoad / 50)))));
            DestroyPanel();
        }
        Debug.Log("You lose");
        StartCoroutine(GameOver());
        
    }

    IEnumerator GameOver()
    {
        //Disable the players
        player1.GetComponent<PlayerController>().alive = false;
        player2.GetComponent<PlayerController>().alive = false;
        
        //fade all cameras to black

        yield return new WaitForSeconds(2f);
        //Spawn in the game over screen
        Instantiate(endScreenPrefab);

    }


}
