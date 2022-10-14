using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSource : MonoBehaviour
{
    public GameObject target;
    LineRenderer laser;

    // Start is called before the first frame update
    void Start()
    {
        laser = GetComponent<LineRenderer>();
        laser.SetPosition(0, Vector3.zero);
        laser.SetPosition(1, Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        if (target.activeSelf)
        {
            laser.SetPosition(0, gameObject.transform.position);
            laser.SetPosition(1, target.transform.position + new Vector3(0, -1, 0));
        }
        else
        {
            laser.SetPosition(0, Vector3.zero);
            laser.SetPosition(1, Vector3.zero);
        }
    }
}
