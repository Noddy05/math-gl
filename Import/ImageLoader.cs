﻿using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using OpenTK.Graphics.OpenGL;
using PixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;

namespace MathGL.Import
{
    class ImageLoader
    {
        //AI generated, using OpenChat AI

        public static int LoadTexture(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Could not find file at path: " + filePath);
            }

            int textureId = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, textureId);

            // Load the image data
            int width, height;
            byte[] imageData = LoadImageData(filePath, out width, out height);

            // Set texture parameters
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            // Load the texture data into OpenGL
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0,
                PixelFormat.Bgra, PixelType.UnsignedByte, imageData);

            return textureId;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", 
        "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public static byte[] LoadImageData(string filePath, out int width, out int height)
        {
            // Use System.Drawing.Bitmap to load the image and get the width and height
            using Bitmap bitmap = new(filePath);
            width = bitmap.Width;
            height = bitmap.Height;

            // Lock the bitmap data and copy it into a byte array
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            byte[] bytes = new byte[data.Stride * data.Height];
            System.Runtime.InteropServices.Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);
            bitmap.UnlockBits(data);

            return bytes;
        }
    }
}
