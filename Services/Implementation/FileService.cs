using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Services.Implementation
{
    public class FileService : IFileService
    {
        public bool SaveImageFile(string image, out string ResponseMsg)
        {
            string _path = @"D:/OCR-School/Services/Images";
            
           
           
            
            try
            {
                //using(FileStream fParameter = new FileStream(dirParameter, FileMode.Create, FileAccess.Write))
                //{
                    using (StreamWriter m_WriterParameter = new StreamWriter(path: _path))
                    
                {
                        // m_WriterParameter.BaseStream.Seek(0, SeekOrigin.End);
                        m_WriterParameter.Write(image);
                        
                        StreamReader s_reader = new StreamReader(image);
                        var fileStream = File.Create(path:_path);
                        s_reader.BaseStream.Seek(0, SeekOrigin.Begin);
                        s_reader.BaseStream.CopyTo(fileStream);
                        s_reader.Dispose();

                        m_WriterParameter.Flush();
                        m_WriterParameter.Close();
                    }
                //}
                
                

                ResponseMsg = "Successful";
                return true;
            }
            catch(Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                    ResponseMsg = ex.Message;
                else
                    ResponseMsg = ex.InnerException.Message;
                return false;
            }
        }
    }
}
