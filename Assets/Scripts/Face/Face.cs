using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : MonoBehaviour
{
    [SerializeField]
    private float eyebrowTilt = 0;
    [SerializeField]
    private float lipTilt = 0;

    [SerializeField] 
    private Transform ebObject;
    [SerializeField] 
    private Transform lObject;
    
    // emotions
    [SerializeField] private float eHappy = 0;
    [SerializeField] private float eStress = 0;
    [SerializeField] private float eAnger = 0;
    [SerializeField] private float eFear = 0;
    
    //gameobjects of face
    private GameObject eyebrowL;
    private GameObject eyebrowR;
    private GameObject lipL;
    private GameObject lipR;
    
    //Origins of face
    private Quaternion eblOrigin;
    private Quaternion ebrOrigin;
    private Quaternion llOrigin;
    private Quaternion lrOrigin;
    
    // Start is called before the first frame update
    void Start()
    {
        lObject = this.transform.GetChild(7).gameObject.transform;
        ebObject = this.transform.GetChild(8).gameObject.transform;
        eyebrowL = this.transform.GetChild(0).gameObject;
        eblOrigin = eyebrowL.transform.rotation;
        eyebrowR = this.transform.GetChild(1).gameObject;
        ebrOrigin = eyebrowR.transform.rotation;
        lipL = this.transform.GetChild(5).gameObject;
        llOrigin = lipL.transform.rotation;
        lipR = this.transform.GetChild(6).gameObject;
        lrOrigin = lipR.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Limit tilts
        if (eyebrowTilt > 1) { eyebrowTilt = 1; }
        if (eyebrowTilt < -1) { eyebrowTilt = -1; }
        if (lipTilt > 1) { lipTilt = 1; }
        if (lipTilt < -1) { lipTilt = -1; }
        if (eHappy > 1) { eHappy = 1; }
        if (eHappy < 0) { eHappy = 0; }
        if (eAnger > 1) { eAnger = 1; }
        if (eAnger < 0) { eAnger = 0; }
        if (eStress > 1) { eStress = 1; }
        if (eStress < 0) { eStress = 0; }
        if (eFear > 1) { eFear = 1; }
        if (eFear < 0) { eFear = 0; }

        //rotate lips and eyebrows
        eyebrowL.transform.LookAt(ebObject);
        eyebrowR.transform.LookAt(ebObject);
        lipL.transform.LookAt(lObject);
        lipR.transform.LookAt(lObject);

        //update look at positions
        ebObject.transform.localPosition = new Vector3(0, 0.7f + (eyebrowTilt * 0.15f), 0);
        lObject.transform.localPosition = new Vector3(0, -0.3f + (-lipTilt * 0.05f), 0);
        
        //handle emotion
        eyebrowTilt = eHappy + (-eAnger) + eFear;
        lipTilt = eHappy + (-eAnger) + (-eStress);
    }
}
