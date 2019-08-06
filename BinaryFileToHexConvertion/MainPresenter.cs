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
        private readonly ReadBytesOptions options;
        private FileStream processingFile = null;
        private byte[] readBytes = null;

        public MainPresenter(IMainView view, ReadBytesOptions options)
        {
            this.view = view ?? throw new ArgumentNullException(nameof(view));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
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

        internal void ReadBytes()
        {
            if (processingFile == null)
            {
                view.ShowWarningMessage("Please choose file!");
                return;
            }
            var offset = options.Offset;
            var count = options.BytesToRead;            

            readBytes = new byte[count];
            processingFile.Seek(offset, SeekOrigin.Begin);
            processingFile.Read(readBytes, 0, (int)count);

            var text = GetFormatedText(readBytes, options.BytesInRow);
            
            view.SetText(text);
        }

        
        private static string GetFormatedText(byte[] readBytes, int countInRow)
        {
            var builder = new StringBuilder();
            int itemsCounter = 0;            
            foreach (var item in readBytes)
            {
                builder.AppendFormat("0x{0:x2}", item);
                builder.Append(", ");

                if(++itemsCounter % countInRow == 0)
                {
                    builder.Append(Environment.NewLine);                    
                }
            }
            builder.Remove(builder.Length - 2, 2);

            return builder.ToString();
        }

        public void Dispose()
        {
            processingFile?.Close();
        }        
    }
}
