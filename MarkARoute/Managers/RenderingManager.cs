﻿using ColossalFramework;
using ColossalFramework.UI;
using MarkARoute.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace MarkARoute.Managers
{
    class RenderingManager : SimulationManagerBase<RenderingManager, DistrictProperties>, IRenderableManager, ISimulationManager
    {
        private Material m_nameMaterial = null;
        private Material m_iconMaterial = null;

        private int m_lastCount = 0;
        private bool textHidden = false;

        public float m_renderHeight = 1000f;
        public bool m_alwaysShowText = false;
        public bool m_registered = false;
        public bool m_routeEnabled = true;

        protected override void Awake()
        {
            base.Awake();

            LoggerUtils.Log("Initialising RoadRenderingManager");

            DistrictManager districtManager = Singleton<DistrictManager>.instance;

            this.m_nameMaterial = new Material(districtManager.m_properties.m_areaNameShader);
            this.m_nameMaterial.CopyPropertiesFromMaterial(districtManager.m_properties.m_areaNameFont.material);

            this.m_iconMaterial = new Material(districtManager.m_properties.m_areaIconShader);
            this.m_iconMaterial.CopyPropertiesFromMaterial(districtManager.m_properties.m_areaIconAtlas.material);
        }

        protected override void BeginOverlayImpl(RenderManager.CameraInfo cameraInfo)
        {
            DistrictManager districtManager = Singleton<DistrictManager>.instance;

            if (m_lastCount != RouteManager.Instance().m_routeDict.Count)
            {
                m_lastCount = RouteManager.Instance().m_routeDict.Count;

                try
                {
                    RenderText();
                }
                catch (Exception ex)
                {
                    LoggerUtils.LogException(ex);
                }
            }

            if (!textHidden && cameraInfo.m_height > m_renderHeight)
            {
                foreach (RouteContainer route in RouteManager.Instance().m_routeDict.Values)
                {
                    route.m_shieldMesh.GetComponent<Renderer>().enabled = false;
                    route.m_numMesh.GetComponent<Renderer>().enabled = false;
                }
                textHidden = true;
            }
            else if (textHidden && cameraInfo.m_height <= m_renderHeight && (districtManager.NamesVisible || m_alwaysShowText)) //This is a mess, and I'll sort it soon :)
            {

                if (m_routeEnabled)
                {
                    foreach (RouteContainer route in RouteManager.Instance().m_routeDict.Values)
                    {
                        route.m_shieldMesh.GetComponent<Renderer>().enabled = true;
                        route.m_numMesh.GetComponent<Renderer>().enabled = true;
                    }
                }
                textHidden = false;
            }
        }

        /// <summary>
        /// Redraw the text to be drawn later with a mesh. Use sparingly, as 
        /// this is an expensive task.
        /// </summary>
        private void RenderText()
        {
            DistrictManager districtManager = DistrictManager.instance;

            if (districtManager.m_properties.m_areaNameFont != null)
            {
                UIFontManager.Invalidate(districtManager.m_properties.m_areaNameFont);

                NetManager netManager = NetManager.instance;

                foreach (RouteContainer route in RouteManager.Instance().m_routeDict.Values)
                {
                    if (route.m_segmentId != 0)
                    {
                        string routeStr = route.m_route;

                        if (routeStr != null)
                        {
                            NetSegment netSegment = netManager.m_segments.m_buffer[route.m_segmentId];
                            NetSegment.Flags segmentFlags = netSegment.m_flags;

                            if (segmentFlags.IsFlagSet(NetSegment.Flags.Created))
                            {
                                //Load a route shield type ( generic motorway shield should be default value )
                                RouteShieldInfo shieldInfo = RouteShieldConfig.Instance().GetRouteShieldInfo(route.m_routePrefix);

                                NetNode startNode = netManager.m_nodes.m_buffer[netSegment.m_startNode];
                                NetNode endNode = netManager.m_nodes.m_buffer[netSegment.m_endNode];
                                //TODO: Make texture addition/selection based on prefix type
                                Material mat = SpriteUtils.m_textureStore[shieldInfo.textureName];
                                route.m_shieldObject.GetComponent<Renderer>().material = mat;
                                //TODO: Make mesh size dependent on text size
                                route.m_shieldMesh.mesh = MeshUtils.CreateRectMesh(mat.mainTexture.width, mat.mainTexture.height);
                                Vector3 startNodePosition = startNode.m_position;
                                route.m_shieldMesh.transform.position = startNodePosition;
                                route.m_shieldMesh.transform.LookAt(endNode.m_position, Vector3.up);
                                route.m_shieldMesh.transform.Rotate(90f, 0f, 90f);
                                //TODO: Bind the elevation of the mesh to the text z offset
                                route.m_shieldMesh.transform.position += (Vector3.up * (0.5f));
                                route.m_shieldMesh.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                                route.m_shieldObject.GetComponent<Renderer>().sortingOrder = 1000;

                                route.m_numMesh.anchor = TextAnchor.MiddleCenter;
                                route.m_numMesh.font = districtManager.m_properties.m_areaNameFont.baseFont;
                                route.m_numMesh.GetComponent<Renderer>().material = route.m_numMesh.font.material;
                                //TODO: Tie the font size to the font size option
                                route.m_numMesh.fontSize = 50;
                                route.m_numMesh.transform.position = startNode.m_position;
                                route.m_numMesh.transform.parent = route.m_shieldObject.transform;
                                route.m_numMesh.transform.LookAt(endNode.m_position, Vector3.up);
                                route.m_numMesh.transform.Rotate(90f, 0f, 90f);
                                route.m_numMesh.transform.position = route.m_shieldObject.GetComponent<Renderer>().bounds.center;
                                //Just a hack, to make sure the text actually shows up above the shield
                                route.m_numMesh.offsetZ = 0.001f;
                                //TODO: Definitely get a map of the texture to the required text offsets 
                                route.m_numMesh.transform.localPosition += (Vector3.up * shieldInfo.upOffset);
                                route.m_numMesh.transform.localPosition += (Vector3.left * shieldInfo.leftOffset);
                                //TODO: Figure out a better ratio for route markers
                                route.m_numMesh.transform.localScale = new Vector3(shieldInfo.textScale, shieldInfo.textScale, shieldInfo.textScale);
                                route.m_numMesh.color = shieldInfo.textColor;
                                route.m_numMesh.text = route.m_route.ToString();
                                route.m_numTextObject.GetComponent<Renderer>().sortingOrder = 1001;

                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Forces rendering to update immediately. Use sparingly, as it
        /// can be quite expensive.
        /// </summary>
        public void ForceUpdate()
        {
            m_lastCount = -1;
        }
    }
}
