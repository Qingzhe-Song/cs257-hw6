CHAPTER 11 MUZZLE FLASH - URP

Source: PacktPublishing/Hands-On-Unity-2023-Game-Development-Fourth-Edition
https://github.com/PacktPublishing/Hands-On-Unity-2023-Game-Development-Fourth-Edition

The original MuzzleFlashEffect.prefab is from:
Chapter 02 to Chapter 19 (SuperShooter)/Assets/Chapter 11 (CSharp)/EffectExamples/WeaponEffects/Prefabs

Its material, texture, and light references point into EffectExamples2 in the
source project. Those exact dependencies and their original GUIDs are included.

CHANGES FOR THIS PACKAGE
- Assets collected under Assets/Chapter11MuzzleFlash.
- MuzzleFlashParticle material converted to URP/Particles/Unlit, Transparent,
  Additive, using the book's Chapter 11 Explosion material as a template.
- Assigned the original FlashParticle.tif; material tint is white.
- Original prefab, texture, light, and their import metadata are unchanged.
- Requires Universal Render Pipeline (URP); it is not bundled here.

IMPORT AND USE
1. Extract the ZIP using Windows Extract All (or your operating system's unzip).
2. Copy the extracted Chapter11MuzzleFlash folder into your Unity project's
   Assets folder. Copy the entire folder, including all .meta files.
   The result should be YOUR_PROJECT/Assets/Chapter11MuzzleFlash/Prefabs/...
3. Return to Unity and wait for asset import to finish. Search the Project window
   for MuzzleFlashEffect. If necessary, use Assets > Refresh.
4. Drag the MuzzleFlashEffect prefab under your weapon and adjust its local
   position, rotation, and scale to match the barrel.
5. Assign its ParticleSystem to your script's muzzleEffect field and call
   muzzleEffect.Play() when a shot actually fires.

The prefab has Looping OFF, Play On Awake OFF, Stop Action None, Duration 0.1.
For an editor preview, select it and use the Scene view Particle Effect Play button.

VALIDATION
All asset references resolve within this package or to known URP/built-in assets.
Package contents and original asset hashes were checked. Unity runtime playback
was not tested in this environment.
