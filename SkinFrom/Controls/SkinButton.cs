using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SkinFrom.Utility;

namespace SkinFrom.Controls
{
    public partial class SkinButton : Control
    {
        #region Eunm

        /// <summary>
        /// 枚举按钮状态
        /// </summary>
        public enum State
        {
            /// <summary>
            /// 按钮默认
            /// </summary>
            Normal = 1,

            /// <summary>
            /// 鼠标移上按钮时
            /// </summary>
            MouseOver = 2,

            /// <summary>
            /// 鼠标按下按钮时
            /// </summary>
            MouseDown = 3,

            /// <summary>
            /// 当不启用按钮时（也就是按钮属性Enabled==Ture时）
            /// </summary>
            Disable = 4,

            /// <summary>
            /// 控件得到Tab焦点时
            /// </summary>
            Default = 5
        }
        #endregion

        private State _state = State.Normal;
        private Bitmap _backImage;
        private Rectangle _backlightLtrb;
        private bool _IsTabFocus;

        #region Propertys

        [CategoryAttribute("Skin"), Description("背景图片")]
        public Bitmap BackImage
        {
            get { return _backImage; }
            set
            {
                _backImage = value;
                Invalidate();
            }
        }

        [CategoryAttribute("Skin"), Description("绘图边界")]
        public Rectangle BacklightLtrb
        {
            get { return _backlightLtrb; }
            set
            {
                _backlightLtrb = value;
                if (_backlightLtrb != Rectangle.Empty)
                    Invalidate();
            }
        }

        #endregion

        public SkinButton()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);//自绘
            SetStyle(ControlStyles.DoubleBuffer, true);// 双缓冲
            SetStyle(ControlStyles.ResizeRedraw, true);//调整大小时重绘
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);// 双缓冲
            //this.SetStyle(ControlStyles.Opaque, true);//如果为真，控件将绘制为不透明，不绘制背景
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);   //透明效果
        }

        protected override void OnPaint(PaintEventArgs pe)
        {

            if (BackImage == null)
            {
                base.OnPaint(pe);
                return;
            }

            var i = (int)_state;
            if (Focused && _state != State.MouseDown)
                i = 5;

            if (!Enabled)
                i = 4;

            Graphics g = pe.Graphics;
            InvokePaintBackground(this, new PaintEventArgs(pe.Graphics, ClientRectangle));

            ImageDrawRect.DrawRect(g, BackImage, ClientRectangle,
                BacklightLtrb != Rectangle.Empty
                    ? Rectangle.FromLTRB(BacklightLtrb.X, BacklightLtrb.Y, BacklightLtrb.Width, BacklightLtrb.Height)
                    : Rectangle.FromLTRB(10, 10, 10, 10),
                i, 5);
        }

        private void SkinButton_MouseDown(object sender, MouseEventArgs e)
        {
            _state = State.MouseDown;
            Invalidate();
        }

        private void SkinButton_MouseMove(object sender, MouseEventArgs e)
        {
            _state = State.MouseOver;
            Invalidate();
        }

        private void SkinButton_MouseLeave(object sender, EventArgs e)
        {
            _state = State.Normal;
            Invalidate();
        }
    }
}
