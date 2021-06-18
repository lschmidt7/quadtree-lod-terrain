Shader "Unlit/terrain_shader"
{
   Properties
    {
        _Texture ("Texture",2D) = "white" {}
        _Snow ("Snow Tex",2D) = "white" {}
        _Mountain ("Mountain Tex",2D) = "white" {}
        _Tilling ("Tilling",Range(0,1)) = 1
        _Frequency ("Frequency",Range(0,40)) = 1
        _Height ("Height",Range(1,200)) = 1
        _Valleys ("Valleys",Range(0,40)) = 1
    }
    SubShader
    {
        Tags { "Queue"="Geometry" "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 5.0

            #include "UnityCG.cginc"
            #include "Noise.cginc"
            #include "Normals.cginc"

            struct vertIN{
				uint vID : SV_VertexID;
				uint insID : SV_InstanceID;
			};

            struct v2f
            {
                float3 worldPos : TEXCOORD1;
                float4 vertex : POSITION;
                float2 uv: TEXCOORD0;
                float3 normal : NORMAL;
            };

            float _Tilling;
            float _Frequency;
            float _Height;
            sampler2D _Texture;
            sampler2D _Snow;
            sampler2D _Mountain;
            float _Valleys;

            #include "Biomes.cginc"

            StructuredBuffer<float3> buffer;

            v2f vert (vertIN i)
            {
                v2f o;
				float4 position = float4(buffer[i.vID],0);
                float3 pos = position.xyz / _Frequency;
                position.y = noise(pos,_Height,_Valleys);

                o.normal = sampleNormal(position.xyz,_Height,_Valleys,_Frequency);
				o.vertex = UnityObjectToClipPos(position);
                o.worldPos = mul(unity_ObjectToWorld,position);
                o.uv = float2(position.x,position.z);
                
                return o;

            }

            fixed4 frag (v2f i) : SV_Target
            {
                float light = dot(_WorldSpaceLightPos0,-i.normal);
                
                float4 col = getBiome(i.worldPos.y/_Height,i.uv) * light;

                return col;
            }
            ENDCG
        }
    }
}
