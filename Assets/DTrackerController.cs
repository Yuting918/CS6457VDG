using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DTrackerController : MonoBehaviour
{
    public MinionAI minionAI;
    public Renderer objectRenderer;
    public GameObject tracker;
    public Material blueMaterial;
    public Material redMaterial;

    

    // Start is called before the first frame update
    void Start()
    {
        objectRenderer.material = blueMaterial;
        // tracker.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = minionAI.predictedPosition;
        // if (minionAI.isChasing)
        // {
        //     tracker.SetActive(true);
        // }
        // else
        // {
        //     tracker.SetActive(false);
        // }

        if (minionAI.isCatached) 
        {
            objectRenderer.material = redMaterial;
        } else 
        {
            objectRenderer.material = blueMaterial;
        }
    }
}
