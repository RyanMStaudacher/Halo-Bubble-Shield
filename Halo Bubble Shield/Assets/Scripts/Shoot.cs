using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public float shakeDuration = 0.1f;
    public float shakeAmount = 0.08f;

    private Camera cam;
    private Animator gunAnim;
    private Vector3 originalCamPosition;
    private bool shouldShake = false;
    private float shakeDecreaseFactor = 1f;
    private float originalShakeDuration;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        gunAnim = GetComponentInChildren<Animator>();

        originalCamPosition = cam.transform.localPosition;
        originalShakeDuration = shakeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        CastRay();
        FireGun();
        CameraShake();
    }

    void FireGun()
    {
        if(Input.GetButtonDown("Fire1") || Input.GetMouseButtonDown(0))
        {
            if(gunAnim != null)
            {
                gunAnim.Play("Shoot");
            }

            shouldShake = true;

            Instantiate(bullet, cam.transform.position + cam.transform.forward, Quaternion.LookRotation(cam.transform.forward));
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
                    item.color = Color.cyan;
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

    void CameraShake()
    {
        if(shouldShake)
        {
            if(shakeDuration > 0)
            {
                cam.transform.localPosition = originalCamPosition + Random.insideUnitSphere * shakeAmount;

                shakeDuration -= Time.deltaTime * shakeDecreaseFactor;
            }
            else
            {
                shakeDuration = 0f;
                cam.transform.localPosition = originalCamPosition;
            }
        }

        if(cam.transform.localPosition == originalCamPosition)
        {
            shakeDuration = originalShakeDuration;
            shouldShake = false;
        }
    }
}