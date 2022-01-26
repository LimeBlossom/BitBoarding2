using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBehindObject : MonoBehaviour
{
    [SerializeField] private GameObject follower;
    [SerializeField] private GameObject objectToFollow;
    [SerializeField] private string nameToFollow;
    [SerializeField] private float distanceBehind;
    [SerializeField] private Vector3 offset;

    private void Awake()
    {
        if(objectToFollow == null)
        {
            objectToFollow = GameObject.Find(nameToFollow);
        }
    }

    private void Update()
    {
        if (objectToFollow != null)
        {
            follower.transform.position = Vector3.Lerp(follower.transform.position, objectToFollow.transform.position - objectToFollow.transform.forward * distanceBehind + offset, Time.deltaTime);
        }
        else
        {
            objectToFollow = GameObject.Find(nameToFollow);
        }
    }
}
