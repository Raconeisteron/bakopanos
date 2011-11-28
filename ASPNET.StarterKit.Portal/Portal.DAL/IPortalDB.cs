namespace ASPNET.StarterKit.Portal.DAL
{
    public interface IPortalDb
    {
        void DeleteModule(params int[] moduleIds);
    }
}