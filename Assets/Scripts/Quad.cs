using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quad
{
	public float size;

	string type;

	Quad parent;

	public bool leaf;

	Vector3[] vertices;

	public Quad[] quadrants;						// CHILDS    0 - TL, 1 - TR, 2 - BL, 3 - BR

	bool[] enabled;

	public Quad[] neighbors;

	public Quad(Vector3 tl, Vector3 br, float size, string type, Quad parent) {
		Vector3 center = Helper.Mean(br, tl);
		vertices = new Vector3[]{
			center,								// 0 - CENTER
			tl,									// 1 - TOP LEFT
			new Vector3(center.x, 0, tl.y),		// 2 - TOP
			new Vector3(br.x, 0, tl.y),			// 3 - TOP RIGHT
			new Vector3(br.x, 0, center.y),		// 4 - RIGHT
			br,									// 5 - BOTTOM RIGHT
			new Vector3(center.x, 0, br.y),		// 6 - BOTTOM
			new Vector3(tl.x, 0, br.y),			// 7 - BOTTOM LEFT
			new Vector3(tl.x, 0, center.y)		// 8 - LEFT
		};

		neighbors = new Quad[] {
			null,						// 0 - TOP LEFT
			null,						// 1 - TOP
			null,						// 2 - TOP RIGHT
			null,						// 3 - LEFT
			null,						// 4 - RIGHT
			null,						// 5 - BOTTOM LEFT
			null,						// 6 - BOTTOM
			null,						// 7 - BOTTOM RIGHT
		};

		enabled = new bool[] {true, true, false, true, false, true, false, true, false};

		quadrants = new Quad[]{null, null, null, null};

		this.size = size;

		this.type = type;

		this.parent = parent;

		this.leaf = true;

	}

	public void setNeighbors()
	{
		if(parent != null)
		{
			if(type == "tl")
			{
				if(parent.neighbors[0]!=null)
					neighbors[0] = parent.neighbors[0].quadrants[3];
				if(parent.neighbors[1]!=null){
					neighbors[1] = parent.neighbors[1].quadrants[2];
					neighbors[2] = parent.neighbors[1].quadrants[3];
				}
				
				if(parent.neighbors[3]!=null)
					neighbors[3] = parent.neighbors[3].quadrants[1];
				neighbors[4] = parent.quadrants[1];

				if(parent.neighbors[3]!=null)
					neighbors[5] = parent.neighbors[3].quadrants[3];
				neighbors[6] = parent.quadrants[2];
				neighbors[7] = parent.quadrants[3];
			}
			if(type == "tr")
			{
				if(parent.neighbors[1]!=null){
					neighbors[0] = parent.neighbors[1].quadrants[2];
					neighbors[1] = parent.neighbors[1].quadrants[3];
				}
				if(parent.neighbors[2]!=null)
					neighbors[2] = parent.neighbors[2].quadrants[2];

				neighbors[3] = parent.quadrants[0];
				if(parent.neighbors[4]!=null)
					neighbors[4] = parent.neighbors[4].quadrants[0];

				neighbors[5] = parent.quadrants[2];
				neighbors[6] = parent.quadrants[3];
				if(parent.neighbors[4]!=null)
					neighbors[7] = parent.neighbors[4].quadrants[2];
			}
			if(type == "bl")
			{
				if(parent.neighbors[3]!=null)
					neighbors[0] = parent.neighbors[3].quadrants[1];
				neighbors[1] = parent.quadrants[0];
				neighbors[2] = parent.quadrants[1];

				if(parent.neighbors[3]!=null)
					neighbors[3] = parent.neighbors[3].quadrants[3];
				neighbors[4] = parent.quadrants[3];

				if(parent.neighbors[5]!=null)
					neighbors[5] = parent.neighbors[5].quadrants[1];
				if(parent.neighbors[6]!=null){
					neighbors[6] = parent.neighbors[6].quadrants[0];
					neighbors[7] = parent.neighbors[6].quadrants[1];
				}
			}
			if(type == "br")
			{
				neighbors[0] = parent.quadrants[0];
				neighbors[1] = parent.quadrants[1];
				if(parent.neighbors[4]!=null)
					neighbors[2] = parent.neighbors[4].quadrants[0];

				neighbors[3] = parent.quadrants[2];
				if(parent.neighbors[4]!=null)
					neighbors[4] = parent.neighbors[4].quadrants[2];

				if(parent.neighbors[6]!=null){
					neighbors[5] = parent.neighbors[6].quadrants[0];
					neighbors[6] = parent.neighbors[6].quadrants[1];
				}
				if(parent.neighbors[7]!=null)
					neighbors[7] = parent.neighbors[7].quadrants[0];
			}
			
			
			
		}
	}

	public bool contain(Vector3 p) 
	{
		if (p.x >= vertices[1].x && p.x <= vertices[5].x) {
			if (p.z >= vertices[1].z && p.z <= vertices[5].z) {
				return true;
			}
		}
		return false;
	}

	public bool inRange(Vector3 p) 
	{
		if (Vector3.Distance(p,vertices[0]) < 200) {
			return true;
		}
		return false;
	}

	public void toSew(){
		
		if(neighbors[1] != null && !neighbors[1].leaf){
			enabled[2] = true;
		}
		if(neighbors[3] != null && !neighbors[3].leaf){
			enabled[8] = true;
		}
		if(neighbors[4] != null && !neighbors[4].leaf){
			enabled[4] = true;
		}
		if(neighbors[6] != null && !neighbors[6].leaf){
			enabled[6] = true;
		}
	}

	public void subdivide()
	{
		leaf = false;
		
		setNeighbors();
		
		quadrants[0] = new Quad(vertices[1], vertices[0], size/2, "tl", this);
		quadrants[1] = new Quad(vertices[2], vertices[4], size/2, "tr", this);
		quadrants[2] = new Quad(vertices[8], vertices[6], size/2, "bl", this);
		quadrants[3] = new Quad(vertices[0], vertices[5], size/2, "br", this);

		for (int i = 0; i < quadrants.Length; i++) {
			quadrants[i].setNeighbors();
		}
	}

}
