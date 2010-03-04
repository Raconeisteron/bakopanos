using System.IO;
using System.Xml.Serialization;
using ArchiCop.Library;

namespace ArchiCop
{
    public static class VisualStudioProjectHandler
    {
        public static VisualStudioProjectList DumpVisualStudioProjectList(string root, string file)
        {
            if (!Directory.Exists(root))
            {
                throw new DirectoryNotFoundException(root);
            }
            
            var projects = new VisualStudioProjectList();

            foreach (string projectFileName in Directory.GetFiles(root, "*.csproj", SearchOption.AllDirectories))
            {
                projects.Add(new VisualStudioProject(projectFileName));
            }

            return projects.Write(file);            
        }

        public static VisualStudioProjectList LoadVisualStudioProjectList(string file)
        {            
            var projects = XmlSerializerHelpers.Read<VisualStudioProjectList>(file);
            return projects;
        }
    }    
}
