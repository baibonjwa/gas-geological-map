// Copyright 2006 ESRI
// 
// All rights reserved under the copyright laws of the United States
// and applicable international laws, treaties, and conventions.
// 
// You may freely redistribute and use this sample code, with or
// without modification, provided you include the original copyright
// notice and use restrictions.
// 
// See use restrictions at /arcgis/developerkit/userestrictions.
// 

using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.ADF.CATIDs;


namespace GIS
{
    public interface IGasPressureSymbol
    {
        ESRI.ArcGIS.Display.IColor ColorTop { get; set; }
        ESRI.ArcGIS.Display.IColor ColorLeft { get; set; }
        ESRI.ArcGIS.Display.IColor ColorRight { get; set; }
        ESRI.ArcGIS.Display.IColor ColorBorder { get; set; }
    }

    [ProgId(GasPressureSymbol.GUID)]
    [Guid("EFEBF67C-2AA9-4182-BE5A-CCFAA6D70D15")]
    [ComVisible(true)]
    public class GasPressureSymbol : ESRI.ArcGIS.Display.ISymbol,
                                    ESRI.ArcGIS.Display.IMarkerSymbol,
                                    ESRI.ArcGIS.Display.IDisplayName,
                                    ESRI.ArcGIS.esriSystem.IClone,
                                    ESRI.ArcGIS.esriSystem.IPersistVariant,
                                    ESRI.ArcGIS.Display.ISymbolRotation,
                                    ESRI.ArcGIS.Display.IMapLevel,
                                    ESRI.ArcGIS.Display.IMarkerMask,
                                    ESRI.ArcGIS.esriSystem.IPropertySupport,
                                    GIS.IGasPressureSymbol
    {
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MarkerSymbol.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MarkerSymbol.Unregister(regKey);

        }

        #endregion
        #endregion

        // Essential interfaces for a MarkerSymbol.

        // Optional interfaces for additional symbol functionality and integration with the ArcGIS framework.

        // Custom interface.

        #region Member variables

        //  These variables store the device context, transformation and pens used for
        //  drawing the different sections of the Marker. The transformation is set in
        //  ISymbol_SetupDC, and used in ISymbol_Draw.
        public const string GUID = "LogoMarkerSym.LogoMarkerSymbol";
        private ESRI.ArcGIS.Display.IDisplayTransformation m_trans;
        private ESRI.ArcGIS.Display.esriRasterOpCode m_lROP2Old;
        private int m_lhDC;
        private int m_lOldPen;
        private int m_lOldBrush;
        private int m_lPen;
        private int m_lBrushTop;
        private int m_lBrushLeft;
        private int m_lBrushRight;

        //  The device ratio is the ratio of screen resolution to Points.  Note that the screen resolution
        //  is not strictly dependent on the output device, but also on the resolution in the DisplayTransformation
        //  (which is driven in part by the zoom level in layout mode). Also
        //  offset values in device units.
        private double m_dDeviceRatio;
        private double m_dDeviceXOffset;
        private double m_dDeviceYOffset;

        //  This variable stores the device coordinates for each of the control points
        //  for the logo, for use in the Chord and Polygon functions. Also stored points
        //  of the interior of each shape for the FloodFill function to use.
        //private Utility.POINTAPI[] m_coords = new Utility.POINTAPI[7];

        //  The r value calculated from the Size (width) of the symbol, in PrintPoints.
        private double m_dDeviceRadius;

        //  These members hold properties of the ISymbol interface.
        private ESRI.ArcGIS.Display.esriRasterOpCode m_lROP2;

        //  These members hold properties of the IMarkerSymbol interface. The Color property on
        //  this interface points to m_colorTop.
        private double m_dSize;
        private double m_dXOffset;
        private double m_dYOffset;
        private double m_dAngle;

        //  These members hold properties of the IDisplayName interface.
        private const string m_sDisplayName = "Logo Marker Symbol C#";

        //  These members hold properties used by the ISymbolRotation interface.
        private bool m_bRotWithTrans;
        private double m_dMapRotation;

        //  These members hold properties of the IMapLevel interface.
        private int m_lMapLevel;

        //  These members hold properties of the ILogoMarkerSymbol interface.
        private ESRI.ArcGIS.Display.IColor m_colorTop;
        private ESRI.ArcGIS.Display.IColor m_colorLeft;
        private ESRI.ArcGIS.Display.IColor m_colorRight;
        private ESRI.ArcGIS.Display.IColor m_colorBorder;

        // Persistence version number
        private const int m_lCurrPersistVers = 1;

        #endregion

        #region Class functions

        private void Initialize()
        {
            InitializeMembers();
        }

        public GasPressureSymbol()
            : base()
        {
            Initialize();
        }

