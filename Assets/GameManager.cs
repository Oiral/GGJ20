using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> attachedPanels;
    int maxPanels;
    public int losePanels;

    List<GameObject> destroyedPanels = new List<GameObject>();

    public static GameManager instance;

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DestroyPanel();
        }
        if (Input.GetMouseButtonDown(0))
        {
            RepairPanel(destroyedPanels[0]);
        }
    }
}
