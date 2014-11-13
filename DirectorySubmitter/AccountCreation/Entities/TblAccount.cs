using System;
using System.Text;
using System.Collections.Generic;
namespace DirectorySubmitterDB.DEL
{
	public partial class TblAccount
	{
		#region Constructor
        public TblAccount()
		{
			
		}//Closed Constructor TblAccount()
		#endregion

		#region All Properties
		public int AccountId {get; set;} 
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
		public string FirstNamePlaceHolder {get; set;} 
		public string LastNamePlaceHolder {get; set;} 
		#endregion
	}//Closed Class TblAccount	
}//Closed Namespace DirectorySubmitterDB.DEL
