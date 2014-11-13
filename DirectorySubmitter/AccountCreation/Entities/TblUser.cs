using System;
using System.Text;
using System.Collections.Generic;
namespace DirectorySubmitterDB.DEL
{
	public partial class TblUser
	{
		#region Constructor
        public TblUser()
		{
		}//Closed Constructor TblUser()
		#endregion

		#region All Properties
		public int UserId {get; set;} 
		public string FirstName {get; set;} 
		public string LastName {get; set;} 
		public string UserName {get; set;} 
		public string Passwords {get; set;} 
		public string ConfirmPasswords {get; set;} 
		public string DOBMonth {get; set;} 
		public string DOBDay {get; set;} 
		public string DOBYear {get; set;} 
		public string Gender {get; set;} 
		public string MobileNo {get; set;} 
		public string CurrentEmail {get; set;} 
		public string Captchas {get; set;} 
		public string Locations {get; set;} 
		public string Orders {get; set;} 
		public string AgreeTerms {get; set;} 
		public string ZipCode {get; set;} 
		public string AccountType {get; set;} 
		public string CountryCode {get; set;} 
		#endregion
	}//Closed Class TblUser	
}//Closed Namespace DirectorySubmitterDB.DEL