        private void InitializeMembers()
        {
            // Set up default values as far as possible.
            m_lhDC = 0;
            m_lOldPen = 0;
            m_lPen = 0;
            m_lOldBrush = 0;
            m_lBrushTop = 0;
            m_lBrushLeft = 0;
            m_lBrushRight = 0;
            m_dDeviceRadius = 0;

            m_trans = null;

            //  LogoMarkerSymbol custom property defaults.
            IColor color = null;

            color = (IColor)ESRI.ArcGIS.ADF.Converter.ToRGBColor(Color.Red);
            m_colorTop = ((IClone)color).Clone() as IColor;
            color = (IColor)ESRI.ArcGIS.ADF.Converter.ToRGBColor(Color.OrangeRed);
            m_colorLeft = ((IClone)color).Clone() as IColor;
            color = (IColor)ESRI.ArcGIS.ADF.Converter.ToRGBColor(Color.Pink);
            m_colorRight = ((IClone)color).Clone() as IColor;
            color = (IColor)ESRI.ArcGIS.ADF.Converter.ToRGBColor(Color.Black);
            m_colorBorder = ((IClone)color).Clone() as IColor;

            //  ISymbol property defaults.
            m_lROP2 = esriRasterOpCode.esriROPCopyPen;

            //  IMarkerSymbol property defaults.
            m_dSize = 10;
            m_dAngle = 0;
            m_dXOffset = 0;
            m_dYOffset = 0;

            //  ISymbolRotation property defaults.
            m_bRotWithTrans = true;

        }

        private void Terminate()
        {
            //  Which variables do we really need to dereference here, just the transformation,
            //  or any other globals as well??
            m_trans = null;
            m_colorTop = null;
            m_colorLeft = null;
            m_colorRight = null;
            m_colorBorder = null;
        }

        ~GasPressureSymbol()
        {
            Terminate();
        }

        #endregion

        #region ISymbol implementation

        void ESRI.ArcGIS.Display.ISymbol.SetupDC(int hDC, ESRI.ArcGIS.Geometry.ITransformation Transformation)
        {
            //// Store the DisplayTransformation and display handle for use by Draw and ResetDC.
            //m_trans = Transformation as IDisplayTransformation;
            //m_lhDC = hDC;

            //// Set up the device ratio for use by Draw and the rest of SetupDC.
            //SetupDeviceRatio(m_lhDC, m_trans);

            ////Calculate the new Radius for the symbol from the Size (width) overall.
            ////Dim dRadius As Double
            ////m_dDeviceRadius = m_dSize / 2 '  This is the simplistic method, actually results in symbol slightly wrong size.
            ////m_dDeviceRadius = m_dSize / ((2 * Cos(45)) - Cos(45) + 1)
            //m_dDeviceRadius = (m_dSize / 2) * m_dDeviceRatio;
            //m_dDeviceXOffset = m_dXOffset * m_dDeviceRatio;
            //m_dDeviceYOffset = m_dYOffset * m_dDeviceRatio;

            ////  Check if we need to rotate the symbol based on the ISymbolRotation interface.
            //if (m_bRotWithTrans)
            //    m_dMapRotation = m_trans.Rotation;
            //else
            //    m_dMapRotation = 0;

            //// Setup the Pen which is used to outline the shapes.
            //// multiplying by m_dDeviceRatio allows the pen size to scale
            //m_lPen = Utility.CreatePen(0, Convert.ToInt32(1 * m_dDeviceRatio), System.Convert.ToInt32(m_colorBorder.RGB));

            //// Set the appropriate raster operation code for this draw, according to the
            //// ISymbol interface.
            //m_lROP2Old = (esriRasterOpCode)Utility.SetROP2(hDC, System.Convert.ToInt32(m_lROP2));

            //// Set up three solid brushes to fill in the shapes with the different color fills.
            //m_lBrushTop = Utility.CreateSolidBrush(System.Convert.ToInt32(m_colorTop.RGB));
            //m_lBrushLeft = Utility.CreateSolidBrush(System.Convert.ToInt32(m_colorLeft.RGB));
            //m_lBrushRight = Utility.CreateSolidBrush(System.Convert.ToInt32(m_colorRight.RGB));

            //// Select in the new pen and store the old pen - essential for use during cleanup.
            //m_lOldPen = Utility.SelectObject(hDC, m_lPen);
        }

