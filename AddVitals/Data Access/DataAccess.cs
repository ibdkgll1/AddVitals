using System;
using System.Data;
using System.Linq;
using System.Text;
using IBM.Data.DB2.iSeries;
using System.Collections.Generic;

namespace AddVitals.Data_Access
{
    class DataAccess
    {
        public DataTable getVitalType(iDB2Connection DBConnection, string SCHEMA, string vitalDescription)
        {
            try
            {
                iDB2Command cmd = DBConnection.CreateCommand();
                cmd.CommandText = string.Format("SELECT EVLTYPE FROM {0}.EMRVTLMST WHERE EVLDSC = '{1}'", SCHEMA, vitalDescription);
                iDB2DataReader db2r = cmd.ExecuteReader(CommandBehavior.Default);
                DataTable myMasterDT = new DataTable();
                myMasterDT.Load(db2r);
                return myMasterDT;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Int32 addVital(iDB2Connection DBConnection, string SCHEMA, string clientID, string vitalType, string vitalDescription, string vitalValue1, string vitalValue2, string myInterviewDate)
        {
            try
            {
                iDB2Command cmd = DBConnection.CreateCommand();
                cmd.CommandText = string.Format("INSERT INTO {0}.CLTCVL (CVLCAS, CVLTYPE, CVLDATE, CVLVAL, CVLVAL2, CVLMODE, CVLNOTES, CVLCRTUSR, CVLCRTDAT, CVLCHGUSR, CVLCHGDAT, CVLACTPLN) VALUES (@CLIENT_ID, @VITAL_TYPE, @DATE_ENTERED, @VITAL_VALUE_1, @VITAL_VALUE_2, ' ', @CVLNOTES, 'Nashua Vitals Loader', CURRENT TIMESTAMP, 'Nashua Vitals Loader', CURRENT TIMESTAMP, 0)", SCHEMA);
                cmd.DeriveParameters();
                cmd.Parameters["@CLIENT_ID"].Value = clientID;
                cmd.Parameters["@VITAL_TYPE"].Value = vitalType;
                cmd.Parameters["@DATE_ENTERED"].Value = myInterviewDate;

                switch (vitalDescription)
                {
                    case "8 Hour Fasting Vital Loader":
                        string HourFastingInd = string.Empty;
                        if (vitalValue1 == "Y")
                            HourFastingInd = "1";
                        else
                            HourFastingInd = "2";

                        cmd.Parameters["@VITAL_VALUE_1"].Value = Convert.ToDouble(HourFastingInd);
                        cmd.Parameters["@VITAL_VALUE_2"].Value = null;
                        break;
                    case "Blood Pressure Vital Loader":
                        cmd.Parameters["@VITAL_VALUE_1"].Value = vitalValue1;
                        cmd.Parameters["@VITAL_VALUE_2"].Value = vitalValue2;
                        break;
                    case "Abdominal Girth Vital Loader":
                        double abdominalGirthcm = Convert.ToDouble(vitalValue1);
                        double abdominalGirth = abdominalGirthcm * .39;
                        cmd.Parameters["@VITAL_VALUE_1"].Value = Math.Round(abdominalGirth, 1);
                        cmd.Parameters["@VITAL_VALUE_2"].Value = null;
                        break;
                    case "Height Vital Loader":
                        double heightKilo = Convert.ToDouble(vitalValue1);
                        double heightInches = heightKilo * .39;
                        cmd.Parameters["@VITAL_VALUE_1"].Value = Math.Round(heightInches, 1);
                        cmd.Parameters["@VITAL_VALUE_2"].Value = null;
                        break;
                    case "Weight Vital Loader":
                        double weightKilo = Convert.ToDouble(vitalValue1);
                        double weightLBS = weightKilo * 2.2046;
                        cmd.Parameters["@VITAL_VALUE_1"].Value = Math.Round(weightLBS, 1);
                        cmd.Parameters["@VITAL_VALUE_2"].Value = null;
                        break;
                    default:
                        cmd.Parameters["@VITAL_VALUE_1"].Value = Convert.ToDouble(vitalValue1);
                        cmd.Parameters["@VITAL_VALUE_2"].Value = null;
                        break;
                }
                
                cmd.Parameters["@CVLNOTES"].Value = vitalDescription;
                //cmd.Parameters["@VITAL_MASTER_UID"].Value = vitalUID;
                iDB2DataReader db2r1 = cmd.ExecuteReader(CommandBehavior.Default);
                Int32 insertedRow = db2r1.RecordsAffected;
                return insertedRow;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
