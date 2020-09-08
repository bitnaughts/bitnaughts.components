using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;


public class CannonController : ComponentController {   

    public override float Action (float input) 
    {
        if (this == null) { Destroy(this); return -999; }

        Instantiate(
            Utilities.Get<GameObject>("Shell"),
            this.transform.position,
            // new Vector3(x*10, 0, z*10),
            this.transform.rotation,      
            GameObject.Find("World").transform
        );
        return 0;
    }
  
    public override string ToString()
    {
        return this.name + "\n│ This component fires projectiles\n";
    }
}