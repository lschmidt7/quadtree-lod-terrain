using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainLOD : MonoBehaviour
{
	Quadtree quadtree;

	ComputeBuffer buffer;

	public bool wireframe;
	public Material material;

	void Start()
	{
		quadtree = new Quadtree();
		Vector3 p = new Vector3(500, 0, 500);
		quadtree.search(quadtree.root, p);
		quadtree.detail(quadtree.root);
		quadtree.setTriangles(quadtree.root);

		Debug.Log(quadtree.triangles[0]);

		Create();
	}

	public void Create()
	{
		int lenght = quadtree.triangles.Count;
		Debug.Log(lenght);
		buffer = new ComputeBuffer ( lenght, 12 );

        buffer.SetData(quadtree.triangles.ToArray());

        material.SetBuffer("buffer",buffer);
	}

	void OnRenderObject(){
        material.SetPass (0);
        if(wireframe){
            Graphics.DrawProceduralNow (MeshTopology.LineStrip, buffer.count, 1);
        }else{
            Graphics.DrawProceduralNow (MeshTopology.Triangles, buffer.count, 1);
        }
    }

    // Update is called once per frame
	void Update()
	{

	}

	private void OnDestroy() {
		Debug.Log("Liberando buffer");
		buffer.Release();
	}
}