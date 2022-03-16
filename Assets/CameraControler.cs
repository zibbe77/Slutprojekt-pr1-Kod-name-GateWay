using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Playercontroller.
        transform.position = player.transform.position;

        //player.GetComponent<Playercontroller>().position;
    }
}
