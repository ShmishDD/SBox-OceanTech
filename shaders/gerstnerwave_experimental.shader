
HEADER
{
	Description = "";
}

FEATURES
{
	#include "common/features.hlsl"
}

MODES
{
	VrForward();
	Depth(); 
	ToolsVis( S_MODE_TOOLS_VIS );
}

COMMON
{
	#ifndef S_ALPHA_TEST
	#define S_ALPHA_TEST 0
	#endif
	#ifndef S_TRANSLUCENT
	#define S_TRANSLUCENT 0
	#endif
	
	#include "common/shared.hlsl"
	#include "procedural.hlsl"

	#define S_UV2 1
	#define CUSTOM_MATERIAL_INPUTS
}

struct VertexInput
{
	#include "common/vertexinput.hlsl"
	float4 vColor : COLOR0 < Semantic( Color ); >;
};

struct PixelInput
{
	#include "common/pixelinput.hlsl"
	float3 vPositionOs : TEXCOORD14;
	float3 vNormalOs : TEXCOORD15;
	float4 vTangentUOs_flTangentVSign : TANGENT	< Semantic( TangentU_SignV ); >;
	float4 vColor : COLOR0;
};

VS
{
	#include "common/vertex.hlsl"
	
	float3 g_vOneDirection < Attribute( "OneDirection" ); >;
	float g_flOneWavelength < Attribute( "OneWavelength" ); >;
	float g_flOneSpeed < Attribute( "OneSpeed" ); >;
	float g_flOneAmplitude < Attribute( "OneAmplitude" ); >;
	float g_flOneSteepness < Attribute( "OneSteepness" ); >;
	float3 g_vTwoDirection < Attribute( "TwoDirection" ); >;
	float g_flTwoWavelength < Attribute( "TwoWavelength" ); >;
	float g_flTwoSpeed < Attribute( "TwoSpeed" ); >;
	float g_flTwoAmplitude < Attribute( "TwoAmplitude" ); >;
	float g_flTwoSteepness < Attribute( "TwoSteepness" ); >;
	float3 g_vThreeDirection < Attribute( "ThreeDirection" ); >;
	float g_flThreeWavelength < Attribute( "ThreeWavelength" ); >;
	float g_flThreeSpeed < Attribute( "ThreeSpeed" ); >;
	float g_flThreeAmplitude < Attribute( "ThreeAmplitude" ); >;
	float g_flThreeSteepness < Attribute( "ThreeSteepness" ); >;
	float3 g_vFourDirection < Attribute( "FourDirection" ); >;
	float g_flFourWavelength < Attribute( "FourWavelength" ); >;
	float g_flFourSpeed < Attribute( "FourSpeed" ); >;
	float g_flFourAmplitude < Attribute( "FourAmplitude" ); >;
	float g_flFourSteepness < Attribute( "FourSteepness" ); >;
	float3 g_vFiveDirection < Attribute( "FiveDirection" ); >;
	float g_flFiveWavelength < Attribute( "FiveWavelength" ); >;
	float g_flFiveSpeed < Attribute( "FiveSpeed" ); >;
	float g_flFiveAmplitude < Attribute( "FiveAmplitude" ); >;
	float g_flFiveSteepness < Attribute( "FiveSteepness" ); >;
	
	PixelInput MainVs( VertexInput v )
	{
		PixelInput i = ProcessVertex( v );
		i.vPositionOs = v.vPositionOs.xyz;
		i.vColor = v.vColor;

		VS_DecodeObjectSpaceNormalAndTangent( v, i.vNormalOs, i.vTangentUOs_flTangentVSign );
		
		float3 l_0 = g_vOneDirection;
		float4 l_1 = float4( l_0.x, l_0.y, 0, 0 );
		float l_2 = l_1.x;
		float3 l_3 = i.vPositionWs;
		float l_4 = l_3.x;
		float l_5 = l_3.y;
		float4 l_6 = float4( l_4, l_5, 0, 0 );
		float l_7 = dot( l_1, l_6 );
		float l_8 = 2 * 3.1415927;
		float l_9 = g_flOneWavelength;
		float l_10 = l_8 / l_9;
		float l_11 = l_7 * l_10;
		float l_12 = g_flOneSpeed;
		float l_13 = l_12 * l_10;
		float l_14 = l_13 * g_flTime;
		float l_15 = l_11 + l_14;
		float l_16 = cos( l_15 );
		float l_17 = g_flOneAmplitude;
		float l_18 = l_16 * l_17;
		float l_19 = g_flOneSteepness;
		float l_20 = l_10 * l_17;
		float l_21 = l_20 * 1;
		float l_22 = l_19 / l_21;
		float l_23 = l_18 * l_22;
		float l_24 = l_2 * l_23;
		float l_25 = l_1.y;
		float l_26 = l_23 * l_25;
		float l_27 = sin( l_15 );
		float l_28 = l_17 * l_27;
		float4 l_29 = float4( l_24, l_26, l_28, 0 );
		float3 l_30 = g_vTwoDirection;
		float4 l_31 = float4( l_30.x, l_30.y, 0, 0 );
		float l_32 = l_31.x;
		float3 l_33 = i.vPositionWs;
		float l_34 = l_33.x;
		float l_35 = l_33.y;
		float4 l_36 = float4( l_34, l_35, 0, 0 );
		float l_37 = dot( l_31, l_36 );
		float l_38 = 2 * 3.1415927;
		float l_39 = g_flTwoWavelength;
		float l_40 = l_38 / l_39;
		float l_41 = l_37 * l_40;
		float l_42 = g_flTwoSpeed;
		float l_43 = l_42 * l_40;
		float l_44 = l_43 * g_flTime;
		float l_45 = l_41 + l_44;
		float l_46 = cos( l_45 );
		float l_47 = g_flTwoAmplitude;
		float l_48 = l_46 * l_47;
		float l_49 = g_flTwoSteepness;
		float l_50 = l_40 * l_47;
		float l_51 = l_50 * 1;
		float l_52 = l_49 / l_51;
		float l_53 = l_48 * l_52;
		float l_54 = l_32 * l_53;
		float l_55 = l_31.y;
		float l_56 = l_53 * l_55;
		float l_57 = sin( l_45 );
		float l_58 = l_47 * l_57;
		float4 l_59 = float4( l_54, l_56, l_58, 0 );
		float4 l_60 = l_29 + l_59;
		float3 l_61 = g_vThreeDirection;
		float4 l_62 = float4( l_61.x, l_61.y, 0, 0 );
		float l_63 = l_62.x;
		float3 l_64 = i.vPositionWs;
		float l_65 = l_64.x;
		float l_66 = l_64.y;
		float4 l_67 = float4( l_65, l_66, 0, 0 );
		float l_68 = dot( l_62, l_67 );
		float l_69 = 2 * 3.1415927;
		float l_70 = g_flThreeWavelength;
		float l_71 = l_69 / l_70;
		float l_72 = l_68 * l_71;
		float l_73 = g_flThreeSpeed;
		float l_74 = l_73 * l_71;
		float l_75 = l_74 * g_flTime;
		float l_76 = l_72 + l_75;
		float l_77 = cos( l_76 );
		float l_78 = g_flThreeAmplitude;
		float l_79 = l_77 * l_78;
		float l_80 = g_flThreeSteepness;
		float l_81 = l_71 * l_78;
		float l_82 = l_81 * 1;
		float l_83 = l_80 / l_82;
		float l_84 = l_79 * l_83;
		float l_85 = l_63 * l_84;
		float l_86 = l_62.y;
		float l_87 = l_84 * l_86;
		float l_88 = sin( l_76 );
		float l_89 = l_78 * l_88;
		float4 l_90 = float4( l_85, l_87, l_89, 0 );
		float4 l_91 = l_60 + l_90;
		float3 l_92 = g_vFourDirection;
		float4 l_93 = float4( l_92.x, l_92.y, 0, 0 );
		float l_94 = l_93.x;
		float3 l_95 = i.vPositionWs;
		float l_96 = l_95.x;
		float l_97 = l_95.y;
		float4 l_98 = float4( l_96, l_97, 0, 0 );
		float l_99 = dot( l_93, l_98 );
		float l_100 = 2 * 3.1415927;
		float l_101 = g_flFourWavelength;
		float l_102 = l_100 / l_101;
		float l_103 = l_99 * l_102;
		float l_104 = g_flFourSpeed;
		float l_105 = l_104 * l_102;
		float l_106 = l_105 * g_flTime;
		float l_107 = l_103 + l_106;
		float l_108 = cos( l_107 );
		float l_109 = g_flFourAmplitude;
		float l_110 = l_108 * l_109;
		float l_111 = g_flFourSteepness;
		float l_112 = l_102 * l_109;
		float l_113 = l_112 * 1;
		float l_114 = l_111 / l_113;
		float l_115 = l_110 * l_114;
		float l_116 = l_94 * l_115;
		float l_117 = l_93.y;
		float l_118 = l_115 * l_117;
		float l_119 = sin( l_107 );
		float l_120 = l_109 * l_119;
		float4 l_121 = float4( l_116, l_118, l_120, 0 );
		float4 l_122 = l_91 + l_121;
		float3 l_123 = g_vFiveDirection;
		float4 l_124 = float4( l_123.x, l_123.y, 0, 0 );
		float l_125 = l_124.x;
		float3 l_126 = i.vPositionWs;
		float l_127 = l_126.x;
		float l_128 = l_126.y;
		float4 l_129 = float4( l_127, l_128, 0, 0 );
		float l_130 = dot( l_124, l_129 );
		float l_131 = 2 * 3.1415927;
		float l_132 = g_flFiveWavelength;
		float l_133 = l_131 / l_132;
		float l_134 = l_130 * l_133;
		float l_135 = g_flFiveSpeed;
		float l_136 = l_135 * l_133;
		float l_137 = l_136 * g_flTime;
		float l_138 = l_134 + l_137;
		float l_139 = cos( l_138 );
		float l_140 = g_flFiveAmplitude;
		float l_141 = l_139 * l_140;
		float l_142 = g_flFiveSteepness;
		float l_143 = l_133 * l_140;
		float l_144 = l_143 * 1;
		float l_145 = l_142 / l_144;
		float l_146 = l_141 * l_145;
		float l_147 = l_125 * l_146;
		float l_148 = l_124.y;
		float l_149 = l_146 * l_148;
		float l_150 = sin( l_138 );
		float l_151 = l_140 * l_150;
		float4 l_152 = float4( l_147, l_149, l_151, 0 );
		float4 l_153 = l_122 + l_152;
		i.vPositionWs.xyz += l_153.xyz;
		i.vPositionPs.xyzw = Position3WsToPs( i.vPositionWs.xyz );
		
		return FinalizeVertex( i );
	}
}

