using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BorgCube : MonoBehaviour
{
    NavMeshAgent borgAgent;

    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        borgAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        borgAgent.SetDestination(target.transform.position);
    }
}
