using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.ComponentModel;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using System.Security.Principal;

namespace TimeCapture
{
    public partial class Licenses : System.Web.UI.Page
    {
        int MailboxeLicenses = 0;
        int Office365Licenses = 0;
        int WorkspaceLicenses = 0;
        int MsOfficeLicenses = 0;
        int MsProjectLicenses = 0;
        int MsVisioLicenses = 0;
        int MsCRMLicenses = 0;
        int MSSharepointLicenses = 0;
        int ThirdPartyLicenses = 0;
        int MEHSLicenses = 0;
        int InTuneLicenses = 0;
        int NFB = 0;

        int WindowsCAL = 0;
        

        const UInt32 BIT_MASK_MAILBOX = 0x1;
        const UInt32 BIT_MASK_OFFICE365 = 0x2;
        const UInt32 BIT_MASK_WORKSPACE = 0x4;
        const UInt32 BIT_MASK_MSOFFICE = 0x8;
        const UInt32 BIT_MASK_MSPROJECT = 0x10;
        const UInt32 BIT_MASK_MSVISIO = 0x20;
        const UInt32 BIT_MASK_MSCRM = 0x40;
        const UInt32 BIT_MASK_MSSHAREPOINT = 0x80;
        const UInt32 BIT_MASK_THIRDPARTY = 0x100;
        const UInt32 BIT_MASK_MEHS = 0x200;
        const UInt32 BIT_MASK_INTUNE = 0x400;
        const UInt32 BIT_MASK_NFB = 0x80000000;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Common.Instance.CheckAccess())
                Response.Redirect("AccessDenied.aspx", true);

        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            string Query = "SELECT * FROM UserLicenses WHERE Date='" + txtDate.Text + "' and UserDisabled=0";
            List<UserLicensesEntity> Licenses = DataAccessLayer.Instance.GetEntities<UserLicensesEntity>(Query);

            foreach (UserLicensesEntity license in Licenses)
            {
                if ((int)(((Convert.ToUInt32(license.LicenseCode) & (UInt32)BIT_MASK_NFB)) / BIT_MASK_NFB) == 0)
                {
                    MailboxeLicenses += (int)(Convert.ToUInt32(license.LicenseCode) & (UInt32)BIT_MASK_MAILBOX);
                    Office365Licenses += (int)(((Convert.ToUInt32(license.LicenseCode) & (UInt32)BIT_MASK_OFFICE365)) / BIT_MASK_OFFICE365);
                    WorkspaceLicenses += (int)(((Convert.ToUInt32(license.LicenseCode) & (UInt32)BIT_MASK_WORKSPACE)) / BIT_MASK_WORKSPACE);
                    MEHSLicenses += (int)(((Convert.ToUInt32(license.LicenseCode) & (UInt32)BIT_MASK_MEHS)) / BIT_MASK_MEHS);

                    MsOfficeLicenses += (int)(((Convert.ToUInt32(license.LicenseCode) & (UInt32)BIT_MASK_MSOFFICE)) / BIT_MASK_MSOFFICE);
                    MsProjectLicenses += (int)(((Convert.ToUInt32(license.LicenseCode) & (UInt32)BIT_MASK_MSPROJECT)) / BIT_MASK_MSPROJECT);
                    MsVisioLicenses += (int)(((Convert.ToUInt32(license.LicenseCode) & (UInt32)BIT_MASK_MSVISIO)) / BIT_MASK_MSVISIO);
                    MsCRMLicenses += (int)(((Convert.ToUInt32(license.LicenseCode) & (UInt32)BIT_MASK_MSCRM)) / BIT_MASK_MSCRM);
                    MSSharepointLicenses += (int)(((Convert.ToUInt32(license.LicenseCode) & (UInt32)BIT_MASK_MSSHAREPOINT)) / BIT_MASK_MSSHAREPOINT);
                    ThirdPartyLicenses += (int)(((Convert.ToUInt32(license.LicenseCode) & (UInt32)BIT_MASK_THIRDPARTY)) / BIT_MASK_THIRDPARTY);
                    InTuneLicenses += (int)(((Convert.ToUInt32(license.LicenseCode) & (UInt32)BIT_MASK_INTUNE)) / BIT_MASK_INTUNE);

                    if ((int)(Convert.ToUInt32(license.LicenseCode) & (UInt32)BIT_MASK_MAILBOX) == 1 || (int)(((Convert.ToUInt32(license.LicenseCode) & (UInt32)BIT_MASK_WORKSPACE)) / BIT_MASK_WORKSPACE) == 1)
                        WindowsCAL += 1;

                }
                else
                {
                    NFB += (int)(((Convert.ToUInt32(license.LicenseCode) & (UInt32)BIT_MASK_NFB)) / BIT_MASK_NFB);
                }
            }

            txtMailboxes.Text = MailboxeLicenses.ToString();
            txtOffice365.Text = Office365Licenses.ToString();
            txtWorkspace.Text = WorkspaceLicenses.ToString();
            txtMsOffice.Text = MsOfficeLicenses.ToString();
            txtMsProject.Text = MsProjectLicenses.ToString();
            txtMsVisio.Text = MsVisioLicenses.ToString();
            txtMsCRM.Text = MsCRMLicenses.ToString();
            txtMsSharepoint.Text = MSSharepointLicenses.ToString();
            txtThirdParty.Text = ThirdPartyLicenses.ToString();
            txtMEHS.Text = MEHSLicenses.ToString();
            txtNFB.Text = NFB.ToString();
            txtWindowsCAL.Text = WindowsCAL.ToString();
        }
    }
}