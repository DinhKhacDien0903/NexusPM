// <copyright file="EnvExtension.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NexusPM.Infrastructure.Helpers;

/// <summary>
/// Provides extension methods for working with environment files (.env).
/// </summary>
public static class EnvExtension
{
    /// <summary>
    /// Searches for a .env file starting from the current directory and moving up the directory tree.
    /// </summary>
    /// <returns>The path to the .env file if found; otherwise, an empty string.</returns>
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
