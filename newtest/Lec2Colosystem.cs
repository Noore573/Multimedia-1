// using System;
// using System.Diagnostics;
// using System.Drawing;
// using System.Drawing.Imaging;
// using System.Runtime.InteropServices;
// using Emgu.CV;
// using Emgu.CV.CvEnum;
// namespace Colorsystem
// {
//     class CS
//     {
//         static void Main(string[] args)
//         {

//             string imagepath = "D:/im.jpg";
//             string outputpath = "newlock.jpg";

//             Console.WriteLine("Index 0: RGB_CYM method");
//             Console.WriteLine("Index 1: HSV method");
//             Console.WriteLine("Index 2: YCbCr method");
//             Console.WriteLine("Index 3: YUV method");
//             Console.WriteLine("Index 4: Lab method");
//             Console.WriteLine("Index 5: RGB_CYM_LockBits method");
//             Console.WriteLine("Index 6: Exercise method");
//             Console.WriteLine("Index 7: ExerciseLockbit method");
//             Console.WriteLine("Index 8: Displayimage");
//             Console.WriteLine("Index 9: GetRGB");

//             System.Console.WriteLine("Enter the operation");

//             int Choice = Convert.ToInt32(Console.ReadLine());
//             switch (Choice)
//             {
//                 case 0:
//                     outputpath = Console.ReadLine();
//                     RGB_CYM(imagepath, outputpath);
//                     break;
//                 case 1:
//                     HSV(imagepath);
//                     break;
//                 case 2:
//                     YCbCr(imagepath);
//                     break;
//                 case 3:
//                     YUV(imagepath);
//                     break;
//                 case 4:
//                     Lab(imagepath);
//                     break;
//                 case 5:
//                     outputpath = Console.ReadLine();
//                     RGB_CYM_LockBits(imagepath, outputpath);
//                     break;
//                 case 6:
//                     outputpath = Console.ReadLine();
//                     Exercise(imagepath, outputpath);
//                     break;
//                 case 7:
//                     outputpath = Console.ReadLine();
//                     ExerciseLockbit(imagepath, outputpath);
//                     break;
//                 case 8:
//                     DisplayImage(imagepath);
//                     break;
//                 case 9:
//                     GetRGP(imagepath);
//                     break;

//                 default:
//                     Console.WriteLine("Invalid method index.");
//                     break;
//             }
//         }


//         // static void DisplayImage(string imagepath)
//         // {
//         //     var p = new Process();
//         //     p.StartInfo = new ProcessStartInfo(imagepath)
//         //     {
//         //         UseShellExecute = true
//         //     };
//         //     p.Start();

//         // }
//         static void RGB_CYM(string imagepath, string outputpath)
//         {
//             Bitmap rgbImage = new Bitmap(imagepath);

//             // Convert RGB to CMY
//             for (int i = 0; i < rgbImage.Width; i++)
//             {
//                 for (int j = 0; j < rgbImage.Height; j++)
//                 {
//                     // Get RGB color of current pixel
//                     Color pixelColor = rgbImage.GetPixel(i, j);
//                     // Calculate CMY values
//                     int c = 255 - pixelColor.R; // Cyan
//                     int m = 255 - pixelColor.G; // Magenta
//                     int y = 255 - pixelColor.B; // Yellow
//                     // Create CMY color using the calculated values
//                     Color cmyColor = Color.FromArgb(c, m, y);
//                     // Set the corresponding pixel in the CMY image

//                     rgbImage.SetPixel(i, j, cmyColor);
//                 }
//             }
//             rgbImage.Save(outputpath);

//         }
//         static void RGB_CYM_LockBits(string imagepath, string outputpath)
//         {
//             Bitmap rgbImage = new Bitmap(imagepath);
//             BitmapData bmpData = rgbImage.LockBits(new Rectangle(0, 0, rgbImage.Width, rgbImage.Height),
//                                                         ImageLockMode.ReadWrite, rgbImage.PixelFormat);

//             IntPtr ptr = bmpData.Scan0;
//             int bytes = Math.Abs(bmpData.Stride) * rgbImage.Height;
//             byte[] rgbValues = new byte[bytes];
//             Marshal.Copy(ptr, rgbValues, 0, bytes);

//             for (int i = 0; i < rgbValues.Length; i += 3)
//             {
//                 byte c = (byte)(255 - rgbValues[i]);
//                 byte m = (byte)(255 - rgbValues[i + 1]);
//                 byte y = (byte)(255 - rgbValues[i + 2]);

//                 rgbValues[i] = c;
//                 rgbValues[i + 1] = m;
//                 rgbValues[i + 2] = y;
//             }

//             Marshal.Copy(rgbValues, 0, ptr, bytes);
//             rgbImage.UnlockBits(bmpData);

//             rgbImage.Save(outputpath);
//         }

