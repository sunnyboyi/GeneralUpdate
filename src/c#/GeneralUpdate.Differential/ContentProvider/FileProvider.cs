﻿using GeneralUpdate.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeneralUpdate.Differential.ContentProvider
{
    public class FileProvider
    {
        private long _fileCount = 0;

        /// <summary>
        /// Compare two binary trees with different children.
        /// </summary>
        /// <param name="leftPath">Left tree folder path.</param>
        /// <param name="rightPath">Right tree folder path.</param>
        /// <returns></returns>
        public async Task<List<FileNode>> Compare(string leftPath, string rightPath)
        {
            return await Task.Run(() => 
            {
                var leftFilenodes = Read(leftPath);
                var rightFilenodes = Read(rightPath);
                var leftTree = new FileTree(leftFilenodes);
                var rightTree = new FileTree(rightFilenodes);
                List<FileNode> diffrentTreeNode = new List<FileNode>();
                leftTree.Compare(leftTree.GetRoot(), rightTree.GetRoot(), ref diffrentTreeNode);
                return diffrentTreeNode;
            });
        }

        /// <summary>
        /// Recursively read all files in the folder path.
        /// </summary>
        /// <param name="path">folder path.</param>
        /// <returns>different childrens.</returns>
        private IEnumerable<FileNode> Read(string path)
        {
            var resultFiles = new List<FileNode>();
            foreach (var subPath in Directory.GetFiles(path))
            {
                var md5 = FileUtil.GetFileMD5(subPath);
                var subFileInfo = new FileInfo(subPath);
                resultFiles.Add(new FileNode() { Id = GetId(), Path = path, Name = subFileInfo.Name, MD5 = md5 });
            }
            foreach (var subPath in Directory.GetDirectories(path))
            {
                resultFiles.AddRange(Read(subPath));
            }
            ResetId();
            return resultFiles;
        }

        private long GetId()=> Interlocked.Increment(ref _fileCount);

        private void ResetId()=> Interlocked.Exchange(ref _fileCount, 0);
    }
}
