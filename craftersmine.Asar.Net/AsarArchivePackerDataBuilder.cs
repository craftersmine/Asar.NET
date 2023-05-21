using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace craftersmine.Asar.Net
{
    /// <summary>
    /// Represents a builder of data for ASAR archive packer
    /// </summary>
    public class AsarArchivePackerDataBuilder
    {
        private AsarArchivePackerData Data { get; set; }
        private AsarArchivePackerDataBuilder Builder { get; set; }

        private AsarArchivePackerDataBuilder(string outputDir, string archiveName)
        {
            Builder = this;
            Data = new AsarArchivePackerData(outputDir, archiveName);
        }

        /// <inheritdoc cref="CreateBuilder(string,string,bool)"/>
        public static AsarArchivePackerDataBuilder CreateBuilder(string outputDir, string archiveName)
        {
            return CreateBuilder(outputDir, archiveName, true);
        }

        /// <summary>
        /// Creates a new archive packer data builder instance
        /// </summary>
        /// <returns><see cref="AsarArchivePackerDataBuilder"/> for creating ASAR archive</returns>
        /// <exception cref="ArgumentNullException">When output directory or archive name is null or empty</exception>
        /// <exception cref="DirectoryNotFoundException">When output directory is not found at specified location</exception>
        public static AsarArchivePackerDataBuilder CreateBuilder(string outputDir, string archiveName, bool createOutputDir)
        {
            if (string.IsNullOrWhiteSpace(outputDir))
                throw new ArgumentNullException(nameof(outputDir));
            if (string.IsNullOrWhiteSpace(archiveName))
                throw new ArgumentNullException(archiveName);

            if (!Directory.Exists(outputDir))
            {
                if (!createOutputDir)
                    throw new DirectoryNotFoundException("Unable to find archive output directory");

                Directory.CreateDirectory(outputDir);
            }
            
            AsarArchivePackerDataBuilder builder = new AsarArchivePackerDataBuilder(outputDir, archiveName);
            return builder;
        }
        
        /// <summary>
        /// Adds file to the archive
        /// </summary>
        /// <param name="filePath">Path to the file</param>
        /// <returns><see cref="AsarArchivePackerDataBuilder"/> for creating ASAR archive</returns>
        /// <exception cref="FileNotFoundException">When specified file is not found</exception>
        /// <exception cref="AsarException">When file with same name is already at the archive root</exception>
        public AsarArchivePackerDataBuilder AddFile(string filePath)
        {
            return AddFile(filePath, false, false);
        }
        
        /// <summary>
        /// Adds file to the archive
        /// </summary>
        /// <param name="fileInfo">File info of the file</param>
        /// <returns><see cref="AsarArchivePackerDataBuilder"/> for creating ASAR archive</returns>
        /// <exception cref="FileNotFoundException">When specified file is not found</exception>
        /// <exception cref="AsarException">When file with same name is already at the archive root</exception>
        public AsarArchivePackerDataBuilder AddFile(FileInfo fileInfo)
        {
            return AddFile(fileInfo, false, false);
        }

        /// <summary>
        /// Adds file to the archive
        /// </summary>
        /// <param name="file">Pending file for packing</param>
        /// <returns><see cref="AsarArchivePackerDataBuilder"/> for creating ASAR archive</returns>
        /// <exception cref="FileNotFoundException">When specified file is not found</exception>
        /// <exception cref="AsarException">When file with same name is already at the archive root</exception>
        public AsarArchivePackerDataBuilder AddFile(AsarArchivePendingFile file)
        {
            if (!(Data.PendingFiles.FirstOrDefault(f => f.FileInfo.Name == file.FileInfo.Name) is null))
                throw new AsarException(string.Format("File with name \"{0}\" is already at the archive root", file.FileInfo.Name));
            Data.PendingFilesInternal.Add(file);
            return Builder;
        }
        
        /// <summary>
        /// Adds file to the archive
        /// </summary>
        /// <param name="filePath">Path to the file</param>
        /// <param name="unpacked">Determines whether this file needs to be packed into ASAR file or left unpacked besides archive file</param>
        /// <returns><see cref="AsarArchivePackerDataBuilder"/> for creating ASAR archive</returns>
        /// <exception cref="FileNotFoundException">When specified file is not found</exception>
        /// <exception cref="AsarException">When file with same name is already at the archive root</exception>
        public AsarArchivePackerDataBuilder AddFile(string filePath, bool unpacked)
        {
            FileInfo file = new FileInfo(filePath);
            return AddFile(file, unpacked, false);
        }
        
        /// <summary>
        /// Adds file to the archive
        /// </summary>
        /// <param name="fileInfo">File info of the file</param>
        /// <param name="unpacked">Determines whether this file needs to be packed into ASAR file or left unpacked besides archive file</param>
        /// <returns><see cref="AsarArchivePackerDataBuilder"/> for creating ASAR archive</returns>
        /// <exception cref="FileNotFoundException">When specified file is not found</exception>
        /// <exception cref="AsarException">When file with same name is already at the archive root</exception>
        public AsarArchivePackerDataBuilder AddFile(FileInfo fileInfo, bool unpacked)
        {
            return AddFile(fileInfo, unpacked, false);
        }
        
        /// <summary>
        /// Adds file to the archive
        /// </summary>
        /// <param name="filePath">Path to the file</param>
        /// <param name="unpacked">Determines whether this file needs to be packed into ASAR file or left unpacked besides archive file</param>
        /// <param name="calculateIntegrity">Determines whether integrity needs to be calculated for this file before packing</param>
        /// <returns><see cref="AsarArchivePackerDataBuilder"/> for creating ASAR archive</returns>
        /// <exception cref="FileNotFoundException">When specified file is not found</exception>
        /// <exception cref="AsarException">When file with same name is already at the archive root</exception>
        public AsarArchivePackerDataBuilder AddFile(string filePath, bool unpacked, bool calculateIntegrity)
        {
            FileInfo file = new FileInfo(filePath);
            return AddFile(file, unpacked, calculateIntegrity);
        }
        
        /// <summary>
        /// Adds file to the archive
        /// </summary>
        /// <param name="fileInfo">File info of the file</param>
        /// <param name="unpacked">Determines whether this file needs to be packed into ASAR file or left unpacked besides archive file</param>
        /// <param name="calculateIntegrity">Determines whether integrity needs to be calculated for this file before packing</param>
        /// <returns><see cref="AsarArchivePackerDataBuilder"/> for creating ASAR archive</returns>
        /// <exception cref="FileNotFoundException">When specified file is not found</exception>
        /// <exception cref="AsarException">When file with same name is already at the archive root</exception>
        public AsarArchivePackerDataBuilder AddFile(FileInfo fileInfo, bool unpacked, bool calculateIntegrity)
        {
            if (!fileInfo.Exists)
                throw new FileNotFoundException("File not found at specified location", fileInfo.FullName);
            if (!(Data.PendingFiles.FirstOrDefault(f => f.FileInfo.Name == fileInfo.Name) is null))
                throw new AsarException(string.Format("File with name \"{0}\" is already at the archive root", fileInfo.Name));
            AsarArchivePendingFile pendingFile = new AsarArchivePendingFile(fileInfo);
            pendingFile.IsUnpacked = unpacked;
            pendingFile.CalculateIntegrity = calculateIntegrity;
            Data.PendingFilesInternal.Add(pendingFile);
            return Builder;
        }

        /// <summary>
        /// Adds files to the archive
        /// </summary>
        /// <param name="filePaths">File paths of the files</param>
        /// <returns><see cref="AsarArchivePackerDataBuilder"/> for creating ASAR archive</returns>
        /// <exception cref="FileNotFoundException">When specified file in array is not found</exception>
        /// <exception cref="AsarException">When file with same name is already at the archive root</exception>
        public AsarArchivePackerDataBuilder AddFiles(params string[] filePaths)
        {
            return AddFiles(false, filePaths);
        }
        
        /// <summary>
        /// Adds files to the archive
        /// </summary>
        /// <param name="fileInfos">File infos of the files</param>
        /// <returns><see cref="AsarArchivePackerDataBuilder"/> for creating ASAR archive</returns>
        /// <exception cref="FileNotFoundException">When specified file in array is not found</exception>
        /// <exception cref="AsarException">When file with same name is already at the archive root</exception>
        public AsarArchivePackerDataBuilder AddFiles(params FileInfo[] fileInfos)
        {
            return AddFiles(false, fileInfos);
        }

        /// <summary>
        /// Adds files to the archive
        /// </summary>
        /// <param name="files">Pending files for packing</param>
        /// <returns><see cref="AsarArchivePackerDataBuilder"/> for creating ASAR archive</returns>
        /// <exception cref="FileNotFoundException">When specified file in array is not found</exception>
        /// <exception cref="AsarException">When file with same name is already at the archive root</exception>
        public AsarArchivePackerDataBuilder AddFiles(params AsarArchivePendingFile[] files)
        {
            foreach (AsarArchivePendingFile file in files)
            {
                if (!(Data.PendingFiles.FirstOrDefault(f => f.FileInfo.Name == file.FileInfo.Name) is null))
                    throw new AsarException(string.Format("File with name \"{0}\" is already at the archive root",
                        file.FileInfo.Name));
            }
            Data.PendingFilesInternal.AddRange(files);
            return Builder;
        }
        
        /// <summary>
        /// Adds files to the archive
        /// </summary>
        /// <param name="filePaths">File paths of the files</param>
        /// <param name="unpacked">Determines whether this files needs to be packed into ASAR file or left unpacked besides archive file</param>
        /// <param name="calculateIntegrity">Determines whether integrity needs to be calculated for this file before packing</param>
        /// <returns><see cref="AsarArchivePackerDataBuilder"/> for creating ASAR archive</returns>
        /// <exception cref="FileNotFoundException">When specified file in array is not found</exception>
        /// <exception cref="AsarException">When file with same name is already at the archive root</exception>
        public AsarArchivePackerDataBuilder AddFiles(bool unpacked, bool calculateIntegrity, params string[] filePaths)
        {
            foreach (string filePath in filePaths)
            {
                AddFile(filePath, unpacked, calculateIntegrity);
            }

            return Builder;
        }
        
        /// <summary>
        /// Adds files to the archive
        /// </summary>
        /// <param name="fileInfos">File infos of the files</param>
        /// <param name="unpacked">Determines whether this files needs to be packed into ASAR file or left unpacked besides archive file</param>
        /// <param name="calculateIntegrity">Determines whether integrity needs to be calculated for this file before packing</param>
        /// <returns><see cref="AsarArchivePackerDataBuilder"/> for creating ASAR archive</returns>
        /// <exception cref="FileNotFoundException">When specified file in array is not found</exception>
        /// <exception cref="AsarException">When file with same name is already at the archive root</exception>
        public AsarArchivePackerDataBuilder AddFiles(bool unpacked, bool calculateIntegrity, params FileInfo[] fileInfos)
        {
            foreach (FileInfo fileInfo in fileInfos)
            {
                AddFile(fileInfo, unpacked, calculateIntegrity);
            }

            return Builder;
        }

        /// <summary>
        /// Adds files to the archive
        /// </summary>
        /// <param name="filePaths">File paths of the files</param>
        /// <param name="unpacked">Determines whether this files needs to be packed into ASAR file or left unpacked besides archive file</param>
        /// <returns><see cref="AsarArchivePackerDataBuilder"/> for creating ASAR archive</returns>
        /// <exception cref="FileNotFoundException">When specified file in array is not found</exception>
        /// <exception cref="AsarException">When file with same name is already at the archive root</exception>
        public AsarArchivePackerDataBuilder AddFiles(bool unpacked, params string[] filePaths)
        {
            return AddFiles(unpacked, false, filePaths);
        }
        
        /// <summary>
        /// Adds files to the archive
        /// </summary>
        /// <param name="fileInfos">File infos of the files</param>
        /// <param name="unpacked">Determines whether this files needs to be packed into ASAR file or left unpacked besides archive file</param>
        /// <returns><see cref="AsarArchivePackerDataBuilder"/> for creating ASAR archive</returns>
        /// <exception cref="FileNotFoundException">When specified file in array is not found</exception>
        /// <exception cref="AsarException">When file with same name is already at the archive root</exception>
        public AsarArchivePackerDataBuilder AddFiles(bool unpacked, params FileInfo[] fileInfos)
        {
            return AddFiles(unpacked, false, fileInfos);
        }

        /// <summary>
        /// Adds directory to the archive
        /// </summary>
        /// <param name="directoryInfo">Directory info</param>
        /// <returns><see cref="AsarArchivePackerDataBuilder"/> for creating ASAR archive</returns>
        /// <exception cref="DirectoryNotFoundException">When specified file in array is not found</exception>
        /// <exception cref="AsarException">When directory with same name is already at the archive root</exception>
        public AsarArchivePackerDataBuilder AddDirectory(DirectoryInfo directoryInfo)
        {
            return AddDirectory(directoryInfo, false);
        }
        
        /// <summary>
        /// Adds directory to the archive
        /// </summary>
        /// <param name="directoryPath">Directory path</param>
        /// <returns><see cref="AsarArchivePackerDataBuilder"/> for creating ASAR archive</returns>
        /// <exception cref="DirectoryNotFoundException">When specified file in array is not found</exception>
        /// <exception cref="AsarException">When directory with same name is already at the archive root</exception>
        public AsarArchivePackerDataBuilder AddDirectory(string directoryPath)
        {
            return AddDirectory(directoryPath, false);
        }
        
        /// <summary>
        /// Adds directory to the archive
        /// </summary>
        /// <param name="directoryInfo">Directory info</param>
        /// <param name="unpacked"><see langword="true"/> if directory needs to remain unpacked</param>
        /// <returns><see cref="AsarArchivePackerDataBuilder"/> for creating ASAR archive</returns>
        /// <exception cref="DirectoryNotFoundException">When specified file in array is not found</exception>
        /// <exception cref="AsarException">When directory with same name is already at the archive root</exception>
        public AsarArchivePackerDataBuilder AddDirectory(DirectoryInfo directoryInfo, bool unpacked)
        {
            return AddDirectory(directoryInfo, unpacked, false);
        }
        
        /// <summary>
        /// Adds directory to the archive
        /// </summary>
        /// <param name="directoryPath">Directory path</param>
        /// <param name="unpacked"><see langword="true"/> if directory needs to remain unpacked</param>
        /// <returns><see cref="AsarArchivePackerDataBuilder"/> for creating ASAR archive</returns>
        /// <exception cref="DirectoryNotFoundException">When specified file in array is not found</exception>
        /// <exception cref="AsarException">When directory with same name is already at the archive root</exception>
        public AsarArchivePackerDataBuilder AddDirectory(string directoryPath, bool unpacked)
        {
            return AddDirectory(directoryPath, unpacked, false);
        }
        
        /// <summary>
        /// Adds directory to the archive
        /// </summary>
        /// <param name="directoryInfo">Directory info</param>
        /// <param name="unpacked"><see langword="true"/> if directory needs to remain unpacked</param>
        /// <param name="calculateIntegrity"><see langword="true"/> if integrity needs to be calculated for directory</param>
        /// <returns><see cref="AsarArchivePackerDataBuilder"/> for creating ASAR archive</returns>
        /// <exception cref="DirectoryNotFoundException">When specified file in array is not found</exception>
        /// <exception cref="AsarException">When directory with same name is already at the archive root</exception>
        public AsarArchivePackerDataBuilder AddDirectory(DirectoryInfo directoryInfo, bool unpacked,
            bool calculateIntegrity)
        {
            if (!directoryInfo.Exists)
                throw new DirectoryNotFoundException("Directory \"" + directoryInfo.FullName + "\" not found");

            if (!(Data.PendingDirectories.FirstOrDefault(d => d.DirectoryInfo.Name == directoryInfo.Name) is null))
                throw new AsarException(string.Format("Directory with name \"{0}\" is already at the archive root",
                    directoryInfo.Name));

            AsarArchivePendingDirectory directory = new AsarArchivePendingDirectory(directoryInfo, unpacked, calculateIntegrity);
            Data.PendingDirectoriesInternal.Add(directory);
            return Builder;
        }
        
        /// <summary>
        /// Adds directory to the archive
        /// </summary>
        /// <param name="directoryPath">Directory path</param>
        /// <param name="unpacked"><see langword="true"/> if directory needs to remain unpacked</param>
        /// <param name="calculateIntegrity"><see langword="true"/> if integrity needs to be calculated for directory</param>
        /// <returns><see cref="AsarArchivePackerDataBuilder"/> for creating ASAR archive</returns>
        /// <exception cref="DirectoryNotFoundException">When specified file in array is not found</exception>
        /// <exception cref="AsarException">When directory with same name is already at the archive root</exception>
        public AsarArchivePackerDataBuilder AddDirectory(string directoryPath, bool unpacked, bool calculateIntegrity)
        {
            DirectoryInfo directory = new DirectoryInfo(directoryPath);
            return AddDirectory(directory, unpacked, calculateIntegrity);
        }
        
        /// <summary>
        /// Adds directory to the archive
        /// </summary>
        /// <param name="directory">Directory information to add</param>
        /// <returns><see cref="AsarArchivePackerDataBuilder"/> for creating ASAR archive</returns>
        /// <exception cref="DirectoryNotFoundException">When specified file in array is not found</exception>
        /// <exception cref="AsarException">When directory with same name is already at the archive root</exception>
        public AsarArchivePackerDataBuilder AddDirectory(AsarArchivePendingDirectory directory)
        {
            if (!(Data.PendingDirectories.FirstOrDefault(d => d.DirectoryInfo.Name == directory.DirectoryInfo.Name) is null))
                throw new AsarException(string.Format("Directory with name \"{0}\" is already at the archive root",
                    directory.DirectoryInfo.Name));

            Data.PendingDirectoriesInternal.Add(directory);
            return Builder;
        }

        /// <summary>
        /// Removes file from pending files
        /// </summary>
        /// <param name="filePath">Path to file to remove</param>
        /// <returns><see cref="AsarArchivePackerDataBuilder"/> for creating ASAR archive</returns>
        public AsarArchivePackerDataBuilder RemoveFile(string filePath)
        {
            Data.PendingFilesInternal.Remove(Data.PendingFilesInternal.FirstOrDefault(f => f.FilePath == filePath));
            return Builder;
        }
        
        /// <summary>
        /// Removes file from pending files
        /// </summary>
        /// <param name="fileInfo">File info to remove</param>
        /// <returns><see cref="AsarArchivePackerDataBuilder"/> for creating ASAR archive</returns>
        public AsarArchivePackerDataBuilder RemoveFile(FileInfo fileInfo)
        {
            Data.PendingFilesInternal.Remove(Data.PendingFilesInternal.FirstOrDefault(f => f.FileInfo == fileInfo));
            return Builder;
        }
        
        /// <summary>
        /// Removes file from pending files
        /// </summary>
        /// <param name="file">Pending file to remove</param>
        /// <returns><see cref="AsarArchivePackerDataBuilder"/> for creating ASAR archive</returns>
        public AsarArchivePackerDataBuilder RemoveFile(AsarArchivePendingFile file)
        {
            Data.PendingFilesInternal.Remove(file);
            return Builder;
        }
        
        /// <summary>
        /// Removes files from pending files
        /// </summary>
        /// <param name="filePaths">Path to file to remove</param>
        /// <returns><see cref="AsarArchivePackerDataBuilder"/> for creating ASAR archive</returns>
        public AsarArchivePackerDataBuilder RemoveFiles(params string[] filePaths)
        {
            foreach (string path in filePaths)
            {
                Data.PendingFilesInternal.Remove(Data.PendingFilesInternal.FirstOrDefault(f => f.FilePath == path));
            }
            
            return Builder;
        }
        
        /// <summary>
        /// Removes files from pending files
        /// </summary>
        /// <param name="fileInfos">File infos to remove</param>
        /// <returns><see cref="AsarArchivePackerDataBuilder"/> for creating ASAR archive</returns>
        public AsarArchivePackerDataBuilder RemoveFiles(params FileInfo[] fileInfos)
        {
            foreach (FileInfo fileInfo in fileInfos)
            {
                Data.PendingFilesInternal.Remove(Data.PendingFilesInternal.FirstOrDefault(f => f.FileInfo == fileInfo));
            }
            
            return Builder;
        }
        
        /// <summary>
        /// Removes files from pending files
        /// </summary>
        /// <param name="files">Pending files to remove</param>
        /// <returns><see cref="AsarArchivePackerDataBuilder"/> for creating ASAR archive</returns>
        public AsarArchivePackerDataBuilder RemoveFiles(params AsarArchivePendingFile[] files)
        {
            foreach (AsarArchivePendingFile file in files)
            {
                Data.PendingFilesInternal.Remove(file);
            }
            
            return Builder;
        }

        /// <summary>
        /// Removes directory from pending directories
        /// </summary>
        /// <param name="directoryPath">Directory path to remove</param>
        /// <returns><see cref="AsarArchivePackerDataBuilder"/> for creating ASAR archive</returns>
        public AsarArchivePackerDataBuilder RemoveDirectory(string directoryPath)
        {
            Data.PendingDirectoriesInternal.Remove(
                Data.PendingDirectoriesInternal.FirstOrDefault(d => d.DirectoryPath == directoryPath));
            return Builder;
        }
        
        /// <summary>
        /// Removes directory from pending directories
        /// </summary>
        /// <param name="directoryInfo">Directory info to remove</param>
        /// <returns><see cref="AsarArchivePackerDataBuilder"/> for creating ASAR archive</returns>
        public AsarArchivePackerDataBuilder RemoveDirectory(DirectoryInfo directoryInfo)
        {
            Data.PendingDirectoriesInternal.Remove(
                Data.PendingDirectoriesInternal.FirstOrDefault(d => d.DirectoryInfo == directoryInfo));
            return Builder;
        }
        
        /// <summary>
        /// Removes directory from pending directories
        /// </summary>
        /// <param name="directory">Pending directory to remove</param>
        /// <returns><see cref="AsarArchivePackerDataBuilder"/> for creating ASAR archive</returns>
        public AsarArchivePackerDataBuilder RemoveDirectory(AsarArchivePendingDirectory directory)
        {
            Data.PendingDirectoriesInternal.Remove(directory);
            return Builder;
        }

        /// <summary>
        /// Sets if files need to be sorted before packing
        /// </summary>
        /// <param name="sort"><see langword="true"/> if sorting is needed</param>
        /// <returns><see cref="AsarArchivePackerDataBuilder"/> for creating ASAR archive</returns>
        public AsarArchivePackerDataBuilder PerformFileSort(bool sort)
        {
            Data.PerformSort = sort;
            return Builder;
        }

        /// <summary>
        /// Creates an ASAR archive data for packer
        /// </summary>
        /// <returns><see cref="AsarArchivePackerData"/> that contains data for packing into ASAR archive</returns>
        public AsarArchivePackerData CreateArchiveData()
        {
            return Data;
        }
    }
}