        void ESRI.ArcGIS.Display.ISymbol.Draw(ESRI.ArcGIS.Geometry.IGeometry Geometry)
        {
            if (m_lhDC == 0 | m_colorTop == null | m_colorLeft == null | m_colorRight == null | m_colorBorder == null)
                return;
            if (Geometry == null)
                return;
            if (!(Geometry is ESRI.ArcGIS.Geometry.IPoint))
                return;

            // Transform the Point coords to device coords, accounting for rotation, offset etc.
            ESRI.ArcGIS.Geometry.IPoint point = (IPoint)Geometry;

            int lCenterX = 0;
            int lCenterY = 0;

            Utility.FromMapPoint(m_trans, ref point, ref lCenterX, ref lCenterY);
            double tempy1 = System.Convert.ToDouble(lCenterY);
            CalcCoords(System.Convert.ToDouble(lCenterX), ref tempy1);

            //  Draw the chord, and two polygons, and flood fill them.
            //int lResult = 0;
            //int lTempBrush = 0;
            //m_lOldBrush = Utility.SelectObject(m_lhDC, m_lBrushTop);
            //lResult = Utility.Chord(m_lhDC, m_coords[5].x, m_coords[5].y, m_coords[6].x, m_coords[6].y, m_coords[4].x, m_coords[4].y, m_coords[1].x, m_coords[1].y);

            //Utility.SelectObject(m_lhDC, m_lBrushLeft);
            //lResult = Utility.Polygon(m_lhDC, ref m_coords[1], 3);

            //Utility.SelectObject(m_lhDC, m_lBrushRight);
            //lResult = Utility.Polygon(m_lhDC, ref m_coords[2], 3);

            //Utility.SelectObject(m_lhDC, m_lOldBrush);
        }

        void ESRI.ArcGIS.Display.ISymbol.ResetDC()
        {
            ////  Select back the old GDI pen and ROP code, and release other GDI resources
            //m_lROP2 = (esriRasterOpCode)Utility.SetROP2(m_lhDC, System.Convert.ToInt32(m_lROP2Old));
            //Utility.SelectObject(m_lhDC, m_lOldPen);
            //Utility.DeleteObject(m_lPen);
            //Utility.SelectObject(m_lhDC, m_lOldBrush);
            //Utility.DeleteObject(m_lBrushTop);
            //Utility.DeleteObject(m_lBrushLeft);
            //Utility.DeleteObject(m_lBrushRight);

            //m_trans = null;
            //m_lhDC = 0;
        }

        ESRI.ArcGIS.Display.esriRasterOpCode ESRI.ArcGIS.Display.ISymbol.ROP2
        {
            get
            {
                return m_lROP2;
            }
            set
            {
                if (Convert.ToInt32(value) < 1)
                    m_lROP2 = ESRI.ArcGIS.Display.esriRasterOpCode.esriROPCopyPen;
                else
                    m_lROP2 = value;
            }
        }

        void ESRI.ArcGIS.Display.ISymbol.QueryBoundary(int hDC, ESRI.ArcGIS.Geometry.ITransformation displayTransform, ESRI.ArcGIS.Geometry.IGeometry Geometry, ESRI.ArcGIS.Geometry.IPolygon Boundary)
        {
            // Check input parameters. Boundary may be a preexisting Polygon, so
            // make sure it's geometry is cleared.
            if (Geometry == null | Boundary == null)
                return;
            if (!(Geometry is ESRI.ArcGIS.Geometry.IPoint))
                return;
            Boundary.SetEmpty();

            IPoint point = (IPoint)Geometry;
            IDisplayTransformation displayTransformation = (IDisplayTransformation)displayTransform;

            QueryBoundsFromGeom(hDC, ref displayTransformation, ref Boundary, ref point);
        }

        #endregion

        #region IMarkerSymbol implementation

        double ESRI.ArcGIS.Display.IMarkerSymbol.Angle
        {
            get
            {
                //The Angle is set as degrees, and also used internally as degrees in this class.
                return m_dAngle;
            }
            set
            {
                //  In this Symbol, we correct for an Angle > 360 degrees.
                if (value > 360)
                    value = value - (Convert.ToInt32(value / 360) * 360);
                //The Angle is set as degrees, and also used internally as degrees in this class.
                m_dAngle = value;
            }
        }

        ESRI.ArcGIS.Display.IColor ESRI.ArcGIS.Display.IMarkerSymbol.Color
        {
            get
            {
                //  Return Color property by Value.
                IClone clone = (IClone)m_colorTop;
                return clone.Clone() as IColor;
            }
            set
            {
                //  Set Color property by Value.
                IClone clone = value as IClone;
                m_colorTop = clone.Clone() as IColor;
            }
        }

        double ESRI.ArcGIS.Display.IMarkerSymbol.Size
        {
            get
            {
                return m_dSize;
            }
            set
            {
                m_dSize = value;
            }
        }

        double ESRI.ArcGIS.Display.IMarkerSymbol.XOffset
        {
            get
            {
                return m_dXOffset;
            }
            set
            {
                m_dXOffset = value;
            }
        }

        double ESRI.ArcGIS.Display.IMarkerSymbol.YOffset
        {
            get
            {
                return m_dYOffset;
            }
            set
            {
                m_dYOffset = value;
            }
        }

        #endregion

        #region IDisplayName implementation

        string ESRI.ArcGIS.Display.IDisplayName.NameString
        {
            get
            {
                return m_sDisplayName;
            }
        }

        #endregion

        #region IClone implementation

