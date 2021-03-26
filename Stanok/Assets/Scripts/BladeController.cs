using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeController : MonoBehaviour
{
    [SerializeField]
    private GameObject BladeWay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
    }

    private void Moving()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Instantiate(BladeWay, transform.position, Quaternion.identity);
            gameObject.transform.position += transform.right * 2.0f * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Instantiate(BladeWay, transform.position, Quaternion.identity);
            gameObject.transform.position += -transform.right * 2.0f * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            Instantiate(BladeWay, transform.position, Quaternion.identity);
            gameObject.transform.position += -transform.forward * 2.0f * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            Instantiate(BladeWay, transform.position, Quaternion.identity);
            gameObject.transform.position += transform.forward * 2.0f * Time.deltaTime;
        }
  
    }

    private void SpawnCollider()
    {

    }
}
