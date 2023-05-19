using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveOnCollision : MonoBehaviour
{
    private bool isDissolving = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isDissolving == false)
        {
            isDissolving = true;
            StartCoroutine(Dissolve());
        }
       
    }

    private IEnumerator Dissolve()
    {
        float amount = 0;

        while (amount < 1.5f)
        {
            amount += Time.deltaTime;

            GetComponent<Renderer>().material.SetFloat("_Amount", amount);
            yield return null;

        }


       Destroy(gameObject);
    }
}
