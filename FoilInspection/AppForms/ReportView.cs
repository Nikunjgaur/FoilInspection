using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Npgsql;

namespace FoilInspection
{
    public partial class ReportView : Form
    {

        string local_connStr = "Server={0};Port=5432; User Id=postgres;Password=1234;Database=mesh_database";  //---Local Database Connection
        string work_order = "";

        Random rnd = new Random();
        Bitmap reportImg;
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        string fullImgPath = "";
        //Bitmap[] inputImages = new Bitmap[4];
        //string[] inputImagesPath = new string[4];
        NpgsqlTypes.NpgsqlPoint point = new NpgsqlTypes.NpgsqlPoint(0, 0);
        public ReportView()
        {
            InitializeComponent();
            //reportImg = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            //chart1.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
            //chart1.ChartAreas[0].AxisX2.Enabled = AxisEnabled.True;
            chart1.ChartAreas[0].AxisX.Maximum = 1500;
            //chart1.ChartAreas[0].AxisY.Maximum = 3000;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            //inputImages[0] = new Bitmap(@"C:\Users\ADVANTECh\Desktop\1.png");
            //inputImages[1] = new Bitmap(@"C:\Users\ADVANTECh\Desktop\2.png");
            //inputImages[2] = new Bitmap(@"C:\Users\ADVANTECh\Desktop\3.png");
            //inputImages[3] = new Bitmap(@"C:\Users\ADVANTECh\Desktop\4.png");

        }


        //public void ShowDateWise()
        //{
        //    using (NpgsqlConnection con = db.GetConnection())
        //    {
        //        con.Open();
        //        string query = @"select Max(_location[0]), Max(_location[1]) from public.logreport where _date = @date and serialnum = @srnum";
        //        NpgsqlCommand cmd = new NpgsqlCommand(query, con);
        //        cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(dateTimePicker1.Value.ToString("yyyy-MM-dd")));
        //        cmd.Parameters.AddWithValue("@srnum", comboBoxSrNum.SelectedItem.ToString());
        //        NpgsqlDataReader reader = cmd.ExecuteReader();

        //        if (reader.HasRows)
        //        {
        //            while (reader.Read())
        //            {
        //                if (chart1.ChartAreas[0].AxisX.Maximum < Convert.ToDouble(reader[0]))
        //                {
        //                    chart1.ChartAreas[0].AxisX.Maximum = Convert.ToDouble(reader[0]) + 50;
        //                    chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;

        //                }
        //                if (chart1.ChartAreas[0].AxisY.Maximum < Convert.ToDouble(reader[1]))
        //                {
        //                    chart1.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(reader[1]);
        //                    chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
        //                }
        //            }
        //        }


        //        reader.Close();

        //        query = @"select * from public.logreport where _date = @date and serialnum = @srnum";



        //        Console.WriteLine(query);

        //        cmd = new NpgsqlCommand(query, con);
        //        cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(dateTimePicker1.Value.ToString("yyyy-MM-dd")));

        //        reader = cmd.ExecuteReader();

        //        if (reader.HasRows)
        //        {
        //            dt.Clear();
        //            dt.Load(reader);
        //            dataGridViewReport.DataSource = dt;
        //            reader = cmd.ExecuteReader();
        //            while (reader.Read())
        //            {

        //                NpgsqlTypes.NpgsqlPoint point = (NpgsqlTypes.NpgsqlPoint)reader[3];
        //                Console.WriteLine("This is Point X {0} and this is Point Y {1}", point.X, point.Y);
        //                chart1.Series[Convert.ToInt32(reader[6])].Points.AddXY(point.X, point.Y);
        //            }

        //            labeltotalDef.Text = dt.Rows.Count.ToString();
        //        }
        //        else
        //        {
        //            MessageBox.Show("No Data Found");
        //        }
        //        reader.Close();

        //    }
        //}



        static (double, double) GetMaxXAndY(string connectionString)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;