//         static void GetRGP(string imagepath)
//         {
//             Bitmap rgbImage = new Bitmap(imagepath);
//             Bitmap redComponentImage = new Bitmap(rgbImage.Width, rgbImage.Height);
//             Bitmap greenComponentImage = new Bitmap(rgbImage.Width, rgbImage.Height);
//             Bitmap blueComponentImage = new Bitmap(rgbImage.Width, rgbImage.Height);
//             // Extract components pixel by pixel
//             for (int y = 0; y < rgbImage.Height; y++)
//             {
//                 for (int x = 0; x < rgbImage.Width; x++)
//                 {
//                     // Get RGB color of current pixel
//                     Color pixelColor = rgbImage.GetPixel(x, y);
//                     // Extract components
//                     int redComponent = pixelColor.R;
//                     int greenComponent = pixelColor.G;
//                     int blueComponent = pixelColor.B;
//                     // Create colors for each component
//                     Color redColor = Color.FromArgb(redComponent, 0, 0);
//                     Color greenColor = Color.FromArgb(0, greenComponent, 0);
//                     Color blueColor = Color.FromArgb(0, 0, blueComponent);
//                     // Set the corresponding pixel in each component image
//                     redComponentImage.SetPixel(x, y, redColor);
//                     greenComponentImage.SetPixel(x, y, greenColor);
//                     blueComponentImage.SetPixel(x, y, blueColor);
//                 }
//             }
//             // Save component images
//             redComponentImage.Save("red_component.jpg");
//             greenComponentImage.Save(" green_component.jpg");
//             blueComponentImage.Save(" blue_component.jpg");
//         }


//         static void HSV(string imagepath)
//         {
//             // Load an RGB image
//             Mat rgbImage = CvInvoke.Imread(imagepath);
//             // Convert RGB to HSV
//             Mat hsvImage = new Mat();
//             CvInvoke.CvtColor(rgbImage, hsvImage,
//             ColorConversion.Bgr2Hsv);
//             CvInvoke.Imshow("HSV Image", hsvImage);
//             CvInvoke.WaitKey(0);
//         }

//         static void YCbCr(string imagepath)
//         {
//             // Load an RGB image
//             Mat rgbImage = CvInvoke.Imread(imagepath);
//             // Convert RGB to YCbCr
//             Mat ycbcrImage = new Mat();
//             CvInvoke.CvtColor(rgbImage, ycbcrImage,
//             ColorConversion.Bgr2YCrCb);
//             CvInvoke.Imshow("YCbCr Image", ycbcrImage);
//             CvInvoke.WaitKey(0);

//         }
//         static void YUV(string imagepath)
//         {
//             // Load an RGB image
//             Mat rgbImage = CvInvoke.Imread(imagepath);
//             // Convert RGB to YUV
//             Mat yuvImage = new Mat();
//             CvInvoke.CvtColor(rgbImage, yuvImage,
//             ColorConversion.Bgr2Yuv);
//             CvInvoke.Imshow("YUV image", yuvImage);
//             CvInvoke.WaitKey(0);

//         }
//         static void Lab(string imagepath)
//         {
//             // Load an RGB image
//             Mat rgbImage = CvInvoke.Imread(imagepath);
//             // Convert RGB to Lab
//             Mat labImage = new Mat();
//             CvInvoke.CvtColor(rgbImage, labImage,
//             ColorConversion.Bgr2Lab);
//             CvInvoke.Imshow("L*a*b", labImage);
//             CvInvoke.WaitKey(0);
//         }



//         static void Exercise(string imagepath, string outputpath)

//         {
//             Bitmap orgimage = new Bitmap(imagepath);

//             for (int x = 0; x < orgimage.Width; x++)
//             {
//                 for (int y = 0; y < orgimage.Height; y++)
//                 {
//                     Color pixel = orgimage.GetPixel(x, y);
//                     Color modpix = Color.FromArgb(pixel.R, 0, pixel.B);
//                     orgimage.SetPixel(x, y, modpix);
//                 }
//             }
//             orgimage.Save(outputpath);

//         }
//         static void ExerciseLockbit(string imagepath, string outputpath)
//         {
//             Bitmap orgimage = new Bitmap(imagepath);


//             BitmapData bmpData = orgimage.LockBits(new Rectangle(0, 0, orgimage.Width, orgimage.Height),
//              ImageLockMode.ReadWrite, orgimage.PixelFormat);

//             IntPtr ptr = bmpData.Scan0;
//             int bytes = Math.Abs(bmpData.Stride) * orgimage.Height;
//             byte[] rgbValues = new byte[bytes];
//             Marshal.Copy(ptr, rgbValues, 0, bytes);

//             for (int i = 0; i < rgbValues.Length; i += 3)
//             {
//                 rgbValues[i + 1] = 0;
//             }

//             Marshal.Copy(rgbValues, 0, ptr, bytes);
//             orgimage.UnlockBits(bmpData);
//             orgimage.Save(outputpath);
//         }


//     }

// }
