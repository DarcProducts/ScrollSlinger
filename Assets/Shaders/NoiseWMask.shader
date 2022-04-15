Shader "Custom/s_Noise With Masker"
{
    Properties
    {
        _MaskTex ("Mask Texture", 2D) = "black" {}
        _YFill("Y Fill", Range(0,1)) = 1.0
        _XFill("X Fill", Range(0,1)) = 1.0
        _speedX("Speed X axis", float) = 0.1
        _speedY("Speed Y axis", float) = 0.0
        _value("Value", Range( -2.0 , 2.0)) = 0.0
        _amplitude("Amplitude", Range( 0.0 , 5.0)) = 1.5
        _range("Range", Range(0.0, 1.0)) = 0.0
        _frequency("Frequency", Range( 0.0 , 6.0)) = 2.0
        _power("Power", Range( 0.1 , 5.0)) = 1.0
        _scale("Scale", Float) = 1.0    
        [HDR] _emissionColor("Color", Color) = (0,0,0)
        _emission("Emission Strength", Range(0.1, 5.0)) = 0
    }
    Subshader
    {
        Tags { 
            "RenderType" ="Transparent" 
            "Queue"="Transparent"
        }
        
        Cull Off
        Blend One One
        ZWrite Off  

        Pass
        {
            CGPROGRAM
            #pragma vertex Interpolators
            #pragma fragment frag
            #pragma target 3.0
           
            struct MeshData
            {
                fixed4 vertex : SV_POSITION;
                fixed2 uv : TEXCOORD0;
                fixed2 mask : TEXCOORD1;
            };
 
            sampler2D _MaskTex;
            fixed _octaves,_lacunarity,_gain,_value,_amplitude,_frequency, _offsetX, _offsetY, 
            _power, _scale, _range, _emission;
            fixed4 _color;
            fixed4 _emissionColor;
            fixed _speedX, _speedY;
            half _YFill;
            half _XFill;
           
            fixed noiseCreator( fixed2 p )
            {
                p = p * _scale + fixed2(_offsetX + (_Time.y * _speedX),_offsetY + (_Time.y * _speedY));
                fixed2 i = floor( p * _frequency);
                fixed2 f = frac( p * _frequency );   
                fixed2 t = f * f * f * ( f * ( f * 6.0 - 15.0 ) + 10.0 );
                fixed2 a = i + fixed2( 0.0, 0.0 );
                fixed2 b = i + fixed2( 1.0, 0.0 );
                fixed2 c = i + fixed2( 0.0, 1.0 );
                fixed2 d = i + fixed2( 1.0, 1.0 );
                a = -1.0 + 2.0 * frac( sin( fixed2( dot( a, fixed2( 127.1, 311.7 ) ),dot( a, fixed2( 269.5,183.3 ) ) ) ) * 43758.5453123 );
                b = -1.0 + 2.0 * frac( sin( fixed2( dot( b, fixed2( 127.1, 311.7 ) ),dot( b, fixed2( 269.5,183.3 ) ) ) ) * 43758.5453123 );
                c = -1.0 + 2.0 * frac( sin( fixed2( dot( c, fixed2( 127.1, 311.7 ) ),dot( c, fixed2( 269.5,183.3 ) ) ) ) * 43758.5453123 );
                d = -1.0 + 2.0 * frac( sin( fixed2( dot( d, fixed2( 127.1, 311.7 ) ),dot( d, fixed2( 269.5,183.3 ) ) ) ) * 43758.5453123 );
                fixed A = dot( a, f - fixed2( 0.0, 0.0 ) );
                fixed B = dot( b, f - fixed2( 1.0, 0.0 ) );
                fixed C = dot( c, f - fixed2( 0.0, 1.0 ) );
                fixed D = dot( d, f - fixed2( 1.0, 1.0 ) );
                fixed noise = ( lerp( lerp( A, B, t.x ), lerp( C, D, t.x ), t.y ) );   
                _value += _amplitude * noise;
                _value = clamp( _value, -1.0, 1.0 );
                return pow(_value * 0.5 + 0.5,_power);
            }
           
            MeshData Interpolators (fixed4 vertex:POSITION, fixed2 uv:TEXCOORD0, fixed2 mask:TEXCOORD1)
            {
                MeshData vs;
                vs.vertex = UnityObjectToClipPos (vertex);
                vs.mask = mask;
                vs.uv = uv;
                return vs;
            }
 
            float4 frag (MeshData o) : SV_TARGET
            {  
                fixed4 m = tex2D (_MaskTex, o.uv, o.mask, 0);
                fixed2 uv = o.uv.xy;
                fixed c = noiseCreator(uv);
                if (uv.y > _YFill)
                discard;
                if (uv.x > _XFill)
                discard;
                if (m.x < 0.05)
                discard;
                fixed4 noise = fixed4(min(c, m.x), min(c, m.y), min(c, m.z), c) * _emission * _emissionColor;
                return noise * m;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
