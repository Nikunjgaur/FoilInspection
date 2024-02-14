using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FoilInspection
{
    class AppData
    {
        static string globalProjectDirectory = Environment.CurrentDirectory;
        public static string ProjectDirectory = Directory.GetParent(globalProjectDirectory).Parent.Parent.FullName;
        public static List<CameraParameters> cameraParameters = new List<CameraParameters>();
        public static Mode appMode = Mode.Idol;
        public static bool InspectionStarted = false;
        public static string ModelName = "";
        public static string SelectedModel = "";
        public int MyProperty { get; set; }
        public static CameraParameters camera1 = new CameraParameters();
        public static CameraParameters camera2 = new CameraParameters();

        

    }
    public enum Mode
    {
        Idol,
        Inspection,
        Settings,
        CreateModel,
        Calib
    }
}