        void ESRI.ArcGIS.esriSystem.IClone.Assign(ESRI.ArcGIS.esriSystem.IClone src)
        {
            GIS.IGasPressureSymbol srcLogoSym = null;
            ESRI.ArcGIS.Display.IMarkerSymbol srcMarkerSym = null;
            ESRI.ArcGIS.Display.IMarkerSymbol recMarkerSym = null;
            ESRI.ArcGIS.Display.ISymbol srcSym = null;
            ESRI.ArcGIS.Display.ISymbol recSym = null;
            ESRI.ArcGIS.Display.ISymbolRotation srcRotSym = null;
            ESRI.ArcGIS.Display.ISymbolRotation recRotSym = null;
            ESRI.ArcGIS.Display.IMapLevel srcMapLev = null;
            ESRI.ArcGIS.Display.IMapLevel recMapLev = null;
            if (src != null)
            {
                if (src is GIS.IGasPressureSymbol)
                {
                    //  Assign custom interface properties of Source to Reciever.
                    //  Color objects are returned from these properties by value.
                    srcLogoSym = src as IGasPressureSymbol;
                    m_colorBorder = srcLogoSym.ColorBorder;
                    m_colorLeft = srcLogoSym.ColorLeft;
                    m_colorRight = srcLogoSym.ColorRight;
                    m_colorTop = srcLogoSym.ColorTop;

                    //  Assign IMarkerSymbol interface properties of Source to Reciever, but
                    //  dont need to set Color because this is set in ColorTop.
                    //  We know that a Logo markerSymbol implements IMarkerSymbol.
                    srcMarkerSym = src as IMarkerSymbol;
                    recMarkerSym = this;
                    recMarkerSym.Angle = srcMarkerSym.Angle;
                    recMarkerSym.Size = srcMarkerSym.Size;
                    recMarkerSym.XOffset = srcMarkerSym.XOffset;
                    recMarkerSym.YOffset = srcMarkerSym.YOffset;

                    //  Assign ISymbol interface properties of Source to Reciever.
                    //  We know that a Logo markerSymbol implements ISymbol.
                    srcSym = src as ISymbol;
                    recSym = this;
                    recSym.ROP2 = srcSym.ROP2;

                    //  Assign ISymbolRotation interface properties of Source to Reciever.
                    //  We know that a Logo markerSymbol implements ISymbolRotation.
                    srcRotSym = src as ISymbolRotation;
                    recRotSym = this;
                    recRotSym.RotateWithTransform = srcRotSym.RotateWithTransform;

                    //  Assign IMapLevel interface properties of Source to Reciever.
                    //  We know that a Logo markerSymbol implements IMapLevel.
                    srcMapLev = src as IMapLevel;
                    recMapLev = this;
                    recMapLev.MapLevel = srcMapLev.MapLevel;

                    //  Also implements IMarkerMask, but this interface has no properties, only
                    //  a method, and therefore we dont have to assign anything for IMarkerMask.
                    //  Also implements IDiplayName, but this interface only has one property,
                    //  which is read-only, so we dont set anything for IDisplayName either.
                }
            }
        }

        ESRI.ArcGIS.esriSystem.IClone ESRI.ArcGIS.esriSystem.IClone.Clone()
        {
            //  Simplest way to implement the Clone function is to make use of the IClone Assign
            //  method. Create a new LogoMarkerSymbol and assign to it the properties of the
            //  current class, then pass back this new class, which is then a copy of the current class.
            GIS.IGasPressureSymbol newLogo = null;
            ESRI.ArcGIS.esriSystem.IClone clone = null;
            newLogo = new GIS.GasPressureSymbol();
            clone = newLogo as IClone;
            clone.Assign(this);
            return clone;
        }

