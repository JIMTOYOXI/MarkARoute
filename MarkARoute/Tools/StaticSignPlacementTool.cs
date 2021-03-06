﻿using MarkARoute.Managers;
using MarkARoute.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkARoute.Tools
{
    class StaticSignPlacementTool : SignPlacementTool
    {
        public string signType;

        protected override void Awake()
        {
            base.BaseAwake();
        }

        public void SetPropInfo(string signType)
        {
            this.signType = signType;
            base.m_propInfo = RenderingManager.instance.m_signPropDict[signType];

        }

        protected override void HandleSignPlaced()
        {
            RouteManager.Instance().SetSign(this.m_cachedPosition, this.m_cachedAngle, routePrefix, routeStr, destination, this.signType);
            RenderingManager.instance.ForceUpdate();
            ToolsModifierControl.toolController.CurrentTool = ToolsModifierControl.GetTool<DefaultTool>();
            ToolsModifierControl.SetTool<DefaultTool>();
        }
    }
}
