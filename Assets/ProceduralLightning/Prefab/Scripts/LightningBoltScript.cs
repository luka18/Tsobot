﻿//
// Procedural Lightning for Unity
// (c) 2015 Digital Ruby, LLC
// Source code may be used for personal or commercial projects.
// Source code may NOT be redistributed or sold.
//

#if UNITY_4_0 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_4 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7 || UNITY_4_8 || UNITY_4_9

#define UNITY_4

#endif

#if UNITY_4 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2

#define UNITY_PRE_5_3

#endif

using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

namespace DigitalRuby.ThunderAndLightning
{
    /// <summary>
    /// Render lightning using meshes
    /// </summary>
    public class LightningBoltMeshRenderer
    {
        [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
        public class LineRendererMesh : MonoBehaviour
        {
            private static readonly Vector2 uv1 = new Vector2(0.0f, 0.0f);
            private static readonly Vector2 uv2 = new Vector2(1.0f, 0.0f);
            private static readonly Vector2 uv3 = new Vector2(0.0f, 1.0f);
            private static readonly Vector2 uv4 = new Vector2(1.0f, 1.0f);

            private readonly List<int> indices = new List<int>();

            private readonly List<Vector3> vertices = new List<Vector3>();
            private readonly List<Vector4> lineDirs = new List<Vector4>();
            private readonly List<Color32> colors = new List<Color32>();
            private readonly List<Vector3> ends = new List<Vector3>();

#if UNITY_PRE_5_3

            private readonly List<Vector2> texCoords = new List<Vector2>();
			private readonly List<Vector2> glowModifiers = new List<Vector2>();
			private readonly List<Vector2> fadeXY = new List<Vector2>();
			private readonly List<Vector2> fadeZW = new List<Vector2>();

#else

            private readonly List<Vector4> texCoordsAndGlowModifiers = new List<Vector4>();
			private readonly List<Vector4> fadeLifetimes = new List<Vector4>();

#endif

            private Vector3 currentBoundsMin;
            private Vector3 currentBoundsMax;
            private Mesh mesh;
            private MeshRenderer meshRenderer;

            private void UpdateBounds(ref Vector3 point1, ref Vector3 point2)
            {
                // r = y + ((x - y) & ((x - y) >> (sizeof(int) * CHAR_BIT - 1))); // min(x, y)
                // r = x - ((x - y) & ((x - y) >> (sizeof(int) * CHAR_BIT - 1))); // max(x, y)

                unchecked
                {
                    {
                        int xCalculation = (int)point1.x - (int)point2.x;
                        xCalculation &= (xCalculation >> 31);
                        int xMin = (int)point2.x + xCalculation;
                        int xMax = (int)point1.x - xCalculation;
                        xCalculation = (int)currentBoundsMin.x - xMin;
                        xCalculation &= (xCalculation >> 31);
                        currentBoundsMin.x = xMin + xCalculation;
                        xCalculation = (int)currentBoundsMax.x - xMax;
                        xCalculation &= (xCalculation >> 31);
                        currentBoundsMax.x = (int)currentBoundsMax.x - xCalculation;
                    }
                    {
                        int yCalculation = (int)point1.y - (int)point2.y;
                        yCalculation &= (yCalculation >> 31);
                        int yMin = (int)point2.y + yCalculation;
                        int yMax = (int)point1.y - yCalculation;
                        yCalculation = (int)currentBoundsMin.y - yMin;
                        yCalculation &= (yCalculation >> 31);
                        currentBoundsMin.y = yMin + yCalculation;
                        yCalculation = (int)currentBoundsMax.y - yMax;
                        yCalculation &= (yCalculation >> 31);
                        currentBoundsMax.y = (int)currentBoundsMax.y - yCalculation;
                    }
                    {
                        int zCalculation = (int)point1.z - (int)point2.z;
                        zCalculation &= (zCalculation >> 31);
                        int zMin = (int)point2.z + zCalculation;
                        int zMax = (int)point1.z - zCalculation;
                        zCalculation = (int)currentBoundsMin.z - zMin;
                        zCalculation &= (zCalculation >> 31);
                        currentBoundsMin.z = zMin + zCalculation;
                        zCalculation = (int)currentBoundsMax.z - zMax;
                        zCalculation &= (zCalculation >> 31);
                        currentBoundsMax.z = (int)currentBoundsMax.z - zCalculation;
                    }
                }
            }

            private void Awake()
            {
                mesh = new Mesh();
                GetComponent<MeshFilter>().sharedMesh = mesh;
                meshRenderer = GetComponent<MeshRenderer>();

#if UNITY_4

#else

                meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
				meshRenderer.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;

#endif

                meshRenderer.useLightProbes = false;
                meshRenderer.receiveShadows = false;
                meshRenderer.enabled = false;
            }

            private void AddIndices()
            {
                int vertexIndex = vertices.Count;
                indices.Add(vertexIndex++);
                indices.Add(vertexIndex++);
                indices.Add(vertexIndex);
                indices.Add(vertexIndex--);
                indices.Add(vertexIndex);
                indices.Add(vertexIndex += 2);
            }

            private void AppendLineInternal(ref Vector3 start, ref Vector3 end, ref Vector4 dir, ref Vector4 dirPrev1, ref Vector4 dirPrev2, Color32 color, ref Vector4 fadeLifeTime, float glowWidthModifier, float glowIntensity)
            {
                AddIndices();

                Vector4 texCoord = new Vector4(uv1.x, uv1.y, glowWidthModifier, glowIntensity);

                vertices.Add(start);
                lineDirs.Add(dirPrev1);
                colors.Add(color);
                ends.Add(dir);

                vertices.Add(end);
                lineDirs.Add(dir);
                colors.Add(color);
                ends.Add(dir);

                dir.w = -dir.w;

                vertices.Add(start);
                lineDirs.Add(dirPrev2);
                colors.Add(color);
                ends.Add(dir);

                vertices.Add(end);
                lineDirs.Add(dir);
                colors.Add(color);
                ends.Add(dir);

#if UNITY_PRE_5_3

                texCoords.Add(uv1);
				texCoords.Add(uv2);
				texCoords.Add(uv3);
				texCoords.Add(uv4);
				glowModifiers.Add(new Vector2(texCoord.z, texCoord.w));
				glowModifiers.Add(new Vector2(texCoord.z, texCoord.w));
				glowModifiers.Add(new Vector2(texCoord.z, texCoord.w));
				glowModifiers.Add(new Vector2(texCoord.z, texCoord.w));
				fadeXY.Add(new Vector2(fadeLifeTime.x, fadeLifeTime.y));
				fadeXY.Add(new Vector2(fadeLifeTime.x, fadeLifeTime.y));
				fadeXY.Add(new Vector2(fadeLifeTime.x, fadeLifeTime.y));
				fadeXY.Add(new Vector2(fadeLifeTime.x, fadeLifeTime.y));
				fadeZW.Add(new Vector2(fadeLifeTime.z, fadeLifeTime.w));
				fadeZW.Add(new Vector2(fadeLifeTime.z, fadeLifeTime.w));
				fadeZW.Add(new Vector2(fadeLifeTime.z, fadeLifeTime.w));
				fadeZW.Add(new Vector2(fadeLifeTime.z, fadeLifeTime.w));

#else

                texCoordsAndGlowModifiers.Add(texCoord);
                texCoord.x = uv2.x;
                texCoord.y = uv2.y;
                texCoordsAndGlowModifiers.Add(texCoord);
                texCoord.x = uv3.x;
                texCoord.y = uv3.y;
                texCoordsAndGlowModifiers.Add(texCoord);
                texCoord.x = uv4.x;
                texCoord.y = uv4.y;
                texCoordsAndGlowModifiers.Add(texCoord);
                fadeLifetimes.Add(fadeLifeTime);
                fadeLifetimes.Add(fadeLifeTime);
                fadeLifetimes.Add(fadeLifeTime);
                fadeLifetimes.Add(fadeLifeTime);

#endif

                UpdateBounds(ref start, ref end);
            }

            public void Begin()
            {
                if (vertices.Count == 0)
                {
                    return;
                }

                meshRenderer.enabled = true;

#if UNITY_PRE_5_3

                mesh.vertices = vertices.ToArray();
                mesh.tangents = lineDirs.ToArray();
                mesh.colors32 = colors.ToArray();
				mesh.uv = texCoords.ToArray();
				mesh.uv2 = glowModifiers.ToArray();

// Unity 5.0 - 5.2.X has to use uv3 and uv4
// Unity 4.X does not support glow or fade or elapsed time
#if UNITY_5_0 || UNITY_5_1 || UNITY_5_2

				mesh.uv3 = fadeXY.ToArray();
				mesh.uv4 = fadeZW.ToArray();

#endif

                mesh.normals = ends.ToArray();
                mesh.triangles = indices.ToArray();

#else

                mesh.SetVertices(vertices);
                mesh.SetTangents(lineDirs);
                mesh.SetColors(colors);
                mesh.SetUVs(0, texCoordsAndGlowModifiers);
                mesh.SetUVs(1, fadeLifetimes);
                mesh.SetNormals(ends);
                mesh.SetTriangles(indices, 0);

#endif

                Bounds b = new Bounds(Vector3.zero, (currentBoundsMax - currentBoundsMin) * 1.2f);
                mesh.bounds = b;
            }

            public bool PrepareForLines(int lineCount)
            {
                int vertexCount = lineCount * 4;
                if (vertices.Count + vertexCount > 64999)
                {
                    return false;
                }
                return true;
            }

            public void BeginLine(Vector3 start, Vector3 end, float radius, Color32 color, Vector4 fadeLifeTime, float glowWidthModifier, float glowIntensity)
            {
                Vector4 dir = (end - start);
                dir.w = radius;
                AppendLineInternal(ref start, ref end, ref dir, ref dir, ref dir, color, ref fadeLifeTime, glowWidthModifier, glowIntensity);
            }

            public void AppendLine(Vector3 start, Vector3 end, float radius, Color32 color, Vector4 fadeLifeTime, float glowWidthModifier, float glowIntensity)
            {
                Vector4 dir = (end - start);
                dir.w = radius;
                Vector4 dirPrev1 = lineDirs[lineDirs.Count - 3];
                Vector4 dirPrev2 = lineDirs[lineDirs.Count - 1];
                AppendLineInternal(ref start, ref end, ref dir, ref dirPrev1, ref dirPrev2, color, ref fadeLifeTime, glowWidthModifier, glowIntensity);
            }

            public void Reset()
            {
                meshRenderer.enabled = false;
                mesh.triangles = null;
                indices.Clear();
                vertices.Clear();
                colors.Clear();
                lineDirs.Clear();
                ends.Clear();

#if UNITY_PRE_5_3

				texCoords.Clear();
				glowModifiers.Clear();
				fadeXY.Clear();
				fadeZW.Clear();

#else

                texCoordsAndGlowModifiers.Clear();
                fadeLifetimes.Clear();

#endif

                currentBoundsMax = new Vector3(float.MinValue, float.MinValue, float.MinValue);
                currentBoundsMin = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            }

            public Material Material
            {
                get { return meshRenderer.sharedMaterial; }
                set { meshRenderer.sharedMaterial = value; }
            }

            public MeshRenderer MeshRenderer
            {
                get { return meshRenderer; }
            }
        }

        private LineRendererMesh currentLineRenderer;
        private readonly Dictionary<LightningBolt, List<LineRendererMesh>> renderers = new Dictionary<LightningBolt, List<LineRendererMesh>>();
        private readonly List<LineRendererMesh> rendererCache = new List<LineRendererMesh>();

        private IEnumerator EnableRenderer(LineRendererMesh renderer, LightningBolt lightningBolt)
        {
            yield return new WaitForSeconds(lightningBolt.MinimumDelay);

            if (renderer != null && lightningBolt.IsActive)
            {
                renderer.Begin();
            }
        }

        private LineRendererMesh CreateLineRenderer(LightningBolt lightningBolt, List<LineRendererMesh> lineRenderers)
        {
            LineRendererMesh lineRenderer;

            if (rendererCache.Count == 0)
            {
                // create a line renderer in a new game object underneath the parent
                GameObject obj = new GameObject();
                obj.name = "LightningBoltMeshRenderer";
                obj.hideFlags = HideFlags.HideAndDontSave;
                lineRenderer = obj.AddComponent<LineRendererMesh>();
            }
            else
            {
                lineRenderer = rendererCache[rendererCache.Count - 1];
                rendererCache.RemoveAt(rendererCache.Count - 1);
            }

            // clear parent - this ensures that the rotation and scale can be reset before assigning a new parent
            lineRenderer.gameObject.transform.parent = null;
            lineRenderer.gameObject.transform.rotation = Quaternion.identity;
            lineRenderer.gameObject.transform.localScale = Vector3.one;
            lineRenderer.gameObject.transform.parent = lightningBolt.Parent.transform;
            lineRenderer.gameObject.layer = lightningBolt.Parent.layer; // maintain the layer of the parent

            if (lightningBolt.UseWorldSpace)
            {
                lineRenderer.gameObject.transform.position = Vector3.zero;
            }
            else
            {
                lineRenderer.gameObject.transform.localPosition = Vector3.zero;
            }

            lineRenderer.Material = (lightningBolt.HasGlow ? Material : MaterialNoGlow);
            lineRenderer.MeshRenderer.sortingLayerName = lightningBolt.SortLayerName;
            lineRenderer.MeshRenderer.sortingOrder = lightningBolt.SortOrderInLayer;
            currentLineRenderer = lineRenderer;
            lineRenderers.Add(lineRenderer);

            return lineRenderer;
        }

        public void Begin(LightningBolt lightningBolt)
        {
            List<LineRendererMesh> lineRenderers;
            if (!renderers.TryGetValue(lightningBolt, out lineRenderers))
            {
                lineRenderers = new List<LineRendererMesh>();
                renderers[lightningBolt] = lineRenderers;
                CreateLineRenderer(lightningBolt, lineRenderers);
            }
        }

        public void End(LightningBolt lightningBolt)
        {
            if (currentLineRenderer != null)
            {
                Script.StartCoroutine(EnableRenderer(currentLineRenderer, lightningBolt));
                currentLineRenderer = null;
            }
        }

        public void AddGroup(LightningBolt lightningBolt, LightningBoltSegmentGroup group, float growthMultiplier)
        {
            if (group.SegmentCount == 0)
            {
                return;
            }

            List<LineRendererMesh> lineRenderers = renderers[lightningBolt];
            LineRendererMesh lineRenderer = lineRenderers[lineRenderers.Count - 1];
            float timeStart = Time.timeSinceLevelLoad + group.Delay;
            Vector4 fadeLifeTime = new Vector4(timeStart, timeStart + group.PeakStart, timeStart + group.PeakEnd, timeStart + group.LifeTime);
            float radius = group.LineWidth * 0.5f * LightningBoltParameters.Scale;
            int lineCount = (group.Segments.Count - group.StartIndex);
            float radiusStep = (radius - (radius * group.EndWidthMultiplier)) / (float)lineCount;

            // growth multiplier
            float timeStep, timeOffset;
            if (growthMultiplier > 0.0f)
            {
                timeStep = (group.LifeTime / (float)lineCount) * growthMultiplier;
                timeOffset = 0.0f;
            }
            else
            {
                timeStep = 0.0f;
                timeOffset = 0.0f;
            }

            if (!lineRenderer.PrepareForLines(lineCount))
            {
                Script.StartCoroutine(EnableRenderer(lineRenderer, lightningBolt));
                lineRenderer = CreateLineRenderer(lightningBolt, lineRenderers);
            }

            lineRenderer.BeginLine(group.Segments[group.StartIndex].Start, group.Segments[group.StartIndex].End, radius, group.Color, fadeLifeTime, GlowWidthMultiplier, GlowIntensityMultiplier);
            for (int i = group.StartIndex + 1; i < group.Segments.Count; i++)
            {
                radius -= radiusStep;
                if (growthMultiplier < 1.0f)
                {
                    timeOffset += timeStep;
                    fadeLifeTime = new Color(timeStart + timeOffset, timeStart + group.PeakStart + timeOffset, timeStart + group.PeakEnd, timeStart + group.LifeTime);
                }
                lineRenderer.AppendLine(group.Segments[i].Start, group.Segments[i].End, radius, group.Color, fadeLifeTime, GlowWidthMultiplier, GlowIntensityMultiplier);
            }
        }

        public void Cleanup(LightningBolt lightningBolt)
        {
            List<LineRendererMesh> lineRenderers;
            if (renderers.TryGetValue(lightningBolt, out lineRenderers))
            {
                renderers.Remove(lightningBolt);
                foreach (LineRendererMesh lineRenderer in lineRenderers)
                {
                    rendererCache.Add(lineRenderer);
                    lineRenderer.Reset();
                }
            }
        }

        /// <summary>
        /// Lightning bolt script
        /// </summary>
        public LightningBoltScript Script { get; set; }

        /// <summary>
        /// Material
        /// </summary>
        public Material Material { get; set; }

        /// <summary>
        /// No glow material
        /// </summary>
        public Material MaterialNoGlow { get; set; }

        /// <summary>
        /// Glow width multiplier (0 for none)
        /// </summary>
        public float GlowWidthMultiplier { get; set; }

        /// <summary>
        /// Glow intensity multiplier (0 for none)
        /// </summary>
        public float GlowIntensityMultiplier { get; set; }
    }

