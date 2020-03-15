using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MusicPlayer
{
    public partial class FrmTheme : Form
    {
        public ThemeCore themeCore = null;

        public FrmTheme(ThemeCore T1)
        {
            InitializeComponent();
            themeCore = T1;

            themeCore.NotityEvent += SetTheme;
            if (Program.themeSkin!= null)
                themeCore.NotifyAll();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DefaultTheme_Click(object sender, EventArgs e)
        {
            PictureBox objPic = (PictureBox)sender;

            switch (objPic.Name)
            {
                //默认主题
                case "DefaultTheme":
                    Program.themeSkin = null;
                    Program.themeSkin = new ThemeSkin();
                    Program.themeSkin.ThemeKey = "DefaultTheme";
                    Program.themeSkin._BackgroudColor
                        = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(128)))), ((int)(((byte)(138)))));
                    Program.themeSkin._FontColor = System.Drawing.Color.White;

                    //保存并通知更改皮肤
                    themeCore.SaveThemSkin();
                    themeCore.NotifyAll();

                    break;

                //蓝色海洋主题
                case "pb2":
                    Program.themeSkin = null;
                    Program.themeSkin = new ThemeSkin();
                    Program.themeSkin._BackgroudColor
                        = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(208)))), ((int)(((byte)(248)))));
                    Program.themeSkin._FontColor = System.Drawing.Color.White;

                    //保存并通知更改皮肤
                    themeCore.SaveThemSkin();
                    themeCore.NotifyAll();

                    break;
                //绿色
                case "pb3":
                    Program.themeSkin = null;
                    Program.themeSkin = new ThemeSkin();
                    Program.themeSkin._BackgroudColor
                        = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(205)))), ((int)(((byte)(50)))));
                    Program.themeSkin._FontColor = System.Drawing.Color.White;


                    //保存并通知更改皮肤
                    themeCore.SaveThemSkin();
                    themeCore.NotifyAll();

                    break;
                case "pb4":
                    Program.themeSkin = null;
                    Program.themeSkin = new ThemeSkin();
                    Program.themeSkin._BackgroudColor
                        = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(206)))), ((int)(((byte)(235)))));
                    Program.themeSkin._FontColor = System.Drawing.Color.White;

                    //保存并通知更改皮肤
                    themeCore.SaveThemSkin();
                    themeCore.NotifyAll();

                    break;
            }
        }

        //通知动作
        private void SetTheme()
        {
            //设置当前窗体主题
            themeCore.SetFormTheme(Program.themeSkin, this);
        }

    }
}
