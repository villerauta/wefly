using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanDetector : MonoBehaviour
{
    public AudioSource oceanAudio;

    private List<Vector3> vector3List = new List<Vector3>();
    private const float RADIUS = 20f;
    private const float HEIGHT = 20f;

    int layerMask;
    int waterMask = 1 << 4;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("Default", "Water");
        InitializeDetectors();
        InvokeRepeating("CheckForOcean", 0f, 1f);
    }

    void InitializeDetectors()
    {
        vector3List.Add(new Vector3(0f, HEIGHT, RADIUS));
        vector3List.Add(new Vector3(0f, HEIGHT, -RADIUS));
        vector3List.Add(new Vector3(RADIUS, HEIGHT, 0f));
        vector3List.Add(new Vector3(-RADIUS, HEIGHT, 0f));

    }

    // Update is called once per frame
    void Update()
    {

        foreach (Vector3 point in vector3List)
        {
            Vector3 origin = transform.position + point;
            Debug.DrawRay(origin, Vector3.down * 10f, Color.red);
        }
        return;

        Collider[] boxColliders = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity, layerMask);

        if (boxColliders.Length > 0)
        {
            ShoreArea shoreArea = boxColliders[0].GetComponent<ShoreArea>();
            if (shoreArea)
            {
                Debug.Log("ShoreArea");
                RaycastHit hit;
                Debug.DrawRay(transform.position, Vector3.down);
                if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, waterMask))
                {
                    Debug.Log("Near Shore, distance to bedrock vertically is " + hit.distance.ToString());
                }
            }
        }
    }

    public void CheckForOcean()
    {
        bool waterFound = false;
        foreach (Vector3 point in vector3List)
        {
            Vector3 origin = transform.position + point;
            RaycastHit hit;
            if (Physics.Raycast(origin, Vector3.down, out hit, Mathf.Infinity, layerMask))
            {
                Debug.Log(hit.transform.tag);
                if(hit.transform.tag == "water") waterFound = true;
            }
        }
        if (waterFound)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, waterMask))
            {
                Debug.Log("Near shore");
                //Debug.Log("Near Shore, distance to bedrock vertically is " + hit.distance.ToString());
            }
        }


    }
}

