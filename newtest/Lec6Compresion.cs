// using System;
// using System.Diagnostics;
// using System.Drawing;
// using System.Drawing.Imaging;
// using System.Runtime.InteropServices;
// using System.Windows.Forms;
// using Emgu.CV;
// using Emgu.CV.CvEnum;
// using Emgu.CV.Structure;
// using NAudio;
// using NAudio.Wave;
// using NAudio.Gui;
// using NAudio.Wave.SampleProviders;
// using OxyPlot;
// using OxyPlot.Series;
// using OxyPlot.WindowsForms;
// using OxyPlot.Axes;
// using MathNet.Numerics.IntegralTransforms;
// using System.Numerics;

// using AForge.Imaging;
// using AForge.Imaging.Filters;
// using AForge.Imaging.ComplexFilters;
// using AForge;
// using System.Collections.Generic;

// namespace Lec6
// {
//     class Lec6Compression
//     {
//         public static void Main(string[] args)
//         {
//             string audiopath = "D:/newtest/guitar - Copy.wav";
//             string audiopath2 = "D:/newtest/piano - Copy.wav";
//             string imagepath1 = "D:/newtest/grayscale.jpg";


//             System.Console.WriteLine("Index 1: RLE Method");
//             System.Console.WriteLine("Index 2: DCT");
//             System.Console.WriteLine("Index 3: EX");

//             int Choice = Convert.ToInt32(Console.ReadLine());
//             switch (Choice)
//             {
//                 case 1:
//                     // PlotingAudio(audiopath);
//                     int[] x = { 5, 5, 2, 1, 1, 1, 1, 3 };
//                     List<int> y = RLE(x);
//                     Console.WriteLine(string.Join(", ", y));
//                     break;
//                 case 2:
//                     DCT(imagepath1);
//                     break;
//                 case 3:
//                     Bitmap rgbImage = new Bitmap(imagepath1);
//                     Bitmap binaryImage = ConvertToBinary(rgbImage);
//                     List<int> rleResult = RLE(binaryImage);
//                     Console.WriteLine(string.Join(", ", rleResult));
//                     break;


//             }
//         }
//         public static List<int> RLE(int[] input)
//         {
//             List<int> y = new List<int>();
//             int count = 1;

//             for (int i = 0; i < input.Length - 1; i++)
//             {
//                 if (input[i] == input[i + 1])
//                 {
//                     count++;
//                 }
//                 else
//                 {
//                     y.Add(count);
//                     y.Add(input[i]);
//                     count = 1;
//                 }
//             }

//             y.Add(count);
//             y.Add(input[input.Length - 1]);
//             return y;
//         }
//         public static void DCT(string imagePath)
//         {
//             // Load the image as a grayscale image
//             Mat imageMat = CvInvoke.Imread(imagePath, ImreadModes.Grayscale);

//             // Ensure the image has even dimensions
//             int width = imageMat.Width;
//             int height = imageMat.Height;
//             if (width % 2 != 0 || height % 2 != 0)
//             {
//                 width = (width % 2 != 0) ? width + 1 : width;
//                 height = (height % 2 != 0) ? height + 1 : height;
//                 CvInvoke.Resize(imageMat, imageMat, new System.Drawing.Size(width, height));
//             }

//             // Convert the image to a 32-bit floating-point image
//             Mat floatImage = new Mat();
//             imageMat.ConvertTo(floatImage, DepthType.Cv32F);

//             // Perform DCT on the grayscale image
//             CvInvoke.Dct(floatImage, floatImage, DctType.Forward);
//             // new
//             // Compute the number of non-zero DCT coefficients
//             float[] floatArray = new float[floatImage.Total.ToInt32()];
//             floatImage.CopyTo(floatArray);
//             int nonZeroCount = 0;
//             foreach (var value in floatArray)
//             {
//                 if (value != 0)
//                 {
//                     nonZeroCount++;
//                 }
//             }

//             // Compute the total number of pixels in the original image
//             // Compute the uncompressed size (in bytes)
//             int totalPixels = imageMat.Width * imageMat.Height;
//             int uncompressedSize = totalPixels * sizeof(byte); // Grayscale image, 1 byte per pixel

//             // Compute the compressed size (in bytes)
//             int compressedSize = nonZeroCount * sizeof(float); // Each coefficient is a 32-bit float

//             // Compute the compression ratio
//             double compressionRatio = (double)uncompressedSize / compressedSize;
//             Console.WriteLine("Compression Ratio: " + compressionRatio);
//             // new
//             // Perform inverse DCT to return to the spatial domain (time domain)
//             CvInvoke.Dct(floatImage, floatImage, DctType.Inverse);

//             // Convert the image back to 8-bit unsigned integer (grayscale) for display
//             Mat outputMat = new Mat();
//             floatImage.ConvertTo(outputMat, DepthType.Cv8U);

//             // Display or save the processed image
//             CvInvoke.Imwrite("D://newtest/DCT.png", outputMat);
//             // long originalimage = GetImageSize(imagePath);
//             // long compressed = GetImageSize("D://newtest/DCT.png");
//             // System.Console.WriteLine($"Original size: {originalimage}");
//             // System.Console.WriteLine($"Compressed size: {compressed}");
//             // System.Console.WriteLine($"Compresstion ratio: {originalimage / compressed}");

//         }
//         public static long GetImageSize(string imagePath)
//         {
//             FileInfo fileInfo = new FileInfo(imagePath);
//             return fileInfo.Length;
//         }
//         // static void ConvRGB_Binary(string imagepath)
//         // {
//         //     // Bitmap rgbImage = new Bitmap(imagepath);
//         //     // Mat grayscaleImage = new Mat();
//         //     Mat imagetest = CvInvoke.Imread(imagepath, ImreadModes.AnyColor);
//         //     CvInvoke.CvtColor(imagetest, grayscaleImage,
//         //      ColorConversion.Bgr2Gray);
//         //     // Convert the grayscale image to a binary 
//         //     Mat binaryImage = new Mat();
//         //     CvInvoke.Threshold(grayscaleImage, binaryImage, 127, 255,
//         //     ThresholdType.Binary);
//         //     CvInvoke.Imshow("Binary Image", binaryImage);
//         //     CvInvoke.WaitKey(0);
//         // }
//         public static Bitmap ConvertToBinary(Bitmap rgbImage)
//         {
//             Bitmap binaryImage = new Bitmap(rgbImage.Width, rgbImage.Height);

//             for (int i = 0; i < rgbImage.Width; i++)
//             {
//                 for (int j = 0; j < rgbImage.Height; j++)
//                 {
//                     Color pixelColor = rgbImage.GetPixel(i, j);
//                     int grayScale = (int)((pixelColor.R * 0.3) + (pixelColor.G * 0.59) + (pixelColor.B * 0.11));
//                     binaryImage.SetPixel(i, j, grayScale > 128 ? Color.White : Color.Black);
//                 }
//             }

//             return binaryImage;
//         }
//         public static List<int> RLE(Bitmap binaryImage)
//         {
//             List<int> input = new List<int>();

//             for (int i = 0; i < binaryImage.Width; i++)
//             {
//                 for (int j = 0; j < binaryImage.Height; j++)
//                 {
//                     input.Add(binaryImage.GetPixel(i, j).R > 128 ? 1 : 0);
//                 }
//             }

//             return RLE(input.ToArray());
//         }

//     }
// }