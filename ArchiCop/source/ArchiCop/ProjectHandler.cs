using System.IO;
using System.Xml.Serialization;

namespace ArchiCop
{
    public static class ProjectHandler
    {
        public static VisualStudioProjects DumpVisualStudioProjectList(string root, string file)
        {
            if (!Directory.Exists(root))
            {
                throw new DirectoryNotFoundException(root);
            }
            var projects = new VisualStudioProjects();

            foreach (string projectFileName in Directory.GetFiles(root, "*.csproj", SearchOption.AllDirectories))
            {
                projects.Add(new VisualStudioProject(projectFileName));
            }
            var serializer = new XmlSerializer(typeof(VisualStudioProjects));
            TextWriter textWriter = new StreamWriter(file);
            serializer.Serialize(textWriter, projects);
            textWriter.Close();
            return projects;
        }

        public static VisualStudioProjects LoadVisualStudioProjectList(string file)
        {
            if (!File.Exists(file))
            {
                throw new FileNotFoundException("Dump File doesn't exist.", file);
            }
            var deserializer = new XmlSerializer(typeof(VisualStudioProjects));
            TextReader textReader = new StreamReader(file);
            var projects = (VisualStudioProjects)deserializer.Deserialize(textReader);
            textReader.Close();
            return projects;
        }
    }    
}
