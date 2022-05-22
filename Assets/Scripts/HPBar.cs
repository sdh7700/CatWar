using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
  public Player playerLogic;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    float newGage = (float)playerLogic.HP / playerLogic.maxHP;
    Vector3 scaleVector = new Vector3(newGage, 1f, 1f);
    Vector3 positionVector = new Vector3(newGage / 2 - 0.5f, 0, 0);
    transform.localScale = scaleVector;
    transform.localPosition = positionVector;
  }
}