    [System.Serializable]
    public class LightningLightParameters
    {
        /// <summary>
        /// Light render mode
        /// </summary>
        [Tooltip("Light render mode - leave as auto unless you have special use cases")]
        [HideInInspector]
        public LightRenderMode RenderMode = LightRenderMode.Auto;

        /// <summary>
        /// Color of light
        /// </summary>
        [Tooltip("Color of the light")]
        public Color LightColor = Color.white;

        /// <summary>
        /// What percent of segments should have a light? Keep this pretty low for performance, i.e. 0.05 or lower depending on generations
        /// Set really really low to only have 1 light, i.e. 0.0000001f
        /// For example, at generations 5, the main trunk has 32 segments, 64 at generation 6, etc.
        /// If non-zero, there wil be at least one light in the middle
        /// </summary>
        [Tooltip("What percent of segments should have a light? For performance you may want to keep this small.")]
        [Range(0.0f, 1.0f)]
        public float LightPercent = 0.000001f;

        /// <summary>
        /// What percent of lights created should cast shadows?
        /// </summary>
        [Tooltip("What percent of lights created should cast shadows?")]
        [Range(0.0f, 1.0f)]
        public float LightShadowPercent;

        /// <summary>
        /// Light intensity
        /// </summary>
        [Tooltip("Light intensity")]
        [Range(0.0f, 8.0f)]
        public float LightIntensity = 0.5f;

