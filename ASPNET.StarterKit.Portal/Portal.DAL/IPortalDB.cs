namespace Portal.Modules.Data
{
    public interface IPortalDb
    {
        void DeleteModule(params int[] moduleIds);
    }
}