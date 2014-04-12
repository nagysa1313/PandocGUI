using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandocGUI.Model
{
    class PandocFileExtension
    {
        public string Extension { get; set; }

        public string PandocType { get; set; }

        private static readonly IEnumerable<PandocFileExtension> _availableExtensions;
        static PandocFileExtension()
        {
            var exts = new List<PandocFileExtension>();

            exts.Add(new PandocFileExtension() { Extension = ".tex", PandocType = "latex" });
            exts.Add(new PandocFileExtension() { Extension = ".html", PandocType = "html" });
            exts.Add(new PandocFileExtension() { Extension = ".docx", PandocType = "docx" });
            exts.Add(new PandocFileExtension() { Extension = ".epub", PandocType = "epub" });

            _availableExtensions = exts;
        }
        public static IEnumerable<PandocFileExtension> AvailableExtentions
        {
            get
            {
                return _availableExtensions;
            }
        }
        public static Dictionary<string, string> Extensions
        {
            get
            {
                return _availableExtensions.ToDictionary(k => k.Extension, v => v.PandocType);
            }
        }
    }
}