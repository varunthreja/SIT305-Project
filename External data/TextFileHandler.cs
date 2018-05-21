using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TextFileHandler : MonoBehaviour {

    public string TextFile = "data";
    string TextContent;

    // Use this for initialization
    void Start()
    {
        TextAsset txtAssets = (TextAsset)Resources.Load(TextFile);
        TextContent = txtAssets.text;

        print(TextContent);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
