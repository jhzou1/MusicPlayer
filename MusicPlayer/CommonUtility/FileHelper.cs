using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MusicPlayer
{
    /// <summary>
    /// 处理本地文件
    /// </summary>
public     class FileHelper
    {
        /// <summary>
        /// 将对象保存为Json文件
        /// </summary>
        /// <param name="objFile"></param>
        /// <param name="FileName"></param>
        /// <param name="SavePath"></param>
        public static void SaveFileAsJson(object objFile,string FileName,string SavePath)
        {
            //转成json
            string json = JsonConvert.SerializeObject(objFile, Formatting.Indented);

            //保存到文件
            using (FileStream fs = new FileStream(SavePath+FileName, FileMode.Create))
            {
                //写入
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(json);
                }

            }
        }
        /// <summary>
        /// 获取json文件数据
        /// </summary>
        /// <param name="path">这一Path是全路径的包括扩展名</param>
        /// <returns></returns>
        public static string GetJson(string path)
        {
            if (File.Exists(path))
            {
                using (FileStream fsRead = new FileStream(path, FileMode.Open))
                {
                    //读取加转换
                    int fsLen = (int)fsRead.Length;
                    byte[] heByte = new byte[fsLen];
                    int r = fsRead.Read(heByte, 0, heByte.Length);
                    return System.Text.Encoding.UTF8.GetString(heByte);
                }
            }
            else
                return "File does not exist!";
        }
    }
}
