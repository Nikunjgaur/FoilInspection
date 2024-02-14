using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using Microsoft.Office.Interop.Excel;


namespace FoilInspection
{
    public partial class InspectionForm : Form
    {
        Size iconSize = new Size(55, 55);

        int originalExStyle = -1;
        bool enableFormLevelDoubleBuffering = true;

        protected override CreateParams CreateParams
        {
            get
            {
                if (originalExStyle == -1)
                    originalExStyle = base.CreateParams.ExStyle;

                CreateParams cp = base.CreateParams;
                if (enableFormLevelDoubleBuffering)
                    cp.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED
                else
                    cp.ExStyle = originalExStyle;

                return cp;
            }
        }


        private void TurnOffFormLevelDoubleBuffering()
        {
            enableFormLevelDoubleBuffering = false;
            this.MaximizeBox = true;
        }


        public InspectionForm()
        {
            InitializeComponent();
            buttonCam1.FitImage($"{AppData.ProjectDirectory}/Icons/camRed.png", iconSize);
            buttonCam2.FitImage($"{AppData.ProjectDirectory}/Icons/camRed.png", iconSize);
            buttonApiStatus.FitImage($"{AppData.ProjectDirectory}/Icons/apiRed.png", iconSize);
            buttonCam1.ForeColor = panel2.BackColor;
            buttonCam2.ForeColor = panel2.BackColor;
            buttonApiStatus.ForeColor = panel2.BackColor;
            SizeChanged += InspectionForm_SizeChanged;

        }

        private void InspectionForm_SizeChanged(object sender, EventArgs e)
        {
            //label2.Width = Width;
            //label3.Width = Width;
        }

        private void InspectionPage_Load(object sender, EventArgs e)
        {
            InitCam();
        }

        void InitCam()
        {
            //File.WriteAllText($"{path}/Camera{camNumber}.json", camSettings);

            //AppData.camera1 = JsonConvert.DeserializeObject<CameraParameters>(File.ReadAllText($"{AppData.ProjectDirectory}/Settings/Camera{1}.json"));
            //AppData.camera1 = JsonConvert.DeserializeObject<CameraParameters>(File.ReadAllText($"{AppData.ProjectDirectory}/Settings/Camera{1}.json"));
            AppData.camera2 = new CameraParameters();
            AppData.camera2 = new CameraParameters();
            AppData.camera1.serialNumber = "24354906";
            AppData.camera2.serialNumber = "24363521";

            bool cam1Active = AppData.camera1.cam.OpenCamera(AppData.camera1.serialNumber);
            bool cam2Active = AppData.camera2.cam.OpenCamera(AppData.camera2.serialNumber);

            if (cam1Active)
            {
                buttonCam1.FitImage($"{AppData.ProjectDirectory}/Icons/camGreen.png", iconSize);
                AppData.camera1.cam.BitmapRecievedEvent += Cam_BitmapRecievedEvent1;
                AppData.camera1.CamStarted = true;
                AppData.camera1.cam.SetTriggerMode(true);
            }
            if (cam2Active)
            {
                buttonCam2.FitImage($"{AppData.ProjectDirectory}/Icons/camGreen.png", iconSize);
                AppData.camera2.cam.BitmapRecievedEvent += Cam_BitmapRecievedEvent2;
                AppData.camera2.CamStarted = true;
            }

        }

        private void Cam_BitmapRecievedEvent1(object sender, Bitmap e)
        {
            zoomInOutPictureBox1.Invoke(new System.Action(() =>
            {
                zoomInOutPictureBox1.PictureBox1.Image = e.DeepClone();
            }));
        }

        private void Cam_BitmapRecievedEvent2(object sender, Bitmap e)
        {
            e.RotateFlip(RotateFlipType.Rotate90FlipNone);
        }

        private void InspectionPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            AppData.camera1.cam.DestroyCamera();
            AppData.camera2.cam.DestroyCamera();
        }

        private void buttonCam1_Click(object sender, EventArgs e)
        {
            CameraSettings cameraSettings = new CameraSettings(ref AppData.camera1, 1);
            cameraSettings.ShowDialog();
        }

