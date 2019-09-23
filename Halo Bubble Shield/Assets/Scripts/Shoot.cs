using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        CastRay();
        FireGun();
    }

    void FireGun()
    {
        if(Input.GetButtonDown("Fire1") || Input.GetMouseButtonDown(0))
        {

        }
    }

    void CastRay()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // Changes reticle and raycast color if player is looking at enemy.
            if (hit.transform.tag != "Enemy")
            {
                Debug.DrawLine(ray.origin, hit.point, Color.green);

                foreach (Image item in GetComponentsInChildren<Image>())
                {
                    item.color = Color.white;
                }
            }
            else if (hit.transform.tag == "Enemy")
            {
                Debug.DrawLine(ray.origin, hit.point, Color.red);

                foreach (Image item in GetComponentsInChildren<Image>())
                {
                    item.color = Color.red;
                }
            }
        }
    }
}