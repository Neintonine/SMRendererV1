﻿using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL4;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace SMRenderer.Visual
{
    public class Texture
    {
        public static Texture empty;

        /// <summary>
        ///     Creates a texture
        /// </summary>
        /// <param name="bm"></param>
        public Texture(Bitmap bm, bool autodispose = false)
        {
            BitmapData data;

            TexId = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, TexId);

            if (bm.PixelFormat == PixelFormat.Format32bppArgb)
            {
                data = bm.LockBits(new Rectangle(0, 0, bm.Width, bm.Height), ImageLockMode.ReadOnly,
                    PixelFormat.Format32bppArgb);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                    OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            }
            else
            {
                data = bm.LockBits(new Rectangle(0, 0, bm.Width, bm.Height), ImageLockMode.ReadOnly,
                    PixelFormat.Format24bppRgb);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, data.Width, data.Height, 0,
                    OpenTK.Graphics.OpenGL4.PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);
            }

            Width = bm.Width;
            Height = bm.Height;

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                (int) TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                (int) TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int) TextureWrapMode.Repeat);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, 0);
            bm.UnlockBits(data);
            if (autodispose) bm.Dispose();
        }

        public int Width { get; }
        public int Height { get; }
        public int TexId { get; } = -1;

        /// <summary>
        ///     Deletes the texture
        /// </summary>
        public void Dispose()
        {
            GL.DeleteTexture(TexId);
        }

        public static void CreateEmpty()
        {
            Bitmap bit = new Bitmap(1, 1);
            bit.SetPixel(0, 0, Color.White);
            empty = new Texture(bit);
            bit.Dispose();
        }
    }
}