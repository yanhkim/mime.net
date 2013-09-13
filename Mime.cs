using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace mime.net
{
    public static class Mime
    {
        private static Dictionary<string, string> types = new Dictionary<string, string>();
        private static bool loaded;

        public static string Lookup(string path) {
            string ext = ExtractExtension(path), mime;
            if (!Types.TryGetValue(ext, out mime)) {
                mime = DefaultType;
            }
            return mime;
        }

        private static string ExtractExtension(string path) {
            return Regex.Replace(path, @".*[\.\/\\]", String.Empty).ToLower();
        }

        private static Dictionary<string, string> Types {
            get {
                if (loaded) return types;

                Load(Properties.Resources.mimeTypes);
                Load(Properties.Resources.nodeTypes);
                loaded = true;

                DefaultType = Lookup("bin");

                return types;
            }
        }

        private static void Load(string content) {
            var map = new Dictionary<string, IEnumerable<string>>();
            var lines = Regex.Split(content, @"[\r\n]+");
            foreach (var line in lines) {
                var striped = Regex.Replace(line, @"\s*#.*|^\s*|\s*$", String.Empty);
                var fields = Regex.Split(striped, @"\s+");
                if (fields[0] == String.Empty) {
                    continue;
                }
                var list = new List<string>(fields);
                var ext = list[0];
                list.RemoveAt(0);
                map[ext] = list;
            }
            Define(map);
        }

        public static void Define(IDictionary<string, IEnumerable<string>> map) {
            foreach (var type in map) {
                var exts = type.Value;
                foreach (var ext in exts) {
                    types[ext] = type.Key;
                }
            }
        }

        public static string DefaultType { get; set; }
    }
}
