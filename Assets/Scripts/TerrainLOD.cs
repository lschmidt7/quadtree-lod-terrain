using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainLOD : MonoBehaviour
{
	Quadtree quadtree;

	ComputeBuffer buffer;

	public bool wireframe;
	public Material material;
	public Texture2D heigthMap;

	Vector3 p;

	void Start()
	{
		quadtree = new Quadtree(heigthMap);
		p = new Vector3(310, 0, -310);
		quadtree.search(quadtree.root, p);
		quadtree.detail(quadtree.root);
		quadtree.setTriangles(quadtree.root);

		Create();
	}

	public void Create()
	{
		int lenght = quadtree.triangles.Count;
		
		buffer = new ComputeBuffer ( lenght, 12 );

        buffer.SetData( quadtree.triangles.ToArray() );

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