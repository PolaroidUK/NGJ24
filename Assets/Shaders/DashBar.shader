Shader "Unlit/DashBar" {
    Properties{
        _Dash("Dash", Range(0, 1)) = 0
        _Color("Bar Color", Color) = (1, 0, 0, 1)
    }
        SubShader{
            Tags { "RenderType" = "Opaque" }
            LOD 100

            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                float _Dash;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    if (i.uv.x <= _Dash) {
                        return fixed4(0, 0, 1, 1);
                    }

                    return fixed4(0, 0, 0, 0);
                }
                ENDCG
               }
            }
        }