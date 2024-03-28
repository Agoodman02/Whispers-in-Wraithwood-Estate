using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoadTrigger : MonoBehaviour
{
    [Tooltip("If there are errors in the console about the scene loading go to File > Build Setting > Add Open Scenes")]
    public string SceneToLoad;
    [Tooltip("Add a collider to the object and set 'IsTrigger'")]
    public Collider ScenePreloadArea;


    // Start is called before the first frame update
    void start()
    {
        if(SceneToLoad == null)
        {
            Debug.LogWarning("Scene To Load on " + gameObject.name + " is missing! Please go back to editor and assign a scene");
            Destroy(gameObject);
        }

        if(ScenePreloadArea == null)
        {
            if(gameObject.GetComponent<Collider>() == null)
            {
                ScenePreloadArea = gameObject.GetComponent<Collider>();
            }
            else
            {
                ScenePreloadArea = new BoxCollider();
            }
            ScenePreloadArea.isTrigger = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered trigger");
        if (other.gameObject.layer == 3)
        {
            Debug.Log("Something is player, loading scene " + SceneToLoad);
            SceneManager.LoadSceneAsync(SceneToLoad, LoadSceneMode.Additive);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Something exited trigger");
        if (other.gameObject.layer == 3)
        {

            Debug.Log("Something is player, unloading scene " + SceneToLoad);
            SceneManager.UnloadSceneAsync(SceneToLoad);
        }
    }
}