        bool ESRI.ArcGIS.esriSystem.IClone.IsEqual(ESRI.ArcGIS.esriSystem.IClone other)
        {
            bool tempIClone_IsEqual = false;
            GIS.IGasPressureSymbol srcLogoSym = null;
            GIS.IGasPressureSymbol pRecLogoSym = null;
            ESRI.ArcGIS.Display.IMarkerSymbol srcMarkerSym = null;
            ESRI.ArcGIS.Display.IMarkerSymbol recMarkerSym = null;
            ESRI.ArcGIS.Display.ISymbol srcSym = null;
            ESRI.ArcGIS.Display.ISymbol recSym = null;
            ESRI.ArcGIS.Display.IDisplayName srcDispName = null;
            ESRI.ArcGIS.Display.IDisplayName recDispName = null;
            ESRI.ArcGIS.Display.ISymbolRotation srcSymRot = null;
            ESRI.ArcGIS.Display.ISymbolRotation recSymRot = null;
            ESRI.ArcGIS.Display.IMapLevel srcMapLev = null;
            ESRI.ArcGIS.Display.IMapLevel recMapLev = null;
            if (other != null)
            {
                if (other is GIS.IGasPressureSymbol)
                {

                    // Check for equality on default interface.
                    srcLogoSym = other as IGasPressureSymbol;
                    pRecLogoSym = this;
                    tempIClone_IsEqual = tempIClone_IsEqual & (System.Drawing.ColorTranslator.FromOle(System.Convert.ToInt32(pRecLogoSym.ColorBorder.RGB)).Equals(System.Drawing.ColorTranslator.FromOle(System.Convert.ToInt32(srcLogoSym.ColorBorder.RGB))));
                    tempIClone_IsEqual = tempIClone_IsEqual & (System.Drawing.ColorTranslator.FromOle(System.Convert.ToInt32(pRecLogoSym.ColorLeft.RGB)).Equals(System.Drawing.ColorTranslator.FromOle(System.Convert.ToInt32(srcLogoSym.ColorLeft.RGB))));
                    tempIClone_IsEqual = tempIClone_IsEqual & (System.Drawing.ColorTranslator.FromOle(System.Convert.ToInt32(pRecLogoSym.ColorRight.RGB)).Equals(System.Drawing.ColorTranslator.FromOle(System.Convert.ToInt32(srcLogoSym.ColorRight.RGB))));
                    tempIClone_IsEqual = tempIClone_IsEqual & (System.Drawing.ColorTranslator.FromOle(System.Convert.ToInt32(pRecLogoSym.ColorTop.RGB)).Equals(System.Drawing.ColorTranslator.FromOle(System.Convert.ToInt32(srcLogoSym.ColorTop.RGB))));

                    //  Check for equality on IMarkerSymbol interface.
                    //  We know that a LogoMarkerSymbol implements IMarkerSymbol.
                    srcMarkerSym = other as IMarkerSymbol;
                    recMarkerSym = this;
                    tempIClone_IsEqual = tempIClone_IsEqual & (recMarkerSym.Angle == srcMarkerSym.Angle);
                    tempIClone_IsEqual = tempIClone_IsEqual & (System.Drawing.ColorTranslator.FromOle(System.Convert.ToInt32(recMarkerSym.Color.RGB)).Equals(System.Drawing.ColorTranslator.FromOle(System.Convert.ToInt32(srcMarkerSym.Color.RGB))));
                    tempIClone_IsEqual = tempIClone_IsEqual & (recMarkerSym.Size == srcMarkerSym.Size);
                    tempIClone_IsEqual = tempIClone_IsEqual & (recMarkerSym.XOffset == srcMarkerSym.XOffset);
                    tempIClone_IsEqual = tempIClone_IsEqual & (recMarkerSym.YOffset == srcMarkerSym.YOffset);

                    //  Check for equality on ISymbol interface.
                    //  We know that a Logo markerSymbol implements ISymbol.
                    srcSym = other as ISymbol;
                    recSym = this;
                    tempIClone_IsEqual = tempIClone_IsEqual & (recSym.ROP2 == srcSym.ROP2);

                    //  Check for equality on IDisplayName interface.
                    //  We know that a Logo markerSymbol implements IDisplayName.
                    srcDispName = other as IDisplayName;
                    recDispName = this;
                    tempIClone_IsEqual = tempIClone_IsEqual & (recDispName.NameString == srcDispName.NameString);

                    //  Check for equality on ISymbolRotation
                    //  We know that a Logo markerSymbol implements IDisplayName.
                    srcSymRot = other as ISymbolRotation;
                    recSymRot = this;
                    tempIClone_IsEqual = tempIClone_IsEqual & (recSymRot.RotateWithTransform == srcSymRot.RotateWithTransform);

                    //  Check for equality on IMapLevel
                    //  We know that a Logo markerSymbol implements IMapLevel.
                    srcMapLev = other as IMapLevel;
                    recMapLev = this;
                    tempIClone_IsEqual = tempIClone_IsEqual & (recMapLev.MapLevel == srcMapLev.MapLevel);

                    //  Also implements IMarkerMask, but IMarkerMask has no properties to check.
                }
            }
            return tempIClone_IsEqual;
        }

        bool ESRI.ArcGIS.esriSystem.IClone.IsIdentical(ESRI.ArcGIS.esriSystem.IClone other)
        {
            bool tempIClone_IsIdentical = false;
            GIS.IGasPressureSymbol sym = null;
            if (other != null)
            {
                if (other is IGasPressureSymbol)
                {
                    sym = (IGasPressureSymbol)other;
                    tempIClone_IsIdentical = (sym == this);
                }
            }
            return tempIClone_IsIdentical;
        }

        #endregion

        #region IPersistVariant implementation

        ESRI.ArcGIS.esriSystem.UID ESRI.ArcGIS.esriSystem.IPersistVariant.ID
        {
            get
            {
                ESRI.ArcGIS.esriSystem.UID uid = new ESRI.ArcGIS.esriSystem.UID();
                uid.Value = GasPressureSymbol.GUID;

                return uid;
            }
        }

