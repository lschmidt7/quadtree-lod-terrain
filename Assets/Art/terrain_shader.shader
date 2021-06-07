Shader "Unlit/terrain_shader"
{
   Properties
    {
        _Color ("Color",Color) = (1,1,1,1)
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

            struct vertIN{
				uint vID : SV_VertexID;
				uint insID : SV_InstanceID;
			};

            struct v2f
            {
                float3 worldPos : TEXCOORD1;
                float4 vertex : POSITION;
            };

            float4 _Color;

            StructuredBuffer<float3> buffer;

            v2f vert (vertIN i)
            {
                v2f o;
				float4 position = float4(buffer[i.vID],0);
				o.vertex = UnityObjectToClipPos(position);
                o.worldPos = mul(unity_ObjectToWorld,position);
                return o;

            }

            fixed4 frag (v2f i) : SV_Target
            {
                
                
                float4 col = _Color;
                
                // MOUSE

                return col;
            }
            ENDCG
        }
    }
}
