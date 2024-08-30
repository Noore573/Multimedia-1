using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using NAudio;
using NAudio.Wave;
using NAudio.Gui;
using NAudio.Wave.SampleProviders;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using OxyPlot.Axes;
using MathNet.Numerics.IntegralTransforms;
using System.Numerics;

using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Imaging.ComplexFilters;
using AForge;
using System.Collections.Generic;
using System.Net;

namespace Lec6
{
    class Lec6Compression
    {
        public static void Main(string[] args)
        {
            string videopath = "D:/newtest/Win7sample.mp4";
            string videopath1 = "D:/newtest/write.mp4";
            string videopath2 = "D:/newtest/videotest.mp4";
            // string audiopath2 = "D:/newtest/piano - Copy.wav";
            // string imagepath1 = "D:/newtest/grayscale.jpg";


            System.Console.WriteLine("Index 1: Read Video");
            System.Console.WriteLine("Index 2: Read Frame");
            System.Console.WriteLine("Index 3: Read Video Frame");
            System.Console.WriteLine("Index 4: Video Writer");

            int Choice = Convert.ToInt32(Console.ReadLine());
            switch (Choice)
            {
                case 1:
                    ReadVideo1(videopath2);
                    break;
                case 2:
                    ReadFrame(videopath2);
                    break;
                case 3:
                    ReadVideoFrme(videopath2);
                    break;
                case 4:
                    VideoWriter1(videopath1);
                    break;


            }
        }
        static void ReadVideo1(string path)
        {
            VideoCapture videoCapture = new VideoCapture(path);
            // finding more info about the video
            int totalFrames = (int)videoCapture.Get(CapProp.FrameCount);
            // Get frame width
            int frameWidth = (int)videoCapture.Get(CapProp.FrameWidth);
            // Get frame height
            int frameHeight = (int)videoCapture.Get(CapProp.FrameHeight);
            // Get frames per second (FPS)
            double fps = videoCapture.Get(CapProp.Fps);
            System.Console.WriteLine($"Video info:\ntotalframes={totalFrames}\nframe width={frameWidth}\nframe height:{frameHeight}\nfps={fps}");
        }
        static void ReadFrame(string path)
        {
            VideoCapture videoCapture = new VideoCapture(path);
            // readin frame
            Mat frame = new Mat();
            while (videoCapture.Read(frame))
            {
                // Check if the frame is valid
                if (!frame.IsEmpty)
                {
                    Console.WriteLine(frame.Width);
                }
            }
        }
        static void ReadVideoFrme(string path)
        {
            VideoCapture videoCapture = new VideoCapture(path);


            // Read Video Frames Starting At Specific Time:
            // Specify the reading to begin 2.5 seconds from the beginning of the video
            double startTimeSeconds = 2.5;
            videoCapture.Set(CapProp.PosMsec, startTimeSeconds * 1000);
            // Create an axes object to display the frame
            Mat frame1 = new Mat();
            // Continue to read and display video frames until no more frames are available to read
            while (true)
            {
                // Read a frame from the video
                if (!videoCapture.Read(frame1))
                    break;
                // Display the frame
                CvInvoke.Imshow("Video Frame", frame1);
                // Wait for a short period (simulate frame rate)
                CvInvoke.WaitKey((int)(1000 / videoCapture.Get(CapProp.Fps)));
            }
            // Release resources
            CvInvoke.DestroyAllWindows();
            videoCapture.Dispose();



        }
        static void VideoWriter1(string path)
        {
            // Create VideoWriter Object and Write Video:
            // Load the image
            Mat image = CvInvoke.Imread("D://newtest/br.jpg");
            // Get the frame size from the image
            int frameWidth = image.Width;
            int frameHeight = image.Height;
            // Create a VideoWriter object to write the video
            int codec = VideoWriter.Fourcc('H', '2', '6', '4'); // Codec for MP4
            double fps = 30; // Frames per second
            using (VideoWriter videoWriter = new VideoWriter(path, codec, fps, new System.Drawing.Size(frameWidth,
            frameHeight), true))
            {
                // Check if the VideoWriter object is initialized successfully
                if (!videoWriter.IsOpened)
                {
                    Console.WriteLine("Failed to create VideoWriter.");
                    return;
                }
                // Write the same image frame to the video multiple times (e.g., 100 frames)
                int numFrames = 100;
                for (int i = 0; i < numFrames; i++)
                {
                    videoWriter.Write(image);
                }
                Console.WriteLine("Video with one image frame has been created successfully.");

            }


        }
    }
}