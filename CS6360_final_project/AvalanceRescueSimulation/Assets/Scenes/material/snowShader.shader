    Shader "Tessellation Sample" {
        Properties {
            _Tess ("Tessellation", Range(1,64)) = 8
            _MainTex ("Base (RGB)", 2D) = "white" {}
            _DispTex ("Disp Texture", 2D) = "gray" {}
            _TerrainDispTex ("Terrain Disp Texture", 2D) = "gray" {}
            _NormalMap ("Normalmap", 2D) = "bump" {}
            _Displacement ("Displacement", Range(-100, 100)) = 0.3
            _TerrainDisplacement ("TerrainDisplacement", Range(-100, 100)) = 0.3
            _Color ("Color", color) = (1,1,1,0)
            _SpecColor ("Spec color", color) = (0.5,0.5,0.5,0.5)
        }
        SubShader {
            Tags { "RenderType"="Opaque" }
            LOD 100
            
            CGPROGRAM
            #pragma surface surf BlinnPhong addshadow fullforwardshadows tessellate:tessFixed nolightmap vertex:disp 
            #pragma target 4.6

            struct appdata {
                float4 vertex : POSITION;
                float4 tangent : TANGENT;
                float3 normal : NORMAL;
                float2 texcoord : TEXCOORD0;
            };


            struct Input {
                float2 uv_MainTex;
                fixed4 color:COLOR;
            };

            float _Tess;

            float4 tessFixed()
            {
                return _Tess;
            }

            sampler2D _DispTex;
            sampler2D _TerrainDispTex;
            float _Displacement;
            float _TerrainDisplacement;

            void disp (inout appdata_full v)
            {
                float d = (tex2Dlod(_DispTex, float4(v.texcoord.x, v.texcoord.y, 0,0)).r)* _Displacement;
                float d_t = 2*(tex2Dlod(_TerrainDispTex, float4(v.texcoord.xy, 0,0)).r - 0.5)* _TerrainDisplacement;
                v.vertex.y -= (d-d_t);
                v.color = float4 (d,0,0,0); // a bug in unity: conflicts veretx shader & tess shader, use COLOR to store dispalcement 
            }


            sampler2D _MainTex;
            sampler2D _NormalMap;
            fixed4 _Color;

            void surf (Input IN, inout SurfaceOutput o) {
                half4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
                float h = IN.color.x;
                o.Albedo = c.rgb * (1-h*1.5);
                o.Specular = 0.2;
                o.Gloss = 1.0;
                o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_MainTex));
            }
            ENDCG
        }
        FallBack "Diffuse"
    }