﻿using ColossalFramework;
using ColossalFramework.Packaging;
using ColossalFramework.Plugins;
using ColossalFramework.UI;
using ICities;
using MarkARoute.Managers;
using MarkARoute.UI;
using MarkARoute.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace MarkARoute
{
    public class MarkARouteLoading : LoadingExtensionBase
    {

        private MarkARouteSerializer m_saveUtility = new MarkARouteSerializer();
        private RenderingManager m_renderingManager = null;
        public MainPanel UI { get; set; }

        public override void OnCreated(ILoading loading)
        {
            try //So we don't fuck up loading the city
            {
                LoadSprites();
            }
            catch (Exception ex)
            {
                LoggerUtils.LogException(ex);
            }
        }

        public override void OnLevelLoaded(LoadMode mode)
        {
            if (mode == LoadMode.LoadGame || mode == LoadMode.NewGame )
            {
                UIView view = UIView.GetAView();
                UI = ToolsModifierControl.toolController.gameObject.AddComponent<MainPanel>();

                ModSettings.LoadSettings();

                m_renderingManager = RenderingManager.instance;
                m_renderingManager.enabled = true;

                if (m_renderingManager != null && !m_renderingManager.m_registered)
                {
                    RenderManager.RegisterRenderableManager(m_renderingManager);
                    m_renderingManager.m_registered = true;
                    m_renderingManager.ForceUpdate();
                }

                MarkARouteOptions.mInGame = true;
                MarkARouteOptions.update();

            }
        }

        public override void OnLevelUnloading()
        {
            // First disable dynamic sign updates
            RenderingManager.instance.disableTimer();
            ModSettings.SaveSettings();

            DynamicSignConfig.SaveVmsMsgList();
            MarkARouteOptions.mInGame = false;
        }

        /// <summary>
        /// Loads all custom sprites
        /// </summary>
        private void LoadSprites()
        {

            bool spriteSuccess = true;
            //TODO: Replace with a loader function( JSON mapping available )

            //We probably need this before we load any displays
            DynamicSignConfig.LoadVmsMsgList();

            RouteShieldConfig.LoadRouteShieldInfo();
            String[] files = Directory.GetFiles(FileUtils.GetModPath()+ Path.DirectorySeparatorChar+"Icons");
            foreach ( string file in files)
            {
                string[] splitValues = file[0] == Path.DirectorySeparatorChar ? file.Substring(1).Split(Path.DirectorySeparatorChar) : file.Split(Path.DirectorySeparatorChar);
                string fileName = splitValues[splitValues.Length - 1];
                string fileKey = fileName.Split('.')[0];
                spriteSuccess = SpriteUtils.AddTexture(file, fileKey) && spriteSuccess;
                if(!RouteShieldConfig.Instance().routeShieldDictionary.ContainsKey(fileKey))
                {
                    RouteShieldConfig.Instance().routeShieldDictionary[fileKey] = new RouteShieldInfo(fileKey);
                }
            }

            //TODO: When we need it, load a json descriptor file for relevant shaders here
            ShaderUtils.AddShader("Shaders/font", "font");

            //TODO: When we need it, load a json descriptor file for relevant fonts here
            FontUtils.AddFonts();

            if (!spriteSuccess)
            {
                LoggerUtils.LogError("Failed to load some sprites!");
            }
            else
            {
                RouteShieldConfig.SaveRouteShieldInfo();
            }
        }
    }
}