                    // SQL query to get max X and Y values
                    cmd.CommandText = @"
                    SELECT
                        MAX((SELECT MAX(point[1]) FROM unnest(defect_loc) AS point)) AS max_x,
                        MAX((SELECT MAX(point[2]) FROM unnest(defect_loc) AS point)) AS max_y
                    FROM
                        fabric_defect_log;";

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            double maxX = Convert.ToDouble(reader["max_x"]);
                            double maxY = Convert.ToDouble(reader["max_y"]);
                            return (maxX, maxY);
                        }
                    }
                }
            }

            // Return default values if no result is found
            return (0.0, 0.0);
        }


        private void GetDefectByRollId()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                using (var connection = new NpgsqlConnection(string.Format(local_connStr, "localhost")))
                {
                    connection.Open();
                    using (var command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"
                        SELECT Max(position_width), Max(position_length) FROM public.defect_table WHERE work_order = @work_order";

                        command.Parameters.AddWithValue("work_order", work_order);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    if (chart1.ChartAreas[0].AxisX.Maximum < Convert.ToDouble(reader[0]))
                                    {
                                        chart1.ChartAreas[0].AxisX.Maximum = Convert.ToDouble(reader[0]) + 50;
                                        chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;

                                    }
                                    if (chart1.ChartAreas[0].AxisY.Maximum < Convert.ToDouble(reader[1]))
                                    {
                                        chart1.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(reader[1]);
                                        chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
                                    }
                                }
                            }
                        }


                        //command.Connection = connection;
                        command.CommandText = @"
                        SELECT COUNT(*) FROM public.defect_table
                        WHERE work_order = @work_order";

                        command.Parameters.AddWithValue("work_order", work_order);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                ////dt.Clear();
                                ////dt.Load(reader);
                                ////dataGridViewReport.DataSource = dt;
                                while (reader.Read())
                                {
                                    labeltotalDef.Text = reader["count"].ToString();
                                }

                            }
                            else
                            {
                                labeltotalDef.Text = "0";

                            }

                        }


                        //command.Connection = connection;
                        command.CommandText = @"
                        SELECT position_length, position_width, size_width, size_height, area, perimeter FROM public.defect_table
                        WHERE work_order = @work_order";

                        command.Parameters.AddWithValue("work_order", work_order);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                //dt.Clear();
                                //dt.Load(reader);
                                //dataGridViewReport.DataSource = dt;
                                while (reader.Read())
                                {
                                    var pointX = Convert.ToInt32(reader["position_width"]);
                                    var pointY = Convert.ToInt32(reader["position_length"]);
                                    //NpgsqlTypes.NpgsqlPoint point = (NpgsqlTypes.NpgsqlPoint)reader[3];
                                    //Console.WriteLine("This is Point X {0} and this is Point Y {1}", point.X, point.Y);
                                    chart1.Series[0].Points.AddXY(pointX, pointY);
                                }

                                labeltotalDef.Text = reader.Rows.ToString();

                                //ProdSchedule data = new ProdSchedule();

                                //data.Shift = reader["size_width"].ToString();
                                //data.ModelName = reader["size_height"];
                                //data.CreatedBy = reader["area"].ToString();
                                //data.ApprovedBy = reader["perimeter"].ToString();

                                //dataList.Add(data);

                            }
                            else
                            {
                                MessageBox.Show("No Data Found");
                            }

                        }

                    }
                }
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                Console.WriteLine(ex.Message);
            }
           
            
        }

        private DataTable Transpose(DataTable dt)
        {

            DataTable dtNew = new DataTable();

            //adding columns    
            for (int i = 0; i <= dt.Rows.Count; i++)
            {
                dtNew.Columns.Add(i.ToString());
            }

            //Changing Column Captions:
            dtNew.Columns[0].ColumnName = "Property";
            dtNew.Columns[1].ColumnName = "Value";

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    //For dateTime columns use like below
            //    dtNew.Columns[i].ColumnName = dt.Rows[i].ItemArray[0].ToString();
            //    //Else just assign the ItermArry[0] to the columnName prooperty
            //}

            //Adding Row Data
            for (int k = 0; k < dt.Columns.Count; k++)
            {
                DataRow r = dtNew.NewRow();
                r[0] = dt.Columns[k].ToString();
                for (int j = 1; j <= dt.Rows.Count; j++)
                {
                    if (j == 1 && k == 2)
                    {
                        r[j] = dt.Rows[j - 1][k];

                    }
                    else
                    {
                        r[j] = dt.Rows[j - 1][k];

                    }
                }
                dtNew.Rows.Add(r);
            }

            return dtNew;
        }

        private void ReportView_Load(object sender, EventArgs e)
        {
            work_order = "";

            labeltotalDef.Text = "";
            //Console.WriteLine("This is rows count" + dt.Rows.Count);
            //timer1.Enabled = true;
            //timer1.Start();
            
        }

        
        //void UpdateFinishList()
        //{
        //    using (NpgsqlConnection con = db.GetConnection())
        //    {
        //        con.Open();
        //        string query = @"select finish from public.logreport where _date = @date group by (finish)";
        //        NpgsqlCommand cmd = new NpgsqlCommand(query, con);
        //        cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(dateTimePicker1.Value.ToString("yyyy-MM-dd")));
        //        //cmd.Parameters.AddWithValue("@date2", dateTimePickerTo.Value.ToString("dd-MM-yyyy"));

        //        NpgsqlDataReader reader = cmd.ExecuteReader();

        //        if (reader.HasRows)
        //        {

        //            while (reader.Read())
        //            {
        //                comboBoxFinish.Items.Add(reader[0]);
        //            }
        //            comboBoxFinish.SelectedIndex = 0;

        //        }

        //    }
        //}

       

        private void SetReportWithPoint()
        {

            using (var connection = new NpgsqlConnection(string.Format(local_connStr, "localhost")))
            {
                connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"
                    SELECT Max(position_width), Max(position_length) FROM public.defect_table WHERE work_order = @work_order";

                    command.Parameters.AddWithValue("work_order", work_order);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                //if (chart1.ChartAreas[0].AxisX.Maximum < Convert.ToDouble(reader[0]))
                                //{
                                chart1.ChartAreas[0].AxisX.Maximum = Convert.ToDouble(reader[0]) + 50;
                                chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;

                                //}
                                // if (chart1.ChartAreas[0].AxisY.Maximum < Convert.ToDouble(reader[1]))
                                //{
                                chart1.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(reader[1]);
                                chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
                                //}
                            }
                        }
                    }

                    //command.Connection = connection;
                    command.CommandText = @"
                    SELECT position_length, position_width, size_width, size_height, area, perimeter FROM public.defect_table
                    WHERE work_order = @work_order";

                    command.Parameters.AddWithValue("work_order", work_order);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {


                            //dt.Clear();
                            //dt.Load(reader);
                            //dataGridViewReport.DataSource = dt;
                            while (reader.Read())
                            {
                                var pointX = Convert.ToInt32(reader["position_width"]);

                                var pointY = Convert.ToInt32(reader["position_length"]);
                                //NpgsqlTypes.NpgsqlPoint point = (NpgsqlTypes.NpgsqlPoint)reader[3];
                                //Console.WriteLine("This is Point X {0} and this is Point Y {1}", point.X, point.Y);
                                chart1.Series[0].Points.AddXY(pointX, pointY);
                            }

                            labeltotalDef.Text = reader.Rows.ToString();


                            //ProdSchedule data = new ProdSchedule();

                            //data.Shift = reader["size_width"].ToString();
                            //data.ModelName = reader["size_height"];
                            //data.CreatedBy = reader["area"].ToString();
                            //data.ApprovedBy = reader["perimeter"].ToString();

                            //dataList.Add(data);

                        }
                        else
                        {
                            MessageBox.Show("No Data Found");
                        }

                    }

                }
            }

        }




        void SetReportWithPoint(Point point)
        {
            //Bitmap bmp = null;
            NpgsqlTypes.NpgsqlPoint npPoint = new NpgsqlTypes.NpgsqlPoint (point.X, point.Y);
            Console.WriteLine(npPoint);
            labelL2M.Text = (npPoint.Y / 1000).ToString("N3");
            dt2.Clear();
            using (var connection = new NpgsqlConnection(string.Format(local_connStr, "localhost")))
            {
                connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = $@"
                    SELECT position_length, position_width, size_width, size_height, area, perimeter FROM public.defect_table
                    WHERE work_order = @work_order and position_length = {point.Y} and position_width = {point.X}";

                    command.Parameters.AddWithValue("work_order", work_order);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            
                            dt2.Load(reader);
                            dataGridView1.DataSource = Transpose(dt2);
                            dataGridView1.Columns[0].Width = 300;
                            dataGridView1.Columns[1].Width = 300;

                        }
                        else
                        {
                            MessageBox.Show("No Data Found");
                        }
                        for (int i = 0; i < dataGridView1.Columns.Count; i++)
                        {
                            DataGridViewCellStyle column = dataGridView1.Columns[i].HeaderCell.Style;
                            column.Font = new Font("Microsoft Sans Serif", 12);
                            column.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                    }

                    command.CommandText = $@"
                    SELECT defect_img FROM public.defect_table
                    WHERE work_order = @work_order and position_length = {point.Y} and position_width = {point.X}";

                    command.Parameters.AddWithValue("work_order", work_order);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            flowLayoutPanelImages.Controls.Clear();
                            while (reader.Read())
                            {
                                byte[] imgData = null;

                                imgData = (byte[])reader[0];
                                Bitmap bitmap = ByteToBitmap(imgData);
                                PictureBox pictureBox = new PictureBox();
                                pictureBox.Image = bitmap;
                                pictureBox.Size = new Size(80, 80);
                                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                                flowLayoutPanelImages.Controls.Add(pictureBox);


                            }

                        }
                        else
                        {
                            MessageBox.Show("No Data Found");
                        }
                       
                    }
                }
            }
                

            

            //using (NpgsqlConnection con = db.GetConnection())
            //{
            //    con.Open();
            //    string query = @"select imagebytes from public.logreport where _date = @date and _location ~= @point";
            //    NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            //    cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(dateTimePicker1.Value.ToString("yyyy-MM-dd")));
            //    cmd.Parameters.AddWithValue("@point", npPoint);

            //    NpgsqlDataReader reader = cmd.ExecuteReader();

            //    if (reader.HasRows)
            //    {
            //        while (reader.Read())
            //        {
            //            imgData = (byte[])reader[0];
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("No Data Found for this date and shift");
            //    }
            //}
            //if (imgData != null)
            //{
            //    using (var ms = new MemoryStream(imgData))
            //    {
            //        bmp = new Bitmap(ms);
            //    }

            //    pictureBoxDefImage.Image = bmp;
            //}
        }

        public Bitmap ByteToBitmap(byte[] imgData)
        {
            Bitmap bmp = null;
            if (imgData != null)
            {
                using (var ms = new MemoryStream(imgData))
                {
                    bmp = new Bitmap(ms);
                }
                
            }
            return bmp;
        }

        public DataPoint GetPointAtMouse(Chart c, MouseEventArgs e)
        {
            var result = c.HitTest(e.X, e.Y);
            // If the mouse if over a data point
            if (result.ChartElementType == ChartElementType.DataPoint)
            {
                // Find selected data point
                var point = c.Series[result.Series.Name].Points[result.PointIndex];
                
                SetReportWithPoint(new Point((int)point.XValue, (int)point.YValues[0]));
                return point;
            }
            return null;
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            GetPointAtMouse(chart1, me);
        }

        private void buttonShowData_Click(object sender, EventArgs e)
        {
            if (work_order != null)
            {
                try
                {
                    for (int i = 0; i < chart1.Series.Count; i++)
                    {
                        chart1.Series[i].Points.Clear();
                    }
                    chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                    chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset();
                    chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
                    chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
                    chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
                    chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;

                    GetDefectByRollId();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
               
              //  ShowDateWise();

               
            }
            else
            {

                MessageBox.Show("Please select a serial Number", "Invalid Serial Number", MessageBoxButtons.OK ,MessageBoxIcon.Information);
            }
        }

        private void radioButtonChartView_CheckedChanged(object sender, EventArgs e)
        {
            dataGridViewReport.Visible = false;
            chart1.Visible = true;
        }

        private void radioButtonGridView_CheckedChanged(object sender, EventArgs e)
        {
            dataGridViewReport.Visible = true;
            chart1.Visible = false;
        }
        

        private void ReportView_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        

        private void buttonViewImg_Click(object sender, EventArgs e)
        {
            if (File.Exists(fullImgPath))
            {
                Process.Start(fullImgPath);
            }
            else
            {
                MessageBox.Show("File empty");
            }
        }

    }
}
