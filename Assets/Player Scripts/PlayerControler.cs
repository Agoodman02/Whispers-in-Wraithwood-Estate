using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    public static PlayerControler player;

    public GameObject CameraCenter;
    public bool DoCameraControl = true;
    public float PlayerWalkSpeed = 50f;
    public float PlayerReach = 5f;
    [SerializeField] public Vector2 MouseSensitivity = new Vector2(125, 125); //serialized for saving

    Vector2 MovementDir;
    Vector3 Movement3d;
    bool IsGrounded = false;
    Rigidbody selfphys;
    InputMap actions;
    RaycastHit LookingAt;
    Vector3 forward;

    private RaycastHit WasLooking;

    private float camrotx;
    private float camroty;

    //Inventory Stuff
    public InventoryManager inventoryManager;
    [HideInInspector] public bool allowPickup = false;

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
        if (player == null)
        {
            player = this;
        }
        else if (player != this)
        {
            Destroy(gameObject);
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {
        if (DoCameraControl)
        {
            float mousex = Input.GetAxisRaw("Mouse X") * MouseSensitivity.x;
            float mousey = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * MouseSensitivity.y;

            camroty += mousex;
            camrotx -= mousey;
            camrotx = Mathf.Clamp(camrotx, -89f, 89f);

            gameObject.transform.Rotate(Vector3.up, camroty);
            CameraCenter.transform.localEulerAngles = Vector3.right * camrotx;
        }

        MovementDir = actions.Player3D.Movement.ReadValue<Vector2>();

        Movement3d = (PlayerWalkSpeed * transform.forward * MovementDir.y) + (PlayerWalkSpeed * transform.right * MovementDir.x);
        Movement3d.y = selfphys.velocity.y;

        // it wasn't samdaman_og

        Ray ray = new Ray(CameraCenter.transform.position, CameraCenter.transform.forward);
        Physics.Raycast(ray, out LookingAt, PlayerReach);
        Debug.DrawRay(CameraCenter.transform.position, CameraCenter.transform.forward, Color.cyan);

        if (actions.Player3D.Interact.WasPressedThisFrame())
        {
            InteractPressed();
        }

        if (WasLooking.transform != null && LookingAt.transform != null | LookingAt.transform.gameObject != WasLooking.transform.gameObject)
        {
            if (WasLooking.transform != null && hasComponent<ItemPickup>(WasLooking.transform.gameObject))
            {
                WasLooking.transform.GetComponent<ItemPickup>().allowPickup = false;
                WasLooking.transform.GetComponent<ItemPickup>().DisablePickupPopup();
            }
            else if (hasComponent<InteractionTextPopup>(WasLooking.transform.gameObject))
            {
                WasLooking.transform.GetComponent<InteractionTextPopup>().DisableTextPopup();
            }
        }

        if (LookingAt.transform != null && LookingAt.transform.gameObject.layer == 6)
        {
            if (hasComponent<ItemPickup>(LookingAt.transform.gameObject))
            {
                LookingAt.transform.GetComponent<ItemPickup>().allowPickup = true;
                LookingAt.transform.GetComponent<ItemPickup>().EnablePickupPopup();
            }
            else if (hasComponent<InteractionTextPopup>(LookingAt.transform.gameObject))
            {
                Debug.Log("showing interact pop up for " + LookingAt.transform.gameObject.name);
                LookingAt.transform.GetComponent<InteractionTextPopup>().EnableTextPopup();
            }
        }
    }

    //We do movement updates here to avoid FPS impacting calculations
    void FixedUpdate()
    {
        selfphys.velocity = Movement3d;
    }

    void LateUpdate()
    {
        WasLooking = LookingAt;
    }

    void InteractPressed()
    {
        if (LookingAt.transform.name == null)
        {
            return;
        }

        Debug.Log(LookingAt.transform.gameObject.name);

        if (LookingAt.transform.gameObject.layer == 6)
        {
            LookingAt.transform.gameObject.GetComponent<InteractReciever>().Interacted();
        }
    }

    public bool hasComponent<T>(GameObject g)
    {
        return g.GetComponent<T>() != null;
    }

    public void TeleportPlayer(Vector3 p, Vector3 re, bool cam)
    {
        gameObject.transform.position = p;
        gameObject.transform.rotation = Quaternion.Euler(0, re.y, 0);
        CameraCenter.transform.localRotation = Quaternion.Euler(re.x, 0, 0);
        DoCameraControl = cam;
    }

    public void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UnlockCursor(bool confined)
    {
        Cursor.visible = true;
        if (confined)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}

