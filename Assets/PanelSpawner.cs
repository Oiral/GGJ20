using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSpawner : MonoBehaviour
{
    public float spawnTime = 5f;

    float timer = 1f;

    public GameObject panelPrefab;

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
                SpawnPanel();
            }
        }
    }

    void SpawnPanel()
    {
        GameObject spawnedPanel = Instantiate(panelPrefab, transform.position, transform.rotation, null);
        if (GetComponent<Panel>() != null)
        {

        }
    }

    public void CollectPanel()
    {
        timer = spawnTime;
    }
}
