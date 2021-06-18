
float3 sampleNormal(float3 v, float height, float valleys, float _Tilling)
{
    float offset = 1;

    float3 v1 = float3(v.x + offset, 0, v.z + offset);
    float3 v2 = float3(v.x - offset, 0, v.z + offset);
    float3 v3 = float3(v.x - offset, 0, v.z - offset);
    float3 v4 = float3(v.x + offset, 0, v.z - offset);

    v1.y = noise(v1 / _Tilling, height, valleys);
    v2.y = noise(v2 / _Tilling, height, valleys);
    v3.y = noise(v3 / _Tilling, height, valleys);
    v4.y = noise(v4 / _Tilling, height, valleys);

    float3 t1a = v1 - v2;
    float3 t1b = v1 - v4;

    float3 t2a = v3 - v4;
    float3 t2b = v3 - v2;

    float3 n1 = normalize(cross(t1a,t1b));
    float3 n2 = normalize(cross(t2a,t2b));

    float3 n = normalize((n1 + n2) / 2.0);
    return n;
}
