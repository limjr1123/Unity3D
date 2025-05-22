// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Magic"
{
	Properties
	{
		_c("c", 2D) = "white" {}
		_d("d", 2D) = "white" {}
		_MoveY("MoveY", Range( -1 , 1)) = 0.5
		_MoveX("MoveX", Range( -1 , 1)) = 0.5
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull Off
		Blend One One
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _c;
		uniform sampler2D _d;
		uniform float _MoveX;
		uniform float _MoveY;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float cos16 = cos( -1.0 * _Time.y );
			float sin16 = sin( -1.0 * _Time.y );
			float2 rotator16 = mul( i.uv_texcoord - float2( 0.5,0.5 ) , float2x2( cos16 , -sin16 , sin16 , cos16 )) + float2( 0.5,0.5 );
			float cos13 = cos( 1.0 * _Time.y );
			float sin13 = sin( 1.0 * _Time.y );
			float2 rotator13 = mul( i.uv_texcoord - float2( 0.5,0.5 ) , float2x2( cos13 , -sin13 , sin13 , cos13 )) + float2( 0.5,0.5 );
			float4 appendResult5 = (float4(_MoveX , _MoveY , 0.0 , 0.0));
			float4 appendResult10 = (float4(0.5 , 0.5 , 0.0 , 0.0));
			o.Emission = ( tex2D( _c, rotator16 ) + tex2D( _d, ( ( float4( rotator13, 0.0 , 0.0 ) + appendResult5 ) * appendResult10 ).xy ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16700
384;340;1738;767;1395.573;877.8408;1.349789;True;True
Node;AmplifyShaderEditor.RangedFloatNode;6;-1737.307,-371.9894;Float;False;Property;_MoveX;MoveX;4;0;Create;True;0;0;False;0;0.5;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-1737.307,-278.0016;Float;False;Property;_MoveY;MoveY;3;0;Create;True;0;0;False;0;0.5;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;4;-1596.01,-697.116;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;11;-1189.301,17.52359;Float;False;Constant;_ScaleU;ScaleU;5;0;Create;True;0;0;False;0;0.5;0;0.5;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-1182.601,101.5236;Float;False;Constant;_ScaleV;ScaleV;5;0;Create;True;0;0;False;0;0.5;0;0.5;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;13;-1301.233,-679.2768;Float;True;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;5;-1430.665,-346.6404;Float;True;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.DynamicAppendNode;10;-865.6014,77.5236;Float;True;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleAddOpNode;3;-927.7065,-479.7721;Float;True;2;2;0;FLOAT2;0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;14;-659.5581,-751.6204;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-611.6013,-51.47629;Float;True;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RotatorNode;16;-408.6001,-647.8976;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;-1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;2;-345.161,-268.6252;Float;True;Property;_d;d;2;0;Create;True;0;0;False;0;0ca658570302c6c49894339b879e86ed;0ca658570302c6c49894339b879e86ed;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-115.9725,-726.1647;Float;True;Property;_c;c;1;0;Create;True;0;0;False;0;3b8f6bb1d38997043aec7cf6ef6a97d0;3b8f6bb1d38997043aec7cf6ef6a97d0;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;17;192.027,-423.4993;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;740.9857,-454.9419;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;Magic;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;False;Transparent;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;4;1;False;-1;1;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;13;0;4;0
WireConnection;5;0;6;0
WireConnection;5;1;7;0
WireConnection;10;0;11;0
WireConnection;10;1;12;0
WireConnection;3;0;13;0
WireConnection;3;1;5;0
WireConnection;9;0;3;0
WireConnection;9;1;10;0
WireConnection;16;0;14;0
WireConnection;2;1;9;0
WireConnection;1;1;16;0
WireConnection;17;0;1;0
WireConnection;17;1;2;0
WireConnection;0;2;17;0
ASEEND*/
//CHKSM=B027E208894390493FFE7E9EF08DF62C7CDA16C3