        void ESRI.ArcGIS.esriSystem.IPersistVariant.Load(ESRI.ArcGIS.esriSystem.IVariantStream Stream)
        {
            // Read the persisted version number first.
            int lSavedVers = 0;
            lSavedVers = Convert.ToInt32(Stream.Read());
            if ((lSavedVers > m_lCurrPersistVers) | (lSavedVers <= 0))
            {
                throw new Exception("Failed to read from stream");
            }

            // Set members to default values.
            InitializeMembers();

            // Load the first persistance pattern.
            if (lSavedVers == 1)
            {
                m_lROP2 = (esriRasterOpCode)Stream.Read();

                m_dSize = Convert.ToDouble(Stream.Read());
                m_dXOffset = Convert.ToDouble(Stream.Read());
                m_dYOffset = Convert.ToDouble(Stream.Read());
                m_dAngle = Convert.ToDouble(Stream.Read());

                m_bRotWithTrans = Convert.ToBoolean(Stream.Read());

                m_lMapLevel = Convert.ToInt32(Stream.Read());

                m_colorTop = Stream.Read() as IColor;
                m_colorLeft = Stream.Read() as IColor;
                m_colorRight = Stream.Read() as IColor;
                m_colorBorder = Stream.Read() as IColor;
            }
        }

        void ESRI.ArcGIS.esriSystem.IPersistVariant.Save(ESRI.ArcGIS.esriSystem.IVariantStream Stream)
        {
            // Write the persistence version number first.
            Stream.Write(m_lCurrPersistVers);

            // Persist ISymbol properties.
            Stream.Write(m_lROP2);

            //  Persist IMarkerSymbol properties.
            Stream.Write(m_dSize);
            Stream.Write(m_dXOffset);
            Stream.Write(m_dYOffset);
            Stream.Write(m_dAngle);

            //  Persist ISymbolRotation properties.
            Stream.Write(m_bRotWithTrans);

            //  Persist IMapLavel properties.
            Stream.Write(m_lMapLevel);

            //  Persist custom properties.
            Stream.Write(m_colorTop);
            Stream.Write(m_colorLeft);
            Stream.Write(m_colorRight);
            Stream.Write(m_colorBorder);
        }

        #endregion

        #region ISymbolRotation implementation

        bool ESRI.ArcGIS.Display.ISymbolRotation.RotateWithTransform
        {
            get
            {
                return m_bRotWithTrans;
            }
            set
            {
                m_bRotWithTrans = value;
            }
        }
        #endregion

        #region IMapLevel implementation

        int ESRI.ArcGIS.Display.IMapLevel.MapLevel
        {
            get
            {
                return m_lMapLevel;
            }
            set
            {
                m_lMapLevel = value;
            }
        }

        #endregion

        #region IMarkerMask implementation

        void ESRI.ArcGIS.Display.IMarkerMask.QueryMarkerMask(int hDC, ESRI.ArcGIS.Geometry.ITransformation Transform, ESRI.ArcGIS.Geometry.IGeometry Geometry, ESRI.ArcGIS.Geometry.IPolygon Boundary)
        {
            //  Code QueryBoundary using same steps as Draw. But add a step where
            //  Points are converted to Map units, and then build an appropriate Polygon.
            if (Geometry == null | Boundary == null)
                return;
            if (!(Transform is ESRI.ArcGIS.Display.IDisplayTransformation))
                return;
            if (!(Geometry is ESRI.ArcGIS.Geometry.IPoint))
                return;
            Boundary.SetEmpty();

            IPoint point = Geometry as IPoint;
            IDisplayTransformation displayTrans = (IDisplayTransformation)Transform;
            QueryBoundsFromGeom(hDC, ref displayTrans, ref Boundary, ref point);

            // Unlike ISymbol.QueryBoundary, QueryMarkerMask requires a Simple geometry.
            ITopologicalOperator topo = Boundary as ITopologicalOperator;
            if (!topo.IsKnownSimple)
            {
                if (!topo.IsSimple)
                    topo.Simplify();
            }
        }

        #endregion

        #region IPropertySupport Members

        public bool Applies(object pUnk)
        {
            IColor color = pUnk as IColor;
            IGasPressureSymbol logoMarkerSymbol = pUnk as IGasPressureSymbol;
            if (null != color || null != logoMarkerSymbol)
                return true;

            return false;
        }

        public object Apply(object newObject)
        {
            object oldObject = null;

            IColor color = newObject as IColor;
            if (null != color)
            {
                oldObject = ((IPropertySupport)this).get_Current(newObject);
                ((IMarkerSymbol)this).Color = color;

            }

            IGasPressureSymbol logoMarkerSymbol = newObject as IGasPressureSymbol;
            {
                oldObject = ((IPropertySupport)this).get_Current(newObject);
                IClone clone = (IClone)newObject;
                ((IClone)this).Assign(clone);
            }

            return oldObject;
        }

