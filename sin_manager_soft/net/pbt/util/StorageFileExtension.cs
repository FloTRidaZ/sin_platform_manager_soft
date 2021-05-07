using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace sin_manager_soft.net.pbt.util
{
    public static class StorageFileExtension
    {
        public static async Task<byte[]> GetBytes(StorageFile file)
        {
            IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read);
            DataReader dataReader = new DataReader(stream.GetInputStreamAt(0));
            byte[] bytes = new byte[stream.Size];
            await dataReader.LoadAsync((uint) stream.Size);
            dataReader.ReadBytes(bytes);
            return bytes;
        }
    }
}