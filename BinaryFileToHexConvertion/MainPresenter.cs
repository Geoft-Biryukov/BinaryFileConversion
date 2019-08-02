using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryFileToHexConvertion
{
    internal class MainPresenter : IDisposable
    {
        private readonly IMainView view;
        private FileStream processingFile = null;
        private byte[] readBytes = null;

        public MainPresenter(IMainView view)
        {
            this.view = view ?? throw new ArgumentNullException(nameof(view));
        }      

        internal void OpenFile()
        {
            var fileName = view.AskUserOpenFileName();

            if(fileName == null || !File.Exists(fileName))            
                return;
            
            processingFile?.Close();
            processingFile = new FileStream(fileName, FileMode.Open);

            view.SetFileName(processingFile.Name);
            view.SetFileLength(processingFile.Length);
            view.SetMaxBytes(processingFile.Length);
        }

        internal void ReadBytes(long offset, long count)
        {
            if (processingFile == null)
                view.ShowWarningMessage("Please choose file!");

            readBytes = new byte[count];
            processingFile.Seek(offset, SeekOrigin.Begin);
            processingFile.Read(readBytes, 0, (int)count);

            var text = GetFormatedText(readBytes);
            
            view.SetText(text);
        }

        private static string GetFormatedText(byte[] readBytes)
        {
            var builder = new StringBuilder();
            foreach (var item in readBytes)
            {
                builder.AppendFormat("0x{0:x2}", item);
                builder.Append(", ");
            }
            builder.Remove(builder.Length - 3, 2);

            return builder.ToString();
        }

        public void Dispose()
        {
            processingFile?.Close();
        }

       
    }
}
