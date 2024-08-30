// using System;
// using System.Diagnostics;
// using System.Drawing;
// using System.Drawing.Imaging;
// using System.Runtime.InteropServices;
// using Emgu.CV;
// using Emgu.CV.CvEnum;
// using Emgu.CV.Structure;
// // using OxyPlot;
// using OxyPlot.Series;
// // using OxyPlot.Wpf;
// // using OxyPlot.WindowsForms;


// namespace Images
// {
//     class Images
//     {
//         public static void Main(string[] args)
//         {
//             // [STAThread]
//             string imagepath = "D:/im.jpg";
//             string filepath = "D:/newtest/new2im.jpg";
//             string filepath2 = "D:/newtest/new2i333m";
//             string outputpath = "newlock.jpg";

//             System.Console.WriteLine("Index 1: RGB - Binary");
//             System.Console.WriteLine("Index 2: RGB - GrayScale");
//             System.Console.WriteLine("Index 3: IndexedImage");
//             System.Console.WriteLine("Index 4: EX1");
//             System.Console.WriteLine("Index 5: EX2 ");
//             System.Console.WriteLine("Index 6: EX2 V2 ");

//             int Choice = Convert.ToInt32(Console.ReadLine());
//             switch (Choice)
//             {
//                 case 1:
//                     ConvRGB_Binary(imagepath);
//                     break;
//                 case 2:
//                     ConvRGB_Grayscale(imagepath);
//                     break;
//                 case 3:
//                     IndexedImages2(imagepath, 60);
//                     break;
//                 case 4:
//                     Ex1(imagepath, filepath);
//                     break;
//                 case 5:
//                     // the first way compression is slightly worse and the code is harder
//                     Ex2(imagepath, filepath2);
//                     break;
//                 case 6:
//                     // the second way is better but uses external functions
//                     Ex2V2(imagepath, filepath2);
//                     break;
//             }




//         }

//         static void DisplayImage(string imagepath)
//         {
//             var p = new Process();
//             p.StartInfo = new ProcessStartInfo(imagepath)
//             { UseShellExecute = true };
//             p.Start();
//         }
//         static void ConvRGB_Binary(string imagepath)
//         {
//             // Bitmap rgbImage = new Bitmap(imagepath);
//             Mat grayscaleImage = new Mat();
//             Mat imagetest = CvInvoke.Imread(imagepath, ImreadModes.AnyColor);
//             CvInvoke.CvtColor(imagetest, grayscaleImage,
//              ColorConversion.Bgr2Gray);
//             // Convert the grayscale image to a binary 
//             Mat binaryImage = new Mat();
//             CvInvoke.Threshold(grayscaleImage, binaryImage, 127, 255,
//             ThresholdType.Binary);
//             CvInvoke.Imshow("Binary Image", binaryImage);
//             CvInvoke.WaitKey(0);
//         }
//         static void ConvRGB_Grayscale(string imagepath)
//         {
//             Mat grayscaleImage = new Mat();
//             Mat imagetest = CvInvoke.Imread(imagepath, ImreadModes.AnyColor);
//             CvInvoke.CvtColor(imagetest, grayscaleImage,
//             ColorConversion.Bgr2Gray);
//             CvInvoke.Imshow("Gray Image", grayscaleImage);
//             CvInvoke.WaitKey(0);
//         }
//         // static void RGB(string imagepath)
//         // {
//         //     // Color color = Color.FromArgb(redChannel, greenChannel, blueChannel);
//         // }
//         static void IndexedImages2(string imagepath, int sampleIndex)
//         {
//             Bitmap originalImage = new Bitmap(imagepath);
//             // converting to an indexed image before extracting the colorpallet(this is the only way to make it work)
//             Bitmap indexedImage = originalImage.Clone(new Rectangle(0, 0, originalImage.Width, originalImage.Height), PixelFormat.Format8bppIndexed);
//             ColorPalette colorPalette = indexedImage.Palette;
//             System.Console.WriteLine(colorPalette.Entries.Length);
//             Color sampleColor = colorPalette.Entries[sampleIndex];
//             Console.WriteLine($"Color at index {sampleIndex}: R ={sampleColor.R}, G ={sampleColor.G}, B ={sampleColor.B}");
//         }
//         static void Ex1(string imagepath, string filepath)
//         {
//             // reading+showing the image
//             Bitmap originalImage = new Bitmap(imagepath);
//             Mat imagetest = CvInvoke.Imread(imagepath, ImreadModes.AnyColor);
//             CvInvoke.Imshow("Showing Image", imagetest);
//             // CvInvoke.WaitKey(0);
//             CvInvoke.Imwrite(filepath, imagetest);
//             // showing additional info
//             int Width = originalImage.Width;
//             int Height = originalImage.Height;
//             int depth = Image.GetPixelFormatSize(originalImage.PixelFormat);
//             ImageFormat format = originalImage.RawFormat;
//             // image size
//             FileInfo fileInfo = new FileInfo(imagepath);
//             long imagesize = fileInfo.Length;

