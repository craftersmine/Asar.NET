using System;
using System.Collections.Generic;
using System.Text;

namespace craftersmine.Asar.Net
{
    /// <summary>
    /// Represents an ASAR archive file collection
    /// </summary>
    public class AsarArchiveFileCollection : Dictionary<string, AsarArchiveFile>
    {
    }

    /// <summary>
    /// Contains static extension methods for <see cref="AsarArchiveFileCollection"/>
    /// </summary>
    public static class AsarArchiveFileCollectionExtensions
    {
        /// <summary>
        /// Converts sorted dictionary to <see cref="AsarArchiveFileCollection"/>
        /// </summary>
        /// <param name="dictionary">Sorted dictionary</param>
        /// <returns><see cref="AsarArchiveFileCollection"/> with sorted data</returns>
        public static AsarArchiveFileCollection ToAsarArchiveFileCollection(
            this SortedDictionary<string, AsarArchiveFile> dictionary)
        {
            AsarArchiveFileCollection collection = new AsarArchiveFileCollection();
            foreach (KeyValuePair<string, AsarArchiveFile> kvp in dictionary)
            {
                collection.Add(kvp.Key, kvp.Value);
            }

            return collection;
        }
    }
}
