namespace ASPNET.StarterKit.Portal.DAL
{
    public interface IPortalDB
    {
        void PortalDeleteModule(params int[] moduleIds);
    }
}