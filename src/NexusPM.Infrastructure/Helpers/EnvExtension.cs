namespace NexusPM.Infrastructure.Helpers;

public static class EnvExtension
{
    public static string FindEnvFile()
    {
        var currentDir = Directory.GetCurrentDirectory();
        var searchDir = currentDir;

        for (int i = 0; i < 5; i++)
        {
            var envPath = Path.Combine(searchDir, ".env");
            if (File.Exists(envPath))
            {
                return envPath;
            }

            var parent = Directory.GetParent(searchDir);
            if (parent == null)
            {
                break;
            }

            searchDir = parent.FullName;
        }

        return Path.Combine(currentDir, ".env");
    }
}
