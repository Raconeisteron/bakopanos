// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using System.Runtime.Serialization;

namespace UIComposition.Contracts
{
    [DataContract]
    public class ProjectItem
    {
        [DataMember]
        public string ProjectName { get; set; }
        [DataMember]
        public string Role { get; set; }
    }
}