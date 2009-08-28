using System;

namespace ASPNET.StarterKit.Portal
{
    public class DocumentItem
    {
        public int ModuleId { get; set; }
        public int ItemId { get; set; }
        public String UserName { get; set; }
        public String Name { get; set; }
        public String Url { get; set; }
        public String Category { get; set; }
        public byte[] Content { get; set; }
        public int Size { get; set; }
        public String ContentType { get; set; }
    }

   
}