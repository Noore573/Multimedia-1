// using System;
// using System.Drawing;
// using System.Drawing.Imaging;
// using System.Runtime.InteropServices;


// // using System.Drawing.Common;
// using Emgu.CV;
// using Emgu.CV.Structure;


// namespace Intro
// {
//     class Image
//     {
//         // string imagepath = "im.jpg";
//         static void Main(string[] args)
//         {
//             Bitmap bmap = new Bitmap("D:/im.jpg");
//             // bmap.Save("out.jpg");
//             Color pixelColor = bmap.GetPixel(300, 425);
//             System.Console.WriteLine($"Pixel color at (100,100) is :\n  R ={pixelColor.R} ,     B ={pixelColor.B} ,     G ={pixelColor.G}");
//             // NSetPixel(bmap,"mode");
//             NLockBit(bmap, "lockmode");
//         }
//         static void NSetPixel(Bitmap bmap, String imagename)
//         {
//             Color newColor = Color.Red;
//             for (int i = 0; i < 200; i++)
//             {
//                 for (int j = 0; j < 400; j++)
//                 {
//                     bmap.SetPixel(i, j, newColor);
//                 }
//             }
//             bmap.Save($"{imagename}.jpg");
//         }
//         static void NLockBit(Bitmap image, String imagename)
//         {
//             // BitmapData bmdata=image.LockBits(new Rectangle(0,0,image.Width,image.Height),
//             // ImageLockMode.ReadOnly,image.PixelFormat);
//             BitmapData bmdata = image.LockBits(new Rectangle(0, 0, image.Width, image.Height),
//     ImageLockMode.ReadWrite, image.PixelFormat);


//             // Getting the address of the first line
//             // Declre an array to
//             // Get the address of the first line.
//             IntPtr ptr = bmdata.Scan0;
//             // Declare an array to hold the bytes of the bitmap.
//             int bytes = Math.Abs(bmdata.Stride) * image.Height;
//             byte[] rgbValues = new byte[bytes];
//             // Copy the RGB values into the array.
//             System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
//             // Set every third value to 255. A 24bpp bitmap will look red. 
//             for (int counter = 2; counter < rgbValues.Length; counter += 3)
//                 rgbValues[counter] = 255;
//             // Copy the RGB values back to the bitmap
//             System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

//             image.UnlockBits(bmdata);

//         }

//     }

// }


