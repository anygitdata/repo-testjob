using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TestJob.Models.UserAPI
{
    public class BaseResult: IdentResult
    {        
        public string FileName { get; set; }
        public string Data { get; set; }
        public Guid Id { get; set; } 
        
    }

    public class BodyRequest : BaseResult
    {
        /// <summary>
        /// true into DataBase else into FileName
        /// </summary>
        public bool CommentType { get; set; } = true;
        
    }

    public partial class UserMix
    {
        public static string Enc_GetStrFromBytes(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            // Encoding.Unicode.GetString(bytes, 0, bytes.Length);
        }

        public static byte[] Enc_GetBytesFromStr(string str)
        {
            return Encoding.UTF8.GetBytes(str);
                // Encoding.Unicode.GetBytes(str) ;
        }


        #region File operations

        public static bool FileExists(string pathTxt,  string file)
        {
            string fullPath = Path.Combine(pathTxt, file);
            return FileExists(fullPath);
        }


        public static bool FileExists(string path)
        {
            if (File.Exists(path))
                return true;
            else
                return false;
        }


        //public static List<FileModel> FilesDescr(string pathTxt)
        //{
        //    string[] filesPaths = Directory.GetFiles(pathTxt);

        //    List<FileModel> files = new List<FileModel>();
        //    foreach (string filePath in filesPaths)
        //    {
        //        files.Add(new FileModel { FileName = Path.GetFileName(filePath) });
        //    }

        //    return files;
        //}
        

        /// <summary>
        /// if not exists -> createFile
        /// </summary>
        /// <param name="pathDir_txt"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static BodyRequest Filedownload_ifNotExists(string pathDir_txt, string file, string data)
        {
            BodyRequest res = new () { FileName = file };

            if (!FileExists(pathDir_txt, file))
            {
                res.Data = "...";
                FileCreate(pathDir_txt, res);
            }
            else
            {
                if (!string.IsNullOrEmpty(data))
                {
                    res.Data = data;
                    FileUpdate(pathDir_txt, res);
                }
                else
                    res.Data = FileDownload(pathDir_txt, file);
            }

            return res;
        }


        /// <summary>
        /// return Full path for each file 
        /// </summary>
        /// <returns></returns>
        public static string[] File_GetFilesSQL(string PathDir_txt)
        {
            string pathDir = Path.Combine(PathDir_txt, @"..\", "sql");

            return Directory.GetFiles(pathDir);
        }


        public static void FileUpdate(string pathTxt, BaseResult model)
        {
            string file = Path.Combine(pathTxt, model.FileName);

            if (!File.Exists(file))
            {
                model.Result = BaseResult.Error;
                model.Message = "File not exists";

                return; 
            }

            using (StreamWriter sw = new (file, false, System.Text.Encoding.UTF8))
            {
                sw.Write(model.Data);
            }

            model.Data = File.ReadAllText(file);
            model.Result = BaseResult.Ok;
        }


        public static bool FileUpdate(string pathFile, string content)
        {
            if (!File.Exists(pathFile))
                return false;

            try
            {
                using (StreamWriter sw = new (pathFile, false, System.Text.Encoding.UTF8))
                {
                    sw.Write(content);
                }

                return true;
            }
            catch { return false; }

        }


        public static bool FileDelete(string fullPath)
        {
            bool res = false;

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                res = true;
            }

            return res;
        }


        public static bool FileDelete(string pathTxt, string fileName)
        {

            string filePath = Path.Combine(pathTxt, fileName);
            
            return FileDelete(filePath); 
        }


        public static string FileDownload(string pathTxt, string fileName)
        {
            string res = "";
            string path = Path.Combine(pathTxt, fileName);

            if (FileExists(path))
                res = File.ReadAllText(path);

            return res;
        }


        public static string FileDownload(string fullPathFile)
        {
            string res = "";            

            if (FileExists(fullPathFile))
                res = File.ReadAllText(fullPathFile);

            return res;
        }

        public static void FileCreate(string PathDir_txt, BodyRequest model)
        {
            string path = Path.Combine(PathDir_txt, model.FileName);

            using (StreamWriter writer = new (path))
            {
                writer.Write(model.Data);
            }

            model.Data = File.ReadAllText(path);
            model.Result = BaseResult.Ok;

        }


        /// <summary>
        /// Create file if exists -> delete
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool FileCreate(string fullPath, string content)
        {
            try
            {
                if (FileExists(fullPath))
                    FileDelete(fullPath);

                using (StreamWriter writer = new (fullPath))
                {
                    writer.Write(content);
                }

                return true;
            }
            catch { 
                return false; 
            }

        }


        #endregion
    }
}
