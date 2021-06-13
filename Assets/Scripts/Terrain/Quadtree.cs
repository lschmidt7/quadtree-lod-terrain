using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quadtree
{
	public Quad root;

	public List<Vector3> triangles;

	public int widthTex;
	public int heightTex;

	public Quadtree()
	{
		widthTex = 2048;
		heightTex = 2048;
		triangles = new List<Vector3>();
		root = new Quad(new Vector3(0, 0, 0), new Vector3(Settings.size, 0, Settings.size), Settings.size, "tr", null);
	}

	public void search(Quad quad, Vector3 p)
	{
		if( ( quad.contain(p) || quad.inRange(p)) && quad.size > 1)
		{
			for (int i = 0; i < quad.neighbors.Length; i++)
			{
				if (quad.neighbors[i] != null)
				{
					quad.neighbors[i].subdivide();
				}
			}

			quad.subdivide();

			search(quad.quadrants[0], p);
			search(quad.quadrants[1], p);
			search(quad.quadrants[2], p);
			search(quad.quadrants[3], p);
		}
	}

	public void detail(Quad quad)
	{
		if (quad.leaf)
		{
			quad.setNeighbors();
			quad.toSew();
		}
		else
		{
			detail(quad.quadrants[0]);
			detail(quad.quadrants[1]);
			detail(quad.quadrants[2]);
			detail(quad.quadrants[3]);
		}
	}

	public void setTriangles(Quad quad )
	{
		if (quad.leaf)
		{
			List<Vector3> quadTriangles = quad.getTriangle();
			foreach (Vector3 v in quadTriangles)
			{
				int x = (int)v.x, z = (int)v.z;
				if(v.x > widthTex){
					x = ((int)v.x % widthTex);
				}
				if(v.z > heightTex){
					z = ((int)v.z % heightTex);
				}
				float h = 0;
				Vector3 nv = v;
				nv.y = h;
				triangles.Add(nv);
			}
		}
		else
		{
			this.setTriangles(quad.quadrants[0]);
			this.setTriangles(quad.quadrants[1]);
			this.setTriangles(quad.quadrants[2]);
			this.setTriangles(quad.quadrants[3]);
		}
	}

}