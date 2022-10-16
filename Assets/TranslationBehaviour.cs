using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TranslationBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 currentPosition = transform.parent.transform.position;
        Vector3 direction = - (mousePosition - currentPosition);
        float angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.parent.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //transform.RotateAround(transform.GetChild(1).transform.position, angle);
    }
}
