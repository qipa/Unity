using System.Collections.Generic;
using System.Threading;

namespace GitHub.Unity
{
    class GitAddTask : ProcessTask<string>
    {
        private readonly string arguments;

        public GitAddTask(IEnumerable<string> files,
            CancellationToken token, IOutputProcessor<string> processor = null)
            : base(token, processor ?? new SimpleOutputProcessor())
        {
            Guard.ArgumentNotNull(files, "files");

            arguments = "add ";
            arguments += " -- ";

            foreach (var file in files)
            {
                arguments += " \"" + file.ToNPath().ToString(SlashMode.Forward) + "\"";
            }
        }

        public override string Name { get { return "git add"; } }
        public override string ProcessArguments { get { return arguments; } }
        public override TaskAffinity Affinity { get { return TaskAffinity.Exclusive; } }
    }
}
