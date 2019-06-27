using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    private Vector3 rotateVec;

    void Start()
    {
        speed = 100;
    }

    // Update is called once per frame
    void Update()
    {

        rotateVec.y = Input.GetAxis("Horizontal") * -speed;
        //rotateVec.x = Input.GetAxis("Vertical") * speed;

        transform.Rotate(rotateVec * Time.deltaTime, Space.World);



    }
}
