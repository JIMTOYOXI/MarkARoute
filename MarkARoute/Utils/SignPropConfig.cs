﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace MarkARoute.Utils
{
    public class SignPropInfo
    {
        public string propName;
        public bool isDoubleGantry;
        public List<Vector3> textOffsetNoSign;
        public List<Vector3> textOffsetSign;
        public List<Vector3> shieldOffset;
        public Vector3 textScale;
        public Vector3 shieldScale;
        public string fontType;
        public float angleOffset;

        public SignPropInfo(string propName,
                            bool isDouble,
                            List<Vector3> textOffsetNoSign,
                            List<Vector3> textOffsetSign,
                            List<Vector3> shieldOffset,
                            Vector3 textScale,
                            Vector3 shieldScale,
                            string fontType,
                            float angleOffset)
        {
            this.propName = propName;
            this.isDoubleGantry = isDouble;
            this.textOffsetNoSign = textOffsetNoSign;
            this.textOffsetSign = textOffsetSign;
            this.shieldOffset = shieldOffset;
            this.textScale = textScale;
            this.shieldScale = shieldScale;
            this.fontType = fontType;
            this.angleOffset = angleOffset;
        }
    }

    public class OverrideSignInfo
    {
        public string propName;
        public float angleOffset;
        public Vector3 positionOffset;
        public float scale;

        public OverrideSignInfo(string propName, float angleOffset, Vector3 positionOffset, float scale=1f)
        {
            this.propName = propName;
            this.angleOffset = angleOffset;
            this.positionOffset = positionOffset;
            this.scale = scale;
        }
    }

    class SignPropConfig
    {
        public static Dictionary<string, OverrideSignInfo> overrideSignValues = new Dictionary<string, OverrideSignInfo>
        {
              { "hwysign",
                new OverrideSignInfo("hwysign",
                                     90,
                                     new Vector3(9f,0,-9f)
                    ) },

            { "double gantry sign",
                new OverrideSignInfo("double gantry sign",
                                     90,
                                     new Vector3(0,0,0)
                    ) },
            { "single gantry",
                 new OverrideSignInfo("single gantry",
                                     90,
                                     new Vector3(5f,0,-5f)
                    ) },
            { "hs_signs01b_german",
              new OverrideSignInfo("hs_signs01b_german",
                                     0,
                                     new Vector3(9,0,-9f),
                                     0.61f
                    ) },
            { "hs_signs02a_german",
               new OverrideSignInfo("hs_signs02a_german",
                                     0,
                                     new Vector3(9,0,-9f),
                                     0.75f
                    ) },
            { "hs_signs01a_german",
               new OverrideSignInfo("hs_signs01a_german",
                                     0,
                                     new Vector3(9,0,-9f),
                                     0.61f
                    ) },
            { "hs_signs02d_german",
                new OverrideSignInfo("hs_signs02d_german",
                                     0,
                                     new Vector3(9f,0,-9f)
                    ) },
            { "hs_signs02cl_german",
                new OverrideSignInfo("hs_signs02cl_german",
                                     0,
                                     new Vector3(-17,0,17f)
                    ) },
            { "hs_signs02cr_german",
                new OverrideSignInfo("hs_signs02cr_german",
                                     0,
                                     new Vector3(17,0,-17)
                    ) },
            { "hs_signs01a_us",
                new OverrideSignInfo("hs_signs01a_us",
                                     0,
                                     new Vector3(9,0,-9f),
                                     0.61f
                    ) },
            { "hs_signs01b_us",
                new OverrideSignInfo("hs_signs01b_us",
                                     0,
                                     new Vector3(9,0,-9f),
                                     0.61f
                    )  }
        };



        public static readonly Dictionary<string, SignPropInfo> signPropInfoDict = new Dictionary<string, SignPropInfo>
        {
            { "hwysign",
                new SignPropInfo("hwysign",
                    false,
                    new List<Vector3> { new Vector3(0.2f, 6.5f, -4.7f) },
                    new List<Vector3> { new Vector3(0.2f, 5.8f, -4.7f) },
                    new List<Vector3> { new Vector3(0.2f, 7.2f, -3.8f) },
                    new Vector3(0.1f, 0.1f, 0.1f),
                    new Vector3(0.18f, 0.18f, 0.18f),
                    "Highway Gothic",
                    0 ) },
            { "uk_sign",
                new SignPropInfo("uk_sign",
                    false,
                    new List<Vector3> { new Vector3(0.2f,6.2f,-1f) },
                    new List<Vector3> { new Vector3(0.2f,6.2f,-1f) },
                    new List<Vector3> {new Vector3(0.2f,7.5f,-1f) },
                    new Vector3(0.1f, 0.1f, 0.1f),
                    new Vector3(0.18f, 0.18f, 0.18f),
                    "Transport",0 ) },
            { "double gantry sign",
                new SignPropInfo("double gantry sign",
                    true,
                    new List<Vector3> { new Vector3(0.75f,6.5f,0f), new Vector3(0.75f,6.5f,4.4f) },
                    new List<Vector3> { new Vector3(0.75f,5.8f,0f),new Vector3(0.75f,6.5f,4.4f) },
                    new List<Vector3> { new Vector3(0.8f,6.7f,0f) },
                    new Vector3(0.1f, 0.1f, 0.1f),
                    new Vector3(0.12f, 0.12f, 0.12f),
                    "Highway Gothic",
                    0 ) },
            { "single gantry",
                new SignPropInfo("single gantry",
                    false,
                    new List<Vector3> { new Vector3(0.53f,6.5f,-1.8f) },
                    new List<Vector3> { new Vector3(0.53f,6f,-1.8f) },
                    new List<Vector3> { new Vector3(0.53f,7.1f,-1.8f) },
                    new Vector3(0.1f, 0.1f, 0.1f),
                    new Vector3(0.12f, 0.12f, 0.12f),
                    "Highway Gothic",
                    0 ) },
            { "hs_signs01b_german",
                new SignPropInfo("hs_signs01b_german",
                    true, new List<Vector3> { new Vector3(21f,10f,0.25f), new Vector3(8.3f,10f,0.25f) },
                    new List<Vector3> { new Vector3(20f, 10f, 0.25f),new Vector3(8.3f, 10f, 0.25f) },
                    new List<Vector3> { new Vector3(23f,10f,0.25f) },
                    new Vector3(0.2f, 0.2f, 0.2f),
                    new Vector3(0.16f, 0.16f, 0.16f),
                    "Highway Gothic",
                    -90 ) },
            { "hs_signs02a_german",
                new SignPropInfo("hs_signs02a_german",
                    true,
                    new List<Vector3> { new Vector3(15f,10f,0.5f), new Vector3(8f,11f,0.5f) },
                    new List<Vector3> { new Vector3(14f, 10f, 0.5f),new Vector3(8f, 11f, 0.5f) },
                    new List<Vector3> { new Vector3(17.5f,10f,0.5f) },
                    new Vector3(0.2f, 0.2f, 0.2f),
                    new Vector3(0.16f, 0.16f, 0.16f),
                    "Highway Gothic",
                    -90 ) },
            { "hs_signs01a_german",
                new SignPropInfo("hs_signs01a_german",
                    true,
                    new List<Vector3> { new Vector3(21f,10f,0.25f), new Vector3(8.3f,10f,0.25f) },
                    new List<Vector3> { new Vector3(20f, 10f, 0.25f),new Vector3(8.3f, 10f, 0.25f) },
                    new List<Vector3> {new Vector3(24f,10f,0.25f) },
                    new Vector3(0.2f, 0.2f, 0.2f),
                    new Vector3(0.16f, 0.16f, 0.16f),
                    "Highway Gothic",
                    -90 ) },
            { "hs_signs02d_german",
                new SignPropInfo("hs_signs02d_german",
                    true,
                    new List<Vector3> { new Vector3(4.6f,10.2f,8.5f), new Vector3(-4.6f, 10.2f, 8.5f) },
                    new List<Vector3> { new Vector3(4.6f,10.2f,8.5f), new Vector3(-4.6f, 10.2f, 8.5f) },
                    new List<Vector3> { new Vector3(4.6f,11.5f,8.5f) },
                    new Vector3(0.2f, 0.2f, 0.2f),
                    new Vector3(0.16f, 0.16f, 0.16f),
                    "Highway Gothic",
                    -90 ) },
            { "hs_signs02e_german",
                new SignPropInfo("hs_signs02e_german",
                    false,
                    new List<Vector3> { new Vector3(-1f, 4.5f, 8.7f) },
                    new List<Vector3> { new Vector3(-1f,4.5f,8.7f) },
                    new List<Vector3> {new Vector3(-1f,6.5f,8.7f) } ,
                    new Vector3(0.15f, 0.15f, 0.15f),
                    new Vector3(0.18f, 0.18f, 0.18f),
                    "Highway Gothic",
                    -90 ) },
            { "hs_signs02cl_german",
                new SignPropInfo("hs_signs02cl_german",
                    false,
                    new List<Vector3> { new Vector3(-12.5f, 10.2f, 0.5f) },
                    new List<Vector3> { new Vector3(-12.5f, 10.2f, 0.5f) },
                    new List<Vector3> {new Vector3(-12.6f, 11.5f, 0.5f) },
                    new Vector3(0.1f, 0.1f, 0.1f),
                    new Vector3(0.15f, 0.15f, 0.15f),
                    "Highway Gothic",
                    -90 ) },
            { "hs_signs02cr_german",
                new SignPropInfo("hs_signs02cr_german",
                    false,
                    new List<Vector3> { new Vector3(12.5f, 10f, 0.5f) },
                    new List<Vector3> { new Vector3(12.5f, 10f, 0.5f) },
                    new List<Vector3> {new Vector3(12.6f, 11.5f, 0.5f) },
                    new Vector3(0.1f, 0.1f, 0.1f),
                    new Vector3(0.15f, 0.15f, 0.15f),
                    "Highway Gothic",
                    -90 ) },
            { "hs_signs01a_us",
                new SignPropInfo("hs_signs01a_us",
                    true,
                    new List<Vector3> { new Vector3(21f,10f,0.25f), new Vector3(11.2f,10f,0.25f) },
                    new List<Vector3> { new Vector3(20f, 10f, 0.25f),new Vector3(11.2f, 10f, 0.25f) },
                    new List<Vector3> {new Vector3(24f,10f,0.25f) },
                    new Vector3(0.2f, 0.2f, 0.2f),
                    new Vector3(0.16f, 0.16f, 0.16f),
                    "Highway Gothic",
                    -90 ) },
            { "hs_signs01b_us",
                new SignPropInfo("hs_signs01b_us",
                    true,
                    new List<Vector3> { new Vector3(21f,10f,0.25f), new Vector3(11.2f,10f,0.25f) },
                    new List<Vector3> { new Vector3(20f, 10f, 0.25f),new Vector3(11.2f, 10f, 0.25f) },
                    new List<Vector3> {new Vector3(24f,10f,0.25f) },
                    new Vector3(0.2f, 0.2f, 0.2f),
                    new Vector3(0.16f, 0.16f, 0.16f),
                    "Highway Gothic",
                    -90 ) },

        };
    }
}
