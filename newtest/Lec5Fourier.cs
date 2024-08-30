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






// namespace Lec4
// {
//     class Lec5Fourier
//     {
//         public static void Main(string[] args)
//         {
//             string audiopath = "D:/newtest/guitar - Copy.wav";
//             string audiopath2 = "D:/newtest/piano - Copy.wav";
//             string imagepath1 = "D:/newtest/grayscale.jpg";


//             System.Console.WriteLine("Index 1: Ploting the audio");
//             System.Console.WriteLine("Index 2: FFT");
//             System.Console.WriteLine("Index 3: FFTShift");
//             System.Console.WriteLine("Index 4: FFT image");
//             System.Console.WriteLine("Index 5: FFT image log  ");
//             System.Console.WriteLine("Index 6: EX");

//             int Choice = Convert.ToInt32(Console.ReadLine());
//             switch (Choice)
//             {
//                 case 1:
//                     PlotingAudio(audiopath);
//                     break;
//                 case 2:
//                     FFT(audiopath);
//                     break;
//                 case 3:
//                     FFTShift(audiopath);
//                     break;
//                 case 4:
//                     FFTImage(imagepath1);
//                     break;
//                 case 5:
//                     FFTImageLog(imagepath1);
//                     break;
//                 case 6:
//                     Ex(imagepath1);
//                     break;


//             }

//             static void PlotingAudio(string samples)
//             {
//                 var plot = new PlotModel { Title = "Original Audio Waveform" };
//                 var series = new LineSeries();
//                 var audioFileReader1 = new AudioFileReader(samples);
//                 int samplerate = audioFileReader1.WaveFormat.SampleRate;
//                 System.Console.WriteLine(samplerate);
//                 for (int i = 0; i < samples.Length; i++)
//                 {
//                     series.Points.Add(new DataPoint(i/(float)samplerate, samples[i]));
//                 }
//                 plot.Series.Add(series);
//                 // Display the plot
//                 var plotView = new PlotView
//                 {
//                     Dock = DockStyle.Fill,
//                     Model = plot
//                 };
//                 var form = new Form();
//                 form.Controls.Add(plotView);
//                 Application.Run(form);

//             }
//         }
//         static void FFT(string audioFile2)
//         {
//             // Read an audio file, then apply FFT function on audio signal
//             WaveFileReader reader = new WaveFileReader(audioFile2);
//             // Read audio data into buffer
//             byte[] buffer = new byte[reader.Length];
//             reader.Read(buffer, 0, (int)reader.Length);
//             // Convert bytes to float samples
//             float[] samples = new float[buffer.Length / 2];
//             for (int i = 0; i < buffer.Length / 2; i++)
//             {
//                 short sample = BitConverter.ToInt16(buffer, i * 2);
//                 samples[i] = sample / 32768f;
//             }
//             // Perform FFT
//             Complex[] fft = new Complex[samples.Length];
//             for (int i = 0; i < samples.Length; i++)
//             {
//                 fft[i] = new Complex(samples[i], 0);
//             }
//             Fourier.Forward(fft);
//             var plot = new PlotModel { Title = "Audio Signal after FFT" };

//             // Create a line series to represent the FFT result
//             var series = new LineSeries();

//             // Add data points to the line series
//             for (int i = 0; i < fft.Length; i++)
//             {
//                 // Assuming you want to plot magnitude of FFT
//                 double magnitude = fft[i].Magnitude;
//                 series.Points.Add(new DataPoint(i, magnitude));
//             }

//             // Add the line series to the plot model
//             plot.Series.Add(series);

//             // Display the plot
//             var plotView = new OxyPlot.WindowsForms.PlotView
//             {
//                 Dock = System.Windows.Forms.DockStyle.Fill,
//                 Model = plot
//             };
//             var form = new System.Windows.Forms.Form();
//             form.Controls.Add(plotView);
//             System.Windows.Forms.Application.Run(form);

//         }
//         static void FFTShift(string audioFile2)
//         {
//             // Read an audio file, then apply FFT function on audio signal
//             WaveFileReader reader = new WaveFileReader(audioFile2);
//             // Read audio data into buffer
//             byte[] buffer = new byte[reader.Length];
//             reader.Read(buffer, 0, (int)reader.Length);
//             // Convert bytes to float samples
//             float[] samples = new float[buffer.Length / 2];
//             for (int i = 0; i < buffer.Length / 2; i++)
//             {
//                 short sample = BitConverter.ToInt16(buffer, i * 2);
//                 samples[i] = sample / 32768f;
//             }
//             // Perform FFT
//             Complex[] fft = new Complex[samples.Length];
//             for (int i = 0; i < samples.Length; i++)
//             {
//                 fft[i] = new Complex(samples[i], 0);
//             }
//             Fourier.Forward(fft);
//             // Apply fftshift to the FFT result
//             Complex[] shiftedFFT = FFTShift(fft);

//             // Create a new plot model
//             var plot = new PlotModel { Title = "FFT Shift of Audio Signal" };

//             // Create a line series to plot the shifted FFT result
//             var series = new LineSeries();

//             // Add data points to the series
//             for (int i = 0; i < shiftedFFT.Length; i++)
//             {
//                 series.Points.Add(new DataPoint(i, shiftedFFT[i].Magnitude));
//             }

//             // Add the series to the plot model
//             plot.Series.Add(series);

//             // Create a plot view and set its model
//             var plotView = new PlotView
//             {
//                 Dock = System.Windows.Forms.DockStyle.Fill,
//                 Model = plot
//             };

