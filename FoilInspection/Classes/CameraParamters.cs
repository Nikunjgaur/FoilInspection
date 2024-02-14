using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Basler.Pylon;

namespace FoilInspection
{

    public class CameraParameters
    {

        [JsonIgnore]
        private BaslerCam camera = new BaslerCam();

        [JsonIgnore]
        public BaslerCam cam
        {
            get
            {
                return camera;
            }
        }
        public string serialNumber = "SrNum";
        private int camHeight = 300;

        public float Height
        {
            get { return camHeight; }
            set
            {
                if (value >= 100 && value <= 2000)
                {
                    camHeight = Convert.ToInt32(value);
                }
            }
        }
        private int camWidth = 300;
        public float Width
        {
            get { return camWidth; }
            set
            {
                if (value >= 100 && value <= 2000)
                {
                    camWidth = Convert.ToInt32(value);
                }
            }
        }

        private float camExposure = 60;
        public float Exposure
        {
            get { return camExposure; }
            set
            {
                try
                {
                    if (value >= 5 && value <= 110 && CamStarted == true && CamEnabled)
                    {
                        camExposure = value;
                        cam.Device.Parameters[PLCamera.ExposureTimeAbs].TrySetValue(camExposure);
                        ConsoleExtension.WriteWithColor($"Camera {serialNumber} exposre {value} set successfuly ", ConsoleColor.Yellow);
                    }
                }
                catch (Exception ex)
                {
                    ConsoleExtension.WriteWithColor($"Exception while setting exposure. Camera sr {serialNumber} value {value} status {CamStatus} {ex.Message}", ConsoleColor.Red);
                }
            }
        }

        private float camGain = 347;
        public float Gain
        {
            get { return camGain; }
            set
            {
                try
                {
                    if (value >= 256 && value <= 2047 && CamStarted == true && CamEnabled)
                    {

                        camGain = value;
                        cam.Device.Parameters[PLCamera.GainSelector].SetValue(PLCamera.GainSelector.DigitalAll);
                        cam.Device.Parameters[PLCamera.GainRaw].SetValue((long)camGain);
                        ConsoleExtension.WriteWithColor($"Camera {serialNumber} gain {value} set successfuly ", ConsoleColor.Yellow);
                    }
                }
                catch (Exception ex)
                {
                    ConsoleExtension.WriteWithColor($"Exception while setting exposure. Camera sr {serialNumber} value {value} status {CamStatus} {ex.Message}", ConsoleColor.Red);
                }
            }
        }

        private int camPacketSize = 9000;

        public int PacketSize
        {
            get
            {
                return camPacketSize;
            }
            set
            {
                try
                {

                    if (value >= 300 && value <= 16000 && CamStarted == true && CamEnabled)
                    {
                        camPacketSize = value;
                        cam.Device.Parameters[PLCamera.GevStreamChannelSelector].TrySetValue(PLCamera.GevStreamChannelSelector.StreamChannel0);
                        cam.Device.Parameters[PLCamera.GevSCPSPacketSize].TrySetValue(camPacketSize);
                        Console.WriteLine($"Camera {serialNumber} packet size set to {value}");
                    }
                }
                catch (NullReferenceException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    ConsoleExtension.WriteWithColor($"Exception while setting camPacketSize. Camera sr {serialNumber} value {value} status {CamStatus} {ex.Message}", ConsoleColor.Red);
                }
            }
        }


        private int packetDelay = 2000;

        public int PacketDelay
        {
            get
            {
                return packetDelay;
            }
            set
            {
                try
                {
                    if (value >= 300 && value <= 16000 && CamStarted == true && CamEnabled)
                    {
                        packetDelay = value;

                        cam.Device.Parameters[PLCamera.GevStreamChannelSelector].TrySetValue(PLCamera.GevStreamChannelSelector.StreamChannel0);
                        cam.Device.Parameters[PLCamera.GevSCPD].TrySetValue(packetDelay);
                        Console.WriteLine($"Camera {serialNumber} packet delay set to {value}");
                    }
                }
                catch (NullReferenceException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    ConsoleExtension.WriteWithColor($"Exception while setting Cam Packet Delay. Camera sr {serialNumber} value {value} status {CamStatus} {ex.Message}", ConsoleColor.Red);
                }
            }
        }


        public int Location = 0;
        //[JsonIgnore]
        public bool CamStatus;

        private bool _camEnabled = true;
        //public bool CamEnabled = false;
        //public bool CamEnabled { get; set; }
        public bool CamEnabled
        {
            get
            {
                return _camEnabled;
            }
            set
            {
                if (_camEnabled != value)
                {
                    _camEnabled = value;
                    if (!_camEnabled && CamStarted)
                    {
                        CamStatus = false;
                        camera.StopAcq();



                    }
                    else if (_camEnabled && CamStarted)
                    {
                        CamStatus = true;
                        cam.StartAcq();

                    }
                }
            }
        }

        public float calibPara = 0;

        [JsonIgnore]
        public bool CamStarted = false;

        [JsonConstructor]
        public CameraParameters()
        {
            Console.WriteLine("JsonConstructor");
        }
        public CameraParameters(CameraParameters cp)
        {
            //cam = new BaslerCam();
            serialNumber = cp.serialNumber;
            CamEnabled = cp.CamEnabled;
            CamStatus = cam.OpenCamera(serialNumber);
            CamStarted = CamStatus;
            CamStatus = CamEnabled && CamStatus;
            Exposure = cp.Exposure;
            camWidth = cp.camWidth;
            camHeight = cp.camHeight;
            PacketSize = cp.PacketSize;
            Location = cp.Location;
            Console.WriteLine("This is serialNum {0} and CamStatus {1}", serialNumber, CamStatus);
            Console.WriteLine("Class constructed");
            //Program.lst_cam5.Add(new cam_5_data(Location, CamStatus,serialNumber));
            //Console.WriteLine("Camera Count=" + Program.lst_cam5.Count);

        }

    }

}
