using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Security;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Unity.Web;

namespace ASPNET.StarterKit.Portal
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // Create a Unity container and load the Enterprise Library extension.
            IUnityContainer myContainer = Application.GetContainer();
            var section = (UnityConfigurationSection) ConfigurationManager.GetSection("unity");
            section.Configure(myContainer);

            // additional container initialization 
            myContainer.RegisterInstance<string>("ConfigFile", HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["configFile"]));
            myContainer.RegisterInstance<ConnectionStringSettings>(ConfigurationManager.ConnectionStrings["connectionString"]);
            myContainer.RegisterType<IDbHelper, DbHelper>(new ContainerControlledLifetimeManager());
            myContainer.RegisterType<IPortalSecurity, PortalSecurity>(new ContainerControlledLifetimeManager());

            // bootstrapp unity modules
            foreach (ContainerRegistration containerRegistration in myContainer.Registrations)
            {
                if (containerRegistration.RegisteredType.Name == "UnityModule")
                {
                    myContainer.Resolve(containerRegistration.RegisteredType);
                }
            }

        }

        /// <summary>
        /// The Application_BeginRequest method is an ASP.NET event that executes 
        /// on each web request into the portal application.  The below method
        /// obtains the current tabIndex and TabId from the querystring of the 
        /// request -- and then obtains the configuration necessary to process
        /// and render the request.
        ///
        /// This portal configuration is stored within the application's "Context"
        /// object -- which is available to all pages, controls and components
        /// during the processing of a single request.
        /// </summary>
        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            int tabIndex = 0;
            int tabId = 1;

            // Get TabIndex from querystring

            if (Request.Params["tabindex"] != null)
            {
                tabIndex = Int32.Parse(Request.Params["tabindex"]);
            }

            // Get TabID from querystring

            if (Request.Params["tabid"] != null)
            {
                tabId = Int32.Parse(Request.Params["tabid"]);
            }

            var model = HttpContext.Current.Application.GetContainer().Resolve<IConfigurationDb>();
            SiteConfiguration siteSettings = model.GetSiteSettings();

            // Build and add the PortalSettings object to the current Context
            Context.Items.Add("PortalSettings", new PortalSettings(siteSettings, tabIndex, tabId));

            try
            {
                Thread.CurrentThread.CurrentCulture = Request.UserLanguages != null
                                                          ? CultureInfo.CreateSpecificCulture(Request.UserLanguages[0])
                                                          : new CultureInfo("en-us");

                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            }
            catch
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-us");
            }
        }

        /// <summary>
        /// If the client is authenticated with the application, then determine
        /// which security roles he/she belongs to and replace the "User" intrinsic
        /// with a custom IPrincipal security object that permits "User.IsInRole"
        /// role checks within the application
        ///
        /// Roles are cached in the browser in an in-memory encrypted cookie.  If the
        /// cookie doesn't exist yet for this session, create it.
        /// </summary>
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            var model = HttpContext.Current.Application.GetContainer().Resolve<IUsersDb>();

            if (Request.IsAuthenticated)
            {
                String[] roles;

                // Create the roles cookie if it doesn't exist yet for this session.
                if ((Request.Cookies["portalroles"] == null) || (Request.Cookies["portalroles"].Value == ""))
                {
                    // Get roles from UserRoles table, and add to cookie
                    roles = model.GetRoles(User.Identity.Name).ToArray();

                    // Create a string to persist the roles
                    String roleStr = "";
                    foreach (String role in roles)
                    {
                        roleStr += role;
                        roleStr += ";";
                    }

                    // Create a cookie authentication ticket.
                    var ticket = new FormsAuthenticationTicket(
                        1, // version
                        Context.User.Identity.Name, // user name
                        DateTime.Now, // issue time
                        DateTime.Now.AddHours(1), // expires every hour
                        false, // don't persist cookie
                        roleStr // roles
                        );

                    // Encrypt the ticket
                    String cookieStr = FormsAuthentication.Encrypt(ticket);

                    // Send the cookie to the client
                    Response.Cookies["portalroles"].Value = cookieStr;
                    Response.Cookies["portalroles"].Path = "/";
                    Response.Cookies["portalroles"].Expires = DateTime.Now.AddMinutes(1);
                }
                else
                {
                    // Get roles from roles cookie
                    FormsAuthenticationTicket ticket =
                        FormsAuthentication.Decrypt(Context.Request.Cookies["portalroles"].Value);

                    //convert the string representation of the role data into a string array
                    roles = ticket.UserData.Split(new[] {';'}).ToArray();
                }

                // Add our own custom principal to the request containing the roles in the auth ticket
                Context.User = new GenericPrincipal(Context.User.Identity, roles);
            }
        }

        /// <summary>
        /// This method returns the correct relative path when installing
        /// the portal on a root web site instead of virtual directory
        /// </summary>
        public static string GetApplicationPath(HttpRequest request)
        {
            string path = string.Empty;
            try
            {
                if (request.ApplicationPath != "/")
                    path = request.ApplicationPath;
            }
            catch (Exception e)
            {
                throw e;
            }

            return path;
        }
    }
}