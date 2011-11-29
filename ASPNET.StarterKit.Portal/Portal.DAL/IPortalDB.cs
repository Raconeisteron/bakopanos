namespace ASPNET.StarterKit.Portal.Modules.DAL
{
    public interface IPortalDb
    {
        void DeleteModule(params int[] moduleIds);
    }
}