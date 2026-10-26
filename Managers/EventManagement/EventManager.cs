using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance {get; private set;}

    [Header("Layer Mask")]
    [SerializeField] LayerMask raftLayer;

    [Space]
    [Header("Events")]
    [SerializeField] List<GameObject> survivorRafts;


    void Awake()
    {
        InitializeSingleton();
    }

    void Start()
    {
        StartCoroutine(SpawnEvent());
    }

    void Update()
    {
        MoveBack(); 
    }

    void InitializeSingleton()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    IEnumerator SpawnEvent()
    {
        while(true)
        {
            yield return new WaitForSecondsRealtime(20);
            Instantiate(survivorRafts[0], transform.position, Quaternion.identity);
        }
    }

    void MoveBack()
    {
        if(Physics.Raycast(transform.position, Vector3.back, 25f, raftLayer))            
            transform.position -= Vector3.back * 50f;
    }
}
