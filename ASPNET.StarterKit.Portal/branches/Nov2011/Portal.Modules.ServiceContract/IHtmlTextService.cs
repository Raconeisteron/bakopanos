using System.ServiceModel;

namespace Portal.Modules.Service
{
    [ServiceContract]
    public interface IHtmlTextService
    {
        [OperationContract]
        PortalHtmlText GetHtmlText(int moduleId);

        [OperationContract]
        void UpdateHtmlText(int moduleId, string desktopHtml, string mobileSummary, string mobileDetails);
    }
}