//             System.Console.WriteLine($"Image info :\nwidth = {Width}\nheight = {Height}\ndepth = {depth}\nformat = {format}\nsize = {imagesize}");
//         }
//         static void Ex2(string imagepath, string filepath)
//         {
//             Dictionary<int, double> compressionRatios = new Dictionary<int, double>();
//             var plotModel = new OxyPlot.PlotModel { Title = "Quality vs Compression Ratio" };
//             var series = new LineSeries();

//             Bitmap originalImage = new Bitmap(imagepath);
//             Mat imagetest = CvInvoke.Imread(imagepath, ImreadModes.AnyColor);
//             // CvInvoke.Imshow("Showing Image", imagetest);
//             // CvInvoke.WaitKey(0);
//             int quality = 50;
//             KeyValuePair<ImwriteFlags, int>[] parameters =
//                 new KeyValuePair<ImwriteFlags, int>[]
//                 {
//         new KeyValuePair<ImwriteFlags, int>(ImwriteFlags.JpegQuality, quality)
//                 };
//             CvInvoke.Imwrite($"{filepath}-50.jpg", imagetest, parameters);
//             int quality1 = 5;
//             KeyValuePair<ImwriteFlags, int>[] parameters1 =
//                 new KeyValuePair<ImwriteFlags, int>[]
//                 {
//         new KeyValuePair<ImwriteFlags, int>(ImwriteFlags.JpegQuality, quality1)
//                 };


//             CvInvoke.Imwrite($"{filepath}-5.jpg", imagetest, parameters1);
//             FileInfo orginal = new FileInfo(imagepath);
//             long originalImagebeforecomp = orginal.Length;

//             FileInfo comp50 = new FileInfo($"{filepath}-50.jpg");
//             long imagesizeafter50 = comp50.Length;
//             double ratio50 = (double)originalImagebeforecomp / imagesizeafter50;
//             compressionRatios.Add(quality, ratio50);

//             FileInfo comp5 = new FileInfo($"{filepath}-5.jpg");
//             long imagesizeafter5 = comp5.Length;
//             double ratio5 = (double)originalImagebeforecomp / imagesizeafter5;
//             compressionRatios.Add(quality1, ratio5);

//             System.Console.WriteLine($"Image before= {originalImagebeforecomp}  -  image after compression = {imagesizeafter50}");
//             System.Console.WriteLine($"Image before= {originalImagebeforecomp}  -  image after compression = {imagesizeafter5}");

//             foreach (KeyValuePair<int, double> kvp in compressionRatios)
//             {
//                 Console.WriteLine($"Key = {kvp.Key}, Value = {kvp.Value}");
//             }

//         }
//         static void Ex2V2(string imagepath, string outputpath)
//         {
//             int[] qualityLevels = new int[] { 50, 25, 15, 5, 0 };
//             long[] sizes = new long[qualityLevels.Length];
//             double[] compressionRatios = new double[qualityLevels.Length];
//             Bitmap originalImage = new Bitmap(imagepath);
//             for (int i = 0; i < qualityLevels.Length; i++)
//             {
//                 string outputPath = $"{outputpath}_{qualityLevels[i]}.jpg";
//                 SaveJpegWithQuality(originalImage, outputPath, qualityLevels[i]);
//                 FileInfo fileInfo = new FileInfo(outputPath);
//                 sizes[i] = fileInfo.Length;
//             }
//             long originalSize = new FileInfo(imagepath).Length;
//             for (int i = 0; i < sizes.Length; i++)
//             {
//                 compressionRatios[i] = (double)originalSize / sizes[i];
//             }

//             OxyPlot.PlotModel model = new OxyPlot.PlotModel { Title = "Quality vs Compression Ratio" };
//             var series = new LineSeries();
//             for (int i = 0; i < qualityLevels.Length; i++)
//             {
//                 series.Points.Add(new OxyPlot.DataPoint(qualityLevels[i], compressionRatios[i]));
//             }
//             model.Series.Add(series);
//             for (int i = 0; i < compressionRatios.Length; i++)
//             {
//                 System.Console.WriteLine($"{i + 1} : {compressionRatios[i]}");

//             }
//             // var pngexp=new OxyPlot.Wpf.PngExporter(Width=1200,Height=800)


//         }

//         private static void SaveJpegWithQuality(Bitmap image, string outputPath, int quality)
//         {
//             EncoderParameter qualityParam = new EncoderParameter(Encoder.Quality, quality);
//             ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");
//             EncoderParameters encoderParams = new EncoderParameters(1);
//             encoderParams.Param[0] = qualityParam;
//             image.Save(outputPath, jpegCodec, encoderParams);
//         }


//         private static ImageCodecInfo GetEncoderInfo(string mimeType)
//         {
//             ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
//             for (int i = 0; i < codecs.Length; i++)
//             {
//                 if (codecs[i].MimeType == mimeType)
//                     return codecs[i];
//             }
//             return null;
//         }
//     }

// }

