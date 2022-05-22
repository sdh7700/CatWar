using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    Slider expBar;
    float fSliderBarTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        expBar = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (expBar.value <= 0)
            transform.Find("Fill Area").gameObject.SetActive(false);
        else
            transform.Find("Fill Area").gameObject.SetActive(true);
    }
}
