using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private GameObject follower;
    [SerializeField] private string toFollow;
    [SerializeField] private Vector3 offset;

    private GameObject target;

    private void Awake()
    {
        target = GameObject.Find(toFollow);
        if (target != null)
        {
            follower.transform.position = target.transform.position + offset;
        }
    }

    private void Update()
    {
        if (target != null)
        {
            follower.transform.position = target.transform.position + offset;
        }
        else
        {
            target = GameObject.Find(toFollow);
        }
    }
}
