using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DirectorySubmitterDB.DEL;
using AccountCreation.Helper;
using AccountCreation.Constants;

namespace AccountCreation
{
    public class MappingAttributes
    {
        Connection objConnection;
        public MappingAttributes ()
	    {
            objConnection = new Connection();
	    }
            
        public bool MapFor(string strAccountType)
        {
            bool blnResult=false;
            //switch (strAccountType)
            //{
            //    case AccountTypes.GMail:
            //        strQuery = "";
            //        objConnection.GetAll(strQuery);
            //        string strQueryAtttribute = "Select " + txtSearh.Tag + " from TblAccount Where AccountType='" + txtSearh.Text + "' ";
            //        string strDBVal = "Select " + txtSearh.Tag + " from TblUser ";

            //        string[] strAttributeName = objConnection.GetAll(strQueryAtttribute).Tables[0].Rows[0].ItemArray.Select(x => x.ToString()).ToArray();
            //        string[] strDBValues = objConnection.GetAll(strDBVal).Tables[0].Rows[0].ItemArray.Select(x => x.ToString()).ToArray();
            //        ChangeMyBroweser(strAttributeName, strDBValues);

            //        blnResult = true;
            //        break;
            //    default:
            //        break;
            //}
            return(blnResult);
        }

        public string strQuery { get; set; }
    }
}
