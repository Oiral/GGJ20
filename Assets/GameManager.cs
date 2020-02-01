using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> attachedPanels;
    int maxPanels;
    public int losePanels = 50;

    List<GameObject> destroyedPanels = new List<GameObject>();

    public static GameManager instance;

    public float gameTime;

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
        panelToDestroy.GetComponent<MeshRenderer>().enabled = false;
        panelToDestroy.GetComponent<Panel>().destroyed = true;
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
        
    }
}
