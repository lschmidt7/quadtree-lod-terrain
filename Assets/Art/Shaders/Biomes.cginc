float4 getBiome(float h, float2 uv)
{
    float4 t1 = tex2D(_Texture, uv * _Tilling);
    float4 t2 = tex2D(_Snow, uv * _Tilling);
    float4 t3 = tex2D(_Mountain, uv * _Tilling);
    if (h > 0.8)
    {
        float blend = (h-0.8) / 0.2;
        return lerp(t3,t2,blend);
    }
    else if(h > 0.1)
    {
        float blend = (h-0.1) / 0.7;
        return lerp(t1,t3,blend);
    }
    else
    {
        return t1;
    }
}