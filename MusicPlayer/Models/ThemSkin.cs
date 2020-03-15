using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicPlayer
{
    /// <summary>
    /// 界面主题实体类
    /// </summary>
 public    class ThemeSkin
    {
        public string ThemeKey { get; set; }
        //背景颜色
        public System.Drawing.Color _BackgroudColor { get; set; }

        //按钮或者标签 文字颜色
        public System.Drawing.Color _FontColor { get; set; }
    }
}
