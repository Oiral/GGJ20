using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{

    public int rows;
    public int collumns;

    public float radius;

    public List<GameObject> spawnedNodes;

    public GameObject nodePrefab;

    private void Awake()
    {
        float xOffset = 360 / collumns;
        float yOffset = 360 / rows;
        Debug.Log(yOffset);

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < 1; x++)
            {
                Vector3 pos = Vector3.zero;

                //Node prefab to choose
                int prefabChoice = y;
                prefabChoice = prefabChoice - ((rows / 2));
                prefabChoice = Mathf.Abs(prefabChoice);

                //Debug.Log(prefabChoice);

                GameObject spawnedObject = Instantiate(nodePrefab, pos, Quaternion.identity, transform);

                float zRot = 0f;//y > rows / 2 ? 180 : 0;

                Quaternion rotation = Quaternion.Euler(new Vector3((yOffset * y) + (yOffset / 2) + 90, 0, zRot));
                spawnedObject.transform.rotation = rotation;

                spawnedObject.GetComponent<Node>().panelAttachPoint.transform.localPosition = new Vector3(0, 0, radius / 2);
                spawnedObject.GetComponent<Node>().rowNum = prefabChoice;
                spawnedObject.GetComponent<Node>().radius = radius;
            }


        }
        /*
            ;
            spawnedObject.transform.LookAt(, Vector3.up);
            spawnedNodes.Add(spawnedObject);
            */
    }
}
