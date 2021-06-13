
float3 sampleNormal(float4 v, sampler2D noise, float offset, float height)
{
    float4 uvl = float4(v.x, v.z,0,0);
    
    float4 v1 = v + float4(offset,0,offset,0);
    v1.y = tex2Dlod(noise, uvl + float4(offset,offset,0,0) ).r * height;
    
    float4 v2 = v + float4(offset,0,-offset,0);
    v2.y = tex2Dlod(noise, uvl + float4(offset,-offset,0,0) ).r * height;
    
    float4 v3 = v + float4(-offset,0,offset,0);
    v3.y = tex2Dlod(noise, uvl + float4(-offset,offset,0,0) ).r * height;

    float4 vec1 = v1 - v3;
    float4 vec2 = v1 - v2;
    return normalize(cross(vec1,vec2));
}
