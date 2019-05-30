using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Softvision.QA.App.Models
{
    public class Employee
    {
        public Employee()
        {
            Position = new Position();
            Organization = new Organization();
            OracleHcm = new OracleHcm();
            Login = new Login();
            Location = new Location();
            Projects = new HashSet<Project>();
            IsEligibleForAttrition = true;
        }

        [DisplayFormat(DataFormatString = "{0:##}")]
        public Decimal Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string FullName { get { return FirstName + " " + LastName; } }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "DOB")]
        public DateTime? DateOfBirth { get; set; }

        public Int32 Gender { get; set; }

        [Display(Name = "Total Experience")]
        public string TotalExperience { get; set; }

        [Display(Name = "Total IT Experience")]
        public string TotalItExperience { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Joined On")]
        public DateTime? JoiningDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        public string Designation { get; set; }

        public string Grade { get; set; }

        [Display(Name = "Hire Type")]
        public string HireType { get; set; }

        public string Role { get; set; }

        public string Technology { get; set; }

        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }

        public string SpouseName { get; set; }
        /* Contact Details */

        [DataType(DataType.EmailAddress)]
        public string OfficeEmail { get; set; }

        public string PersonalEmail { get; set; }

        public string OfficePhone { get; set; }

        public string HomePhone { get; set; }

        public string Fax { get; set; }

        public string Mobile { get; set; }

        public string HomeTown { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "")]
        public string ExtensionNumber { get; set; }

        public string PermanentAddress1 { get; set; }

        public string PermanentAddress2 { get; set; }

        public string PermanentCity { get; set; }

        public string PermanentState { get; set; }

        public string PermanentPostalCode { get; set; }

        public string PermanentCountry { get; set; }

        public string CurrentAddress1 { get; set; }

        public string CurrentAddress2 { get; set; }

        public string CurrentCity { get; set; }

        public string CurrentState { get; set; }

        public string CurrentPostalCode { get; set; }

        public string CurrentCountry { get; set; }

        public string PassportNumber { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PassportExpiryDate { get; set; }

        public string PassportDocUrl { get; set; }

        /* Organization Details */
        public string Status { get; set; }

        public string PhotoUrl { get; set; }

        public bool IsResourceUtilized { get; set; }

        public string Band { get; set; }

        public string Company { get; set; }

        public string Division { get; set; }

        public string Department { get; set; }

        public string EmployeementStatus { get; set; }

        public string EmployeementTypeStatus { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Career Started On")]
        public DateTime? CareerStartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public string Resume { get; set; }

        public bool IsEligibleForAttrition { get; set; }

        // Reference Properties
        [Display(Name = "Position")]
        public decimal? PositionId { get; set; }

        public Position Position { get; set; }

        [Display(Name = "Organization")]
        public int? OrganizationId { get; set; }

        public Organization Organization { get; set; }

        public int? OracleHcmId { get; set; }
        public OracleHcm OracleHcm { get; set; }

        public string LoginId { get; set; }
        public Login Login { get; set; }

        [Required]
        public int? LocationId { get; set; }
        public Location Location { get; set; }

        public ICollection<Project> Projects { get; set; }
    }

    public class Login
    {
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }

        [DisplayFormat(DataFormatString = "{0:##}")]
        public decimal ClientId { get; set; }

        [DisplayFormat(DataFormatString = "{0:##}")]
        public decimal Position { get; set; }
    }

    public class OracleHcm
    {
        public Int64 Id { get; set; }
        public Int64 Batch { get; set; }
        public DateTime? EndDate { get; set; }
        public Int64 PayrollActionId { get; set; }
        public int NormalHours { get; set; }
        public string BandGrade { get; set; }
        public string PayrollType { get; set; }
        public string EmployeeType { get; set; }
        public string ActionCode { get; set; }
        public string AssignmentStatusPay { get; set; }
        public string SalaryBasis { get; set; }
        public string PayrollName { get; set; }
        public string ManagerFlag { get; set; }
        public decimal SupervisorId { get; set; }
    }

    public class Organization
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }

    public class Position
    {
        public decimal? Id { get; set; }
        public string Name { get; set; }
    }

    public class Location
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }

    public partial class Project
    {
        [DisplayFormat(DataFormatString = "{0:##}")]
        public Decimal? Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Decimal? IndiaManagerId { get; set; }

        public virtual string OffshoreManager { get; set; }

        public string USManager { get; set; }

        public Decimal? UsManagerId { get; set; }

        public virtual string OnsiteManager { get; set; }

        public Decimal? IndiaDirectorId { get; set; }

        public virtual string Gdm { get; set; }

        public Decimal? UsDirectorId { get; set; }

        public virtual string Gem { get; set; }

        public int? AccountManagerId { get; set; }

        public virtual string AccountManager { get; set; }

        public Decimal? OffshoreSrManagerId { get; set; }

        public virtual string OffshoreSrManager { get; set; }

        public Decimal? QubeRepresentativeId { get; set; }

        public virtual string QubeRepresentative { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM, yyyy}")]
        public DateTime? StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM, yyyy}")]
        public DateTime? EndDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM, yyyy}")]
        public DateTime? ActualEndDate { get; set; }

        public string Status { get; set; }

        public string Code { get; set; }

        public string BillingType { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM, yyyy}")]
        public DateTime ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

        public virtual Client Client { get; set; }

        public virtual Location Location { get; set; }

        public virtual Organization Organization { get; set; }
    }

    public class Client
    {
        [DisplayFormat(DataFormatString = "{0:##}")]
        public decimal? Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? CreationOn { get; set; }

        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}