        /// <summary>
        /// Bounce intensity
        /// </summary>
        [Tooltip("Bounce intensity")]
        [Range(0.0f, 8.0f)]
        public float BounceIntensity;

        /// <summary>
        /// Shadow strength, 0 - 1. 0 means all light, 1 means all shadow
        /// </summary>
        [Tooltip("Shadow strength, 0 means all light, 1 means all shadow")]
        [Range(0.0f, 1.0f)]
        public float ShadowStrength = 1.0f;

        /// <summary>
        /// Shadow bias
        /// </summary>
        [Tooltip("Shadow bias, 0 - 2")]
        [Range(0.0f, 2.0f)]
        public float ShadowBias = 0.05f;

        /// <summary>
        /// Shadow normal bias
        /// </summary>
        [Tooltip("Shadow normal bias, 0 - 3")]
        [Range(0.0f, 3.0f)]
        public float ShadowNormalBias = 0.4f;

        /// <summary>
        /// Light range
        /// </summary>
        [Tooltip("The range of each light created")]
        public float LightRange;

        /// <summary>
        /// Only light up objects that match this layer mask
        /// </summary>
        [Tooltip("Only light objects that match this layer mask")]
        public LayerMask CullingMask = ~0;

        /// <summary>
        /// Should light be shown for these parameters?
        /// </summary>
        public bool HasLight
        {
            get { return (LightColor.a > 0.0f && LightIntensity >= 0.01f && LightPercent >= 0.0000001f && LightRange > 0.01f); }
        }
    }

