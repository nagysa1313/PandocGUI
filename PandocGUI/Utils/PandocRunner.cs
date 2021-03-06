﻿using PandocGUI.Model;
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
        public static string BuildArguments(string sourceFile, string targetFile, PandocTaskConfig config)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(string.Format("{0} -f {1} -t {3} -s -o {2}"
                        , sourceFile
                        , PandocFileExtension.Extensions[Path.GetExtension(sourceFile)]
                        , targetFile
                        , PandocFileExtension.Extensions[Path.GetExtension(targetFile)]
                        ));

            if (config.ParseRaw) sb.Append(" -R");
            if (config.Normalize) sb.Append(" --normalize");
            if (config.PreserveTabs) sb.Append(" -p");
            if (config.TabStop != 4) sb.Append(" --tab-stop=" + config.TabStop);

            return sb.ToString();
        }

        public static IEnumerable<PandocTaskResult> Run(string pandocExePath, PandocTask task, Action<PandocTask, PandocTaskResult> callback = null)
        {
            if (pandocExePath == null) throw new ArgumentNullException("pandocExePath");
            if (task == null) throw new ArgumentNullException("task");

            var results = new List<PandocTaskResult>();
            var result = new PandocTaskResult();
            var msgBuilder = new StringBuilder(task.SourceFile + "\n");

            if (task.TargetFiles.Count < 1) throw new InvalidOperationException("At least 1 target file needed for the task!");

            foreach (var targetFile in task.TargetFiles)
            {
                var actResult = new PandocTaskResult() { OutputFile = targetFile.Path };
                result.OutputFile = Path.GetFileName(targetFile.Path);
                msgBuilder.Append(result.OutputFile);

                try
                {
                    var args = BuildArguments(task.SourceFile, targetFile.Path, task.Config);

                    var startInfo = new ProcessStartInfo(pandocExePath, args);
                    startInfo.UseShellExecute = false;
                    startInfo.RedirectStandardOutput = true;
                    startInfo.CreateNoWindow = true;

                    var process = System.Diagnostics.Process.Start(startInfo);

                    while (!process.HasExited) ;

                    msgBuilder.AppendLine(" successful.");
                    actResult.Message = "Successful";
                    actResult.Failed = false;
                }
                catch (Exception e)
                {
                    Debugger.Break();
                    msgBuilder.AppendLine(" failed with: ");
                    msgBuilder.AppendLine(e.Message);
                    result.Exception = e;
                    result.Failed = true;
                    actResult.Message = "Failed with " + e.Message;
                    actResult.Exception = e;
                    actResult.Failed = true;
                }

                results.Add(actResult);
                if (callback != null) callback(task, actResult);
            }

            result.Message = msgBuilder.ToString();
            if (callback != null) callback(task, result);
            results.Add(result);
            return results;
        }

        public static IEnumerable<PandocTaskResult> Run(string pandocExePath, IEnumerable<PandocTask> tasks, Action<PandocTask, PandocTaskResult> callback = null)
        {
            if (pandocExePath == null) throw new ArgumentNullException("pandocExePath");
            if (tasks == null) throw new ArgumentNullException("tasks");

            var results = new List<PandocTaskResult>();

            foreach (var task in tasks)
            {
                var result = Run(pandocExePath, task, callback);
                results.AddRange(result);
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
