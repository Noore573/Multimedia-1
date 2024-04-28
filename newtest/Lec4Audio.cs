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




namespace Lec4
{
    class Lec4Audio
    {
        public static void Main(string[] args)
        {
            System.Console.WriteLine("Nfef");

            string audiopath = "D:/newtest/sample-12s.wav";
            string audiopath2 = "D:/newtest/aaa.mp3";
            string audiopath3 = "D:/newtest/sample-12s.wav";
            string audiopath4 = "D:/newtest/vader.wav";
            string audiopath5 = "D:/newtest/imperial_march.wav";
            string audiooutput = "D:/newtest/";
            System.Console.WriteLine("Index 1: Dealing with audio");
            System.Console.WriteLine("Index 2: PlayBack Test");
            System.Console.WriteLine("Index 3: Modify");
            System.Console.WriteLine("Index 4: DrawSignal");
            System.Console.WriteLine("Index 5: Record Audio ");
            System.Console.WriteLine("Index 6: Merge");
            System.Console.WriteLine("Index 7: EX");

            int Choice = Convert.ToInt32(Console.ReadLine());
            switch (Choice)
            {
                case 1:
                    DealingWithAudio(audiopath2, audiooutput);
                    break;
                case 2:
                    PlayBack(audiopath2);
                    break;
                case 3:
                    Modify(audiopath2, audiooutput, 50);
                    break;
                case 4:
                    DrawSignal(audiopath);
                    break;
                case 5:
                    RecordAudio2(audiooutput);
                    break;
                case 6:
                    Merge(audiopath4, audiopath5, audiooutput);
                    break;
                case 7:
                    Ex(audiopath, audiooutput);
                    break;
            }

        }
        static void DealingWithAudio(string audioFile, string audiooutput)
        {
            // Reading audio file
            var audioFileReader = new AudioFileReader(audioFile);
            // Writing audio file
            WaveFileWriter.CreateWaveFile(audiooutput + "fil.wav", audioFileReader);
            // Playing an audio
            using (var outputDevice = new WaveOutEvent())
            {
                PlayBack(audioFile);
            }

            // file info
            using (var audioFileReader1 = new AudioFileReader(audioFile))
            {
                // Get information about the audio file
                TimeSpan duration = audioFileReader1.TotalTime;
                int sampleRate = audioFileReader1.WaveFormat.SampleRate;
                int channels = audioFileReader1.WaveFormat.Channels;
                // Display the information
                Console.WriteLine($"Duration: {duration}");
                Console.WriteLine($"Sample Rate: {sampleRate}");
                Console.WriteLine($"Channels: {channels}");
            }
        }
        static void Modify(string audioFile, string outputAudioFile, float scaleFactor)
        {
            using (var audioFileReader = new AudioFileReader(audioFile))
            {
                // Create a new buffer to store scaled audio data
                float[] buffer = new float[audioFileReader.Length];
                // Read audio data into the buffer
                int samplesRead = audioFileReader.Read(buffer, 0, buffer.Length);
                // Scale each sample in the buffer
                for (int i = 0; i < samplesRead; i++)
                {
                    buffer[i] *= scaleFactor;
                }
                // Create a new WaveFileWriter to write scaled audio data to a new file
                using (var waveFileWriter = new WaveFileWriter(outputAudioFile + "Modified.wav", audioFileReader.WaveFormat))
                {
                    // Write the scaled audio data to the output file
                    byte[] byteBuffer = new byte[samplesRead * sizeof(float)];
                    Buffer.BlockCopy(buffer, 0, byteBuffer, 0, byteBuffer.Length);
                    waveFileWriter.Write(byteBuffer, 0, byteBuffer.Length);
                }
            }

        }
        static void DrawSignal(string audioFile)
        {
            WaveViewer waveViewer = new WaveViewer();
            waveViewer.Dock = DockStyle.Fill;
            waveViewer.SamplesPerPixel = 400;
            waveViewer.StartPosition = 10000;
            // Load the audio file and set it as the WaveStream for the WaveViewer
            WaveFileReader waveFileReader = new WaveFileReader(audioFile);
            waveViewer.WaveStream = waveFileReader;
            // Create and configure the form
            Form form = new Form();
            form.Text = "WaveViewer Example";
            form.Controls.Add(waveViewer);
            form.ClientSize = new System.Drawing.Size(800, 600); // Set the form size
            form.ShowDialog();

        }
        static void RecordAudio(string outputAudioFile)
        {
            using (var waveIn = new WaveInEvent())
            {
                waveIn.WaveFormat = new WaveFormat(44100, 1); // 44100 Hz, mono
                WaveFileWriter waveFileWriter = null;
                waveIn.DataAvailable += (sender, e) =>
                {
                    // Initialize the WaveFileWriter on the first call to the DataAvailable event
                    if (waveFileWriter == null)
                    {
                        waveFileWriter = new WaveFileWriter(outputAudioFile + "Rec.wav", waveIn.WaveFormat);
                    }
                    // Write the recorded audio data to the WAV file
                    waveFileWriter.Write(e.Buffer, 0, e.BytesRecorded);
                };
                // Recording Audio File
                waveIn.StartRecording();
                Console.WriteLine("Recording. Press any key to stop...");
                // Console.ReadKey();
                if (Console.IsInputRedirected)
                {
                    // Handle redirection scenario (e.g., display a message)
                    Console.WriteLine("Cannot read keys when input is redirected.");
                    // You might want to wait for a keypress or perform another action here
                }
                else
                {
                    // Normal scenario: Read key from console
                    Console.WriteLine("Recording. Press any key to stop...");
                    Console.ReadKey();
                }


                waveIn.StopRecording();
                // Close the WaveFileWriter after recording is stopped
                waveFileWriter?.Dispose();
                Console.WriteLine("Recording stopped. Audio saved to: " + outputAudioFile);
            }

        }
        static void RecordAudio2(string outputAudioFile)
        {
            using (var waveIn = new WaveInEvent())
            {
                waveIn.WaveFormat = new WaveFormat(44100, 1); // 44100 Hz, mono
                WaveFileWriter waveFileWriter = null;
                waveIn.DataAvailable += (sender, e) =>
                {
                    // Initialize the WaveFileWriter on the first call to the DataAvailable event
                    if (waveFileWriter == null)
                    {
                        waveFileWriter = new WaveFileWriter(outputAudioFile + "Rec.wav", waveIn.WaveFormat);
                    }
                    // Write the recorded audio data to the WAV file
                    waveFileWriter.Write(e.Buffer, 0, e.BytesRecorded);
                };

                // Recording Audio File
                waveIn.StartRecording();
                Console.WriteLine("Recording for 10 seconds...");
                Task.Delay(10000).Wait(); // Record for 10 seconds
                waveIn.StopRecording();

                // Close the WaveFileWriter after recording is stopped
                waveFileWriter?.Dispose();
                Console.WriteLine("Recording stopped. Audio saved to: " + outputAudioFile);
            }
        }
        static void Merge(string audioFile1, string audioFile2, string outputAudioFile)
        {
            using (var reader1 = new AudioFileReader(audioFile1))
            using (var reader2 = new AudioFileReader(audioFile2))
            {
                var mixer = new MixingSampleProvider(new[] { reader1, reader2});
                WaveFileWriter.CreateWaveFile16(outputAudioFile + "merged.wav", mixer);
            }

        }
        static void pl(double[] reversedBuffer, string outputFile)
        {
            var model = new PlotModel { Title = "Reversed Audio Signal" };

            // Set up the axes
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Sample" });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Amplitude" });

