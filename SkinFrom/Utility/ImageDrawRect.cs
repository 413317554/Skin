using System.Drawing;
using System.IO;
using System.Reflection;

namespace SkinFrom.Utility
{
    public class ImageDrawRect
    {
        /// <summary>
        /// 绘图对像
        /// </summary>
        /// <param name="g">绘图对像</param>
        /// <param name="img">图片</param>
        /// <param name="r">绘置的图片大小、坐标</param>
        /// <param name="lr">绘置的图片边界</param>
        /// <param name="index">当前状态</param>
        /// <param name="totalindex">状态总数</param>
        public static void DrawRect(Graphics g, Bitmap img, Rectangle r, Rectangle lr, int index, int totalindex)
        {
            if (img == null) return;
            Rectangle r1, r2;
            int x = (index - 1)*img.Width/totalindex;
            int y = 0;
            int x1 = r.Left;
            int y1 = r.Top;

            if (r.Height > img.Height && r.Width <= img.Width/totalindex)
            {
                r1 = new Rectangle(x, y, img.Width/totalindex, lr.Top);
                r2 = new Rectangle(x1, y1, r.Width, lr.Top);
                g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                r1 = new Rectangle(x, y + lr.Top, img.Width/totalindex, img.Height - lr.Top - lr.Bottom);
                r2 = new Rectangle(x1, y1 + lr.Top, r.Width, r.Height - lr.Top - lr.Bottom);
                if ((lr.Top + lr.Bottom) == 0) r1.Height = r1.Height - 1;
                g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                r1 = new Rectangle(x, y + img.Height - lr.Bottom, img.Width/totalindex, lr.Bottom);
                r2 = new Rectangle(x1, y1 + r.Height - lr.Bottom, r.Width, lr.Bottom);
                g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);
            }
            else if (r.Height <= img.Height && r.Width > img.Width/totalindex)
            {
                r1 = new Rectangle(x, y, lr.Left, img.Height);
                r2 = new Rectangle(x1, y1, lr.Left, r.Height);
                g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);
                r1 = new Rectangle(x + lr.Left, y, img.Width/totalindex - lr.Left - lr.Right, img.Height);
                r2 = new Rectangle(x1 + lr.Left, y1, r.Width - lr.Left - lr.Right, r.Height);
                g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);
                r1 = new Rectangle(x + img.Width/totalindex - lr.Right, y, lr.Right, img.Height);
                r2 = new Rectangle(x1 + r.Width - lr.Right, y1, lr.Right, r.Height);
                g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);
            }
            else if (r.Height <= img.Height && r.Width <= img.Width/totalindex)
            {
                r1 = new Rectangle((index - 1)*img.Width/totalindex, 0, img.Width/totalindex, img.Height);
                g.DrawImage(img, new Rectangle(x1, y1, r.Width, r.Height), r1, GraphicsUnit.Pixel);
            }
            else if (r.Height > img.Height && r.Width > img.Width/totalindex)
            {
                //top-left
                r1 = new Rectangle(x, y, lr.Left, lr.Top);
                r2 = new Rectangle(x1, y1, lr.Left, lr.Top);
                g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                //top-bottom
                r1 = new Rectangle(x, y + img.Height - lr.Bottom, lr.Left, lr.Bottom);
                r2 = new Rectangle(x1, y1 + r.Height - lr.Bottom, lr.Left, lr.Bottom);
                g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                //left
                r1 = new Rectangle(x, y + lr.Top, lr.Left, img.Height - lr.Top - lr.Bottom);
                r2 = new Rectangle(x1, y1 + lr.Top, lr.Left, r.Height - lr.Top - lr.Bottom);
                g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                //top
                r1 = new Rectangle(x + lr.Left, y,
                    img.Width/totalindex - lr.Left - lr.Right, lr.Top);
                r2 = new Rectangle(x1 + lr.Left, y1,
                    r.Width - lr.Left - lr.Right, lr.Top);
                g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                //right-top
                r1 = new Rectangle(x + img.Width/totalindex - lr.Right, y, lr.Right, lr.Top);
                r2 = new Rectangle(x1 + r.Width - lr.Right, y1, lr.Right, lr.Top);
                g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                //Right
                r1 = new Rectangle(x + img.Width/totalindex - lr.Right, y + lr.Top,
                    lr.Right, img.Height - lr.Top - lr.Bottom);
                r2 = new Rectangle(x1 + r.Width - lr.Right, y1 + lr.Top,
                    lr.Right, r.Height - lr.Top - lr.Bottom);
                g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                //right-bottom
                r1 = new Rectangle(x + img.Width/totalindex - lr.Right, y + img.Height - lr.Bottom,
                    lr.Right, lr.Bottom);
                r2 = new Rectangle(x1 + r.Width - lr.Right, y1 + r.Height - lr.Bottom,
                    lr.Right, lr.Bottom);
                g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                //bottom
                r1 = new Rectangle(x + lr.Left, y + img.Height - lr.Bottom,
                    img.Width/totalindex - lr.Left - lr.Right, lr.Bottom);
                r2 = new Rectangle(x1 + lr.Left, y1 + r.Height - lr.Bottom,
                    r.Width - lr.Left - lr.Right, lr.Bottom);
                g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                //Center
                r1 = new Rectangle(x + lr.Left, y + lr.Top,
                    img.Width/totalindex - lr.Left - lr.Right, img.Height - lr.Top - lr.Bottom);
                r2 = new Rectangle(x1 + lr.Left, y1 + lr.Top,
                    r.Width - lr.Left - lr.Right, r.Height - lr.Top - lr.Bottom);
                g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);
            }
        }

        /// <summary>
        /// 绘图对像
        /// </summary>
        /// <param name="g"> 绘图对像</param>
        /// <param name="img">图片</param>
        /// <param name="r">绘置的图片大小、坐标</param>
        /// <param name="index">当前状态</param>
        /// <param name="totalindex">状态总数</param>
        public static void DrawRect(Graphics g, Bitmap img, Rectangle r, int index, int totalindex)
        {
            if (img == null) return;
            int width = img.Width / totalindex;
            int height = img.Height;
            int x = (index - 1) * width;
            int y = 0;
            Rectangle r1 = new Rectangle(x, y, width, height);
            Rectangle r2 = new Rectangle(r.Left, r.Top, r.Width, r.Height);
            g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);
        }

        /// <summary>
        /// 得到要绘置的图片对像
        /// </summary>
        /// <param name="str">图像在程序集中的地址</param>
        /// <returns></returns>
        public static Bitmap GetResBitmap(string str)
        {
            Stream sm = FindStream(str);
            if (sm == null) return null;
            return new Bitmap(sm);
        }

        /// <summary>
        /// 得到图程序集中的图片对像
        /// </summary>
        /// <param name="str">图像在程序集中的地址</param>
        /// <returns></returns>
        private static Stream FindStream(string str)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string[] resNames = assembly.GetManifestResourceNames();
            foreach (string s in resNames)
            {
                if (s == str)
                {
                    return assembly.GetManifestResourceStream(s);
                }
            }
            return null;
        }
    }
}
