using System;
using System.Collections;
using System.Configuration;
using System.Globalization;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using ASPNET.StarterKit.Portal.Components;
using ASPNET.StarterKit.Portal.Data.SqlClient;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace ASPNET.StarterKit.Portal
{
   
    public class Global : HttpApplication, IContainerAccessor
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            IUnityContainer container = new UnityContainer();
            //configure
            var section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            section.Containers.Default.Configure(container);
            container.RegisterInstance(ConfigurationManager.ConnectionStrings["ConnectionString"]);
            // Register the relevant types for the 
            // container here through classes or configuration
            // register the container in the container property
            Container = container;
        }

        protected void Application_End(object sender, EventArgs e)
        {
            if (Container != null)
            {
                Container.Dispose();
            }
        }

        /// <summary>
        /// Returns the Unity container of the application 
        /// </summary>
        IUnityContainer IContainerAccessor.Container
        {
            get { return Container; }
        }

        /// <summary>
        /// The Unity container for the current application
        /// </summary>
        public static IUnityContainer Container
        {
            get;
            set;
        }

        public static void BuildItemWithCurrentContext<T>(Control ctrl)
        {
            Container.BuildUp(typeof(T), ctrl);
        }


        //*********************************************************************
        //
        // Application_BeginRequest Event
        //
        // The Application_BeginRequest method is an ASP.NET event that executes 
        // on each web request into the portal application.  The below method
        // obtains the current tabIndex and TabId from the querystring of the 
        // request -- and then obtains the configuration necessary to process
        // and render the request.
        //
        // This portal configuration is stored within the application's "Context"
        // object -- which is available to all pages, controls and components
        // during the processing of a single request.
        // 
        //*********************************************************************

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

            // Build and add the PortalSettings object to the current Context
            Context.Items.Add("PortalSettings", new PortalSettings(tabIndex, tabId));

            // Retrieve and add the SiteConfiguration DataSet to the current Context
            HttpContext.Current.Items.Add("SiteSettings", PortalConfiguration.GetSiteSettings());

            try
            {
                if (Request.UserLanguages != null)
                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Request.UserLanguages[0]);
                else
                    // Default to English if there are no user languages
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-us");

                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            }
            catch /*(Exception ex)*/
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-us");
            }
        }

        //*********************************************************************
        //
        // Application_AuthenticateRequest Event
        //
        // If the client is authenticated with the application, then determine
        // which security roles he/she belongs to and replace the "User" intrinsic
        // with a custom IPrincipal security object that permits "User.IsInRole"
        // role checks within the application
        //
        // Roles are cached in the browser in an in-memory encrypted cookie.  If the
        // cookie doesn't exist yet for this session, create it.
        //
        //*********************************************************************

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                String[] roles;

                // Create the roles cookie if it doesn't exist yet for this session.
                if ((Request.Cookies["portalroles"] == null) || (Request.Cookies["portalroles"].Value == ""))
                {
                    // Get roles from UserRoles table, and add to cookie
                    var user = new UsersDB();
                    roles = user.GetRoles(User.Identity.Name);

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
                    var userRoles = new ArrayList();

                    foreach (String role in ticket.UserData.Split(new[] {';'}))
                    {
                        userRoles.Add(role);
                    }

                    roles = (String[]) userRoles.ToArray(typeof (String));
                }

                // Add our own custom principal to the request containing the roles in the auth ticket
                Context.User = new GenericPrincipal(Context.User.Identity, roles);
            }
        }

        //*********************************************************************
        //
        // GetApplicationPath Method
        //
        // This method returns the correct relative path when installing
        // the portal on a root web site instead of virtual directory
        //
        //*********************************************************************
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