using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionFormation.Models
{
    public class DirectoryModel
    {
        public string DirPath { get; set; }
        public List<string> Directories { get; set; }
        public List<string> Files { get; set; }

        public DirectoryModel()
        {
            Directories = new List<string>();
            Files = new List<string>();
        }
    }
}