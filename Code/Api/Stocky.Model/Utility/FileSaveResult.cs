using System.Collections.Generic;

namespace Stocky.Model.Utility
{
    public class FileSaveResult
    {
        public List<string> FileNames { get; set; } = new List<string>();

        public bool FileIsThere { get; set; } = false;
    }
}
