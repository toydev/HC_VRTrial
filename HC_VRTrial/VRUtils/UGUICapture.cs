﻿using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Collections;

using HC_VRTrial.Logging;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.Attributes;

namespace HC_VRTrial.VRUtils
{
    /// <summary>
    /// This class captures uGUI elements and displays them on a RenderTexture.
    /// </summary>
    public class UGUICapture : MonoBehaviour
    {
        static UGUICapture() { ClassInjector.RegisterTypeInIl2Cpp<UGUICapture>(); }

        /// <summary>
        /// Creates and initializes a UGUICapture instance.
        /// </summary>
        /// <param name="parentGameObject">Parent game object.</param>
        /// <param name="name">Name.</param>
        /// <param name="layer">Working layer occupied for capture.</param>
        /// <returns>A new instance of UGUICapture.</returns>
        [HideFromIl2Cpp]
        public static UGUICapture Create(GameObject parentGameObject, string name, int layer)
        {
            var gameObject = new GameObject($"{parentGameObject.name}{name}");
            // Ensure the lifecycle of the GameObject is synchronized with its parent.
            gameObject.transform.parent = parentGameObject.transform;
            gameObject.SetActive(false);
            var result = gameObject.AddComponent<UGUICapture>();
            result.Layer = layer;
            gameObject.SetActive(true);
            return result;
        }

        private int Layer { get; set; }
        public RenderTexture Texture { get; private set; }
        private Il2CppSystem.Collections.Generic.Dictionary<Canvas, IndexedSet<Graphic>> CanvasGraphics { get; set; }
        [HideFromIl2Cpp]
        private ISet<Canvas> ProcessedCanvas { get; set; } = new HashSet<Canvas>();

        void Awake()
        {
            PluginLog.Debug($"Awake: {name}");

            Texture = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.ARGB32);

            // Creates a camera to project the captured uGUI elements onto the RenderTexture.
            var camera = gameObject.AddComponent<Camera>();
            camera.cullingMask = 1 << Layer;
            camera.depth = float.MaxValue;
            camera.nearClipPlane = -1000f;
            camera.farClipPlane = 1000f;
            camera.targetTexture = Texture;
            camera.backgroundColor = Color.clear;
            camera.clearFlags = CameraClearFlags.Color;
            camera.orthographic = true;
            camera.useOcclusionCulling = false;
            CanvasGraphics = GraphicRegistry.instance.m_Graphics;
        }

        void OnDestroy()
        {
            PluginLog.Debug($"OnDestroy: {name}");

            if (Texture != null)
            {
                Texture.Release();
                Texture = null;
            }
        }

        void Update()
        {
            // Redirects the uGUI canvas output to a pre-prepared RenderTexture for capturing.
            var camera = GetComponent<Camera>();
            foreach (var canvas in CanvasGraphics.Keys)
            {
                if (canvas.enabled && (!ProcessedCanvas.Contains(canvas) || canvas.renderMode != RenderMode.ScreenSpaceCamera || canvas.worldCamera != camera))
                {
                    PluginLog.Debug($"Add canvas to capture target: {canvas.name} in {canvas.gameObject.layer}:{LayerMask.LayerToName(canvas.gameObject.layer)}");
                    ProcessedCanvas.Add(canvas);
                    canvas.renderMode = RenderMode.ScreenSpaceCamera;
                    canvas.worldCamera = camera;
                    foreach (var i in canvas.gameObject.GetComponentsInChildren<Transform>()) i.gameObject.layer = Layer;
                }
            }
        }
    }
}
