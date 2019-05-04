using UnityEngine;
using System.Collections;

public class Heatmap : MonoBehaviour
{
    public Vector4[] positions;
    public Vector4[] properties;

    public Material material;

    public GameObject tests1;
    public GameObject tests2;
    public GameObject tests3;
    public GameObject tests4;


    public int count = 50;

    void Start()
    {
        positions = new Vector4[count];
        properties = new Vector4[count];

         positions[0] = new Vector4(tests1.transform.position.x, tests1.transform.position.y, 0, 0);
         properties[0] = new Vector4(0.1f, 1, 0, 0);

        positions[1] = new Vector4(tests2.transform.position.x, tests2.transform.position.y, 0, 0);
        properties[1] = new Vector4(0, 1, 0, 0);

        positions[2] = new Vector4(tests3.transform.position.x, tests3.transform.position.y, 0, 0);
        properties[2] = new Vector4(0, 1, 0, 0);

        positions[3] = new Vector4(tests4.transform.position.x, tests4.transform.position.y, 0, 0);
        properties[3] = new Vector4(0, 1, 0, 0);

    }

    void Update()
    {
        for (int i = 0; i < positions.Length; i++)
            positions[i] += new Vector4(Random.Range(-0.1f, +0.1f), Random.Range(-0.1f, +0.1f), 0, 0) * Time.deltaTime;

        material.SetInt("_Points_Length", count);
        material.SetVectorArray("_Points", positions);
        material.SetVectorArray("_Properties", properties);
    }
}