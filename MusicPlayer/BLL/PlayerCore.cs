﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;

namespace MusicPlayer
{
    /// <summary>
    /// 喜科堂互联教育网址：xiketang.ke.qq.com
    /// 原创设计：常慧勇    QQ：2934008878
    /// 版权声明：版权所有，侵权必究。本项目源码是开放式设计，不能用于任何商业用途！
    /// 如有更改，请标注哪些更改！
    /// 本课程地址：https://ke.qq.com/course/248962
    /// </summary>
    public class PlayerCore : IPlayerCore
    {
        public PlayerCore()
        {
            this.FileList = new Dictionary<string, PlayedFile>();
        }

        /// <summary>
        /// 文件列表属性
        /// </summary>
        public Dictionary<string, PlayedFile> FileList { get; set; }

        /// <summary>
        /// 播放模式
        /// </summary>
        public string PlayMode { get; set; }


        #region 播放列表的保存与读取

        /// <summary>
        /// 保存最新的播放列表到文件
        /// </summary>
        public void SaveNewList()
        {
            //FileStream fs = new FileStream("newList.obj", FileMode.Create);
            //BinaryFormatter bf = new BinaryFormatter();
            //bf.Serialize(fs, this.FileList);
            //fs.Close();

            FileHelper.SaveFileAsJson(this.FileList, "newList.json", "");
        }

        /// <summary>
        /// 从文件中读取最新播放列表
        /// </summary>
        /// <returns>返回读取是否成功</returns>
        public bool LoadNewList()
        {
            #region 方法1
            //if (File.Exists("newList.obj"))
            //{
            //    //【1】读取上次的播放列表
            //    FileStream fs = new FileStream("newList.obj", FileMode.Open);
            //    BinaryFormatter bf = new BinaryFormatter();
            //    Dictionary<string, PlayedFile> vList = (Dictionary<string, PlayedFile>)bf.Deserialize(fs);
            //    fs.Close();

            //    //【2】判断这些文件是否还存在（将已经存在的添加到集合）
            //    foreach (PlayedFile item in vList.Values)
            //    {
            //        if (File.Exists(item.FilePath))
            //        {
            //            //如果重新加载回去的是带后缀名的话那就和第一次加的时候不匹配，第一次加的是不带后缀名的.
            //            string file_Name = item.FileName.Substring(0, item.FileName.LastIndexOf("."));//没有扩展名的文件名，作为集合的Key使用

            //            this.FileList.Add(file_Name, item);
            //        }
            //    }

            //    //【3】更新列表
            //    if (this.FileList.Count == 0)//如果文件都没有了，则删除整个列表的保存
            //    {
            //        File.Delete("newList.obj");
            //        return false;
            //    }
            //    else
            //    {
            //        SaveNewList();//重新保存最新的列表
            //        return true;
            //    }
            //}
            //else return false;

            #endregion

            string fileInfo = FileHelper.GetJson("newList.json");
            if (fileInfo != "File does not exist!")
            {
                //【1】读取上次的播放列表
                var vList = JsonConvert.DeserializeObject<Dictionary<string, PlayedFile>>(fileInfo);
                //【2】判断这些文件是否还存在（将已经存在的添加到集合）
                foreach (PlayedFile item in vList.Values)
                {
                    if (File.Exists(item.FilePath))
                    {
                        //如果重新加载回去的是带后缀名的话那就和第一次加的时候不匹配，第一次加的是不带后缀名的.
                        string file_Name = item.FileName.Substring(0, item.FileName.LastIndexOf("."));//没有扩展名的文件名，作为集合的Key使用

                        this.FileList.Add(file_Name, item);
                    }
                }
                //【3】更新列表
                if (this.FileList.Count == 0)//如果文件都没有了，则删除整个列表的保存
                {
                    File.Delete("newList.obj");
                    return false;
                }
                else
                {
                    SaveNewList();//重新保存最新的列表
                    return true;
                }
            }
            else
                return false;

        }
        #endregion

        #region  上次播放位置的保存与读取

        /// <summary>
        /// 保存播放位置：我当前保存的是索引，如果某些文件被删除，则这个顺序肯定不对。应该记住文件名。
        /// </summary>
        /// <param name="index"></param>
        public void SavePlayIndex(int index)
        {
            FileStream fs = new FileStream("playIndex.obj", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(index);
            sw.Close();
            fs.Close();
        }
        /// <summary>
        /// 使用记住文件名方式保存上次播放位置
        /// </summary>
        public void SavSavePlayFileName(string fileName)
        {
            FileStream fs = new FileStream("playFileName.obj", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(fileName);
            sw.Close();
            fs.Close();
        }
        /// <summary>
        /// 这是保存上次播放进度
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="currentPosition"></param>
        public void SavSavePlayFileName(string fileName,double currentPosition)
        {
            CurrentPlay PlayFile = new CurrentPlay
            {
                FileName= fileName,
                CurrentPosition= currentPosition
            };

            FileHelper.SaveFileAsJson(PlayFile,"CurrentPlay.json","");
        }

        /// <summary>
        /// 读取上一次播放位置：如果记住的是文件名，或索引，如果这个文件不存在，则要考虑，否则出错...
        /// </summary>
        /// <returns></returns>
        public int ReadPlayIndex()
        {
            if (File.Exists("playIndex.obj"))
            {
                FileStream fs = new FileStream("playIndex.obj", FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string index = sr.ReadToEnd().Trim();
                sr.Close();
                fs.Close();
                return Convert.ToInt32(index);
            }
            else return -1;
        }
        /// <summary>
        /// 读取上次播放的文件名
        /// </summary>
        /// <returns></returns>
        public string ReadPlayFileName()
        {
            ReadPlayFile();

            if (File.Exists("playFileName.obj"))
            {
                FileStream fs = new FileStream("playFileName.obj", FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string lastFileName = sr.ReadToEnd().Trim();
                sr.Close();
                fs.Close();
                return lastFileName;
            }
            else return "";
        }

        public CurrentPlay ReadPlayFile()
        {
            string fileInfo=FileHelper.GetJson("CurrentPlay.json");

            var jArray = JsonConvert.DeserializeObject<CurrentPlay>(fileInfo);

            return jArray;
        }
        #endregion

        #region 文件另存为和清空播放列表

        /// <summary>
        /// 文件另存为
        /// </summary>
        /// <param name="newPath">目录</param>
        public void SaveFileAs(string newDirectory)
        {
            foreach (PlayedFile video in this.FileList.Values)
            {
                string newPath = newDirectory + "\\" + video.FileName;  //新路径
                File.Copy(video.FilePath, newPath, true); //开始复制，并允许覆盖同名文件
            }
        }
        /// <summary>
        /// 清空播放列表
        /// </summary>
        public void ClearPlayList()
        {
            FileList.Clear();//首先从内存中清除

            //删除本地保存的文件
            if (File.Exists("newList.obj"))
            {
                File.Delete("newList.obj");
            }
            if (File.Exists("playIndex.obj"))
            {
                File.Delete("playIndex.obj");
            }
            if (File.Exists("playFileName.obj"))
            {
                File.Delete("playFileName.obj");
            }
            
        }

        #endregion

    }

    /// <summary>
    /// 最近播放文件信息类
    /// </summary>
    [Serializable]
    public class CurrentPlay
    {
        public string FileName { get; set; }
        public double CurrentPosition { get; set; }
    }
}
