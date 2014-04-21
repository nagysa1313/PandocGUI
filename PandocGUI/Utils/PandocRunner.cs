using PandocGUI.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandocGUI.Utils
{
    public class PandocRunner
    {
        public static PandocTaskResult Run(string pandocExePath, PandocTask task)
        {
            if (pandocExePath == null) throw new ArgumentNullException("pandocExePath");
            if (task == null) throw new ArgumentNullException("task");

            var result = new PandocTaskResult();
            var msgBuilder = new StringBuilder(task.SourceFile + "\n");

            try
            {
                if (task.TargetFiles.Count < 1) throw new InvalidOperationException("At least 1 target file needed for the task!");

                foreach (var targetFile in task.TargetFiles)
                {
                    result.OutputFile = Path.GetFileName(targetFile.Path);
                    msgBuilder.Append(result.OutputFile);

                    var startInfo = new ProcessStartInfo(pandocExePath, string.Format("{0} -f {1} -t {3} -s -o {2}"
                        , task.SourceFile
                        , PandocFileExtension.Extensions[Path.GetExtension(task.SourceFile)]
                        , targetFile.Path
                        , PandocFileExtension.Extensions[Path.GetExtension(targetFile.Path)]
                        ));
                    startInfo.UseShellExecute = false;
                    startInfo.RedirectStandardOutput = true;
                    startInfo.CreateNoWindow = true;

                    var process = System.Diagnostics.Process.Start(startInfo);

                    while (!process.HasExited) ;

                    msgBuilder.AppendLine(" successful.");
                }

                result.OutputFile = string.Empty;
            }
            catch (Exception e)
            {
                Debugger.Break();
                msgBuilder.AppendLine(" failed with: ");
                msgBuilder.AppendLine(e.Message);
                result.Exception = e;
                result.Failed = true;
            }

            result.Message = msgBuilder.ToString();
            return result;
        }

        public static IEnumerable<PandocTaskResult> Run(string pandocExePath, IEnumerable<PandocTask> tasks)
        {
            if (pandocExePath == null) throw new ArgumentNullException("pandocExePath");
            if (tasks == null) throw new ArgumentNullException("tasks");

            var results = new List<PandocTaskResult>();

            foreach (var task in tasks)
            {
                var result = Run(pandocExePath, task);
                results.Add(result);
            }

            return results.ToArray();
        }

        public static PandocTaskResult Version(string pandocExePath)
        {
            if (pandocExePath == null) throw new ArgumentNullException("pandocExePath");

            var result = new PandocTaskResult();

            try
            {
                var startInfo = new ProcessStartInfo(pandocExePath, "--version");
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                startInfo.CreateNoWindow = true;

                var process = System.Diagnostics.Process.Start(startInfo);

                while (!process.HasExited) ;

                result.Message = process.StandardOutput.ReadLine();
            }
            catch (Exception e)
            {
                Debugger.Break();
                result.Exception = e;
                result.Failed = true;
            }

            return result;
        }
    }
}