            // Create a LineSeries to hold the reversed audio data
            var series = new LineSeries();

            // Assume 'reversedBuffer' is an array containing your reversed audio data
            for (int i = 0; i < reversedBuffer.Length; i++)
            {
                // Add each point of the reversed audio signal to the series
                series.Points.Add(new DataPoint(i, reversedBuffer[i]));
            }

            // Add the series to the PlotModel
            model.Series.Add(series);

            // Create a PlotView to display the model and add it to your form or window
            var plotView = new PlotView
            {
                Model = model,
                Dock = System.Windows.Forms.DockStyle.Fill
            };
            System.Console.WriteLine("plotting");
            // // Add the PlotView to the display container (e.g., a form)
            // this.Controls.Add(plotView);
            // Render the plot to a bitmap
            var pngExporter = new PngExporter { Width = 600, Height = 400, };
            var bitmap = pngExporter.ExportToBitmap(model);

            // Save the bitmap to a file
            bitmap.Save(outputFile + "ReversedAudioSignal.png", System.Drawing.Imaging.ImageFormat.Png);
        }
        static void ReverseAudioSignal(string inputFile, string outputFile)
        {
            using (var reader = new WaveFileReader(inputFile))
            {
                var samples = new byte[reader.Length];
                reader.Read(samples, 0, samples.Length);

                Array.Reverse(samples);

                using (var writer = new WaveFileWriter(outputFile + "Reversed.wav", reader.WaveFormat))
                {
                    writer.Write(samples, 0, samples.Length);
                }
            }
            // double[] audioData = GetAudioData(outputFile + "Reversed.wav");
            double[] audioData = GetReversedBuffer(outputFile + "Reversed.wav");
            // PlotAudioSignal(audioData);
            // pl(audioData, outputFile);
            DrawSignal(inputFile);
        }
        static void ReverseAudioChannels(string inputFile, string outputFile)
        {
            using (var reader = new WaveFileReader(inputFile))
            {
                var samples = new byte[reader.Length];
                reader.Read(samples, 0, samples.Length);

                var channels = reader.WaveFormat.Channels;
                var bytesPerSample = reader.WaveFormat.BitsPerSample / 8;

                if (channels == 2) // Only process stereo audio
                {
                    for (int i = 0; i < samples.Length; i += channels * bytesPerSample)
                    {
                        // Swap left and right channels
                        for (int j = 0; j < bytesPerSample; j++)
                        {
                            byte temp = samples[i + j];
                            samples[i + j] = samples[i + bytesPerSample + j];
                            samples[i + bytesPerSample + j] = temp;
                        }
                    }
                }

                using (var writer = new WaveFileWriter(outputFile + "RevAudioChannel.wav", reader.WaveFormat))
                {
                    writer.Write(samples, 0, samples.Length);
                }
            }
        }
        static void PlotAudioSignal(double[] audioData)
        {
            // Create a new PlotModel
            var plotModel = new PlotModel { Title = "Reversed Audio Signal" };

            // Create a LineSeries to represent the audio signal
            var lineSeries = new LineSeries();

            // Add data points to the LineSeries
            for (int i = 0; i < audioData.Length; i++)
            {
                lineSeries.Points.Add(new DataPoint(i, audioData[i]));
            }

            // Add the LineSeries to the PlotModel
            plotModel.Series.Add(lineSeries);

            // Customize the plot appearance if needed
            // e.g., plotModel.DefaultColors = new List<OxyColor> { OxyColors.Red };

            // Display the plot
            OxyPlot.WindowsForms.PlotView plotView = new OxyPlot.WindowsForms.PlotView();
            plotView.Model = plotModel;
            plotView.Dock = System.Windows.Forms.DockStyle.Fill;

            var form = new System.Windows.Forms.Form();
            form.Size = new System.Drawing.Size(800, 600);
            form.Controls.Add(plotView);
            form.ShowDialog();

        }
        static double[] GetAudioData(string inputFile)
        {
            using (var reader = new WaveFileReader(inputFile))
            {
                int bytesPerSample = reader.WaveFormat.BitsPerSample / 8;
                int bytesPerBlock = reader.WaveFormat.BlockAlign; // Get block alignment
                int samplesPerBlock = bytesPerBlock / bytesPerSample;
                int sampleCount = (int)(reader.Length / bytesPerBlock) * samplesPerBlock; // Adjusted sample count
                double[] audioData = new double[sampleCount];

                byte[] buffer = new byte[bytesPerBlock];
                int offset = 0;

                while (reader.Position < reader.Length)
                {
                    // Read a complete block of samples from the WAV file
                    int bytesRead = reader.Read(buffer, 0, bytesPerBlock);

                    // Convert byte buffer to doubles
                    for (int i = 0; i < bytesRead / bytesPerSample; i++)
                    {
                        if (bytesPerSample == 2) // 16-bit audio
                        {
                            short sampleValue = BitConverter.ToInt16(buffer, i * bytesPerSample);
                            audioData[offset] = sampleValue / (double)short.MaxValue;
                        }
                        else if (bytesPerSample == 4) // 32-bit audio
                        {
                            float sampleValue = BitConverter.ToSingle(buffer, i * bytesPerSample);
                            audioData[offset] = sampleValue;
                        }
                        // Add support for other sample formats if needed

                        offset++;
                    }
                }

                return audioData;
            }
        }
        static double[] GetReversedBuffer(string filePath)
        {
            using var reader = new WaveFileReader(filePath);

            // Determine the number of samples
            int sampleCount = (int)reader.SampleCount;
            double[] buffer = new double[sampleCount];

            // Read samples into the buffer
            for (int i = 0; i < sampleCount; i++)
            {
                buffer[i] = reader.ReadNextSampleFrame()[0] / 32768.0; // Normalize to range [-1, 1]
            }

            // Reverse the buffer
            Array.Reverse(buffer);

            return buffer;
        }




        static void Ex(string audioFile, string outputAudioFile)
        {
            ReverseAudioSignal(audioFile, outputAudioFile);
            ReverseAudioChannels(audioFile, outputAudioFile);

            // WaveFileWriter.CreateWaveFile(audiooutput + "fil.wav", audioFileReader);


        }





        static void PlayBack(string audiopath)
        {
            using (var audioFile = new AudioFileReader(audiopath))
            {
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();
                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                }
            }

        }

    }
}

