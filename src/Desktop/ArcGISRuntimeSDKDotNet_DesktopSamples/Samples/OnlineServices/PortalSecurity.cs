﻿using Esri.ArcGISRuntime.Security;
using System.Threading.Tasks;

namespace ArcGISRuntimeSDKDotNet_DesktopSamples.Samples
{
    /// <summary>
    /// Security class that defines global challenge method for accessing arcgis.com portal services
    /// </summary>
    internal class PortalSecurity
    {
        private const string PORTAL_URL = "https://www.arcgis.com/sharing/rest";

        // *** TODO: Replace CLIENT_ID with your arcgis.com App ID ***
        private const string CLIENT_ID = "2HEtx9ujil5rac8K";
        private const string REDIRECT_URI = "urn:ietf:wg:oauth:2.0:oob";

        // Challenge method should prompt for portal oauth username / password if necessary
        public static async Task<IdentityManager.Credential> Challenge(IdentityManager.CredentialRequestInfo arg)
        {
            // Register Portal Server if necessary
            var serverInfo = IdentityManager.Current.FindServerInfo(PORTAL_URL);
            if (serverInfo == null)
            {
                serverInfo = new IdentityManager.ServerInfo()
                {
                    ServerUri = PORTAL_URL,
                    TokenAuthenticationType = IdentityManager.TokenAuthenticationType.OAuthAuthorizationCode,
                    OAuthClientInfo = new IdentityManager.OAuthClientInfo()
                    {
                        ClientId = CLIENT_ID,
                        RedirectUri = REDIRECT_URI
                    }
                };

                IdentityManager.Current.RegisterServer(serverInfo);
            }

            // Use portal URL always (we know all layers are owned by arcgis.com)
            return await IdentityManager.Current.GenerateCredentialAsync(PORTAL_URL);
        }
    }
}
