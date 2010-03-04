using System.IO;
using System.Xml.Serialization;
using ArchiCop.Library;

namespace ArchiCop
{
    public static class VsProjectHandler
    {
        public static VsProjectList DumpVisualStudioProjectList(string root, string file)
        {
            if (!Directory.Exists(root))
            {
                throw new DirectoryNotFoundException(root);
            }
            
            var projects = new VsProjectList();

            foreach (string projectFileName in Directory.GetFiles(root, "*.csproj", SearchOption.AllDirectories))
            {
                projects.Add(new VsProject(projectFileName));
            }

            return projects.Write(file);            
        }

        public static VsProjectList LoadVisualStudioProjectList(string file)
        {            
            var projects = XmlSerializerHelpers.Read<VsProjectList>(file);
            return projects;
        }
    }    
}
