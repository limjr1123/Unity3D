// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Study3"
{
	Properties
	{
		_bb("bb", 2D) = "white" {}
		_MoveY("MoveY", Range( -1 , 1)) = 0.066
		_MoveX("MoveX", Range( -1 , 1)) = 0.3423781
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		Blend One One
		
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _bb;
		uniform float _MoveX;
		uniform float _MoveY;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 appendResult8 = (float2(_MoveX , _MoveY));
			o.Emission = tex2D( _bb, ( i.uv_texcoord + appendResult8 ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16700
297;300;1738;915;2257.986;1354.058;2.131185;True;True
Node;AmplifyShaderEditor.RangedFloatNode;10;-715.3929,-14.00342;Float;False;Property;_MoveY;MoveY;2;0;Create;True;0;0;False;0;0.066;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-716.1127,-99.89748;Float;False;Property;_MoveX;MoveX;3;0;Create;True;0;0;False;0;0.3423781;0.3423781;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;8;-351,-130.5;Float;True;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;7;-354.44,-392.2538;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;6;-37.90846,-286.2262;Float;True;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;5;191.0915,-294.2262;Float;True;Property;_bb;bb;1;0;Create;True;0;0;False;0;a6633ce776234a34593cc63ed3f5ab8d;a6633ce776234a34593cc63ed3f5ab8d;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;4;651.5695,-318.418;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;Study3;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Transparent;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;4;1;False;-1;1;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;8;0;9;0
WireConnection;8;1;10;0
WireConnection;6;0;7;0
WireConnection;6;1;8;0
WireConnection;5;1;6;0
WireConnection;4;2;5;0
ASEEND*/
//CHKSM=67716179AD1C20C6C36B8DFFCABAE55B2A9E953D