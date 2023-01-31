Toony Colors Pro, version 2.4
2020/04/01
© 2020 - Jean Moreno
=============================

QUICK START
-----------
Select one of the following shader in your Material:
- Toony Colors Pro 2/Desktop
- Toony Colors Pro 2/Mobile
- Toony Colors Pro 2/Standard PBS
- Toony Colors Pro 2/Standard PBS (Specular)
Then select the features you want to enable (bump, specular, rim...), and the correct shader will automatically
be selected for you.

If using the Lightweight/Universal Render Pipeline, you will have to generate your own shaders using the Shader Generator 2.
You can also extract the LWRP Cat demo scene from the "Cat Demo LWRP.unitypackage" file.

Read the documentation for more information.

PLEASE LEAVE A REVIEW OR RATE THE PACKAGE IF YOU FIND IT USEFUL!
Enjoy! :)


CONTACT
-------
Questions, suggestions, help needed?
Contact me at:
jean.moreno.public+unity@gmail.com

I'd be happy to see Toony Colors Pro 2 used in your project, so feel free to drop me a line about that! :)


UPDATE NOTES
------------

See full and formatted changelog here: https://jeanmoreno.com/unity/toonycolorspro/doc/changelog

2.4.33.2
- [Shader Generator 2] (URP) Fixed possible compilation errors because of new "SRP Batcher Compatibility" option
- [Shader Generator 2] Minor UI fixes

2.4.33.1
- [Shader Generator 2] (URP) Fixed broken additional passes because of new "SRP Batcher Compatibility" option

