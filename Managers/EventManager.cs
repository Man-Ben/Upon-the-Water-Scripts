using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [Header("Layer Mask")]
    [SerializeField] LayerMask raftLayer;

    [Space]
    [Header("Events")]
    [SerializeField] List<GameObject> survivorRafts;

    public static EventManager Instance {get; private set;}

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        StartCoroutine(SpawnEvent());
    }

    void Update()
    {
        MoveBack(); 
    }

    IEnumerator SpawnEvent()
    {
        while(true)
        {
            yield return new WaitForSecondsRealtime(60);
            Instantiate(survivorRafts[0], transform.position, Quaternion.identity);
        }
    }

    void MoveBack()
    {
        if(Physics.Raycast(transform.position, Vector3.back, 100f, raftLayer))            
            transform.position -= Vector3.back * 50f;
    }
}
