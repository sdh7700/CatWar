using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityMap : MonoBehaviour
{
  public GameObject[][] FloorSet;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  void OnTriggerExit2D(Collider2D other)
  {
    if (other.tag != "Floor") return;
    // 플레이어 위치 - 타일 중심(중심으로부터의 벡터값)
    Vector3 dir = other.transform.position - transform.position;
    float angle = Vector3.Angle(transform.up, dir);
    Debug.Log(dir);
    Debug.Log(angle);
  }

  void OnTriggerEnter2D(Collider2D other)
  {

    // GameObject[] temp = new GameObject[3];
    // if (other.gameObject == FloorSet[3])
    // {
    //   FloorSet[2].transform.localPosition -= new Vector3(195, 0, 0);
    //   FloorSet[5].transform.localPosition -= new Vector3(195, 0, 0);
    //   FloorSet[8].transform.localPosition -= new Vector3(195, 0, 0);
    //   temp[0] = FloorSet[2];
    //   temp[1] = FloorSet[5];
    //   temp[2] = FloorSet[8];
    //   FloorSet[2] = FloorSet[1];
    //   FloorSet[5] = FloorSet[4];
    //   FloorSet[8] = FloorSet[7];
    //   FloorSet[1] = FloorSet[0];
    //   FloorSet[4] = FloorSet[3];
    //   FloorSet[7] = FloorSet[6];
    //   FloorSet[0] = temp[0];
    //   FloorSet[3] = temp[1];
    //   FloorSet[6] = temp[2];
    // }
    // if (other.gameObject == FloorSet[5])
    // {
    //   FloorSet[0].transform.localPosition += new Vector3(195, 0, 0);
    //   FloorSet[3].transform.localPosition += new Vector3(195, 0, 0);
    //   FloorSet[6].transform.localPosition += new Vector3(195, 0, 0);
    //   temp[0] = FloorSet[0];
    //   temp[1] = FloorSet[3];
    //   temp[2] = FloorSet[6];
    //   FloorSet[0] = FloorSet[1];
    //   FloorSet[3] = FloorSet[4];
    //   FloorSet[6] = FloorSet[7];
    //   FloorSet[1] = FloorSet[2];
    //   FloorSet[4] = FloorSet[5];
    //   FloorSet[7] = FloorSet[8];
    //   FloorSet[2] = temp[0];
    //   FloorSet[5] = temp[1];
    //   FloorSet[8] = temp[2];
    // }
  }
}
