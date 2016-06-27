using System.Management.Automation;
using System.Management.Automation.Provider;

namespace ASPNETMembership
{
    [CmdletProvider( "ASPNETMembership", ProviderCapabilities.None )]
    public class Provider : DriveCmdletProvider
    {
        protected override PSDriveInfo NewDrive( PSDriveInfo drive )
        {
            return new MembershipDriveInfo(drive);
        }

        protected override object NewDriveDynamicParameters()
        {
            return base.NewDriveDynamicParameters();
        }
    }
}
