using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using LibGit2Sharp;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
// [assembly: Repositry(typeof(LibGit2Sharp.Repository))]
namespace AWSLambda1
{
    public class Function
    {
        
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string FunctionHandler(object input, ILambdaContext context)
        {
            try
            {
                Repository repo = new Repository();

                string path = "https://github.com/milky5/milky5.github.io.git";

                // \\Mac\Home\Documents\test
                string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "test");
                Console.WriteLine(folderPath);

                CloneOptions options = new CloneOptions();
                options.BranchName = "master";

                Repository.Clone(path, folderPath, options);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error: {ex}");
                throw;
            }

            context.Logger.LogLine($"Arg : [{input}]");
            return "OK";
        }
    }
}