PS
{
	#include "common/pixel.hlsl"
	
	SamplerState g_sSampler0 < Filter( POINT ); AddressU( WRAP ); AddressV( WRAP ); >;
	CreateInputTexture2D( Texture_ps_0, Srgb, 8, "None", "_color", ",0/,0/0", Default4( 1.00, 1.00, 1.00, 1.00 ) );
	CreateInputTexture2D( Texture_ps_1, Srgb, 8, "None", "_color", ",0/,0/0", Default4( 1.00, 1.00, 1.00, 1.00 ) );
	CreateInputTexture2D( Texture_ps_2, Srgb, 8, "None", "_color", ",0/,0/0", Default4( 1.00, 1.00, 1.00, 1.00 ) );
	CreateInputTexture2D( Texture_ps_3, Srgb, 8, "None", "_color", ",0/,0/0", Default4( 1.00, 1.00, 1.00, 1.00 ) );
	Texture2D g_tTexture_ps_0 < Channel( RGBA, Box( Texture_ps_0 ), Srgb ); OutputFormat( DXT5 ); SrgbRead( True ); >;
	Texture2D g_tTexture_ps_1 < Channel( RGBA, Box( Texture_ps_1 ), Srgb ); OutputFormat( DXT5 ); SrgbRead( True ); >;
	Texture2D g_tTexture_ps_2 < Channel( RGBA, Box( Texture_ps_2 ), Srgb ); OutputFormat( DXT5 ); SrgbRead( True ); >;
	Texture2D g_tTexture_ps_3 < Channel( RGBA, Box( Texture_ps_3 ), Srgb ); OutputFormat( DXT5 ); SrgbRead( True ); >;
	float4 g_vSecondColor < UiType( Color ); UiGroup( ",0/,0/0" ); Default4( 0.00, 0.02, 0.08, 1.00 ); >;
	float4 g_vFirstColor < UiType( Color ); UiGroup( ",0/,0/0" ); Default4( 0.00, 0.38, 0.48, 1.00 ); >;
	float3 g_vOneDirection < Attribute( "OneDirection" ); >;
	float g_flOneWavelength < Attribute( "OneWavelength" ); >;
	float g_flOneAmplitude < Attribute( "OneAmplitude" ); >;
	float g_flOneSpeed < Attribute( "OneSpeed" ); >;
	float g_flOneSteepness < Attribute( "OneSteepness" ); >;
	float g_flTwoWavelength < Attribute( "TwoWavelength" ); >;
	float g_flTwoAmplitude < Attribute( "TwoAmplitude" ); >;
	float3 g_vTwoDirection < Attribute( "TwoDirection" ); >;
	float g_flTwoSpeed < Attribute( "TwoSpeed" ); >;
	float g_flTwoSteepness < Attribute( "TwoSteepness" ); >;
	float3 g_vThreeDirection < Attribute( "ThreeDirection" ); >;
	float g_flThreeWavelength < Attribute( "ThreeWavelength" ); >;
	float g_flThreeAmplitude < Attribute( "ThreeAmplitude" ); >;
	float g_flThreeSpeed < Attribute( "ThreeSpeed" ); >;
	float g_flThreeSteepness < Attribute( "ThreeSteepness" ); >;
	float3 g_vFourDirection < Attribute( "FourDirection" ); >;
	float g_flFourWavelength < Attribute( "FourWavelength" ); >;
	float g_flFourAmplitude < Attribute( "FourAmplitude" ); >;
	float g_flFourSpeed < Attribute( "FourSpeed" ); >;
	float g_flFourSteepness < Attribute( "FourSteepness" ); >;
	float3 g_vFiveDirection < Attribute( "FiveDirection" ); >;
	float g_flFiveWavelength < Attribute( "FiveWavelength" ); >;
	float g_flFiveAmplitude < Attribute( "FiveAmplitude" ); >;
	float g_flFiveSpeed < Attribute( "FiveSpeed" ); >;
	float g_flFiveSteepness < Attribute( "FiveSteepness" ); >;
		
	float3 Vec3OsToTs( float3 vVectorOs, float3 vNormalOs, float3 vTangentUOs, float3 vTangentVOs )
	{
		float3 vVectorTs;
		vVectorTs.x = dot( vVectorOs.xyz, vTangentUOs.xyz );
		vVectorTs.y = dot( vVectorOs.xyz, vTangentVOs.xyz );
		vVectorTs.z = dot( vVectorOs.xyz, vNormalOs.xyz );
		return vVectorTs.xyz;
	}
	
	float4 MainPs( PixelInput i ) : SV_Target0
	{
		Material m;
		m.Albedo = float3( 1, 1, 1 );
		m.Normal = TransformNormal( i, float3( 0, 0, 1 ) );
		m.Roughness = 1;
		m.Metalness = 0;
		m.AmbientOcclusion = 1;
		m.TintMask = 1;
		m.Opacity = 1;
		m.Emission = float3( 0, 0, 0 );
		m.Transmission = 0;
		
		float4 l_0 = g_vSecondColor;
		float4 l_1 = g_vFirstColor;
		float2 l_2 = i.vTextureCoords.xy * float2( 1, 1 );
		float l_3 = -0.34110728 * g_flTime;
		float4 l_4 = float4( l_3, 0, 0, 0 );
		float2 l_5 = TileAndOffsetUv( l_2, float2( 1, 1 ), l_4.xy );
		float4 l_6 = Tex2DS( g_tTexture_ps_0, g_sSampler0, l_5 );
		float3 l_7 = TransformNormal( i, Vec3OsToTs( DecodeNormal( l_6.xyz ), i.vNormalOs.xyz, i.vTangentUOs_flTangentVSign.xyz, cross( i.vNormalOs.xyz, i.vTangentUOs_flTangentVSign.xyz ) * i.vTangentUOs_flTangentVSign.w ) );
		float l_8 = -0.24870874 * g_flTime;
		float4 l_9 = float4( 0, l_8, 0, 0 );
		float2 l_10 = TileAndOffsetUv( l_2, float2( 1, 1 ), l_9.xy );
		float4 l_11 = Tex2DS( g_tTexture_ps_1, g_sSampler0, l_10 );
		float3 l_12 = TransformNormal( i, Vec3OsToTs( DecodeNormal( l_11.xyz ), i.vNormalOs.xyz, i.vTangentUOs_flTangentVSign.xyz, cross( i.vNormalOs.xyz, i.vTangentUOs_flTangentVSign.xyz ) * i.vTangentUOs_flTangentVSign.w ) );
		float3 l_13 = saturate( lerp( l_7, l_12, 0.5 ) );
		float l_14 = 0.5 * g_flTime;
		float4 l_15 = float4( l_14, 0, 0, 0 );
		float2 l_16 = TileAndOffsetUv( l_2, float2( 1, 1 ), l_15.xy );
		float4 l_17 = Tex2DS( g_tTexture_ps_2, g_sSampler0, l_16 );
		float3 l_18 = TransformNormal( i, Vec3OsToTs( DecodeNormal( l_17.xyz ), i.vNormalOs.xyz, i.vTangentUOs_flTangentVSign.xyz, cross( i.vNormalOs.xyz, i.vTangentUOs_flTangentVSign.xyz ) * i.vTangentUOs_flTangentVSign.w ) );
		float l_19 = 0 * 0.2;
		float4 l_20 = float4( 0, l_19, 0, 0 );
		float2 l_21 = TileAndOffsetUv( l_2, float2( 1, 1 ), l_20.xy );
		float4 l_22 = Tex2DS( g_tTexture_ps_3, g_sSampler0, l_21 );
		float3 l_23 = TransformNormal( i, Vec3OsToTs( DecodeNormal( l_22.xyz ), i.vNormalOs.xyz, i.vTangentUOs_flTangentVSign.xyz, cross( i.vNormalOs.xyz, i.vTangentUOs_flTangentVSign.xyz ) * i.vTangentUOs_flTangentVSign.w ) );
		float3 l_24 = saturate( lerp( l_18, l_23, 0.5 ) );
		float3 l_25 = saturate( lerp( l_13, l_24, 0.5 ) );
		float3 l_26 = g_vOneDirection;
		float4 l_27 = float4( l_26.x, l_26.y, 0, 0 );
		float l_28 = l_27.x;
		float l_29 = 2 * 3.1415927;
		float l_30 = g_flOneWavelength;
		float l_31 = l_29 / l_30;
		float l_32 = g_flOneAmplitude;
		float l_33 = l_31 * l_32;
		float l_34 = l_33 * -1;
		float3 l_35 = i.vPositionWithOffsetWs.xyz + g_vHighPrecisionLightingOffsetWs.xyz;
		float l_36 = l_35.x;
		float l_37 = l_35.y;
		float4 l_38 = float4( l_36, l_37, 0, 0 );
		float l_39 = dot( l_27, l_38 );
		float l_40 = l_39 * l_31;
		float l_41 = g_flOneSpeed;
		float l_42 = l_41 * l_31;
		float l_43 = l_42 * g_flTime;
		float l_44 = l_40 + l_43;
		float l_45 = cos( l_44 );
		float l_46 = l_34 * l_45;
		float l_47 = l_28 * l_46;
		float l_48 = l_27.y;
		float l_49 = l_48 * l_46;
		float l_50 = sin( l_44 );
		float l_51 = l_33 * l_50;
		float l_52 = g_flOneSteepness;
		float l_53 = l_31 * l_32;
		float l_54 = l_53 * 1;
		float l_55 = l_52 / l_54;
		float l_56 = l_51 * l_55;
		float4 l_57 = float4( l_47, l_49, l_56, 0 );
		float l_58 = 2 * 3.1415927;
		float l_59 = g_flTwoWavelength;
		float l_60 = l_58 / l_59;
		float l_61 = g_flTwoAmplitude;
		float l_62 = l_60 * l_61;
		float l_63 = l_62 * -1;
		float3 l_64 = g_vTwoDirection;
		float4 l_65 = float4( l_64.x, l_64.y, 0, 0 );
		float3 l_66 = i.vPositionWithOffsetWs.xyz + g_vHighPrecisionLightingOffsetWs.xyz;
		float l_67 = l_66.x;
		float l_68 = l_66.y;
		float4 l_69 = float4( l_67, l_68, 0, 0 );
		float l_70 = dot( l_65, l_69 );
		float l_71 = l_70 * l_60;
		float l_72 = g_flTwoSpeed;
		float l_73 = l_72 * l_60;
		float l_74 = l_73 * g_flTime;
		float l_75 = l_71 + l_74;
		float l_76 = cos( l_75 );
		float l_77 = l_63 * l_76;
		float l_78 = l_65.x;
		float l_79 = l_77 * l_78;
		float l_80 = l_65.y;
		float l_81 = l_80 * l_77;
		float l_82 = sin( l_75 );
		float l_83 = l_62 * l_82;
		float l_84 = g_flTwoSteepness;
		float l_85 = l_60 * l_61;
		float l_86 = l_85 * 1;
		float l_87 = l_84 / l_86;
		float l_88 = l_83 * l_87;
		float4 l_89 = float4( l_79, l_81, l_88, 0 );
		float4 l_90 = l_57 + l_89;
		float3 l_91 = g_vThreeDirection;
		float4 l_92 = float4( l_91.x, l_91.y, 0, 0 );
		float l_93 = l_92.x;
		float l_94 = 2 * 3.1415927;
		float l_95 = g_flThreeWavelength;
		float l_96 = l_94 / l_95;
		float l_97 = g_flThreeAmplitude;
		float l_98 = l_96 * l_97;
		float l_99 = l_98 * -1;
		float3 l_100 = i.vPositionWithOffsetWs.xyz + g_vHighPrecisionLightingOffsetWs.xyz;
		float l_101 = l_100.x;
		float l_102 = l_100.y;
		float4 l_103 = float4( l_101, l_102, 0, 0 );
		float l_104 = dot( l_92, l_103 );
		float l_105 = l_104 * l_96;
		float l_106 = g_flThreeSpeed;
		float l_107 = l_106 * l_96;
		float l_108 = l_107 * g_flTime;
		float l_109 = l_105 + l_108;
		float l_110 = cos( l_109 );
		float l_111 = l_99 * l_110;
		float l_112 = l_93 * l_111;
		float l_113 = l_92.y;
		float l_114 = l_113 * l_111;
		float l_115 = sin( l_109 );
		float l_116 = l_98 * l_115;
		float l_117 = g_flThreeSteepness;
		float l_118 = l_96 * l_97;
		float l_119 = l_118 * 1;
		float l_120 = l_117 / l_119;
		float l_121 = l_116 * l_120;
		float4 l_122 = float4( l_112, l_114, l_121, 0 );
		float4 l_123 = l_90 + l_122;
		float3 l_124 = g_vFourDirection;
		float4 l_125 = float4( l_124.x, l_124.y, 0, 0 );
		float l_126 = l_125.x;
		float l_127 = 2 * 3.1415927;
		float l_128 = g_flFourWavelength;
		float l_129 = l_127 / l_128;
		float l_130 = g_flFourAmplitude;
		float l_131 = l_129 * l_130;
		float l_132 = l_131 * -1;
		float3 l_133 = i.vPositionWithOffsetWs.xyz + g_vHighPrecisionLightingOffsetWs.xyz;
		float l_134 = l_133.x;
		float l_135 = l_133.y;
		float4 l_136 = float4( l_134, l_135, 0, 0 );
		float l_137 = dot( l_125, l_136 );
		float l_138 = l_137 * l_129;
		float l_139 = g_flFourSpeed;
		float l_140 = l_139 * l_129;
		float l_141 = l_140 * g_flTime;
		float l_142 = l_138 + l_141;
		float l_143 = cos( l_142 );
		float l_144 = l_132 * l_143;
		float l_145 = l_126 * l_144;
		float l_146 = l_125.y;
		float l_147 = l_146 * l_144;
		float l_148 = sin( l_142 );
		float l_149 = l_131 * l_148;
		float l_150 = g_flFourSteepness;
		float l_151 = l_129 * l_130;
		float l_152 = l_151 * 1;
		float l_153 = l_150 / l_152;
		float l_154 = l_149 * l_153;
		float4 l_155 = float4( l_145, l_147, l_154, 0 );
		float4 l_156 = l_123 + l_155;
		float3 l_157 = g_vFiveDirection;
		float4 l_158 = float4( l_157.x, l_157.y, 0, 0 );
		float l_159 = l_158.x;
		float l_160 = 2 * 3.1415927;
		float l_161 = g_flFiveWavelength;
		float l_162 = l_160 / l_161;
		float l_163 = g_flFiveAmplitude;
		float l_164 = l_162 * l_163;
		float l_165 = l_164 * -1;
		float3 l_166 = i.vPositionWithOffsetWs.xyz + g_vHighPrecisionLightingOffsetWs.xyz;
		float l_167 = l_166.x;
		float l_168 = l_166.y;
		float4 l_169 = float4( l_167, l_168, 0, 0 );
		float l_170 = dot( l_158, l_169 );
		float l_171 = l_170 * l_162;
		float l_172 = g_flFiveSpeed;
		float l_173 = l_172 * l_162;
		float l_174 = l_173 * g_flTime;
		float l_175 = l_171 + l_174;
		float l_176 = cos( l_175 );
		float l_177 = l_165 * l_176;
		float l_178 = l_159 * l_177;
		float l_179 = l_158.y;
		float l_180 = l_179 * l_177;
		float l_181 = g_flFiveSteepness;
		float l_182 = l_162 * l_163;
		float l_183 = l_182 * 1;
		float l_184 = l_181 / l_183;
		float l_185 = l_164 * l_184;
		float l_186 = sin( l_175 );
		float l_187 = l_185 * l_186;
		float4 l_188 = float4( l_178, l_180, l_187, 0 );
		float4 l_189 = l_156 + l_188;
		float3 l_190 = g_vFiveDirection;
		float4 l_191 = float4( l_190.x, l_190.y, 0, 0 );
		float l_192 = l_191.x;
		float l_193 = 2 * 3.1415927;
		float l_194 = g_flFiveWavelength;
		float l_195 = l_193 / l_194;
		float l_196 = g_flFiveAmplitude;
		float l_197 = l_195 * l_196;
		float l_198 = l_197 * -1;
		float3 l_199 = i.vPositionWithOffsetWs.xyz + g_vHighPrecisionLightingOffsetWs.xyz;
		float l_200 = l_199.x;
		float l_201 = l_199.y;
		float4 l_202 = float4( l_200, l_201, 0, 0 );
		float l_203 = dot( l_191, l_202 );
		float l_204 = l_203 * l_195;
		float l_205 = g_flFiveSpeed;
		float l_206 = l_205 * l_195;
		float l_207 = l_206 * g_flTime;
		float l_208 = l_204 + l_207;
		float l_209 = cos( l_208 );
		float l_210 = l_198 * l_209;
		float l_211 = l_192 * l_210;
		float l_212 = l_191.y;
		float l_213 = l_212 * l_210;
		float l_214 = g_flFiveSteepness;
		float l_215 = l_195 * l_196;
		float l_216 = l_215 * 1;
		float l_217 = l_214 / l_216;
		float l_218 = l_197 * l_217;
		float l_219 = sin( l_208 );
		float l_220 = l_218 * l_219;
		float4 l_221 = float4( l_211, l_213, l_220, 0 );
		float4 l_222 = l_189 + l_221;
		float l_223 = l_222.x;
		float l_224 = l_222.y;
		float l_225 = l_222.z;
		float l_226 = 1.3 - l_225;
		float4 l_227 = float4( l_223, l_224, l_226, 0 );
		float4 l_228 = normalize( l_227 );
		float4 l_229 = float4( l_25, 0 ) * l_228;
		float3 l_230 = i.vPositionWithOffsetWs.xyz + g_vHighPrecisionLightingOffsetWs.xyz;
		float3 l_231 = pow( 1.0 - dot( normalize( l_229.xyz ), normalize( l_230 ) ), 1 );
		float4 l_232 = lerp( l_0, l_1, float4( l_231, 0 ) );
		
		m.Albedo = l_232.xyz;
		m.Opacity = 1;
		m.Normal = l_229.xyz;
		m.Roughness = 0;
		m.Metalness = 0.1056152;
		m.AmbientOcclusion = 1;
		
		m.AmbientOcclusion = saturate( m.AmbientOcclusion );
		m.Roughness = saturate( m.Roughness );
		m.Metalness = saturate( m.Metalness );
		m.Opacity = saturate( m.Opacity );
		
		return ShadingModelStandard::Shade( i, m );
	}
}
