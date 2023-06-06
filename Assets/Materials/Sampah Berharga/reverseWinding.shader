Shader "reverseWinding" {
    Properties {
        _CaveIntensity("Cave Intensity", Range(0, 1)) = 0.5
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
    }
    SubShader {
        Pass {
            Cull Front // Reverse the winding direction
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
         
            struct v2f {
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD0;
                float3 normal : TEXCOORD1;
                fixed4 color : COLOR;
            };
            float random (float2 uv)
            {
                return frac(sin(dot(uv,float2(12.9898,78.233)))*43758.5453123);
            }
 
            float _CaveIntensity;
            sampler2D _MainTex;
            float4 _Color;

            v2f vert(appdata_base v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
    
                float3 viewDir = normalize(UnityObjectToViewPos(v.vertex).xyz);
                float3 displacement = viewDir * _CaveIntensity; // Displace vertices inward based on the view direction
    
                // Use the vertex ID as a seed for randomization
               // float randomValue = rand(1.0);
               // displacement += normalize(viewDir) * randomValue * _CaveIntensity;
    
                //o.vertex.xyz += displacement;
    
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.normal = normalize(mul(unity_ObjectToWorld, v.normal));
                o.color = _Color;
    
                return o;
            }



            fixed4 frag (v2f i) : SV_Target { return i.color; }
            ENDCG
        }
    } 
}