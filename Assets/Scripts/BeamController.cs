using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamController : MonoBehaviour
{
  public Transform playerOrbTransform;
  public GameManager gameManager;

  public ParticleSystem particle_glitter;
  public GameObject Beam;

  public float beamControlTime;

  void Awake()
  {

  }

  // Update is called once per frame
  void Update()
  {
    beamControlTime += Time.deltaTime;
    if (Beam.activeSelf == true)
    {
      if (beamControlTime >= 0.5)
      {
        BeamOff();
        beamControlTime = 0;
      }
    }
    if (Beam.activeSelf == false)
    {
      if (beamControlTime >= 2)
      {
        Enemy closestEnemy = gameManager.FindClosestEnemy();
        if (closestEnemy != null)
        {
          Vector2 offset = closestEnemy.transform.position - transform.position;
          float deg = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
          Beam.transform.rotation = Quaternion.Euler(0, 0, deg - 15);
        }
        Beam.transform.position = playerOrbTransform.position;
        Beam.SetActive(true);
        beamControlTime = 0;
      }
    }
  }

  void BeamOff()
  {
    if (particle_glitter.particleCount == 0)
    {
      Beam.SetActive(false);
    }
  }
}
