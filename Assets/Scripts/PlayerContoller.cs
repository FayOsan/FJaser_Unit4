using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    Rigidbody rbPlayer;
    GameObject focalPoint;
    Renderer rendererPlayer;
    public float speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
        rendererPlayer = GetComponent<Renderer>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float fowardInput = Input.GetAxis("Vertical");
        float magnitude = fowardInput * speed * Time.deltaTime;
        rbPlayer.AddForce(focalPoint.transform.forward * magnitude, ForceMode.Force);

        if(fowardInput > 0)
        {
            rendererPlayer.material.color = new Color(1.0f - fowardInput, 1.0f, 1.0f - fowardInput);
        }
        else
        {
            rendererPlayer.material.color = new Color(1.0f + fowardInput, 1.0f, 1.0f + fowardInput);
        }


    }
}
