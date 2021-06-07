using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quadtree
{

    public Quadtree()
    {
        Quad root = new Quad(new Vector3(0,0,0), new Vector3(1000,1000),1000,"tr",null);
    }

    public void search(Quad quad, Vector3 p) {
        if( quad.size <= 20 || !quad.contain(p) )
        {
            return;
        }
        else
        {
            for (int i = 0; i < quad.neighbors.Length; i++) {
                if(quad.neighbors[i]!=null){
                    quad.neighbors[i].subdivide();
                }
            }

            quad.subdivide();

            this.search(quad.quadrants[0],p);
            this.search(quad.quadrants[1],p);
            this.search(quad.quadrants[2],p);
            this.search(quad.quadrants[3],p);
        }
    }

    public void detail(Quad quad){
        if(quad.leaf)
        {
            quad.setNeighbors();
            quad.toSew();
        }
        else
        {
            this.detail(quad.quadrants[0]);
            this.detail(quad.quadrants[1]);
            this.detail(quad.quadrants[2]);
            this.detail(quad.quadrants[3]);
        }
    }

}