2.4.33
- [Shader Generator 2] (Default) (URP) Added "Shadow Line" feature in Stylization, to create a crisp line between highlighted and shadowed part. Can be used to create comic-book style shadows for example.
- [Shader Generator 2] Added "World Position" UV option for textures: use the vertex world position as texture UV. This can allow different effects, like using a world-space texture blending map to control multiple objects (ground, grass), or add a scrolling clouds shadow map for example.
- [Shader Generator 2] (URP) Added "SRP Batcher Compatibility" in the Options (experimental, let me know if it doesn't work!)
- [Shader Generator 2] Modified Shader Properties are now retained when changing template; this should make it easier to convert a shader from the built-in pipeline to URP for example.
- Added a few Sketch textures
- [Shader Generator 2] Added ability to use a custom font in the Options
- [Shader Generator 2] Improved UI and readability, especially in Unity 2019.3+
- [Shader Generator 2] (URP) Fixed shadow coordinates calculation changes introduced in URP 7.2.0
- [Shader Generator 2] (URP) Fixed Meta Pass
- [Shader Generator 2] Fixed possible unwanted variable name changes when changing template on an existing shader.

2.4.32
- [Shader Generator 2] (Default) (LWRP/URP) Added "Shading Ramp" hook
- [Shader Generator 2] (URP) Added options for "Silhouette" and "Outline" to make them explicitly use the "URP Render Features" and updated documentation about it
- [Shader Generator 2] (Default) (LWRP/URP) Changed "smoothness" to "roughness" for Specular PBR/GGX
- [Shader Generator 2] (LWRP/URP) Added GPU instancing and stereo rendering support
- [Shader Generator 2] (LWRP/URP) Fixed error when using "Vertical Fog" and "Enable Fog" in the same shader
- [Shader Generator 1] Fixed visual bug with spot light falloff bypass

2.4.31
- Added `Cat Demo URP` scene (extract it from the `Cat Demo URP.unitypackage` file)
- [Shader Generator 2] (LWRP/URP) Fixed compilation error when using Specular GGX along with Reflection Probes
- [Shader Generator 2] Fixed frequent compilation errors due to the new custom code prepend option
- [Shader Generator 2] Reloading shaders from the current output directory now works
- [Shader Generator 2] Fixed bug that was showing all Shader Properties as modified when copying a shader
- [Shader Generator 2] Performance optimizations in the UI
- Hopefully fixed mesh references that get lost when importing unitypackage in Cat demo scene

2.4.3
- [Shader Generator 2] Added 'Prepend Code' option for Custom Code implementation: add arbitrary code before the implementation to allow for more complex code insertion
- [Shader Generator 2] (Default) (LWRP/URP) Added "Alpha to Coverage" option for "Alpha Testing"
- [Shader Generator 2] (Default) Added "Aura 2" third-party support (experimental)
- [Shader Generator 2] Add Unity version on generated shaders, will be useful for support
- [Shader Generator 2] Changed how hash is calculated: old shaders opened in the tool will show a warning that they are modified externally, even though they are not. The warning will disappear once the shader is updated.
- [Shader Generator] (Water) Enabled 'Add Shadow Pass' option to enable shadow receiving for water shaders (note: not compatible with depth-buffer effects)
- [Shader Generator 2] Fixed UI bug that prevented some options from showing if `Show disabled fields` was disabled
- [Shader Generator 2] Fixed possible compilation error with texture coordinates
- [Shader Generator 2] Fixed "Vertical Fog" that was reapplied when using additional lights
- "Demo TCP2 Cat LWRP" scene: water shader now also works with orthographic cameras
- Fixed possible corrupted shaders when unpacking from the menu

2.4.2
- [Shader Generator 2] (Default) (LWRP/URP) Added "Vertical Fog" special effect
- [Shader Generator 2] (LWRP/URP) Fixed shader compilation error when using features with world-space view direction (e.g. "Specular")
- [Shader Generator 2] Fixed shader compilation error when using `Min Max Threshold` ceiling mode with "Triplanar Mapping"
- [Shader Generator 2] `Custom Code` implementation: fixed reference bug and improved UI on referenced implementations
- [Shader Generator 2] Fixed tiling and scrolling calculation order which could result in animation pops for Texture implementations
- [Shader Generator 1] Fixed issues with non-US culture when using C# 4.x (when using constant float values in Shader Generator 1)
- [Shader Generator 2] Fixed issue that produced a warning for generated shaders
- Cat Demo Scene: fixed mesh references in Unity 2019.1+

2.4.1
- As of version 2.4.0, the "Shaders 2.0" folder has been renamed to "Shaders".
  Any shaders and files in "Shaders 2.0" or a sub-folder (e.g. "Variants") has to be moved there for the shaders to compile properly.
- Added HTML formatted changelog
- Fixed packed shaders that were unpacking in the wrong folder, causing an #include error

2.4.0
- Added "Shader Generator 2" beta: more flexible tool, with a fully-featured Lightweight/Universal Pipeline template
See the documentation to learn more!
- Added "Cat Demo LWRP" scene (extract it from the "Cat Demo LWRP.unitypackage" file)
- Shader Generator: Added "VertExmotion" support (under "Special Effects")
- Shader Generator: Enabled Dithered Shadows when using Alpha Testing with Dithered Transparency
- Shader Generator: fixed Outline in Single-Pass Stereo rendering mode (VR)
- Added 26 MatCap textures
- Reorganized the Textures folder
- Renamed the "Shaders 2.0" folder to "Shaders"
- Added namespaces for all TCP2 classes (except material inspectors for compatibility)
- Added .asmdef files for TCP2 scripts (Unity 2019.1+)

2.3.572
- Fixed compilation error on MacOS
- Fixed issues with non-US culture when using C# 4.x
- Regression fixed: Shader Generator: "Constant Size Outline" was broken, will take objects' scale into account again

2.3.571
- Shader Generator: "Default" template: fixed Specular Mask when using PBR Blinn-Phong or GGX models
- Shader Generator: "PBS" templates: fixed compilation issues on builds when using outlines
- Shader Generator: "Surface PBS" template: fixed Emission feature
- Shader Generator: "Water" template: fixed precision issue causing artifacts on some mobile platforms
- Shader Generator: fixed "Constant Size Outline" option so that it ignores objects' scale
- Shader Generator: UI fixes with inline features

2.3.57
- Shader Generator: upgraded "SRP/Lightweight" template to latest version (4.1.0-preview with Unity 2018.3.0b9)
- Shader Generator: "Default" template: fixed "Pixel MatCap" option when using a normal map with skinned meshes
- Shader Generator: always start with a new shader when opening the tool (don't load the last generated/loaded shader anymore)
- Added example MatCap textures
- Removed 'JMOAssets.dll', became obsolete with the Asset Store update notification system

2.3.56
- Shader Generator: added "Texture Blending" feature for "Surface PBS" template
- Shader Generator: fixed non-repeating tiling for "Texture Blending" in relevant templates
- Shader Generator: fixed masks for "Surface PBS" template (Albedo Color Mask, HSV Mask, Subsurface Scattering Mask)
- Added default non-repeating tiling noise textures

2.3.55
- Shader Generator: added "Silhouette Pass" option: simple solid color silhouette for when objects are behind obstacles
- Shader Generator: fixed fog for "Standard PBS" shaders in Unity 2018.2+
- Reorganized some files and folders

2.3.54
- Shader Generator: added more Tessellation options for "Default" template: Fixed, Distance Based and Edge Length Based
- Shader Generator: added Tessellation support for "Standard PBS" template
- Desktop/Mobile shaders: removed Directional Lightmap support for shaders, so that all variants can compile properly (max number of interpolators was reached for some combination in Unity 2018+)
- Mpbile shaders: disabled view direction calculated in the vertex shader, will be calculated in the fragment instead, so that all variants compile properly (slightly slower but it frees up one interpolator)
- Shader Generator: restored 'VertexLit' fallback for Surface PBS template, so that shadow receiving works by default

2.3.53
- Shader Generator: added "Shadow Color Mode" feature with "Replace Color" option to entirely replace the Albedo with the shadow color
- Shader Generator: updated GGX Specular to be more faithful to the Standard shader implementation
- Shader Generator: fixed GGX Specular when using Linear color space
- Shader Generator: updated Lightweight SRP template to work with Unity 2018.2 and lightweight 2.0.5-preview
- Shader Generator: Lightweight SRP template still works with Unity 2017.1 and lightweight 1.1.11-preview

2.3.52
- Shader Generator: added "Vertical Fog" option for "Default" template
- Shader Generator: fixed wrong colors and transparency in "Fade" mode with "Surface Standard PBS" template
- Shader Generator: fixed disabling depth buffer writing mode depending on transparency mode with "Surface Standard PBS" template
- Shader Generator: reorganized templates UI in foldout boxes for clarity
- Shader Generator: updated UI for clarity
- Shader Generator: harmonized UI colors for Unity Editor pro skin

2.3.51
- Shader Generator: fixed issue with "Sketch" option in "Surface Standard PBS" template
- Shader Generator: fixed "Bump Scale" option in "SRP/Lightweight" template

2.3.5
- Shader Generator: added experiment "SRP/Lightweight" template with limited features (Unity 2018.1+)
	- extract "Lightweight SRP Template.unitypackage" for it to work
- Shader Generator: added "LOD Group Blending" feature (dither or cross-fade)
- script warning fix