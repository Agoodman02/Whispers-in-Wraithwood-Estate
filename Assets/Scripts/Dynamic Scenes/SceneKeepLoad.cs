using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneKeepLoad : MonoBehaviour
{

#if UNITY_EDITOR

    [Header("Thsi script needs to be the parrent object of everything in the scene and be at world 0 0 0")]

    [InspectorButton("SetField")]
    public bool SetTrigger;

    public Bounds GroupBoundingbox;

    BoxCollider BoundTrigger;

    private void SetField()
    {
        Debug.Log("Generating scene bounds for keep load trigger");

        Transform[] transinscene = gameObject.GetComponentsInChildren<Transform>();

        foreach (Transform Tf in transinscene)
        {
            if (Tf != this.gameObject.transform)
            {
                GameObject go = Tf.gameObject;

                if (go.GetComponent<Renderer>() != null)
                {
                    GroupBoundingbox.Encapsulate(go.GetComponent<Renderer>().bounds);
                }
                else
                {
                    GroupBoundingbox.Encapsulate(go.transform.position);
                }

                Debug.Log("Adding " + go.name + " to bounds");
            }
        }

        if (gameObject.GetComponent<BoxCollider>() != null)
        {
            BoundTrigger = gameObject.GetComponent<BoxCollider>();
        }
        else
        {
            BoundTrigger = gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;
        }

        BoundTrigger.center = GroupBoundingbox.center;
        BoundTrigger.size = GroupBoundingbox.extents;
        BoundTrigger.isTrigger = true;
    }

#endif
}
