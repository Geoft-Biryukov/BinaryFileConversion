using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryFileToHexConvertion
{
    internal interface IMainView
    {
        string AskUserOpenFileName();
        void SetFileName(string name);
        void SetFileLength(long length);
        void SetMaxBytes(long length);
        void SetText(string text);
        void ShowWarningMessage(string message);
    }
}
