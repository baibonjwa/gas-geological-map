using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.esriSystem;

namespace GIS.Common
{
    public class LicenseInitializer
    {
        private IAoInitialize m_AoInitialize = new AoInitializeClass();

        public bool InitializeApplication()
        {
            bool bInitialized = true;

            if (m_AoInitialize == null)
            {
                System.Windows.Forms.MessageBox.Show("Unable to initialize. This application cannot run!");
                bInitialized = false;
            }

            //初始化应用程序
            esriLicenseStatus licenseStatus = esriLicenseStatus.esriLicenseUnavailable;

            licenseStatus = CheckOutLicenses(esriLicenseProductCode.esriLicenseProductCodeEngineGeoDB);
            if (licenseStatus != esriLicenseStatus.esriLicenseCheckedOut)
            {
                licenseStatus = CheckOutLicenses(esriLicenseProductCode.esriLicenseProductCodeEngineGeoDB);
                if (licenseStatus != esriLicenseStatus.esriLicenseCheckedOut)
                {
                    licenseStatus = CheckOutLicenses(esriLicenseProductCode.esriLicenseProductCodeStandard);
                    if (licenseStatus != esriLicenseStatus.esriLicenseCheckedOut)
                    {
                        licenseStatus = CheckOutLicenses(esriLicenseProductCode.esriLicenseProductCodeBasic);
                        if (licenseStatus != esriLicenseStatus.esriLicenseCheckedOut)
                        {
                            System.Windows.Forms.MessageBox.Show(LicenseMessage(licenseStatus));
                            bInitialized = false;
                        }
                    }
                }
            }

            return bInitialized;
        }

        public void ShutdownApplication()
        {
            if (m_AoInitialize == null) return;

            //关闭 AoInitilaize对象
            m_AoInitialize.Shutdown();
            m_AoInitialize = null;
        }

        private esriLicenseStatus CheckOutLicenses(esriLicenseProductCode productCode)
        {
            esriLicenseStatus licenseStatus;

            //是否产品是可能的
            licenseStatus = m_AoInitialize.IsProductCodeAvailable(productCode);
            if (licenseStatus == esriLicenseStatus.esriLicenseAvailable)
            {

                //用相应的许可文件进行初始化
                licenseStatus = m_AoInitialize.Initialize(productCode);
            }
            return licenseStatus;
        }

        private string LicenseMessage(esriLicenseStatus licenseStatus)
        {
            string message = "";

            //没有许可
            if (licenseStatus == esriLicenseStatus.esriLicenseNotInitialized)
            {
                message = "You are not licensed to run this product!";
            }
            //许可正在使用
            else if (licenseStatus == esriLicenseStatus.esriLicenseUnavailable)
            {
                message = "There are insuffient licenses to run!";
            }
            //未知错误
            else if (licenseStatus == esriLicenseStatus.esriLicenseFailure)
            {
                message = "Unexpected license failure! Please contact your administrator.";
            }
            //已经初始化
            else if (licenseStatus == esriLicenseStatus.esriLicenseAlreadyInitialized)
            {
                message = "The license has already been initialized! Please check your implementation.";
            }
            return message;
        }
    }
}
