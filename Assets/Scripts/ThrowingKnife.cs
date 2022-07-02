using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingKnife : MonoBehaviour
{
  public int rotateSpeed;
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    Vector3 rotVec = Vector3.forward;
    transform.Rotate(rotVec * rotateSpeed);
  }
}
