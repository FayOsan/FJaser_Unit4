using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    Rigidbody rbPlayer;
    GameObject focalPoint;
    Renderer rendererPlayer;
    public float speed = 10.0f;
    public float powerUpSpeed = 10.0f;
    public GameObject PowerUpInd;

    bool hasPowerUp = false;
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
        PowerUpInd.transform.position = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountDown());
            PowerUpInd.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasPowerUp && collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player has collid with" + collision.gameObject + "with powerup set to:" + hasPowerUp);
            Rigidbody rbEnemy = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayDir = collision.gameObject.transform.position - transform.position;

            rbEnemy.AddForce(awayDir * powerUpSpeed, ForceMode.Impulse);
        }
    }

    IEnumerator PowerUpCountDown()
    {
        yield return new WaitForSeconds(8);
        hasPowerUp = false;
        PowerUpInd.SetActive(false);
    }
}
