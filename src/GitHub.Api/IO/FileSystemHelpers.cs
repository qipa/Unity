using System.Collections.Generic;
using System.Linq;

namespace GitHub.Unity
{
    static class FileSystemHelpers
    {
        public static string FindCommonPath(IEnumerable<string> paths)
        {
            var pathsArray = paths.Where(s => s != null).Select(s => s.ToNPath().Parent).ToArray();
            var maxDepth = pathsArray.Max(path => path.Depth);
            var deepestPath = pathsArray.First(path => path.Depth == maxDepth);

            var commonParent = deepestPath;
            foreach (var path in pathsArray)
            {
                var cp = path.Elements.Any() ? commonParent.GetCommonParent(path) : null;
                if (cp != null)
                    commonParent = cp;
                else
                {
                    commonParent = null;
                    break;
                }
            }
            return commonParent;
        }
    }
}