using System.Collections.Generic;
using TEngine.Utils.Logging;

namespace TEngine.Core.AssetManagement.Loading
{
    internal class AssetParser
    {

        public static IEnumerable<string> LoadRawData(string path)
        {
            if (File.Exists(path)) 
            {
                long fileSize = new FileInfo(path).Length;

                // Determine the buffer size based on the file size
                int bufferSize = DetermineBufferSize(fileSize);

                using (StreamReader reader = new StreamReader(path, System.Text.Encoding.Default, true, bufferSize))
                {
                    string? line;
  
                    while ((line = reader.ReadLine()) != null)
                    {
                        yield return line;
                    }
                }
            }
            else
            {
                Logging.Critical($"File not found: {path}");
                throw new FileNotFoundException($"File not found: {path}");
            }
        }

        private static int DetermineBufferSize(long fileSize)
        {
            // Direct byte size comparisons
            if (fileSize < 1048576) // 1 MB
                return 4096; // 4 KB
            else if (fileSize < 5242880) // 5 MB
                return 8192; // 8 KB
            else if (fileSize < 10485760) // 10 MB
                return 16384; // 16 KB
            else
                return 32768; // 32 KB for larger files
        }
    }
}