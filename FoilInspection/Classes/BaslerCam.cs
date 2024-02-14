using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Basler.Pylon;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace FoilInspection
{
    public class BaslerCam : Control
    {
        public Camera Device;
        PixelDataConverter converter = new PixelDataConverter();
        int count = 0;
        public Queue<Bitmap> BitmapQueue = new Queue<Bitmap>();
        public event EventHandler<Bitmap> BitmapRecievedEvent;
       
        public string srNum;
        List<ICameraInfo> allCameras = CameraFinder.Enumerate();
        public int triggerCount = 0;

        // Updates the list of available camera devices.
        public void UpdateDeviceList()
        {
            try
            {
                // Ask the camera finder for a list of camera devices.
                MessageBox.Show(allCameras.Count.ToString());

                // Loop over all cameras found.
                foreach (ICameraInfo cameraInfo in allCameras)
                {
                    // Loop over all cameras in the list of cameras.
                    Console.WriteLine($"This is camera full name {cameraInfo[CameraInfoKey.SerialNumber]}");
                }

            }
            catch (Exception exception)
            {
                ShowException(exception);
            }
        }

        public bool OpenCamera(string serialNum)
        {
           
            try
            {
                ConsoleExtension.WriteWithColor("opening camera", ConsoleColor.Magenta);
                //backgroundWorkerProcessImage.DoWork += BackgroundWorker_DoWork;
                //backgroundWorkerDisplayImage.DoWork += BackgroundWorker_DoWork;
                Device = new Camera(serialNum);
                srNum = serialNum;
                //BitmapRecievedEvent += BaslerCam_BitmapRecievedEvent;
                Device.CameraOpened += Configuration.AcquireContinuous;

                // Register for the events of the image provider needed for proper operation.
                Device.ConnectionLost += OnConnectionLost;
                Device.CameraOpened += OnCameraOpened;
                Device.CameraClosed += OnCameraClosed;
                //camera.StreamGrabber.GrabStarted += OnGrabStarted;
                Device.StreamGrabber.ImageGrabbed += OnImageGrabbed;
                Device.StreamGrabber.GrabStopped += OnGrabStopped;


                //Open the connection to the camera device.
                Device.Open();
                Device.Parameters[PLCamera.UserSetSelector].SetValue(PLCamera.UserSetSelector.UserSet1);

                //camera.Parameters[PLTransportLayer.HeartbeatTimeout].TrySetValue(500, IntegerValueCorrection.Nearest);  // 1000 ms timeout
                Device.Parameters[PLCamera.TriggerSelector].SetValue(PLCamera.TriggerSelector.AcquisitionStart);
                Device.Parameters[PLCamera.AcquisitionFrameCount].SetValue(7);
                Device.Parameters[PLCamera.TriggerMode].SetValue(PLCamera.TriggerMode.On);
                Device.Parameters[PLCamera.TriggerSource].SetValue(PLCamera.TriggerSource.Line1);
                Device.Parameters[PLCamera.TriggerActivation].SetValue(PLCamera.TriggerActivation.RisingEdge);
               
                //camera.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.Continuous);
                Device.StreamGrabber.Start(GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
                ConsoleExtension.WriteWithColor($"Exposure for camera {srNum} is {Device.Parameters[PLCamera.ExposureTimeAbs].GetValue()}", ConsoleColor.Yellow);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception while opening camera {0}. {1}", serialNum, ex.Message);
                return false;
            }

        }

        private void BaslerCam_BitmapRecievedEvent(object sender, Bitmap bitmap)
        {
            //if (mode == 0)
            //{
            //    backgroundWorkerProcessImage.CancelAsync();
            //    if (!backgroundWorkerProcessImage.CancellationPending && !backgroundWorkerDisplayImage.IsBusy)
            //    {
            //        backgroundWorkerDisplayImage.RunWorkerAsync();
            //    }
            //}
            //else if(mode == 1)
            //{
            //    backgroundWorkerDisplayImage.CancelAsync();
            //    if (!backgroundWorkerDisplayImage.CancellationPending && !backgroundWorkerProcessImage.IsBusy)
            //    {
            //        backgroundWorkerProcessImage.RunWorkerAsync();
            //    }
            //}

            //triggerCount++;
            //Console.WriteLine(bitmapQueue.Count);
        }

        Font myFont = new Font("Arial", 80);

        bool flag = false;

        int lap = 0;
        int timesImageDisplyed = 0;
        
        public void SetTriggerMode(bool freeRun)
        {
            try
            {
                if (freeRun)
                {
                    try
                    {
                        //camera.Parameters[PLCamera.TriggerSelector].SetValue(PLCamera.TriggerSelector.FrameStart);
                        //camera.Parameters[PLCamera.TriggerMode].SetValue(PLCamera.TriggerMode.Off);
                        Device.Parameters[PLCamera.TriggerSelector].SetValue(PLCamera.TriggerSelector.AcquisitionStart);
                        Device.Parameters[PLCamera.TriggerMode].SetValue(PLCamera.TriggerMode.Off);
                        Device.Parameters[PLCamera.TriggerSource].SetValue(PLCamera.TriggerSource.Software);
                    }
                    catch (NullReferenceException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        //camera.Parameters[PLCamera.TriggerSelector].SetValue(PLCamera.TriggerSelector.FrameStart);
                        //camera.Parameters[PLCamera.TriggerMode].SetValue(PLCamera.TriggerMode.Off);
                        Device.Parameters[PLCamera.TriggerSelector].SetValue(PLCamera.TriggerSelector.AcquisitionStart);
                        Device.Parameters[PLCamera.TriggerMode].SetValue(PLCamera.TriggerMode.On);
                        Device.Parameters[PLCamera.TriggerSource].SetValue(PLCamera.TriggerSource.Line1);
                        Device.Parameters[PLCamera.TriggerActivation].SetValue(PLCamera.TriggerActivation.RisingEdge);
                    }
                    catch (NullReferenceException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                //ShowException(exception);
            }
        }



        //public void SetTriggerMode(bool value)
        //{
        //    try
        //    {
        //        if (value)
        //        {
        //            camera.Parameters[PLCamera.TriggerMode].SetValue(PLCamera.TriggerMode.On);
        //        }
        //        else
        //        {
        //            camera.Parameters[PLCamera.TriggerMode].SetValue(PLCamera.TriggerMode.Off);
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        ShowException(exception);
        //    }
        //}
        public void OneShot()
        {
            try
            {
                Device.StreamGrabber.Stop();
                // Starts the grabbing of one image.
                Device.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.SingleFrame);
                Device.StreamGrabber.Start(1, GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
            }
            catch (Exception exception)
            {
                ShowException(exception);
            }
        }

        public void ContinuousShot()
        {
            try
            {
                Device.StreamGrabber.Stop();
                // Start the grabbing of images until grabbing is stopped.
                Device.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.Continuous);
                Device.StreamGrabber.Start(GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);

            }
            catch (Exception exception)
            {
                ShowException(exception);
            }
        }


        public void StopAcq()
        {
            try
            {
                Device.Parameters[PLCamera.AcquisitionStop].Execute();
                //camera.StreamGrabber.Stop();

            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine($"NullReferenceException in camera AcquisitionStop {ex.Message}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in camera AcquisitionStop {ex.Message}.");
            }

        }


        public void StartAcq()
        {
            try
            {
                //camera.StreamGrabber.Start(GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
                Device.Parameters[PLCamera.AcquisitionStart].Execute();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in camera AcquisitionStart {ex.Message}.");

                //ShowException(ex);
            }
        }


        private void OnConnectionLost(Object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                BeginInvoke(new EventHandler<EventArgs>(OnConnectionLost), sender, e);
                return;
            }
            Console.WriteLine("Connection lost.");
            // Close the camera object.
            //DestroyCamera();
            // Try to re-establish a connection to the camera device until timeout.
            // Reopening the camera triggers the above registered Configuration.AcquireContinous.
            // Therefore, the camera is parameterized correctly again.
            Device.Open(60000, TimeoutHandling.ThrowException);
            Device.Parameters[PLTransportLayer.HeartbeatTimeout].TrySetValue(500, IntegerValueCorrection.Nearest);  // 1000 ms timeout

            //ContinuousShot();

        }

        // Occurs when the connection to a camera device is opened.
        private void OnCameraOpened(Object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                BeginInvoke(new EventHandler<EventArgs>(OnCameraOpened), sender, e);
                return;
            }

            //The image provider is ready to grab. Enable the grab buttons.

        }


        // Occurs when the connection to a camera device is closed.
        private void OnCameraClosed(Object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                BeginInvoke(new EventHandler<EventArgs>(OnCameraClosed), sender, e);
                return;
            }

            // The camera connection is closed. Disable all buttons.
        }

        // Occurs when a camera starts grabbing.

        private void OnGrabStarted(Object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                BeginInvoke(new EventHandler<EventArgs>(OnGrabStarted), sender, e);
                return;
            }

            //while (camera.StreamGrabber.IsGrabbing)
            //{
            //    try
            //    {
            //        // Wait for an image and then retrieve it. A timeout of 5000 ms is used.
            //        IGrabResult grabResult = camera.StreamGrabber.RetrieveResult(5000, TimeoutHandling.ThrowException);
            //        using (grabResult)
            //        {
            //            // Image grabbed successfully?
            //            if (grabResult.GrabSucceeded)
            //            {
            //                // Display the grabbed image.
            //                Bitmap bitmap = new Bitmap(grabResult.Width, grabResult.Height, PixelFormat.Format24bppRgb);
            //                // Lock the bits of the bitmap.
            //                BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
            //                // Place the pointer to the buffer of the bitmap.
            //                converter.OutputPixelFormat = PixelType.BGR8packed;
            //                IntPtr ptrBmp = bmpData.Scan0;
            //                converter.Convert(ptrBmp, bmpData.Stride * bitmap.Height, grabResult);
            //                bitmap.UnlockBits(bmpData);

            //                Bitmap grabbedImageColor = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            //                //Console.WriteLine($"Grabbed image format {grabbedImageColor.PixelFormat}");

            //                if (bitmapQueue.Count < 4)
            //                {
            //                    bitmapQueue.Enqueue(grabbedImageColor);
            //                    BitmapRecievedEvent?.Invoke(this, bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), PixelFormat.Format24bppRgb));

            //               }

            //            }
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        // An exception occurred. Is it because the camera device has been physically removed?

            //        // Known issue: Wait until the system safely detects a possible removal.
            //        System.Threading.Thread.Sleep(50);

            //        if (!camera.IsConnected)
            //        {
            //            // Yes, the camera device has been physically removed.
            //            Console.WriteLine("The camera device has been removed. Please reconnect. (Timeout {0}s)", 60000 / 1000.0);

            //            // Close the camera object to close underlying resources used for the previous connection.
            //            camera.Close();

            //            // Try to re-establish a connection to the camera device until timeout.
            //            // Reopening the camera triggers the above registered Configuration.AcquireContinous.
            //            // Therefore, the camera is parameterized correctly again.
            //            //camera.Open(60000, TimeoutHandling.ThrowException);

            //            // Due to unplugging the camera, settings have changed, e.g. the heartbeat timeout value for GigE cameras.
            //            // After the camera has been reconnected, all settings must be restored. This can be done in the CameraOpened
            //            // event as shown for the Configuration.AcquireContinous.
            //            //camera.Parameters[PLTransportLayer.HeartbeatTimeout].TrySetValue(1000, IntegerValueCorrection.Nearest);

            //            // Restart grabbing.
            //            //ContinuousShot();

            //            // Restart the timeout timer.
            //            //Console.WriteLine("Camera reconnected. You may disconnect the camera device again (Timeout {0}s)", 60000 / 1000.0);

            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }

            //}

        }

        //Occurs when an image has been acquired and is ready to be processed.
        private void OnImageGrabbed(Object sender, ImageGrabbedEventArgs e)
        {
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper GUI thread.
                // The grab result will be disposed after the event call. Clone the event arguments for marshaling to the GUI thread.
                BeginInvoke(new EventHandler<ImageGrabbedEventArgs>(OnImageGrabbed), sender, e.Clone());
                return;
            }

            try
            {
                // Acquire the image from the camera. Only show the latest image. The camera may acquire images faster than the images can be displayed.

                // Get the grab result.
                IGrabResult grabResult = e.GrabResult;
                //Console.WriteLine(sender);
                // Check if the image can be displayed.
                if (grabResult.IsValid)
                {
                    // Reduce the number of displayed images to a reasonable amount if the camera is acquiring images very fast.
                    Bitmap bitmap = new Bitmap(grabResult.Width, grabResult.Height, PixelFormat.Format24bppRgb);
                    // Lock the bits of the bitmap.
                    BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
                    // Place the pointer to the buffer of the bitmap.
                    converter.OutputPixelFormat = PixelType.BGR8packed;
                    IntPtr ptrBmp = bmpData.Scan0;
                    converter.Convert(ptrBmp, bmpData.Stride * bitmap.Height, grabResult);
                    bitmap.UnlockBits(bmpData);

                    Bitmap grabbedImageColor = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                   
                    BitmapRecievedEvent?.Invoke(Device, bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), PixelFormat.Format24bppRgb));

                }
            }
            catch (Exception exception)
            {
                ShowException(exception);
            }
            finally
            {
                // Dispose the grab result if needed for returning it to the grab loop.
                e.DisposeGrabResultIfClone();
            }
        }


        // Occurs when a camera has stopped grabbing.
        private void OnGrabStopped(Object sender, GrabStopEventArgs e)
        {
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                BeginInvoke(new EventHandler<GrabStopEventArgs>(OnGrabStopped), sender, e);
                return;
            }

            // If the grabbed stop due to an error, display the error message.
            if (e.Reason != GrabStopReason.UserRequest)
            {
                MessageBox.Show("A grab error occured:\n" + e.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void CloseCamera()
        {

            // Destroy the camera object.
            try
            {
                if (Device != null)
                {
                    Device.Close();
                }
            }
            catch (Exception exception)
            {
                ShowException(exception);
            }

            // Destroy the converter object.
            if (converter != null)
            {
                converter.Dispose();
            }
        }

        public void DestroyCamera()
        {

            // Destroy the camera object.
            try
            {
                if (Device != null)
                {
                    Device.Close();
                    Device.Dispose();
                    Device = null;
                }
            }
            catch (Exception exception)
            {
                ShowException(exception);
            }

            // Destroy the converter object.
            if (converter != null)
            {
                converter.Dispose();
                converter = null;
            }
        }

       

        private void ShowException(Exception exception)
        {
            ConsoleExtension.WriteWithColor("Exception caught:\n" + exception.Message, ConsoleColor.Red);
            //MessageBox.Show("Exception caught:\n" + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
