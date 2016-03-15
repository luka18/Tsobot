//
// Procedural Lightning for Unity
// (c) 2015 Digital Ruby, LLC
// Source code may be used for personal or commercial projects.
// Source code may NOT be redistributed or sold.
// 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DigitalRuby.ThunderAndLightning
{
    public class LightningSplineScript : LightningBoltPathScriptBase
    {
        /// <summary>
        /// For performance, cap generations
        /// </summary>
        public const int MaxSplineGenerations = 5;

        [Header("Lightning Spline Properties")]
        [Tooltip("The distance hint for each spline segment. Set to <= 0 to use the generations to determine how many spline segments to use. " +
            "If > 0, it will be divided by Generations before being applied. This value is a guideline and is approximate, and not uniform on the spline.")]
        public float DistancePerSegmentHint = 0.0f;

        private readonly List<Vector3> prevSourcePoints = new List<Vector3>(new Vector3[] { Vector3.zero });
        private readonly List<Vector3> sourcePoints = new List<Vector3>();
        private List<Vector3> savedSplinePoints;

        private int previousGenerations = -1;
        private float previousDistancePerSegment = -1.0f;

        private bool SourceChanged()
        {
            if (sourcePoints.Count != prevSourcePoints.Count)
            {
                return true;
            }
            for (int i = 0; i < sourcePoints.Count; i++)
            {
                if (sourcePoints[i] != prevSourcePoints[i])
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Create lightning bolt path parameters
        /// </summary>
        /// <returns>Lightning bolt path parameters</returns>
        protected override LightningBoltParameters OnCreateParameters()
        {
            return new LightningBoltPathParameters { Generator = LightningGeneratorPath.PathInstance };
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();
        }

        public override void CreateLightningBolt(LightningBoltParameters parameters)
        {
            if (LightningPath == null || LightningPath.List == null)
            {
                return;
            }

            sourcePoints.Clear();
            try
            {
                foreach (GameObject obj in LightningPath.List)
                {
                    if (obj != null)
                    {
                        sourcePoints.Add(obj.transform.position);
                    }
                }
            }
            catch (System.NullReferenceException)
            {
                return;
            }

            if (sourcePoints.Count < PathGenerator.MinPointsForSpline)
            {
                Debug.LogError("To create spline lightning, you need a lightning path with at least " + PathGenerator.MinPointsForSpline + " points.");
            }

            Generations = parameters.Generations = Mathf.Clamp(Generations, 1, MaxSplineGenerations);
            LightningBoltPathParameters sp = parameters as LightningBoltPathParameters;

            if (previousGenerations != Generations || previousDistancePerSegment != DistancePerSegmentHint || SourceChanged())
            {
                previousGenerations = Generations;
                previousDistancePerSegment = DistancePerSegmentHint;
                sp.Points = new List<Vector3>(sourcePoints.Count * Generations);
                PopulateSpline(sp.Points, sourcePoints, Generations, DistancePerSegmentHint, Camera);
                prevSourcePoints.Clear();
                prevSourcePoints.AddRange(sourcePoints);
                savedSplinePoints = sp.Points;
            }
            else
            {
                sp.Points = savedSplinePoints;
            }
            sp.SmoothingFactor = (sp.Points.Count - 1) / sourcePoints.Count;

            base.CreateLightningBolt(parameters);
        }

        /// <summary>
        /// Triggers lightning that follows a set of points, rather than the standard lightning bolt that goes between two points.
        /// </summary>
        /// <param name="points">Points to follow</param>
        /// <param name="spline">Whether to spline the lightning through the points or not</param>
        public void Trigger(List<Vector3> points, bool spline)
        {
            if (points.Count < 2)
            {
                return;
            }
            Generations = Mathf.Clamp(Generations, 1, MaxSplineGenerations);
            LightningBoltPathParameters pathParameters = CreateParameters() as LightningBoltPathParameters;
            if (spline && points.Count > 3)
            {
                pathParameters.Points = new List<Vector3>(points.Count * Generations);
                LightningSplineScript.PopulateSpline(pathParameters.Points, points, Generations, DistancePerSegmentHint, Camera);
                pathParameters.SmoothingFactor = (pathParameters.Points.Count - 1) / points.Count;
            }
            else
            {
                pathParameters.Points = new List<Vector3>(points);
                pathParameters.SmoothingFactor = 1;
            }
            base.CreateLightningBolt(pathParameters);
            CreateLightningBoltsNow();
        }

        /// <summary>
        /// Populate a list of spline points from source points
        /// </summary>
        /// <param name="splinePoints">List to fill with spline points</param>
        /// <param name="sourcePoints">Source points</param>
        /// <param name="generations">Generations</param>
        /// <param name="distancePerSegmentHit">Distance per segment hint - if non-zero, attempts to maintain this distance between spline points.</param>
        /// <param name="camera">Optional camera</param>
        public static void PopulateSpline(List<Vector3> splinePoints, List<Vector3> sourcePoints, int generations, float distancePerSegmentHit, Camera camera)
        {
            splinePoints.Clear();
            PathGenerator.Is2D = (camera != null && camera.orthographic);
            if (distancePerSegmentHit > 0.0f)
            {
                PathGenerator.CreateSplineWithSegmentDistance(splinePoints, sourcePoints, distancePerSegmentHit / generations, false);
            }
            else
            {
                PathGenerator.CreateSpline(splinePoints, sourcePoints, sourcePoints.Count * generations * generations, false);
            }
        }
    }
}