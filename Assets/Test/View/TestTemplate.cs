using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Asterism.UI;

namespace Test.UI 
{
    public partial class TestTemplate : UIBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            testButton1.RegisterClick(_ => { Debug.Log("OnClick testButton1"); });
            testButton2.RegisterClick(_ => { Debug.Log("OnClick testButton2"); });
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
