Shader "ColorToBlack" {
    Properties {
        _Color ("Color", Color) = (1, 1, 1, 1)
        _Grad("Grad", Range(0, 1)) = 0.07
        _Offset("Offset", Range(-10, 10)) = 1
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 100
        
        CGPROGRAM
        #pragma surface surf Lambert vertex:vert
        
        sampler2D _MainTex;
        fixed4 _Color;
        float _Grad;
        float _Offset;
        
        struct Input {
            float2 uv_MainTex;
            float3 worldPos;
        };
        
        void vert(inout appdata_full v, out Input o) {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
        }
        
        void surf (Input IN, inout SurfaceOutput o) {
            float darkenFactor = _Offset + IN.worldPos.y * _Grad;
            //if(IN.worldPos.y > -0.001) darkenFactor = 1;
            o.Albedo = _Color.rgb * darkenFactor;
            o.Alpha = _Color.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
