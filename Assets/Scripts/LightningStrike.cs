using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrike : MonoBehaviour
{
  public int dmg;
  public float maxOnTime;
  public float curOnTime;
  // Start is called before the first frame update

  void OnEnable()
  {
    Invoke("OffLightningEffect", 0.5f);
  }
  // Update is called once per frame
  void Update()
  {

  }

  void OffLightningEffect()
  {
    gameObject.SetActive(false);
  }

}
