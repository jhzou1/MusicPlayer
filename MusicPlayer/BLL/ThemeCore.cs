using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace MusicPlayer
{

    /// <summary>
    /// 主题辅助服务类
    /// </summary>
    public class ThemeCore
    {
        public delegate void NotifyAllTheme();

        /// <summary>
        /// 设置各种控件主题
        /// </summary>
        /// <param name="theme"></param>
        /// <param name="fatherControl"></param>
        public void SetFormTheme(ThemeSkin theme, Control fatherControl)
        {
            if (fatherControl is Form)
            {
                Form objFrm = (Form)fatherControl;
                objFrm.BackColor = theme._BackgroudColor;
            }
            Control.ControlCollection sonControls = fatherControl.Controls;
            foreach (Control control in sonControls)
            {
                if (control is Label)
                {
                    Label lbl = (Label)control;
                    lbl.ForeColor = theme._FontColor;
                    continue;
                }
                if (control is Button)
                {
                    Button btn = (Button)control;
                    btn.ForeColor = theme._FontColor;
                    btn.BackColor = theme._BackgroudColor;
                    continue;
                }
                if (control is Panel)
                {
                    Panel panel = (Panel)control;
                    panel.BackColor = theme._BackgroudColor;
                }

                //继续循环
                SetFormTheme(theme, control);
            }
        }

        /// <summary>
        /// 保存主题到本地
        /// </summary>
        public void SaveThemSkin()
        {
            //保存主题设置
            FileHelper.SaveFileAsJson(Program.themeSkin, "CurrentSkin.json", "");
        }

        /// <summary>
        /// 通知所有
        /// </summary>
        public void NotifyAll()
        {
            //这里一旦通知，所有的关联观察者都会收到通知
            NotityEvent();
        }


        public event NotifyAllTheme NotityEvent;

    }
}