        public bool CanApply(object pUnk)
        {
            return ((IPropertySupport)this).Applies(pUnk);
        }

        public object get_Current(object pUnk)
        {
            IColor color = pUnk as IColor;
            if (null != color)
            {
                IColor currentColor = ((IMarkerSymbol)this).Color;
                return (object)currentColor;
            }

            IGasPressureSymbol logoMarkerSymbol = pUnk as IGasPressureSymbol;
            {
                IClone clone = ((IClone)this).Clone();
                return (object)clone;
            }
        }

        #endregion

        #region ILogoMarkerSymbol implmentation

        ESRI.ArcGIS.Display.IColor IGasPressureSymbol.ColorBorder
        {
            get
            {
                //  Return ColorBorder by Value.
                ESRI.ArcGIS.esriSystem.IClone clone = null;
                clone = m_colorBorder as IClone;
                return clone.Clone() as IColor;
            }
            set
            {
                //  Set ColorBorder by Value.
                ESRI.ArcGIS.esriSystem.IClone clone = null;
                clone = value as IClone;
                m_colorBorder = clone.Clone() as IColor;
            }
        }

        ESRI.ArcGIS.Display.IColor IGasPressureSymbol.ColorLeft
        {
            get
            {
                //  Return ColorLeft by Value.
                ESRI.ArcGIS.esriSystem.IClone clone = null;
                clone = m_colorLeft as IClone;
                return clone.Clone() as IColor;
            }
            set
            {
                //  Set ColorTop by Value.
                ESRI.ArcGIS.esriSystem.IClone clone = null;
                clone = value as IClone;
                m_colorLeft = clone.Clone() as IColor;
            }
        }

        ESRI.ArcGIS.Display.IColor IGasPressureSymbol.ColorRight
        {
            get
            {
                //  Return ColorRight by Value.
                ESRI.ArcGIS.esriSystem.IClone clone = null;
                clone = m_colorRight as IClone;
                return clone.Clone() as IColor;
            }
            set
            {
                //  Set ColorRight by Value.
                ESRI.ArcGIS.esriSystem.IClone clone = null;
                clone = value as IClone;
                m_colorRight = clone.Clone() as IColor;
            }
        }

        ESRI.ArcGIS.Display.IColor IGasPressureSymbol.ColorTop
        {
            get
            {
                //  Return ColorTop by Value.
                ESRI.ArcGIS.esriSystem.IClone clone = null;
                clone = m_colorTop as IClone;
                return clone.Clone() as IColor;
            }
            set
            {
                //  Set ColorTop by Value.
                ESRI.ArcGIS.esriSystem.IClone clone = null;
                clone = value as IClone;
                m_colorTop = clone.Clone() as IColor;
            }
        }

        #endregion

        #region Internal class functions

        private void QueryBoundsFromGeom(int hDC, ref ESRI.ArcGIS.Display.IDisplayTransformation transform, ref ESRI.ArcGIS.Geometry.IPolygon boundary, ref ESRI.ArcGIS.Geometry.IPoint point)
        {
            // Calculate Size, XOffset and YOffset of the shape in Map units.
            double dMapXOffset = 0;
            double dMapSize = 0;
            double dMapYOffset = 0;

            dMapSize = PointsToMap(transform, m_dSize);
            if (m_dXOffset != 0)
                dMapXOffset = PointsToMap(transform, m_dXOffset);
            if (m_dYOffset != 0)
                dMapYOffset = PointsToMap(transform, m_dYOffset);
            point.PutCoords(point.X + dMapXOffset, point.Y + dMapYOffset);

            // Set up the device ratio.
            SetupDeviceRatio(hDC, transform);

            ESRI.ArcGIS.Geometry.IPointCollection ptColl = null;
            ESRI.ArcGIS.Geometry.ISegmentCollection segColl = null;
            double dVal = 0; // dVal is the measurement of the short side of a Triangles are based on.
            double dRad = 0;
            ptColl = (IPointCollection)boundary;
            segColl = (ISegmentCollection)boundary;
            dRad = dMapSize / 2;
            dVal = System.Math.Sqrt((dRad * dRad) / 2);
            object missing = System.Reflection.Missing.Value;
            ptColl.AddPoint(Utility.CreatePoint(point.X + dVal, point.Y - dVal), ref missing, ref missing);
            ptColl.AddPoint(Utility.CreatePoint(point.X - dVal, point.Y - dVal), ref missing, ref missing);
            ptColl.AddPoint(Utility.CreatePoint(point.X - dVal, point.Y + dVal), ref missing, ref missing);

            IPoint p = ptColl.get_Point(0);
            segColl.AddSegment((ISegment)Utility.CreateCircArc(point, ptColl.get_Point(2), ref p), ref missing, ref missing);

            //  Account for rotation also.
            ESRI.ArcGIS.Geometry.ITransform2D trans2D = null;
            if ((m_dAngle + m_dMapRotation) != 0)
            {
                trans2D = boundary as ITransform2D;
                trans2D.Rotate(point, Utility.Radians(m_dAngle + m_dMapRotation));
            }
        }

