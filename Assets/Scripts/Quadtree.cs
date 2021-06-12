using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quadtree
{
	public Quad root;

	public List<Vector3> triangles;

	Texture2D texture;

	public Quadtree(Texture2D texture)
	{
		this.texture = texture;
		triangles = new List<Vector3>();
		root = new Quad(new Vector3(0, 0, 0), new Vector3(600, 0, -600), 600, "tr", null);
	}

	public void search(Quad quad, Vector3 p)
	{
		if( ( quad.contain(p) || quad.inRange(p)) && quad.size > 2)
		{
			for (int i = 0; i < quad.neighbors.Length; i++)
			{
				if (quad.neighbors[i] != null && quad.neighbors[i].leaf)
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
				float h = texture.GetPixel((int)v.x,(int)v.z).r * 100;
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