        private void InspectionForm_Shown(object sender, EventArgs e)
        {
            TurnOffFormLevelDoubleBuffering();
        }

        private void buttonCam2_Click(object sender, EventArgs e)
        {
            CameraSettings cameraSettings = new CameraSettings(ref AppData.camera2, 2);
            cameraSettings.ShowDialog();
        }

        private void btn_report_Click(object sender, EventArgs e)
        {
            ReportView reportView = new ReportView();
            reportView.ShowDialog();
        }

        private void buttonCam1_Click_1(object sender, EventArgs e)
        {
            //Console.WriteLine(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"));

            //string path = $@"{AppData.ProjectDirectory}/Defect/Images";
            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(path);
            //}
        }


        void SaveReport()
        {
            // Sample data
            string[] data = {
            "_date, _time, part_number, model, status, defect_type, machine_name",
            "2024-02-04, 0:05:03, P169143400ASSNB240343733550, TS01S3, NG, Ng, Handling dent, Ng, ROLLING04",
            "2024-02-04, 0:11:00, P169143400ASSNB240343732290, TS01S3, OK, , , ROLLING04",
            "2024-02-04, 0:11:56, P169143400ASSNB240343734480, TS01S3, NG, Ng, Heat treatment dent, ROLLING04",
            "2024-02-04, 0:12:41, P169143400ASSNB240333732140, TS01S3, OK, , , ROLLING04",
            "2024-02-04, 0:13:30, P169143400ASSNB240343732340, TS01S3, OK, , , ROLLING04",
            "2024-02-04, 0:14:16, P169143400ASSNB240343732260, TS01S3, OK, , , ROLLING04",
            "2024-02-04, 0:15:26, P169143400ASSNB240343734680, TS01S3, OK, , , ROLLING04",
            "2024-02-04, 0:16:04, P169143400ASSNB240343732240, TS01S3, OK, , , ROLLING04",
            "2024-02-04, 0:16:39, P169143400ASSNB240343734550, TS01S3, OK, , , ROLLING04",
            "2024-02-04, 0:17:17, P169143400ASSNB240343733230, TS01S3, OK, , , ROLLING04",
            "2024-02-04, 0:18:00, P169143400ASSNB240333731820, TS01S3, OK, , , ROLLING04",
            "2024-02-04, 0:18:54, P169143400ASSNB240333731950, TS01S3, OK, , , ROLLING04",
            "2024-02-04, 0:20:02, P169143400ASSNB240343732700, TS01S3, OK, , , ROLLING04",
            "2024-02-04, 0:20:40, P169143400ASSNB240343732510, TS01S3, OK, , , ROLLING04"
        };

            // Create Excel application and workbook
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            excelApp.Visible = true;
            Workbook workbook = excelApp.Workbooks.Add();
            Worksheet worksheet = (Worksheet)workbook.Sheets[1];


            // Load data into worksheet
            for (int i = 0; i < data.Length; i++)
            {
                string[] rowData = data[i].Split(',');
                for (int j = 0; j < rowData.Length; j++)
                {
                    worksheet.Cells[i + 1, j + 1].Value = rowData[j];
                }
            }

            // Add a bar chart
            ChartObjects chartObjects = (ChartObjects)worksheet.ChartObjects(Type.Missing);
            ChartObject chartObject = chartObjects.Add(100, 80, 300, 200);
            Chart chart = chartObject.Chart;
            SeriesCollection seriesCollection = (SeriesCollection)chart.SeriesCollection(Type.Missing);
            Series series = seriesCollection.NewSeries();
            series.Values = new int[] { 52, 69};
            series.XValues = new string[] { "Ok", "Ng"} ;
            // Move the chart to cell K1
            chartObject.Top = (double)worksheet.Range["K1"].Top;
            chartObject.Left = (double)worksheet.Range["K1"].Left;

            chart.ChartType = XlChartType.xlPie;

            // Save Excel file
            string filePath = "DataAndChart_Interop.xlsx";
            workbook.SaveAs(filePath);
            workbook.Close();
            excelApp.Quit();

            Console.WriteLine($"Excel file saved successfully at {filePath}.");
        }
    


        private void btn_stop_Click(object sender, EventArgs e)
        {
            SaveReport();
        }
    }
}