//             // Create a form and add the plot view to it
//             var form = new System.Windows.Forms.Form();
//             form.Controls.Add(plotView);

//             // Run the application
//             System.Windows.Forms.Application.Run(form);
//         }

//         // Function to apply fftshift to the FFT result
//         static Complex[] FFTShift(Complex[] fft)
//         {
//             int n = fft.Length;
//             Complex[] shiftedFFT = new Complex[n];

//             // Compute the index for shifting
//             int shiftIndex = n / 2;

//             // Shift the FFT result
//             for (int i = 0; i < n; i++)
//             {
//                 shiftedFFT[i] = fft[(i + shiftIndex) % n];
//             }

//             return shiftedFFT;
//         }

//         static void FFTImage(string imagePath)
//         {
//             // Read an image in gray scale, then apply FFT on image signal
//             Bitmap originalImage = new Bitmap(imagePath);
//             // Calculate the nearest power of 2 for width and height
//             int newWidth = (int)Math.Pow(2, Math.Ceiling(Math.Log(originalImage.Width, 2)));
//             int newHeight = (int)Math.Pow(2, Math.Ceiling(Math.Log(originalImage.Height, 2)));

//             // Resize the image
//             ResizeBilinear resizeFilter = new ResizeBilinear(newWidth, newHeight);
//             Bitmap image = resizeFilter.Apply(originalImage);
//             // Convert the image to grayscale
//             Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);
//             Bitmap grayImage = filter.Apply(image);
//             // Apply FFT to the grayscale image
//             ComplexImage complexImage = ComplexImage.FromBitmap(grayImage);
//             complexImage.ForwardFourierTransform();
//             // Compute the magnitude spectrum for visualization
//             Bitmap magnitudeImage = complexImage.ToBitmap();
//             // Display or save the magnitude spectrum
//             magnitudeImage.Save("D:/newtest/magnitude_spectrum.jpg");

//         }
//         static void FFTImageLog(string imagePath)
//         {
//             // Read an image in gray scale, then apply FFT on image signal
//             Bitmap originalImage = new Bitmap(imagePath);
//             // Calculate the nearest power of 2 for width and height
//             int newWidth = (int)Math.Pow(2, Math.Ceiling(Math.Log(originalImage.Width, 2)));
//             int newHeight = (int)Math.Pow(2, Math.Ceiling(Math.Log(originalImage.Height, 2)));

//             // Resize the image
//             ResizeBilinear resizeFilter = new ResizeBilinear(newWidth, newHeight);
//             Bitmap image = resizeFilter.Apply(originalImage);
//             // Convert the image to grayscale
//             Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);
//             Bitmap grayImage = filter.Apply(image);
//             // Apply FFT to the grayscale image
//             ComplexImage complexImage = ComplexImage.FromBitmap(grayImage);
//             complexImage.ForwardFourierTransform();
//             // Compute the magnitude spectrum for visualization
//             Bitmap logMagnitudeImage = LogTransform(complexImage.ToBitmap());
//             // Display or save the magnitude spectrum
//             logMagnitudeImage.Save("D:/newtest/log_magnitude_spectrum.jpg");

//         }
//         static Bitmap LogTransform(Bitmap image)
//         {
//             // Create a blank bitmap with the same dimensions
//             Bitmap logImage = new Bitmap(image.Width, image.Height);

//             // Iterate over the pixels in the image
//             for (int x = 0; x < image.Width; x++)
//             {
//                 for (int y = 0; y < image.Height; y++)
//                 {
//                     // Get the pixel value
//                     Color pixel = image.GetPixel(x, y);

//                     // Compute the log transform
//                     int logTransform = (int)(255 * Math.Log(1 + pixel.R));

//                     // Ensure the result is within the valid range for a pixel value
//                     logTransform = Math.Max(0, Math.Min(255, logTransform));

//                     // Set the pixel in the new image
//                     logImage.SetPixel(x, y, Color.FromArgb(logTransform, logTransform, logTransform));
//                 }
//             }

//             // Return the transformed image
//             return logImage;
//         }
//         static void Ex(string imagePath)
//         {
//             // Load the image
//             Bitmap image = new Bitmap(imagePath);

//             // Resize the image to the nearest power of 2 dimensions
//             int width = (int)Math.Pow(2, Math.Ceiling(Math.Log(image.Width) / Math.Log(2)));
//             int height = (int)Math.Pow(2, Math.Ceiling(Math.Log(image.Height) / Math.Log(2)));
//             ResizeBilinear resizeFilter = new ResizeBilinear(width, height);
//             Bitmap resizedImage = resizeFilter.Apply(image);

//             // Convert to grayscale
//             Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);
//             Bitmap grayImage = filter.Apply(resizedImage);

//             // Apply FFT
//             ComplexImage complexImage = ComplexImage.FromBitmap(grayImage);
//             complexImage.ForwardFourierTransform();

//             // Create a low pass filter
//             FrequencyFilter frequencyFilter = new FrequencyFilter(new IntRange(0, 50)); // Adjust the range as needed

//             // Apply the low pass filter
//             frequencyFilter.Apply(complexImage);

//             // Apply IFFT
//             complexImage.BackwardFourierTransform();

//             // Get the filtered image
//             Bitmap filteredImage = complexImage.ToBitmap();

//             // Display or save the filtered image
//             filteredImage.Save("D:/newtest/filtered_image.jpg");

//         }
//     }

// }

