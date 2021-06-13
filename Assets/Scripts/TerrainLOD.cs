using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class TerrainLOD : MonoBehaviour
{

	[Header("Setup")]
	public bool wireframe;
	public Material material;
	public Texture2D heigthMap;

	[Header("Settings")]
	public float dist;
	public float distToRec;

	
	public Transform player;


	Quadtree quadtree;

	ComputeBuffer buffer;
	Vector3 lastPosition;

	public int lenght;

	public bool ready = false;

	Color[] colors;
	

	void Start()
	{
		colors = heigthMap.GetPixels();
		Settings.dist = dist;
		Settings.distToRec = distToRec;

		lastPosition = player.position;
		
		UpdateTerrainQuadTree();
		UpdateTerrainBuffer();
	}

	public void UpdateTerrainQuadTree()
	{
		quadtree = new Quadtree(colors);
		quadtree.search(quadtree.root, lastPosition);
		quadtree.detail(quadtree.root);

		quadtree.setTriangles(quadtree.root);

		lenght = quadtree.triangles.Count;
		
		ready = true;
	}

	public void UpdateTerrainBuffer()
	{

		ready = false;

		if(buffer!=null)
			buffer.Dispose();

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
		if(Vector3.Distance(player.position,lastPosition) > Settings.distToRec)
		{
			lastPosition = player.position;
			new Thread(UpdateTerrainQuadTree).Start();
		}
		if(ready){
			UpdateTerrainBuffer();
		}
	}

	private void OnDestroy() {
		Debug.Log("Liberando buffer");
		buffer.Release();
	}

}