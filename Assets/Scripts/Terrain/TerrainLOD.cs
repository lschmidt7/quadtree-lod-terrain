using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class TerrainLOD : MonoBehaviour
{

	[Header("View")]
	public bool wireframe;

	[Header("Setup")]
	public Material material;
	public Transform player;


	Quadtree quadtree;
	ComputeBuffer buffer;
	Vector3 lastPosition;

	bool ready = false;
	

	void Start()
	{

		lastPosition = player.position;
		
		UpdateTerrainQuadTree();
		UpdateTerrainBuffer();
	}

	public void UpdateTerrainQuadTree()
	{
		quadtree = new Quadtree();
		quadtree.search(quadtree.root, lastPosition);
		quadtree.detail(quadtree.root);

		quadtree.setTriangles(quadtree.root);
		
		ready = true;
	}

	public void UpdateTerrainBuffer()
	{

		ready = false;

		if(buffer!=null)
			buffer.Dispose();

		buffer = new ComputeBuffer ( quadtree.triangles.Count, 12 );

        buffer.SetData( quadtree.triangles.ToArray() );

        material.SetBuffer("buffer",buffer);
	}

	void OnRenderObject(){
        material.SetPass (0);
        if(wireframe){
            Graphics.DrawProceduralNow (MeshTopology.Lines, buffer.count, 1);
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