        private void RotateCoords()
        {
            //  This function provides an alternative to rotate the coordinates
            //  which should be more efficient, as it is performed in floating point numbers
            //  instead of creating objects. Need to copy the Point values temporarily.
            //  Correct for anticlockwise rotation as positive angle here.
            double dAngle = 0.0;
            dAngle = 360.0 - (m_dAngle + m_dMapRotation);

            short i = 0;
            //Utility.POINTAPI p = new Utility.POINTAPI();
            //for (i = 0; i <= 4; i++)
            //{
            //    if (!(i == 2))
            //    {
            //        p = m_coords[i];
            //        m_coords[i].x = m_coords[2].x + Convert.ToInt32((p.x - m_coords[2].x) * System.Math.Cos(Utility.Radians(dAngle))) - Convert.ToInt32((p.y - m_coords[2].y) * System.Math.Sin(Utility.Radians(dAngle)));
            //        m_coords[i].y = m_coords[2].y + Convert.ToInt32((p.x - m_coords[2].x) * System.Math.Sin(Utility.Radians(dAngle))) + Convert.ToInt32((p.y - m_coords[2].y) * System.Math.Cos(Utility.Radians(dAngle)));
            //    }
            //}
        }

        private void CalcCoords(double x, ref double y)
        {
            //m_coords[0].x = Convert.ToInt32(x);
            //m_coords[0].y = Convert.ToInt32(y);

            ////  This function calculates the required coordinates for each Symbol based on the
            ////  feature's geometry. These coordinates are in Device units.
            //double dVal = 0;
            //dVal = System.Math.Sqrt((m_dDeviceRadius * m_dDeviceRadius) / 2);

            ////  We account for the offset in the calculation of the center point. All other
            ////  points are then calculated from this.  Y coordinates are top to bottom in Y axis.
            //m_coords[2].x = Convert.ToInt32(x + m_dDeviceXOffset);
            //m_coords[2].y = Convert.ToInt32(y - m_dDeviceYOffset);

            //m_coords[1].x = m_coords[2].x - Convert.ToInt32(dVal);
            //m_coords[1].y = m_coords[2].y - Convert.ToInt32(dVal);
            //m_coords[4].x = m_coords[2].x + Convert.ToInt32(dVal);
            //m_coords[4].y = m_coords[2].y + Convert.ToInt32(dVal);
            //m_coords[3].x = m_coords[1].x;
            //m_coords[3].y = m_coords[4].y;
            //m_coords[5].x = m_coords[2].x - Convert.ToInt32(m_dDeviceRadius);
            //m_coords[5].y = m_coords[2].y - Convert.ToInt32(m_dDeviceRadius);
            //m_coords[6].x = m_coords[2].x + Convert.ToInt32(m_dDeviceRadius);
            //m_coords[6].y = m_coords[2].y + Convert.ToInt32(m_dDeviceRadius);

            //RotateCoords();
        }

        private void SetupDeviceRatio(int hDC, ESRI.ArcGIS.Display.IDisplayTransformation displayTransform)
        {
            if (displayTransform != null)
            {
                if (displayTransform.Resolution != 0)
                {
                    m_dDeviceRatio = displayTransform.Resolution / 72;
                    //  Check the ReferenceScale of the display transformation. If not zero, we need to
                    //  adjust the Size, XOffset and YOffset of the Symbol we hold internally before drawing.
                    if (displayTransform.ReferenceScale != 0)
                        m_dDeviceRatio = m_dDeviceRatio * displayTransform.ReferenceScale / displayTransform.ScaleRatio;
                }
            }
            else
            {
                // If we dont have a displaytransformation, calculate the resolution
                // from the actual device.
                if (hDC != 0)
                {
                    // Get the resolution from the device context hDC.
                    m_dDeviceRatio = System.Convert.ToDouble(Utility.GetDeviceCaps(hDC, Utility.LOGPIXELSX)) / 72;
                }
                else
                {
                    // If invalid hDC assume we're drawing to the screen.
                    m_dDeviceRatio = 1 / (Utility.TwipsPerPixelX() / 20); // 1 Point = 20 Twips.
                }
            }
        }

        private double PointsToMap(ESRI.ArcGIS.Geometry.ITransformation displayTransform, double dPointSize)
        {
            double tempPointsToMap = 0;
            ESRI.ArcGIS.Display.IDisplayTransformation tempTransform = null;
            if (displayTransform == null)
                tempPointsToMap = dPointSize * m_dDeviceRatio;
            else
            {
                tempTransform = (IDisplayTransformation)displayTransform;
                tempPointsToMap = tempTransform.FromPoints(dPointSize);
            }
            return tempPointsToMap;
        }

        #endregion

    }
} //end of root namespace