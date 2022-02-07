using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace EpicGamesMover
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string DefaultManifestFolder = @"C:\ProgramData\Epic\EpicGamesLauncher\Data\Manifests";

        private long _filesCopied;
        private long _bytesCopied;

        public MainWindow()
        {
            InitializeComponent();
            _filesCopied = 0;
            _bytesCopied = 0;

            ManifestDirectoryBox.Text = DefaultManifestFolder;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = NameBox.Text;
            string sourceDir = SourceBox.Text;
            string destinationDir = DestinationBox.Text;
            string manifestDirectory = ManifestDirectoryBox.Text;

            bool finished = false;
            _filesCopied = 0;
            _bytesCopied = 0;
            Task.Run(() =>
            {
                try
                {
                    UpdateStatus("Running...");

                    MoveFiles(name, sourceDir, destinationDir);
                    UpdateManifests(sourceDir, destinationDir, name, manifestDirectory);

                    UpdateStatus("Completed!");
                }
                catch (Exception ex)
                {
                    UpdateStatus($"An error occurred: {ex.Message}");
                }
                finished = true;
            });

            Task.Run(() =>
            {
                while (!finished)
                {
                    UpdateStatus($"Files copied: {_filesCopied} - {_bytesCopied / 1000000}MB");
                    Thread.Sleep(500);
                }
            });
        }

        private void UpdateStatus(string status)
        {
            Dispatcher.Invoke(() => { Status.Text = status; });
        }

        private void MoveFiles(string name, string sourceDir, string destinationDir)
        {
            if (!sourceDir.EndsWith("\\"))
            {
                sourceDir += "\\";
            }

            if (!destinationDir.EndsWith("\\"))
            {
                destinationDir += "\\";
            }

            sourceDir += name;
            destinationDir += name;

            DirectoryCopy(sourceDir, destinationDir, true);
            Directory.Delete(sourceDir, true);
        }

        private void UpdateManifests(string sourceDir, string targetDir, string name, string manifestDirectory)
        {
            string formattedSourceDir = sourceDir.Replace("\\", "\\\\");

            if(!formattedSourceDir.EndsWith("\\\\"))
            {
                formattedSourceDir += "\\\\";
            }

            string formattedTargetDir = targetDir.Replace("\\", "\\\\");

            if (!formattedTargetDir.EndsWith("\\\\"))
            {
                formattedTargetDir += "\\\\";
            }

            string toSearchFor = $"\"InstallLocation\": \"{formattedSourceDir}{name}\"";
            string[] relevantFiles = FindFilesContainingText(toSearchFor, manifestDirectory);

            foreach (string file in relevantFiles)
            { 
                FindReplaceInFile(file, formattedSourceDir, formattedTargetDir);
            }
        }

        private void FindReplaceInFile(string file, string sourceDir, string targetDir)
        {
            string contents = File.ReadAllText(file);

            contents = contents.Replace(sourceDir, targetDir);
            File.WriteAllText(file, contents);
        }

        private void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            var dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            Directory.CreateDirectory(destDirName);

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = System.IO.Path.Combine(destDirName, file.Name);
                file.CopyTo(tempPath, false);
                _filesCopied++;
                _bytesCopied += file.Length;
            }

            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string tempPath = System.IO.Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
                }
            }
        }

        private string[] FindFilesContainingText(string text, string dir)
        {
            if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(dir))
            {
                return new string[0];
            }

            string[] allFiles = Directory.GetFiles(dir);

            var result = new List<string>();

            foreach (string file in allFiles)
            {
                string content = File.ReadAllText(file);
                if (content.ToLower().Contains(text.ToLower()))
                {
                    result.Add(file);
                }
            }

            return result.ToArray();
        }
    }
}