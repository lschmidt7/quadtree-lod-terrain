using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quadtree
{
	public Quad root;

	public List<Vector3> triangles;

	

	public Quadtree()
	{
		triangles = new List<Vector3>();
		root = new Quad(new Vector3(0, 0, 0), new Vector3(Settings.size, 0, Settings.size), Settings.size, "tr", null);
	}

	public void search(Quad quad, Vector3 p, int level)
	{
		if( ( quad.contain(p) || quad.inBounds(p,Settings.dist)) && quad.size > 1)
		{
			for (int i = 0; i < quad.neighbors.Length; i++)
			{
				if (quad.neighbors[i] != null)
				{
					quad.neighbors[i].subdivide();
				}
			}

			quad.subdivide();

			search(quad.quadrants[0], p, level + 1);
			search(quad.quadrants[1], p, level + 1);
			search(quad.quadrants[2], p, level + 1);
			search(quad.quadrants[3], p, level + 1);
		}
		else if (quad.inBounds(p,Settings.dist * 2))
		{
			if(level<15)
			{
				for (int i = 0; i < quad.neighbors.Length; i++)
				{
					if (quad.neighbors[i] != null)
					{
						quad.neighbors[i].subdivide();
					}
				}

				quad.subdivide();

				search(quad.quadrants[0], p, level + 1);
				search(quad.quadrants[1], p, level + 1);
				search(quad.quadrants[2], p, level + 1);
				search(quad.quadrants[3], p, level + 1);
			}
		}
		else if (quad.inBounds(p,Settings.dist * 3))
		{
			if(level<14)
			{
				for (int i = 0; i < quad.neighbors.Length; i++)
				{
					if (quad.neighbors[i] != null)
					{
						quad.neighbors[i].subdivide();
					}
				}

				quad.subdivide();

				search(quad.quadrants[0], p, level + 1);
				search(quad.quadrants[1], p, level + 1);
				search(quad.quadrants[2], p, level + 1);
				search(quad.quadrants[3], p, level + 1);
			}
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
				Vector3 nv = v;
				nv.y = 0;
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