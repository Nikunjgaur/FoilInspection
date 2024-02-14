namespace FoilInspection
{
    partial class ReportView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.labeltotalDef = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonShowData = new System.Windows.Forms.Button();
            this.radioButtonChartView = new System.Windows.Forms.RadioButton();
            this.radioButtonGridView = new System.Windows.Forms.RadioButton();
            this.dataGridViewReport = new System.Windows.Forms.DataGridView();
            this.comboBoxStartLen = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxEndLen = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBoxDefImage = new System.Windows.Forms.PictureBox();
            this.label12 = new System.Windows.Forms.Label();
            this.labelL2M = new System.Windows.Forms.Label();
            this.buttonViewImg = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanelImages = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDefImage)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea1.AxisX.LabelAutoFitMaxFontSize = 16;
            chartArea1.AxisX.LabelAutoFitMinFontSize = 16;
            chartArea1.AxisX.Title = "Width in Mili Meters";
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Nirmala UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisX.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(134)))), ((int)(((byte)(212)))));
            chartArea1.AxisX2.LabelAutoFitMaxFontSize = 16;
            chartArea1.AxisX2.LabelAutoFitMinFontSize = 16;
            chartArea1.AxisY.LabelAutoFitMaxFontSize = 16;
            chartArea1.AxisY.LabelAutoFitMinFontSize = 16;
            chartArea1.AxisY.Title = "Length in Mili Meters";
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Nirmala UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(134)))), ((int)(((byte)(212)))));
            chartArea1.AxisY2.LabelAutoFitMaxFontSize = 16;
            chartArea1.AxisY2.LabelAutoFitMinFontSize = 16;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend1.IsTextAutoFit = false;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(12, 107);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series1.Legend = "Legend1";
            series1.MarkerColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            series1.MarkerSize = 20;
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Cross;
            series1.Name = "Defects";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(1421, 925);
            this.chart1.TabIndex = 5;
            this.chart1.Text = "chart1";
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(134)))), ((int)(((byte)(212)))));
            this.label3.Font = new System.Drawing.Font("Nirmala UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(1536, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 31);
            this.label3.TabIndex = 7;
            this.label3.Text = "Select Date";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Location = new System.Drawing.Point(1704, 177);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(181, 29);
            this.dateTimePicker1.TabIndex = 8;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(1482, 450);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.RowTemplate.Height = 34;
            this.dataGridView1.Size = new System.Drawing.Size(415, 206);
            this.dataGridView1.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(134)))), ((int)(((byte)(212)))));
            this.label4.Font = new System.Drawing.Font("Nirmala UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(1536, 230);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 31);
            this.label4.TabIndex = 10;
            this.label4.Text = "Total Defects:";
            // 
            // labeltotalDef
            // 
            this.labeltotalDef.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(134)))), ((int)(((byte)(212)))));
            this.labeltotalDef.Font = new System.Drawing.Font("Nirmala UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labeltotalDef.ForeColor = System.Drawing.Color.White;
            this.labeltotalDef.Location = new System.Drawing.Point(1704, 230);
            this.labeltotalDef.Name = "labeltotalDef";
            this.labeltotalDef.Size = new System.Drawing.Size(181, 31);
            this.labeltotalDef.TabIndex = 10;
            this.labeltotalDef.Text = "14";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(134)))), ((int)(((byte)(212)))));
            this.label5.Font = new System.Drawing.Font("Nirmala UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(1482, 414);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(415, 31);
            this.label5.TabIndex = 10;
            this.label5.Text = "Defect Details";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonShowData
            // 
            this.buttonShowData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(134)))), ((int)(((byte)(212)))));
            this.buttonShowData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonShowData.Font = new System.Drawing.Font("Nirmala UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonShowData.ForeColor = System.Drawing.Color.White;
            this.buttonShowData.Location = new System.Drawing.Point(1614, 338);
            this.buttonShowData.Name = "buttonShowData";
            this.buttonShowData.Size = new System.Drawing.Size(236, 44);
            this.buttonShowData.TabIndex = 11;
            this.buttonShowData.Text = "Show Data";
            this.buttonShowData.UseVisualStyleBackColor = false;
            this.buttonShowData.Click += new System.EventHandler(this.buttonShowData_Click);
            // 
            // radioButtonChartView
            // 
            this.radioButtonChartView.AutoSize = true;
            this.radioButtonChartView.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonChartView.Location = new System.Drawing.Point(1623, 294);
            this.radioButtonChartView.Name = "radioButtonChartView";
            this.radioButtonChartView.Size = new System.Drawing.Size(104, 25);
            this.radioButtonChartView.TabIndex = 12;
            this.radioButtonChartView.TabStop = true;
            this.radioButtonChartView.Text = "Chart View";
            this.radioButtonChartView.UseVisualStyleBackColor = true;
            this.radioButtonChartView.Visible = false;
            this.radioButtonChartView.CheckedChanged += new System.EventHandler(this.radioButtonChartView_CheckedChanged);
            // 
            // radioButtonGridView
            // 
            this.radioButtonGridView.AutoSize = true;
            this.radioButtonGridView.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonGridView.Location = new System.Drawing.Point(1734, 294);
            this.radioButtonGridView.Name = "radioButtonGridView";
            this.radioButtonGridView.Size = new System.Drawing.Size(96, 25);
            this.radioButtonGridView.TabIndex = 13;
            this.radioButtonGridView.TabStop = true;
            this.radioButtonGridView.Text = "Grid View";
            this.radioButtonGridView.UseVisualStyleBackColor = true;
            this.radioButtonGridView.Visible = false;
            this.radioButtonGridView.CheckedChanged += new System.EventHandler(this.radioButtonGridView_CheckedChanged);
            // 
            // dataGridViewReport
            // 
            this.dataGridViewReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReport.Location = new System.Drawing.Point(23, 107);
            this.dataGridViewReport.Name = "dataGridViewReport";
            this.dataGridViewReport.Size = new System.Drawing.Size(754, 884);
            this.dataGridViewReport.TabIndex = 14;
            this.dataGridViewReport.Visible = false;
            // 
            // comboBoxStartLen
            // 
            this.comboBoxStartLen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStartLen.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxStartLen.FormattingEnabled = true;
            this.comboBoxStartLen.Location = new System.Drawing.Point(1709, 72);
            this.comboBoxStartLen.Name = "comboBoxStartLen";
            this.comboBoxStartLen.Size = new System.Drawing.Size(181, 29);
            this.comboBoxStartLen.TabIndex = 6;
            this.comboBoxStartLen.Visible = false;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(134)))), ((int)(((byte)(212)))));
            this.label6.Font = new System.Drawing.Font("Nirmala UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(1541, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 31);
            this.label6.TabIndex = 7;
            this.label6.Text = "Start Length";
            this.label6.Visible = false;
            // 
            // comboBoxEndLen
            // 
            this.comboBoxEndLen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEndLen.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxEndLen.FormattingEnabled = true;
            this.comboBoxEndLen.Location = new System.Drawing.Point(1709, 124);
            this.comboBoxEndLen.Name = "comboBoxEndLen";
            this.comboBoxEndLen.Size = new System.Drawing.Size(181, 29);
            this.comboBoxEndLen.TabIndex = 6;
            this.comboBoxEndLen.Visible = false;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(134)))), ((int)(((byte)(212)))));
            this.label7.Font = new System.Drawing.Font("Nirmala UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(1541, 122);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(150, 31);
            this.label7.TabIndex = 7;
            this.label7.Text = "End Length:";
            this.label7.Visible = false;
            // 
            // pictureBoxDefImage
            // 
            this.pictureBoxDefImage.Location = new System.Drawing.Point(1880, 986);
            this.pictureBoxDefImage.Name = "pictureBoxDefImage";
            this.pictureBoxDefImage.Size = new System.Drawing.Size(17, 15);
            this.pictureBoxDefImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxDefImage.TabIndex = 15;
            this.pictureBoxDefImage.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Nirmala UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(35, 57);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(282, 30);
            this.label12.TabIndex = 20;
            this.label12.Text = "Length converted to Meters :";
            this.label12.Visible = false;
            // 
            // labelL2M
            // 
            this.labelL2M.AutoSize = true;
            this.labelL2M.Font = new System.Drawing.Font("Nirmala UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelL2M.Location = new System.Drawing.Point(334, 57);
            this.labelL2M.Name = "labelL2M";
            this.labelL2M.Size = new System.Drawing.Size(37, 30);
            this.labelL2M.TabIndex = 21;
            this.labelL2M.Text = "---";
            this.labelL2M.Visible = false;
            // 
            // buttonViewImg
            // 
            this.buttonViewImg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonViewImg.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonViewImg.Location = new System.Drawing.Point(1589, 905);
            this.buttonViewImg.Name = "buttonViewImg";
            this.buttonViewImg.Size = new System.Drawing.Size(196, 41);
            this.buttonViewImg.TabIndex = 22;
            this.buttonViewImg.Text = "View HD image";
            this.buttonViewImg.UseVisualStyleBackColor = true;
            this.buttonViewImg.Click += new System.EventHandler(this.buttonViewImg_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(134)))), ((int)(((byte)(212)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Nirmala UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1924, 39);
            this.label1.TabIndex = 56;
            this.label1.Text = "PRODUCTION REPORT";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanelImages
            // 
            this.flowLayoutPanelImages.AutoScroll = true;
            this.flowLayoutPanelImages.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanelImages.Location = new System.Drawing.Point(1483, 665);
            this.flowLayoutPanelImages.Name = "flowLayoutPanelImages";
            this.flowLayoutPanelImages.Size = new System.Drawing.Size(413, 218);
            this.flowLayoutPanelImages.TabIndex = 57;
            // 
            // ReportView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1061);
            this.Controls.Add(this.flowLayoutPanelImages);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonViewImg);
            this.Controls.Add(this.labelL2M);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.pictureBoxDefImage);
            this.Controls.Add(this.radioButtonGridView);
            this.Controls.Add(this.radioButtonChartView);
            this.Controls.Add(this.buttonShowData);
            this.Controls.Add(this.labeltotalDef);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBoxEndLen);
            this.Controls.Add(this.comboBoxStartLen);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.dataGridViewReport);
            this.Name = "ReportView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReportView_FormClosing);
            this.Load += new System.EventHandler(this.ReportView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDefImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labeltotalDef;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonShowData;
        private System.Windows.Forms.RadioButton radioButtonChartView;
        private System.Windows.Forms.RadioButton radioButtonGridView;
        private System.Windows.Forms.DataGridView dataGridViewReport;
        private System.Windows.Forms.PictureBox pictureBoxDefImage;
        private System.Windows.Forms.ComboBox comboBoxStartLen;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxEndLen;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label labelL2M;
        private System.Windows.Forms.Button buttonViewImg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelImages;
    }
}