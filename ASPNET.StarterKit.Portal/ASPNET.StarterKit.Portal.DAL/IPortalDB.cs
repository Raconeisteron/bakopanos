namespace ASPNET.StarterKit.Portal.DAL
{
    public interface IPortalDB
    {
        void DeleteModule(params int[] moduleIds);
    }
}