using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  public Transform target;
  public float speed;
  public float withinRange;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    // Get the distance between enemy and player
    float dist = Vector3.Distance(target.position, transform.position);
    // check if it is within the range you set
    if (dist <= withinRange)
    {
      // move to target
      transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
  }
}
