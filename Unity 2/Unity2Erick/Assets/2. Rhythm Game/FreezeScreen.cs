using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeScreen : MonoBehaviour
{

    IEnumerator Start()
    {
        yield return new WaitForSeconds(2);


        //freeze the entire app for 2000ms
        System.Threading.Thread.Sleep(2000);
    }




    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 90* Time.deltaTime,0);
    }
}
