using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    public GameObject CameraCenter;
    public bool DoCameraControl = true;
    public float PlayerWalkSpeed = 50f;
    public float PlayerReach = 5f;
    public Vector2 MouseSensitivity = new Vector2(125, 125);

    Vector2 MovementDir;
    Vector3 Movement3d;
    bool IsGrounded = false;
    Rigidbody selfphys;
    InputMap actions;
    RaycastHit LookingAt;

    private float camrotx;
    private float camroty;

    // Start is called before the first frame update
    void Start()
    {
        //Just got to make sure
        if (CameraCenter == null)
        {
            Debug.LogError("Null Object Exception: Please assign the camera center on the player controller");
        }

        selfphys = gameObject.GetComponent<Rigidbody>();

        //activate actions
        actions = new InputMap();
        
        actions.Player3D.Enable();
    }

    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {
        MovementDir = actions.Player3D.Movement.ReadValue<Vector2>();

        Movement3d = gameObject.transform.forward * MovementDir.y + gameObject.transform.right * MovementDir.x;
        Movement3d.y = selfphys.velocity.y;

        if (DoCameraControl)
        {
            float mousex = Input.GetAxisRaw("Mouse X") * Time.deltaTime * MouseSensitivity.x;
            float mousey = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * MouseSensitivity.y;

            camroty += mousex;
            camrotx -= mousey;
            Mathf.Clamp(camrotx, -89f, 89);

            gameObject.transform.rotation = Quaternion.Euler(0, camroty, 0);
            CameraCenter.transform.rotation = Quaternion.Euler(camrotx, camroty, 0);
        }

        // it wasn't samdaman_og

        Ray ray = new Ray(CameraCenter.transform.position, CameraCenter.transform.forward);
        Physics.Raycast(ray, out LookingAt, PlayerReach);

        if (actions.Player3D.Interact.WasPressedThisFrame())
        {
            InteractPressed();
        }
    }

    //We do movement updates here to avoid FPS impacting calculations
    void FixedUpdate()
    {
        selfphys.velocity = Movement3d;
    }

    void InteractPressed()
    {
        if(LookingAt.transform.name == null)
        {
            return;
        }

        Debug.Log(LookingAt.transform.gameObject.name);

        if(LookingAt.transform.gameObject.layer == 6)
        {
            LookingAt.transform.gameObject.GetComponent<InteractReciever>().Interacted();
        }
    }
}
