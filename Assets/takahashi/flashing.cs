using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashing : MonoBehaviour {

    public float appear_interval = 0.9f;
    public float hide_interval = 0.1f;

    void Start () {
        StartCoroutine ("Flash");
    }

    IEnumerator Flash() {
        while ( true ) {
            var renderComponent = GetComponent<Renderer>();
            renderComponent.enabled = !renderComponent.enabled;
            yield return new WaitForSeconds(hide_interval);

            renderComponent.enabled = !renderComponent.enabled;
            yield return new WaitForSeconds(appear_interval);
        } 
    }

}