    /// <summary>
    /// Parameters that control lightning bolt behavior
    /// </summary>
    [System.Serializable]
    public class LightningBoltParameters
    {
        internal int generationWhereForksStop;
        internal int forkednessCalculated;
        internal LightningBoltQualitySetting quality;

        /// <summary>
        /// Scale all scalar parameters by this value (i.e. trunk width, turbulence, turbulence velocity)
        /// </summary>
        public static float Scale = 1.0f;

        /// <summary>
        /// Contains quality settings for different quality levels. By default, this assumes 6 quality levels, so if you have your own
        /// custom quality setting levels, you may want to clear this dictionary out and re-populate it with your own limits
        /// </summary>
        public static readonly Dictionary<int, LightningQualityMaximum> QualityMaximums = new Dictionary<int, LightningQualityMaximum>();

        static LightningBoltParameters()
        {
            string[] names = QualitySettings.names;
            for (int i = 0; i < names.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        QualityMaximums[i] = new LightningQualityMaximum { MaximumGenerations = 3, MaximumLightPercent = 0, MaximumShadowPercent = 0.0f };
                        break;
                    case 1:
                        QualityMaximums[i] = new LightningQualityMaximum { MaximumGenerations = 4, MaximumLightPercent = 0, MaximumShadowPercent = 0.0f };
                        break;
                    case 2:
                        QualityMaximums[i] = new LightningQualityMaximum { MaximumGenerations = 5, MaximumLightPercent = 0.1f, MaximumShadowPercent = 0.0f };
                        break;
                    case 3:
                        QualityMaximums[i] = new LightningQualityMaximum { MaximumGenerations = 5, MaximumLightPercent = 0.1f, MaximumShadowPercent = 0.0f };
                        break;
                    case 4:
                        QualityMaximums[i] = new LightningQualityMaximum { MaximumGenerations = 6, MaximumLightPercent = 0.05f, MaximumShadowPercent = 0.1f };
                        break;
                    case 5:
                        QualityMaximums[i] = new LightningQualityMaximum { MaximumGenerations = 7, MaximumLightPercent = 0.025f, MaximumShadowPercent = 0.05f };
                        break;
                    default:
                        QualityMaximums[i] = new LightningQualityMaximum { MaximumGenerations = 8, MaximumLightPercent = 0.025f, MaximumShadowPercent = 0.05f };
                        break;
                }
            }
        }

        /// <summary>
        /// Generator to create the lightning bolt from the parameters
        /// </summary>
        public LightningGenerator Generator = LightningGenerator.GeneratorInstance;

        /// <summary>
        /// Start of the bolt
        /// </summary>
        public Vector3 Start;

        /// <summary>
        /// End of the bolt
        /// </summary>
        public Vector3 End;

