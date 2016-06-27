using System.Configuration;
using System.Management.Automation;
using System.Web.Security;

namespace ASPNETMembership
{
    public class MembershipDriveInfo : PSDriveInfo
    {
        MembershipProvider provider;

        public MembershipDriveInfo( PSDriveInfo driveInfo )
            : base( driveInfo )
        {
            // get the configured connection string collection
            var connectionStrings = ConfigurationManager.ConnectionStrings;

            // get the private bReadOnly field of the collection type
            var fi = typeof( ConfigurationElementCollection )
                .GetField( "bReadOnly", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic );

            // set the bReadOnly field of the connection string collection to false 
            fi.SetValue( connectionStrings, false );

            // add a new connection string
            connectionStrings.Add(
                new ConnectionStringSettings(
                    "ProviderConnectionString",
                    "data source=localhost;Integrated Security=SSPI;Initial Catalog=M"
                )
            );

            // configure the membership provider programmatically
            provider = new SqlMembershipProvider();
            var nvc = new System.Collections.Specialized.NameValueCollection
            {
                { "connectionStringName", "ProviderConnectionString" },
                { "enablePasswordRetrieval", "false" },
                { "enablePasswordReset", "true" },
                { "requiresQuestionAndAnswer", "false" },
                { "requiresUniqueEmail", "false" },
                { "passwordFormat", "Hashed" },
                { "maxInvalidPasswordAttempts", "5" },
                { "minRequiredPasswordLength", "6" },
                { "minRequiredNonalphanumericCharacters", "0" },
                { "passwordAttemptWindow", "10" },
                { "passwordStrengthRegularExpression", "" },
                { "applicationName", "/" },
            };

            provider.Initialize( "AspNetSqlMembershipProvider", nvc );
        }
        
        public MembershipProvider MembershipProvider
        {
            get
            {
                return this.provider;
            }
        }
    }
}
