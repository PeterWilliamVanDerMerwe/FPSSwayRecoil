using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RayCastInteractor : MonoBehaviour
{
    public static RayCastInteractor Instance { get; private set; }

    [SerializeField] private new Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CenterOfScreenRaycast();
    }

    public string CenterOfScreenRaycast()
    {
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //Just a ray through the middle of your current view
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2))
        {
            print(hit.collider.tag);
            return hit.collider.tag;
        }

        return null;
    }
}