        private int generations;
        /// <summary>
        /// Number of generations (0 for just a point light, otherwise 1 - 8). Higher generations have lightning with finer detail but more expensive to create.
        /// </summary>
        public int Generations
        {
            get { return generations; }
            set
            {
                int v = value;
                if (v < 1)
                {
                    // we just want a point light and we set the trunk width to 0 so the line doesn't show up
                    TrunkWidth = 0.0f;
                    v = 0;
                }
                else if (v > 8)
                {
                    v = 8;
                }

                if (quality == LightningBoltQualitySetting.UseScript)
                {
                    generations = v;
                }
                else
                {
                    LightningQualityMaximum maximum;
                    int level = QualitySettings.GetQualityLevel();
                    if (QualityMaximums.TryGetValue(level, out maximum))
                    {
                        generations = Mathf.Min(maximum.MaximumGenerations, v);
                    }
                    else
                    {
                        generations = v;
                        Debug.LogError("Unable to read lightning quality settings from level " + level.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// How long the bolt should live in seconds
        /// </summary>
        public float LifeTime;

        /// <summary>
        /// How long to wait in seconds before starting the lightning bolt
        /// </summary>
        public float Delay;

        /// <summary>
        /// How chaotic is the lightning? (0 - 1). Higher numbers create more chaotic lightning.
        /// </summary>
        public float ChaosFactor;

        /// <summary>
        /// The width of the trunk
        /// </summary>
        public float TrunkWidth;

        /// <summary>
        /// The ending width of a segment of lightning
        /// </summary>
        public float EndWidthMultiplier = 0.5f;

        /// <summary>
        /// Intensity of glow (0-1)
        /// </summary>
        public float GlowIntensity;

        /// <summary>
        /// Glow width multiplier
        /// </summary>
        public float GlowWidthMultiplier;

        /// <summary>
        /// How forked the lightning should be, 0 for none, 1 for LOTS of forks
        /// </summary>
        public float Forkedness;

        /// <summary>
        /// This is subtracted from the initial generations value, and any generation below that cannot have a fork
        /// </summary>
        public int GenerationWhereForksStopSubtractor = 5;

        /// <summary>
        /// Used to generate random numbers, can be null. Passing a random with the same seed and parameters will result in the same lightning.
        /// </summary>
        public System.Random Random;

        /// <summary>
        /// The percent of time the lightning should fade in and out (0 - 1). Example: 0.2 would fade in for 20% of the lifetime and fade out for 20% of the lifetime. Set to 0 for no fade.
        /// </summary>
        public float FadePercent;

        private float growthMultiplier;
        /// <summary>
        /// A value between 0 and 0.999 that determines how fast the lightning should grow over the lifetime. A value of 1 grows slowest, 0 grows instantly
        /// </summary>
        public float GrowthMultiplier
        {
            get { return growthMultiplier; }
            set { growthMultiplier = Mathf.Clamp(value, 0.0f, 0.999f); }
        }

        /// <summary>
        /// Minimum distance multiplier for forks
        /// </summary>
        public float ForkLengthMultiplier = 0.6f;

        /// <summary>
        /// Variance of the fork distance (random range of 0 to n is added to ForkLengthMultiplier)
        /// </summary>
        public float ForkLengthVariance = 0.2f;

        /// <summary>
        /// Forks will have their end widths multiplied by this value
        /// </summary>
        public float ForkEndWidthMultiplier = 1.0f;

        /// <summary>
        /// Light parameters, null for none
        /// </summary>
        public LightningLightParameters LightParameters;

        /// <summary>
        /// Get a multiplier for fork distance
        /// </summary>
        /// <returns>Fork multiplier</returns>
        public float ForkMultiplier()
        {
            return ((float)Random.NextDouble() * ForkLengthVariance) + ForkLengthMultiplier;
        }
    }

    /// <summary>
    /// A group of lightning bolt segments, such as the main trunk of the lightning bolt
    /// </summary>
    public class LightningBoltSegmentGroup
    {
        /// <summary>
        /// Width
        /// </summary>
        public float LineWidth;

        /// <summary>
        /// Start index of the segment to render (for performance, some segments are not rendered and only used for calculations)
        /// </summary>
        public int StartIndex;

        /// <summary>
        /// Generation
        /// </summary>
        public int Generation;

        /// <summary>
        /// Delay before rendering should start
        /// </summary>
        public float Delay;

        /// <summary>
        /// Peak start, the segments should be fully visible at this point
        /// </summary>
        public float PeakStart;

        /// <summary>
        /// Peak end, the segments should start to go away after this point
        /// </summary>
        public float PeakEnd;

        /// <summary>
        /// Total life time the group will be alive in seconds
        /// </summary>
        public float LifeTime;

        /// <summary>
        /// The width can be scaled down to the last segment by this amount if desired
        /// </summary>
        public float EndWidthMultiplier;

        /// <summary>
        /// Color for the group
        /// </summary>
        public Color32 Color;

        /// <summary>
        /// Total number of active segments
        /// </summary>
        public int SegmentCount { get { return Segments.Count - StartIndex; } }

        /// <summary>
        /// Segments
        /// </summary>
        public readonly List<LightningBoltSegment> Segments = new List<LightningBoltSegment>();

        /// <summary>
        /// Lights
        /// </summary>
        public readonly List<Light> Lights = new List<Light>();

        /// <summary>
        /// Light parameters
        /// </summary>
        public LightningLightParameters LightParameters;
    }

    /// <summary>
    /// A single segment of a lightning bolt
    /// </summary>
    public struct LightningBoltSegment
    {
        public Vector3 Start;
        public Vector3 End;

        public override string ToString()
        {
            return Start.ToString() + ", " + End.ToString();
        }
    }

    /// <summary>
    /// Contains maximum values for a given quality settings
    /// </summary>
    public class LightningQualityMaximum
    {
        /// <summary>
        /// Maximum generations
        /// </summary>
        public int MaximumGenerations { get; set; }

        /// <summary>
        /// Maximum light percent
        /// </summary>
        public float MaximumLightPercent { get; set; }

        /// <summary>
        /// Maximum light shadow percent
        /// </summary>
        public float MaximumShadowPercent { get; set; }
    }

    /// <summary>
    /// Lightning bolt
    /// </summary>
    public class LightningBolt
    {
        /// <summary>
        /// The maximum number of lights to allow for all lightning
        /// </summary>
        public static int MaximumLightCount = 128;

        /// <summary>
        /// The maximum number of lights to create per batch of lightning emitted
        /// </summary>
        public static int MaximumLightsPerBatch = 8;

        /// <summary>
        /// Parent game object
        /// </summary>
        public GameObject Parent { get; private set; }

        /// <summary>
        /// The current minimum delay until anything will start rendering
        /// </summary>
        public float MinimumDelay { get; private set; }

        /// <summary>
        /// Is there any glow for any of the lightning bolts?
        /// </summary>
        public bool HasGlow { get; private set; }

        /// <summary>
        /// Is this lightning bolt active any more?
        /// </summary>
        public bool IsActive { get { return elapsedTime < lifeTime; } }

        /// <summary>
        /// The camera the lightning should be visible in
        /// </summary>
        public Camera Camera { get; private set; }

        /// <summary>
        /// True to use world space, false to use local space
        /// </summary>
        public bool UseWorldSpace { get; set; }

        /// <summary>
        /// Sort layer name
        /// </summary>
        public string SortLayerName { get; set; }

        /// <summary>
        /// Order in sort layer
        /// </summary>
        public int SortOrderInLayer { get; set; }

        /// <summary>
        /// Script this lightning bolt was created from
        /// </summary>
        public LightningBoltScript Script { get; private set; }

        // how long this bolt has been alive
        private float elapsedTime;

        // total life span of this bolt
        private float lifeTime;

        private LightningBoltMeshRenderer lightningBoltRenderer;
        private Light lightOnlyObject;
        private LightningLightParameters lightOnlyParameters;
        private bool hasLight;

        internal readonly List<LightningBoltSegmentGroup> segmentGroups = new List<LightningBoltSegmentGroup>();
        internal readonly List<LightningBoltSegmentGroup> segmentGroupsWithLight = new List<LightningBoltSegmentGroup>();

        private static int lightCount;
        private static readonly List<Light> lightCache = new List<Light>();
        private static readonly List<LightningBolt> lightningBoltCache = new List<LightningBolt>();

        private void CleanupLight(Light l)
        {
            if (l != null)
            {
                lightCache.Add(l);
                l.gameObject.SetActive(false);
                lightCount--;
            }
        }

        public static LightningBolt GetOrCreateLightningBolt()
        {
            if (lightningBoltCache.Count == 0)
            {
                return new LightningBolt();
            }
            LightningBolt b = lightningBoltCache[lightningBoltCache.Count - 1];
            lightningBoltCache.RemoveAt(lightningBoltCache.Count - 1);

            return b;
        }

        public void Initialize(Camera camera, bool useWorldSpace, LightningBoltMeshRenderer lightningBoltRenderer,
            GameObject parent, LightningBoltScript script, ParticleSystem originParticleSystem,
            ParticleSystem destParticleSystem, ICollection<LightningBoltParameters> parameters)
        {

#if DEBUG

            // setup properties
            if (parameters == null || lightningBoltRenderer == null || parameters.Count == 0 || script == null)
            {
                Debug.LogError("Parameters, renderer and script must be non-null");
                return;
            }

#endif

            this.Camera = camera;
            this.UseWorldSpace = useWorldSpace;
            this.SortLayerName = script.SortLayerName;
            this.SortOrderInLayer = script.SortOrderInLayer;
            this.lightningBoltRenderer = lightningBoltRenderer;
            this.Parent = parent;
            this.Script = script;

            CheckForGlow(parameters);
            lightningBoltRenderer.Begin(this);
            MinimumDelay = float.MaxValue;
            int maxLightsForEachParameters = MaximumLightsPerBatch / parameters.Count;
            foreach (LightningBoltParameters p in parameters)
            {
                ProcessParameters(p, originParticleSystem, destParticleSystem, maxLightsForEachParameters);
            }
            lightningBoltRenderer.End(this);
        }

        public bool Update()
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > lifeTime)
            {
                Cleanup();
                return false;
            }
            else if (hasLight)
            {
                UpdateLights();
            }
            return true;
        }

        public void Cleanup()
        {
            foreach (LightningBoltSegmentGroup g in segmentGroups)
            {
                foreach (Light l in g.Lights)
                {
                    CleanupLight(l);
                }
                g.LightParameters = null;
                g.Segments.Clear();
                g.Lights.Clear();
                g.StartIndex = 0;
                LightningGenerator.groupCache.Add(g);
            }
            CleanupLight(lightOnlyObject);
            lightOnlyObject = null;
            lightOnlyParameters = null;
            segmentGroups.Clear();
            segmentGroupsWithLight.Clear();
            if (lightningBoltRenderer != null)
            {
                lightningBoltRenderer.Cleanup(this);
                lightningBoltRenderer = null;
            }
            hasLight = false;
            elapsedTime = 0.0f;
            lifeTime = 0.0f;
            lightningBoltCache.Add(this);
        }

        private void ProcessParameters(LightningBoltParameters p, ParticleSystem sourceParticleSystem, ParticleSystem destinationParticleSystem, int maxLights)
        {
            p.Generator.CurrentBolt = this;
            MinimumDelay = Mathf.Min(p.Delay, MinimumDelay);
            p.generationWhereForksStop = p.Generations - p.GenerationWhereForksStopSubtractor;
            p.GlowIntensity = Mathf.Clamp(p.GlowIntensity, 0.0f, 1.0f);
            p.Random = (p.Random ?? new System.Random(System.Environment.TickCount));
            lifeTime = Mathf.Max(p.LifeTime + p.Delay, lifeTime);
            LightningLightParameters lp = p.LightParameters;
            if (lp != null)
            {
                if ((hasLight |= lp.HasLight))
                {
                    lp.LightPercent = Mathf.Clamp(lp.LightPercent, Mathf.Epsilon, 1.0f);
                    lp.LightShadowPercent = Mathf.Clamp(lp.LightShadowPercent, 0.0f, 1.0f);
                }
                else
                {
                    lp = null;
                }
            }

            p.forkednessCalculated = (int)Mathf.Ceil(p.Forkedness * (float)p.Generations);
            int groupIndex = segmentGroups.Count;
            if (p.Generations > 0)
            {
                p.Generator.GenerateLightningBolt(p);
            }
            RenderLightningBolt(p.quality, p.Generations, p.Start, p.End, groupIndex, sourceParticleSystem, destinationParticleSystem, p, lp, maxLights);
        }

        private void RenderLightningBolt(LightningBoltQualitySetting quality, int generations, Vector3 start, Vector3 end, int groupIndex,
            ParticleSystem originParticleSystem, ParticleSystem destParticleSystem, LightningBoltParameters parameters,
            LightningLightParameters lp, int maxLights)
        {
            if (parameters.Generations < 1)
            {
                CreateLightOnly(parameters, lp, start);
                return;
            }
            else if (segmentGroups.Count == 0 || groupIndex >= segmentGroups.Count)
            {
                return;
            }

            float delayBase = parameters.LifeTime / (float)segmentGroups.Count;
            float minDelayValue = delayBase * 0.9f;
            float maxDelayValue = delayBase * 1.1f;
            float delayDiff = maxDelayValue - minDelayValue;
            parameters.FadePercent = Mathf.Clamp(parameters.FadePercent, 0.0f, 0.5f);

            if (originParticleSystem != null)
            {
                // we have a strike, create a particle where the lightning is coming from
                Script.StartCoroutine(GenerateParticle(originParticleSystem, start, parameters.Delay));
            }
            if (destParticleSystem != null)
            {
                Script.StartCoroutine(GenerateParticle(destParticleSystem, end, parameters.Delay * 1.1f));
            }

            if (HasGlow)
            {
                lightningBoltRenderer.GlowIntensityMultiplier = parameters.GlowIntensity;
                lightningBoltRenderer.GlowWidthMultiplier = parameters.GlowWidthMultiplier;
            }

            float currentDelayAmount = 0.0f;
            for (int i = groupIndex; i < segmentGroups.Count; i++)
            {
                LightningBoltSegmentGroup group = segmentGroups[i];
                group.Delay = currentDelayAmount + parameters.Delay;
                group.LifeTime = parameters.LifeTime - currentDelayAmount;
                group.PeakStart = group.LifeTime * parameters.FadePercent;
                group.PeakEnd = group.LifeTime - group.PeakStart;
                group.LightParameters = lp;

                lightningBoltRenderer.AddGroup(this, group, parameters.GrowthMultiplier);
                currentDelayAmount += ((float)parameters.Random.NextDouble() * minDelayValue) + delayDiff;

                // create lights only on the main trunk
                if (lp != null && group.Generation == generations)
                {
                    CreateLightsForGroup(group, lp, quality, maxLights, groupIndex);
                }
            }
        }

        private void CreateLightOnly(LightningBoltParameters p, LightningLightParameters lp, Vector3 pos)
        {
            lightOnlyParameters = lp;
            lightOnlyObject = CreateLight(lp);
            hasLight = true;
        }

        private void CreateLightsForGroup(LightningBoltSegmentGroup group, LightningLightParameters lp, LightningBoltQualitySetting quality,
            int maxLights, int groupIndex)
        {
            if (lightCount == MaximumLightCount || maxLights <= 0)
            {
                return;
            }

            segmentGroupsWithLight.Add(group);

            int segmentCount = group.SegmentCount;
            float lightPercent, lightShadowPercent;
            if (quality == LightningBoltQualitySetting.LimitToQualitySetting)
            {
                int level = QualitySettings.GetQualityLevel();
                LightningQualityMaximum maximum;
                if (LightningBoltParameters.QualityMaximums.TryGetValue(level, out maximum))
                {
                    lightPercent = Mathf.Min(lp.LightPercent, maximum.MaximumLightPercent);
                    lightShadowPercent = Mathf.Min(lp.LightShadowPercent, maximum.MaximumShadowPercent);
                }
                else
                {
                    Debug.LogError("Unable to read lightning quality for level " + level.ToString());
                    lightPercent = lp.LightPercent;
                    lightShadowPercent = lp.LightShadowPercent;
                }
            }
            else
            {
                lightPercent = lp.LightPercent;
                lightShadowPercent = lp.LightShadowPercent;
            }

            maxLights = Mathf.Max(1, Mathf.Min(maxLights, (int)(segmentCount * lightPercent)));
            int nthLight = Mathf.Max(1, (int)((segmentCount / maxLights)));
            int nthShadows = maxLights - (int)((float)maxLights * lightShadowPercent);

            int nthShadowCounter = nthShadows;

            // add lights evenly spaced
            for (int i = group.StartIndex + (int)(nthLight * 0.5f); i < group.Segments.Count; i += nthLight)
            {
                if (AddLightToGroup(group, lp, i, nthLight, nthShadows, ref maxLights, ref nthShadowCounter))
                {
                    return;
                }
            }

            // Debug.Log("Lightning light count: " + lightCount.ToString());
        }

        private bool AddLightToGroup(LightningBoltSegmentGroup group, LightningLightParameters lp, int segmentIndex,
            int nthLight, int nthShadows, ref int maxLights, ref int nthShadowCounter)
        {
            Light light = CreateLight(lp);
            group.Lights.Add(light);
            Vector3 pos = (group.Segments[segmentIndex].Start + group.Segments[segmentIndex].End) * 0.5f;
            if (Camera != null && Camera.orthographic)
            {
                pos.z = Camera.transform.position.z;
            }
            light.gameObject.transform.position = pos;
            if (lp.LightShadowPercent == 0.0f || ++nthShadowCounter < nthShadows)
            {
                light.shadows = LightShadows.None;
            }
            else
            {
                light.shadows = LightShadows.Soft;
                nthShadowCounter = 0;
            }

            // return true if no more lights possible, false otherwise
            return (++lightCount == MaximumLightCount || --maxLights == 0);
        }

        private Light CreateLight(LightningLightParameters lp)
        {
            Light light;
            while (true)
            {
                if (lightCache.Count == 0)
                {
                    GameObject lightningLightObject = new GameObject();
                    lightningLightObject.hideFlags = HideFlags.HideAndDontSave;
                    lightningLightObject.name = "LightningBoltLight";
                    light = lightningLightObject.AddComponent<Light>();
                    light.type = LightType.Point;
                    break;
                }
                else
                {
                    light = lightCache[lightCache.Count - 1];
                    lightCache.RemoveAt(lightCache.Count - 1);
                    if (light == null)
                    {
                        // may have been disposed or the level re-loaded
                        continue;
                    }
                    break;
                }
            }

#if UNITY_4

#else

			light.bounceIntensity = lp.BounceIntensity;
			light.shadowNormalBias = lp.ShadowNormalBias;

#endif

            light.color = lp.LightColor;
            light.renderMode = lp.RenderMode;
            light.range = lp.LightRange;
            light.shadowStrength = lp.ShadowStrength;
            light.shadowBias = lp.ShadowBias;
            light.intensity = 0.0f;
            light.gameObject.transform.parent = Parent.transform;
            light.gameObject.SetActive(true);

            return light;
        }

        private void UpdateLightOnlyObject()
        {
            if (lightOnlyObject == null)
            {
                return;
            }

            UpdateLight(lightOnlyParameters, new Light[] { lightOnlyObject }, MinimumDelay, 0.2f, lifeTime - 0.2f, lifeTime);
        }

        private void UpdateLight(LightningLightParameters lp, IEnumerable<Light> lights, float delay, float peakStart, float peakEnd, float lifeTime)
        {
            if (elapsedTime < delay)
            {
                return;
            }

            // depending on whether we have hit the mid point of our lifetime, fade the light in or out
            float realElapsedTime = elapsedTime - delay;
            if (realElapsedTime >= peakStart)
            {
                if (realElapsedTime <= peakEnd)
                {
                    // fully lit
                    foreach (Light l in lights)
                    {
                        l.intensity = lp.LightIntensity;
                    }
                }
                else
                {
                    // fading out
                    float lerp = (realElapsedTime - peakEnd) / (lifeTime - peakEnd);
                    foreach (Light l in lights)
                    {
                        l.intensity = Mathf.Lerp(lp.LightIntensity, 0.0f, lerp);
                    }
                }
            }
            else
            {
                // fading in
                float lerp = realElapsedTime / peakStart;
                foreach (Light l in lights)
                {
                    l.intensity = Mathf.Lerp(0.0f, lp.LightIntensity, lerp);
                }
            }
        }

        private void UpdateLights()
        {
            UpdateLightOnlyObject();
            foreach (LightningBoltSegmentGroup group in segmentGroupsWithLight)
            {
                UpdateLight(group.LightParameters, group.Lights, group.Delay, group.PeakStart, group.PeakEnd, group.LifeTime);
            }
        }

        private IEnumerator GenerateParticle(ParticleSystem p, Vector3 pos, float delay)
        {
            yield return new WaitForSeconds(delay);

            p.transform.position = pos;

#if UNITY_PRE_5_3

            p.Emit((int)p.emissionRate);

#else

            p.Emit((int)p.emission.rate.constantMax);

#endif

        }

        private void CheckForGlow(IEnumerable<LightningBoltParameters> parameters)
        {
            // we need to know if there is glow so we can choose the glow or non-glow setting in the renderer
            foreach (LightningBoltParameters p in parameters)
            {
                HasGlow = (p.GlowIntensity >= Mathf.Epsilon && p.GlowWidthMultiplier >= Mathf.Epsilon);

                if (HasGlow)
                {
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Quality settings for lightning
    /// </summary>
    public enum LightningBoltQualitySetting
    {
        /// <summary>
        /// Use all settings from the script, ignoring the global quality setting
        /// </summary>
        UseScript,

        /// <summary>
        /// Use the global quality setting to determine lightning quality and maximum number of lights and shadowing
        /// </summary>
        LimitToQualitySetting
    }

    public class LightningBoltScript : MonoBehaviour
    {
        private LightningBoltMeshRenderer lightningBoltRenderer;

        [Header("Lightning General Properties")]
        [Tooltip("The camera the lightning should be shown in. Defaults to the current camera, or the main camera if current camera is null. If you are using a different " +
            "camera, you may want to put the lightning in it's own layer and cull that layer out of any other cameras.")]
        public Camera Camera;

        [Tooltip("Allow orthographic mode for orthographic cameras. If you are using an orthographic camera with a non XY plane, set this to false.")]
        public bool AllowOrthographicMode = true;

        [Tooltip("True if you are using world space coordinates for the lightning bolt, false if you are using coordinates relative to the parent game object.")]
        public bool UseWorldSpace = true;

        [Tooltip("Lightning quality setting. This allows setting limits on generations, lights and shadow casting lights based on the global quality setting.")]
        public LightningBoltQualitySetting QualitySetting = LightningBoltQualitySetting.UseScript;

        [Header("Lightning Rendering Properties")]
        [Tooltip("The render queue for the lightning. -1 for default.")]
        public int RenderQueue = -1;

        //Edit tooltip in "LightningBoltEditor.cs" line 46
        [HideInInspector]
        public string SortLayerName;

        //Edit tooltip in "LightningBoltEditor.cs" line 50
        [HideInInspector]
        public int SortOrderInLayer;

        [Tooltip("Lightning material for mesh renderer")]
        public Material LightningMaterialMesh;

        [Tooltip("Lightning material for mesh renderer, without glow")]
        public Material LightningMaterialMeshNoGlow;

        [Tooltip("The texture to use for the lightning bolts, or null for the material default texture.")]
        public Texture2D LightningTexture;

        [Tooltip("Particle system to play at the point of emission (start). 'Emission rate' particles will be emitted all at once.")]
        public ParticleSystem LightningOriginParticleSystem;

        [Tooltip("Particle system to play at the point of impact (end). 'Emission rate' particles will be emitted all at once.")]
        public ParticleSystem LightningDestinationParticleSystem;

        [Tooltip("Tint color for the lightning")]
        public Color LightningTintColor = Color.white;

        [Tooltip("Tint color for the lightning glow")]
        public Color GlowTintColor = new Color(0.1f, 0.2f, 1.0f, 1.0f);

        [Tooltip("Source blend mode. Default is SrcAlpha.")]
        public UnityEngine.Rendering.BlendMode SourceBlendMode = UnityEngine.Rendering.BlendMode.SrcAlpha;

        [Tooltip("Destination blend mode. Default is One. For additive blend use One. For alpha blend use OneMinusSrcAlpha.")]
        public UnityEngine.Rendering.BlendMode DestinationBlendMode = UnityEngine.Rendering.BlendMode.One;

        [Header("Lightning Movement Properties")]
        [Tooltip("Jitter multiplier to randomize lightning size. Jitter depends on trunk width and will make the lightning move rapidly and jaggedly, " +
            "giving a more lively and sometimes cartoony feel. Jitter may be shared with other bolts depending on materials. If you need different " +
            "jitters for the same material, create a second script object.")]
        public float JitterMultiplier = 0.0f;

        [Tooltip("Built in turbulance based on the direction of each segment. Small values usually work better, like 0.2.")]
        public float Turbulence = 0.0f;

        [Tooltip("Global turbulence velocity for this script")]
        public Vector3 TurbulenceVelocity = Vector3.zero;

        private Material lightningMaterialMeshInternal;
        private Material lightningMaterialMeshNoGlowInternal;
        private Texture2D lastLightningTexture;
        private readonly List<LightningBolt> bolts = new List<LightningBolt>();
        private readonly LightningBoltParameters[] oneParameterArray = new LightningBoltParameters[1];

        private void UpdateMaterialsForLastTexture()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            lightningMaterialMeshInternal = new Material(LightningMaterialMesh);
            lightningMaterialMeshNoGlowInternal = new Material(LightningMaterialMeshNoGlow);

            if (LightningTexture != null)
            {
                lightningMaterialMeshInternal.SetTexture("_MainTex", LightningTexture);
                lightningMaterialMeshNoGlowInternal.SetTexture("_MainTex", LightningTexture);
            }

            UpdateRenderer();
        }

        private void UpdateRenderer()
        {
            if (lightningBoltRenderer == null)
            {
                lightningBoltRenderer = new LightningBoltMeshRenderer();
            }
            lightningBoltRenderer.MaterialNoGlow = lightningMaterialMeshNoGlowInternal;
            lightningBoltRenderer.Script = this;
            lightningBoltRenderer.Material = lightningMaterialMeshInternal;
        }

        private void UpdateTexture()
        {
            if (LightningTexture != null && LightningTexture != lastLightningTexture)
            {
                lastLightningTexture = LightningTexture;
                UpdateMaterialsForLastTexture();
            }
            else
            {
                UpdateRenderer();
            }
        }

        private void UpdateShaderParameters()
        {
            lightningMaterialMeshInternal.SetColor("_TintColor", LightningTintColor);
            lightningMaterialMeshInternal.SetColor("_GlowTintColor", GlowTintColor);
            lightningMaterialMeshInternal.SetFloat("_JitterMultiplier", JitterMultiplier);
            lightningMaterialMeshInternal.SetFloat("_Turbulence", Turbulence * LightningBoltParameters.Scale);
            lightningMaterialMeshInternal.SetVector("_TurbulenceVelocity", TurbulenceVelocity * LightningBoltParameters.Scale);
            lightningMaterialMeshInternal.SetInt("_SrcBlendMode", (int)SourceBlendMode);
            lightningMaterialMeshInternal.SetInt("_DstBlendMode", (int)DestinationBlendMode);
            lightningMaterialMeshInternal.renderQueue = RenderQueue;
            lightningMaterialMeshNoGlowInternal.SetColor("_TintColor", LightningTintColor);
            lightningMaterialMeshNoGlowInternal.SetFloat("_JitterMultiplier", JitterMultiplier);
            lightningMaterialMeshNoGlowInternal.SetFloat("_Turbulence", Turbulence * LightningBoltParameters.Scale);
            lightningMaterialMeshNoGlowInternal.SetVector("_TurbulenceVelocity", TurbulenceVelocity * LightningBoltParameters.Scale);
            lightningMaterialMeshNoGlowInternal.SetInt("_SrcBlendMode", (int)SourceBlendMode);
            lightningMaterialMeshNoGlowInternal.SetInt("_DstBlendMode", (int)DestinationBlendMode);
            lightningMaterialMeshNoGlowInternal.renderQueue = RenderQueue;

            if (Camera == null || !Camera.orthographic || !AllowOrthographicMode)
            {
                lightningMaterialMeshInternal.DisableKeyword("ORTHOGRAPHIC_ON");
                lightningMaterialMeshNoGlowInternal.DisableKeyword("ORTHOGRAPHIC_ON");
            }
            else
            {
                lightningMaterialMeshInternal.EnableKeyword("ORTHOGRAPHIC_ON");
                lightningMaterialMeshNoGlowInternal.EnableKeyword("ORTHOGRAPHIC_ON");
            }
        }

        private void OnDestroy()
        {
            foreach (LightningBolt bolt in bolts)
            {
                bolt.Cleanup();
            }
        }

        protected virtual void Start()
        {
            if (LightningMaterialMesh == null || LightningMaterialMeshNoGlow == null)
            {
                Debug.LogError("Must assign all lightning materials");
            }
            UpdateMaterialsForLastTexture();

            if (Camera == null)
            {
                Camera = Camera.current;
                if (Camera == null)
                {
                    Camera = Camera.main;
                }
            }
        }

        protected virtual void Update()
        {
            if (bolts.Count == 0)
            {
                return;
            }

            UpdateShaderParameters();
            for (int i = bolts.Count - 1; i >= 0; i--)
            {
                // update each lightning bolt, removing it if update returns false
                if (!bolts[i].Update())
                {
                    bolts.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Create a lightning bolt
        /// </summary>
        /// <param name="p">Lightning bolt creation parameters</param>
        public virtual void CreateLightningBolt(LightningBoltParameters p)
        {
            if (p != null)
            {
                UpdateTexture();
                oneParameterArray[0] = p;
                LightningBolt bolt = LightningBolt.GetOrCreateLightningBolt();
                bolt.Initialize(Camera, UseWorldSpace, lightningBoltRenderer, gameObject, this,
                    LightningOriginParticleSystem, LightningDestinationParticleSystem, oneParameterArray);
                bolts.Add(bolt);
            }
        }

        /// <summary>
        /// Create multiple lightning bolts, attempting to batch them into as few draw calls as possible
        /// </summary>
        /// <param name="parameters">Lightning bolt creation parameters</param>
        public void CreateLightningBolts(ICollection<LightningBoltParameters> parameters)
        {
            if (parameters != null && parameters.Count != 0)
            {
                UpdateTexture();
                LightningBolt bolt = LightningBolt.GetOrCreateLightningBolt();
                bolt.Initialize(Camera, UseWorldSpace, lightningBoltRenderer, gameObject, this,
                    LightningOriginParticleSystem, LightningDestinationParticleSystem, parameters);
                bolts.Add(bolt);
            }
        }
    }
}