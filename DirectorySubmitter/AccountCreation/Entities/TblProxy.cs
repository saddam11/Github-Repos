using System;
using System.Text;
using System.Collections.Generic;
namespace DirectorySubmitterDB.DEL
{
	public partial class TblProxy
	{
		#region Constructor
        public TblProxy()
		{
		}//Closed Constructor TblProxy()
		#endregion

		#region All Properties
		public int ProxyId {get; set;} 
		public string IPAddress {get; set;} 
		public string Port {get; set;} 
		public string Country {get; set;} 
		public string Type {get; set;} 
		public DateTime StartTime {get; set;} 
		public DateTime EndTime {get; set;} 
		public int TotalProcess {get; set;} 
		public int SuccessProcess {get; set;} 
		#endregion
	}//Closed Class TblProxy	
}//Closed Namespace DirectorySubmitterDB.DEL
