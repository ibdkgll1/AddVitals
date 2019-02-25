using System;

using System.Data;
using System.Linq;
using System.Text;
using AddVitals.Data_Access;
using IBM.Data.DB2.iSeries;
using System.Collections.Generic;

namespace AddVitals.Business_Logic
{
    class BusinessLogic
    {
        DataAccess DA = new DataAccess();

        public DataTable getVitalType(iDB2Connection DBConnection, string SCHEMA, string vitalDescription)
        {
            try 
	        {	        
		        DataTable myDT = DA.getVitalType(DBConnection, SCHEMA, vitalDescription);
                return myDT;
	        }
	        catch (Exception ex)
	        {		
		        throw new Exception(ex.Message);
	        }
        }

        public Int32 processVital(iDB2Connection DBConnection, string SCHEMA, string clientID, string vitalType, string vitalDescription, string vitalValue1, string vitalValue2, string myInterviewDate)
        {
            try
            {
                Int32 insertedRow = DA.addVital(DBConnection, SCHEMA, clientID, vitalType, vitalDescription, vitalValue1, vitalValue2, myInterviewDate);
                return insertedRow;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
