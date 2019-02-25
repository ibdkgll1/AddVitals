using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;

using IBM.Data.DB2.iSeries;
using System.Data.OleDb;
using System.Globalization;
using AddVitals.Business_Logic;

namespace AddVitals
{
    public partial class Form1 : Form
    {
        BusinessLogic BL = new BusinessLogic();

        const string SCHEMA = "schema";     //Production...

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lbFileName.Text = string.Empty;
            lbFullPath.Text = string.Empty;
            lbDirectoryPath.Text = string.Empty;
            lbStatus.Text = "Ready...     Select A File...";
        }

        private void btUpload_Click(object sender, EventArgs e)
        {
            if (dgvDetail.RowCount == 0)
            {
                MessageBox.Show("There is no data to Upload...", "Upload Data");
                return;
            }

            DialogResult dialogResult1 = MessageBox.Show("Are You Sure You Want To Upload Data?", "Upload Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult1 == DialogResult.No)
            {
                return;
            }

            lbStatus.Text = "Processing Request...     Please Wait...";
            Application.DoEvents();

            DataTable myDT = (DataTable)(dgvDetail.DataSource);

            Int32 totalTblCnt = myDT.Rows.Count;
            Int32 totalAdded = 0;
            Int32 totalFailed = 0;
            Int32 rowCnt = 0;

            iDB2Connection DBConnection = createDBConnection();

            foreach (DataRow dr in myDT.Rows)
            {
                if (checkValidClient(DBConnection, dr, rowCnt) == true)
                {         //Make Every Client Is Valid...
                    string[] EnterDate = dr[2].ToString().Split('/');
                    string enteredMonth = EnterDate[0].ToString().PadLeft(2, '0');
                    string enteredDay = EnterDate[1].ToString().PadLeft(2, '0');
                    string enteredYear = EnterDate[2].ToString();
                    string myInterviewDate = enteredYear.Substring(0, 4) + "-" + enteredMonth + "-" + enteredDay + " " + enteredYear.Substring(5, 8) + ".000001";

                    processVital(DBConnection, dr, myInterviewDate, rowCnt, 3, 4, "Blood Pressure");
                    processVital(DBConnection, dr, myInterviewDate, rowCnt, 5, 0, "Weight");
                    processVital(DBConnection, dr, myInterviewDate, rowCnt, 6, 0, "Height");                    
                    processVital(DBConnection, dr, myInterviewDate, rowCnt, 6, 0, "BMI");                  //Added BMI Calculation to Vital...
                    processVital(DBConnection, dr, myInterviewDate, rowCnt, 7, 0, "Abdominal Girth");
                    processVital(DBConnection, dr, myInterviewDate, rowCnt, 8, 0, "CO Level");
                    processVital(DBConnection, dr, myInterviewDate, rowCnt, 9, 0, "8 Hour Fasting");
                    //Blood Draw Date...
                    processVital(DBConnection, dr, myInterviewDate, rowCnt, 11, 0, "Glucose Random");
                    processVital(DBConnection, dr, myInterviewDate, rowCnt, 12, 0, "Hgb A1C");
                    processVital(DBConnection, dr, myInterviewDate, rowCnt, 13, 0, "Total Cholesterol");
                    processVital(DBConnection, dr, myInterviewDate, rowCnt, 14, 0, "HDL Cholesterol");
                    processVital(DBConnection, dr, myInterviewDate, rowCnt, 15, 0, "LDL Cholesterol");
                    processVital(DBConnection, dr, myInterviewDate, rowCnt, 16, 0, "Triglycerides");
                    processVital(DBConnection, dr, myInterviewDate, rowCnt, 17, 0, "Respirations");
                    processVital(DBConnection, dr, myInterviewDate, rowCnt, 18, 0, "Spirometery");
                    processVital(DBConnection, dr, myInterviewDate, rowCnt, 19, 0, "O2 Saturation");
                    processVital(DBConnection, dr, myInterviewDate, rowCnt, 20, 0, "Intraocular Pressure");
                    processVital(DBConnection, dr, myInterviewDate, rowCnt, 21, 0, "Pulse");
                    processVital(DBConnection, dr, myInterviewDate, rowCnt, 22, 0, "Temperature");
                    processVital(DBConnection, dr, myInterviewDate, rowCnt, 23, 0, "Cigarettes Per Day");
                    processVital(DBConnection, dr, myInterviewDate, rowCnt, 24, 0, "Drinks Per Day");

                    totalAdded++;
                }
                else
                {
                    MessageBox.Show("Client: " + dr[0].ToString() + " is not a valid Client. Bypassing this record...", "Upload Data");
                    totalFailed++;
                }
                rowCnt++;
            }

            DBConnection.Close();

            //dgvDetail.DataSource = null;      //I want the user to see the finished results...

            lbFullPath.Text = string.Empty;
            lbDirectoryPath.Text = string.Empty;
            lbFileName.Text = string.Empty;

            lbStatus.Text = "Upload Completed Successfully...     Results: Total: " + totalTblCnt.ToString() + ", Inserted: " + totalAdded.ToString() + ", Failed: " + totalFailed.ToString();
        }

        private Boolean checkValidClient(iDB2Connection DBConnection, DataRow myDR, Int32 rowCnt)
        {
            if (myDR[0].ToString() == null || myDR[0].ToString() == "" || myDR[0].ToString() == " " || myDR[2].ToString() == null || myDR[2].ToString() == "" || myDR[2].ToString() == " ")
            {
                dgvDetail.Rows[rowCnt].Cells[0].Style.BackColor = Color.Red;
                dgvDetail.Rows[rowCnt].Cells[1].Style.BackColor = Color.Red;
                dgvDetail.Rows[rowCnt].Cells[2].Style.BackColor = Color.Red;
                Application.DoEvents();
                return false;
            }

            try
            {
                iDB2Command cmd = DBConnection.CreateCommand();
                cmd.CommandText = string.Format("SELECT CLTSTS FROM {0}.CLTMST WHERE CLTCAS = @CLIENT_ID", SCHEMA);

                cmd.DeriveParameters();
                cmd.Parameters["@CLIENT_ID"].Value = myDR[0].ToString();

                iDB2DataReader db2r = cmd.ExecuteReader(CommandBehavior.Default);
                DataTable myDT = new DataTable();
                myDT.Load(db2r);

                if (myDT.Rows.Count > 0)
                {
                    if (myDT.Rows[0][0].ToString() == "A")
                    {
                        dgvDetail.Rows[rowCnt].Cells[0].Style.BackColor = Color.LightGreen;
                        dgvDetail.Rows[rowCnt].Cells[1].Style.BackColor = Color.LightGreen;
                        dgvDetail.Rows[rowCnt].Cells[2].Style.BackColor = Color.LightGreen;
                        Application.DoEvents();
                        return true;
                    }
                    else
                    {
                        dgvDetail.Rows[rowCnt].Cells[0].Style.BackColor = Color.Red;
                        dgvDetail.Rows[rowCnt].Cells[1].Style.BackColor = Color.Red;
                        dgvDetail.Rows[rowCnt].Cells[2].Style.BackColor = Color.Red;
                        Application.DoEvents();
                        return false;
                    }
                }
                else
                {
                    dgvDetail.Rows[rowCnt].Cells[0].Style.BackColor = Color.Red;
                    dgvDetail.Rows[rowCnt].Cells[1].Style.BackColor = Color.Red;
                    dgvDetail.Rows[rowCnt].Cells[2].Style.BackColor = Color.Red;
                    Application.DoEvents();
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void processVital(iDB2Connection DBConnection, DataRow myDR, string myInterviewDate, Int32 rowCnt, Int32 colCnt1, Int32 colCnt2, string vital)
        {
            if (myDR[colCnt1].ToString() == null || myDR[colCnt1].ToString() == " ")
            {
                dgvDetail.Rows[rowCnt].Cells[colCnt1].Style.BackColor = Color.Red;
                Application.DoEvents();
                return;
            }

            try
            {
                DataTable myMasterDT = BL.getVitalType(DBConnection, SCHEMA, vital);
                string vitalType = myMasterDT.Rows[0][0].ToString();
                //string vitalUID = myMasterDT.Rows[0][1].ToString();

                if (vitalType.Length > 0)
                {
                    Int32 insertedRow = 0;
                    if (vital == "BMI")
                    {
                        double weightLBS = Convert.ToInt32(myDR[5]) * 2.2046;
                        double heightInches = Convert.ToInt32(myDR[6]) * .39;
                        double bmi = (weightLBS / (heightInches * heightInches) * 703);                            //BMI = (MASS(LBS) / (HEIGHT(IN)2 * 703))
                        Int32 bmiWhole = Convert.ToInt32(bmi);
                        insertedRow = BL.processVital(DBConnection, SCHEMA, myDR[0].ToString(), vitalType, vital + " Vital Loader", Convert.ToString(bmiWhole), Convert.ToString(0), myInterviewDate);
                    }
                    else
                        insertedRow = BL.processVital(DBConnection, SCHEMA, myDR[0].ToString(), vitalType, vital + " Vital Loader", myDR[colCnt1].ToString(), myDR[colCnt2].ToString(), myInterviewDate);

                    if (insertedRow != 1)
                        dgvDetail.Rows[rowCnt].Cells[colCnt1].Style.BackColor = Color.Red;
                    else
                        dgvDetail.Rows[rowCnt].Cells[colCnt1].Style.BackColor = Color.LightGreen;
                }
                else
                    dgvDetail.Rows[rowCnt].Cells[colCnt1].Style.BackColor = Color.Red;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            Application.DoEvents();
        }

        private void btOpenFileDialog_Click(object sender, EventArgs e)
        {
            dgvDetail.DataSource = null;
            Application.DoEvents();

            OpenFileDialog ofd = new OpenFileDialog();
            //ofd.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            ofd.Filter = "Excel |*.xls";
            ofd.FilterIndex = 1;
            ofd.Multiselect = false;

            DialogResult result = ofd.ShowDialog();         //This will show the the Dialog...
            if (result == DialogResult.OK)
            {
                lbFullPath.Text = ofd.FileName;
                lbFileName.Text = Path.GetFileName(lbFullPath.Text);
                lbDirectoryPath.Text = Path.GetDirectoryName(lbFullPath.Text);
                lbStatus.Text = "Valid File Selected... Ready To Load Excel File...";
            }
            else
                MessageBox.Show("No File was selected...");
        }

        private void btLoadFile_Click(object sender, EventArgs e)
        {
            DataTable myInputFileDT = new DataTable();

            if (lbFullPath.Text != string.Empty)
                myInputFileDT = GetDataFromExcel(lbFullPath.Text);
            else
            {
                MessageBox.Show("Please Select A Valid File");
                return;
            }

            if (myInputFileDT.Rows.Count > 0)
            {
                dgvDetail.DataSource = myInputFileDT;

                //dgvDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;     //I needed this tighter...
                dgvDetail.Columns[0].Width = 40;    //Client ID
                dgvDetail.Columns[1].Width = 70;    //Assessment
                dgvDetail.Columns[2].Width = 70;    //Interview Date
                dgvDetail.Columns[3].Width = 50;    //Blood Pressure Systolic
                dgvDetail.Columns[4].Width = 50;    //Blood Pressure Diastolic
                dgvDetail.Columns[5].Width = 50;    //Weight
                dgvDetail.Columns[6].Width = 50;    //Height
                dgvDetail.Columns[7].Width = 80;    //Waist Circumference
                dgvDetail.Columns[8].Width = 50;    //Breath
                dgvDetail.Columns[9].Width = 50;    //Blood Draw Fasting Simple
                dgvDetail.Columns[10].Width = 75;   //Blood Draw Date
                dgvDetail.Columns[11].Width = 50;   //Blood Draw Glucose
                dgvDetail.Columns[12].Width = 50;   //Blood Draw HgBA1c
                dgvDetail.Columns[13].Width = 65;   //Cholesterol Total
                dgvDetail.Columns[14].Width = 65;   //Cholesterol HDL
                dgvDetail.Columns[15].Width = 65;   //Cholesterol LDL
                dgvDetail.Columns[16].Width = 70;   //Cholesterol Triglycerides
                dgvDetail.Columns[17].Width = 75;   //Other Measures Respirations
                dgvDetail.Columns[18].Width = 70;   //Other Measures Spirometery
                dgvDetail.Columns[19].Width = 60;   //Other Measures O2 Saturation
                dgvDetail.Columns[20].Width = 65;   //Other Measures Intraocular Pressure
                dgvDetail.Columns[21].Width = 60;   //Other Measures Pulse
                dgvDetail.Columns[22].Width = 75;   //Other Measures Temperature
                dgvDetail.Columns[23].Width = 70;   //Other Measures Cigarettes Per Day
                dgvDetail.Columns[24].Width = 70;   //Other Measures # Drinks Per Day

                //dgvDetail.Sort(dgvDetail.Columns[0], ListSortDirection.Ascending);            //This was messing me up...
                foreach (DataGridViewColumn col in dgvDetail.Columns)
                {
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                    col.Resizable = DataGridViewTriState.False;
                }

                dgvDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvDetail.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
                dgvDetail.ReadOnly = true;

                lbStatus.Text = "File Loaded into SpreadSheet Details... Ready To Upload Data...";
            }
            else
            {
                lbStatus.Text = "No Details in Selected File...";
            }
        }

        void GridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)          //Give DataGridView Row Numbers... This is awesome...
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        public DataTable GetDataFromExcel(string filePath)
        {
            try
            {
                DataTable dtexcel = new DataTable();
                bool hasHeaders = false;
                string HDR = hasHeaders ? "Yes" : "No";
                //string HDR = hasHeaders ? "No" : "No";
                string strConn;

                if (filePath.Substring(filePath.LastIndexOf('.')).ToLower() == ".xlsx")
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=0\"";
                else
                    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=0\"";

                OleDbConnection conn = new OleDbConnection(strConn);
                conn.Open();
                DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                //foreach (DataRow schemaRow in schemaTable.Rows)       //Looping Through All The Worksheets to find the one you want...
                //{}

                DataRow schemaRow = schemaTable.Rows[0];
                string sheet = schemaRow[2].ToString();      //This will give you the name of the worksheet you need...
                if (!sheet.EndsWith("_"))
                {
                    string query = "SELECT * FROM [" + sheet + "]";
                    OleDbDataAdapter daexcel = new OleDbDataAdapter(query, conn);
                    dtexcel.Locale = CultureInfo.CurrentCulture;
                    daexcel.Fill(dtexcel);
                }

                conn.Close();
                //dtexcel.Columns.RemoveAt(25);       //I just had to do this but dont like it...
                return dtexcel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private iDB2Connection createDBConnection()
        {
            string dbDataSource = "DataSource=" + Properties.Settings.Default.DB_DATA_SOURCE;      //Production...
            string dbUserId = ";UserID=" + Properties.Settings.Default.DB_USER;                    //Production...
            string dbPassWord = ";Password=" + Properties.Settings.Default.DB_PASSWORD;            //Production...

            string dbDataComp = ";DataCompression=True;";
            string dbConnectionString = dbDataSource + dbUserId + dbPassWord + dbDataComp;

            iDB2Connection DBConnection = new iDB2Connection(dbConnectionString);
            DBConnection.Open();

            return DBConnection;
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult1 = MessageBox.Show("Are You Sure You Want To Exit?", "Exit Application", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult1 == DialogResult.No)
            {
                return;
            }

            System.Windows.Forms.Application.Exit();
        }

    }
}
