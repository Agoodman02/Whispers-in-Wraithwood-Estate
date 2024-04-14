using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneKeepLoad : MonoBehaviour
{
    public string ScenePath;
    public float DistanceToUnload;
    [Header("Center May not be exact and may need some small adjustments")]
    public Bounds GroupBoundingbox;

#if UNITY_EDITOR
    [Header("Thsi script needs to be the parrent object of everything in the scene and in bounds")]

    [InspectorButton("SetField")]
    public bool SetBounds;

    BoxCollider BoundTrigger;

    private List<Vector3> centers;

    private void SetField()
    {
        Debug.Log("Generating scene bounds for Unloading");

        GroupBoundingbox.extents = new Vector3(0, 0, 0);
        GroupBoundingbox.center = new Vector3(0, 0, 0);

        Transform[] transinscene = gameObject.GetComponentsInChildren<Transform>();

        //please dont look at my awful code it's done this way because Bounds.Encapsulate is weird
        foreach (Transform Tf in transinscene)
        {
            if (Tf != this.gameObject.transform)
            {
                GameObject go = Tf.gameObject;

                if (go.GetComponent<Renderer>() != null)
                {
                    go.transform.position = go.transform.localPosition;

                    Debug.Log("Adding " + go.name + " to bounds");
                    GroupBoundingbox.Encapsulate(go.GetComponent<Renderer>().bounds);

                    go.transform.localPosition = go.transform.position;

                    centers.Add(go.GetComponent<Renderer>().bounds.center);
                }
                else
                {
                    go.transform.position = go.transform.localPosition;

                    centers.Add(go.transform.position);

                    Debug.Log("Adding " + go.name + " Point to bounds");
                    GroupBoundingbox.Encapsulate(go.transform.position);

                    go.transform.localPosition = go.transform.position;

                    
                }
            }
        }

        //GroupBoundingbox.extents *= 2;

        Vector3 averagecenter;
        averagecenter = new Vector3(0, 0, 0);
        foreach(Vector3 v3 in centers)
        {
            averagecenter += v3;
        }
        averagecenter = averagecenter / centers.Count;

        GroupBoundingbox.center = averagecenter;
    }

    // Draws a wireframe box around the selected object,
    // indicating world space bounding volume.
    public void OnDrawGizmosSelected()
    {
        Gizmos.matrix = Matrix4x4.identity;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(GroupBoundingbox.center, GroupBoundingbox.extents * 2);
    }
#endif

    //we call late update because we want to know the players distance before the next frame
    void LateUpdate()
    {
        float distance = Vector3.Distance(GroupBoundingbox.ClosestPoint(PlayerControler.player.transform.position), PlayerControler.player.transform.position);

        Debug.Log("Player Distance to " + SceneManager.GetSceneByPath(ScenePath).name + " is " + distance);

        if (Mathf.Abs(distance) >= DistanceToUnload)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByPath(ScenePath));
        }
    }
}
