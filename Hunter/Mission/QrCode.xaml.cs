﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Xaml.Interactivity;
using Behaviors;
using ZXing;
using Windows.Media.Capture;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Storage.FileProperties;
using Windows.Media.MediaProperties;
using System.Diagnostics;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Core;
using Windows.Devices.Enumeration;


// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Hunter.Mission
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    /// 
    public sealed partial class QrCode : Page
    {
        private Result _result;
        private readonly MediaCapture _mediaCapture = new MediaCapture();
        private DispatcherTimer _timer;
        private bool IsBusy;
        public QrCode()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            InitVideoCapture();
            InitVideoTimer();
        }

        private void InitVideoTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(3);
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }
        private async void _timer_Tick(object sender, object e)
        {
            try
            {
                Debug.WriteLine(@"[INFO]开始扫描 -> " + DateTime.Now.ToString());
                if (!IsBusy)
                {
                    IsBusy = true;
                    IRandomAccessStream stream = new InMemoryRandomAccessStream();
                    await _mediaCapture.CapturePhotoToStreamAsync(ImageEncodingProperties.CreateJpeg(), stream);

                    var writeableBmp = await ReadBitmap(stream, ".jpg");

                    await Task.Factory.StartNew(async () => { await ScanBitmap(writeableBmp); });
                }
                IsBusy = false;
                await Task.Delay(50);
            }
            catch (Exception)
            {
                IsBusy = false;
            }
        }
        private static async Task ReencodeAndSavePhotoAsync(IRandomAccessStream stream, string fileName, PhotoOrientation photoOrientation = PhotoOrientation.Normal)
        {
            using (var inputStream = stream)
            {
                var decoder = await BitmapDecoder.CreateAsync(inputStream);

                var file = await KnownFolders.PicturesLibrary.CreateFileAsync(fileName, CreationCollisionOption.GenerateUniqueName);

                using (var outputStream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    var encoder = await BitmapEncoder.CreateForTranscodingAsync(outputStream, decoder);

                    // Set the orientation of the capture
                    var properties = new BitmapPropertySet { { "System.Photo.Orientation", new BitmapTypedValue(photoOrientation, PropertyType.UInt16) } };
                    await encoder.BitmapProperties.SetPropertiesAsync(properties);

                    await encoder.FlushAsync();
                }
            }
        }
        static Guid DecoderIDFromFileExtension(string strExtension)
        {
            Guid encoderId;
            switch (strExtension.ToLower())
            {
                case ".jpg":
                case ".jpeg":
                    encoderId = BitmapDecoder.JpegDecoderId;
                    break;
                case ".bmp":
                    encoderId = BitmapDecoder.BmpDecoderId;
                    break;
                case ".png":
                default:
                    encoderId = BitmapDecoder.PngDecoderId;
                    break;
            }
            return encoderId;
        }
        public static Size MaxSizeSupported = new Size(4000, 3000);
        public async static Task<WriteableBitmap> ReadBitmap(IRandomAccessStream fileStream, string type)
        {
            WriteableBitmap bitmap = null;
            try
            {
                Guid decoderId = DecoderIDFromFileExtension(type);

                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(decoderId, fileStream);
                BitmapTransform tf = new BitmapTransform();

                uint width = decoder.OrientedPixelWidth;
                uint height = decoder.OrientedPixelHeight;
                double dScale = 1;

                if (decoder.OrientedPixelWidth > MaxSizeSupported.Width || decoder.OrientedPixelHeight > MaxSizeSupported.Height)
                {
                    dScale = Math.Min(MaxSizeSupported.Width / decoder.OrientedPixelWidth, MaxSizeSupported.Height / decoder.OrientedPixelHeight);
                    width = (uint)(decoder.OrientedPixelWidth * dScale);
                    height = (uint)(decoder.OrientedPixelHeight * dScale);

                    tf.ScaledWidth = (uint)(decoder.PixelWidth * dScale);
                    tf.ScaledHeight = (uint)(decoder.PixelHeight * dScale);
                }


                bitmap = new WriteableBitmap((int)width, (int)height);



                PixelDataProvider dataprovider = await decoder.GetPixelDataAsync(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight, tf,
                ExifOrientationMode.RespectExifOrientation, ColorManagementMode.DoNotColorManage);
                byte[] pixels = dataprovider.DetachPixelData();

                Stream pixelStream2 = bitmap.PixelBuffer.AsStream();

                pixelStream2.Write(pixels, 0, pixels.Length);
                //bitmap.SetSource(fileStream);
            }
            catch
            {
            }

            return bitmap;
        }
        private async Task ScanBitmap(WriteableBitmap writeableBmp)
        {
            try
            {
                var barcodeReader = new BarcodeReader
                {
                    AutoRotate = true,
                    Options = new ZXing.Common.DecodingOptions { TryHarder = true }
                };
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    _result = barcodeReader.Decode(writeableBmp);
                });



                if (_result != null)
                {
                    Debug.WriteLine(@"[INFO]扫描到二维码:{result}   ->" + _result.Text);
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        tbkResult.Text = _result.Text;
                    });
                }
            }
            catch (Exception)
            {
            }
        }
        private async void InitVideoCapture()
        {
            ///摄像头的检测
            var cameraDevice = await FindCameraDeviceByPanelAsync(Windows.Devices.Enumeration.Panel.Back);

            if (cameraDevice == null)
            {
                Debug.WriteLine("No camera device found!");
                return;
            }
            var settings = new MediaCaptureInitializationSettings
            {
                StreamingCaptureMode = StreamingCaptureMode.Video,
                MediaCategory = MediaCategory.Other,
                AudioProcessing = Windows.Media.AudioProcessing.Default,
                VideoDeviceId = cameraDevice.Id
            };
            await _mediaCapture.InitializeAsync(settings);
            VideoCapture.Source = _mediaCapture;
            await _mediaCapture.StartPreviewAsync();
        }

        private static async Task<DeviceInformation> FindCameraDeviceByPanelAsync(Windows.Devices.Enumeration.Panel desiredPanel)
        {
            var allVideoDevices = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);

            DeviceInformation desiredDevice = allVideoDevices.FirstOrDefault(x => x.EnclosureLocation != null && x.EnclosureLocation.Panel == desiredPanel);

            return desiredDevice ?? allVideoDevices.FirstOrDefault();
        }
    }
}
