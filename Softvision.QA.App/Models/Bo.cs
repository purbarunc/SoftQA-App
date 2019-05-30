using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Softvision.QA.App.Models
{
    public class Bo
    {
        private static volatile Bo _instance;
        private static readonly object SyncRoot = new Object();

        public static Bo Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new Bo();
                    }
                }
                return _instance;
            }
        }

        public Employee GetEmployee(string login)
        {
            using (DataSet ds = Dao.Instance.GetEmployee(login))
            {
                return GetEmployeeDetails(ds);
            }
        }

        public Employee GetEmployee(int employeeId)
        {
            using (DataSet ds = Dao.Instance.GetEmployee(employeeId))
            {
                return GetEmployeeDetails(ds);
            }
        }

        private Employee GetEmployeeDetails(DataSet ds)
        {
            Employee obj = null;
            ICollection<Project> projects;
            try
            {
                using (ds)
                {
                    projects = ds.Tables[1].AsEnumerable().Select(row => new Project
                    {
                        Id = row.Field<Decimal>("Project_Id"),
                        Name = row.Field<string>("Prj_Name"),
                        OffshoreManager = row.Field<string>("OffshoreManager"),
                        //   USManager=row.Field<string>("USManager"),
                        Client = new Client
                        {
                            Id = row.Field<Decimal>("ClientId"),
                            Name = row.Field<string>("ClientName")
                        }
                    }).ToList();

                    obj = ds.Tables[0].AsEnumerable().Select(row => new Employee
                    {
                        Id = row.Field<Decimal>("Emp_id"),
                        FirstName = row.Field<string>("Emp_Fname"),
                        MiddleName = row.Field<string>("Emp_Mname"),
                        LastName = row.Field<string>("Emp_Lname"),
                        DateOfBirth = row.Field<DateTime?>("Emp_date_of_birth"),
                        Gender = row.Field<Int32>("emp_gender_id"),
                        TotalExperience = row.Field<string>("Emp_total_experience"),
                        TotalItExperience = row.Field<string>("Emp_IT_experience"),
                        JoiningDate = row.Field<DateTime?>("Emp_join_date"),
                        EndDate = row.Field<DateTime?>("Emp_End_Date"),
                        Designation = row.Field<string>("Emp_Designation"),
                        Grade = row.Field<string>("Emp_Grade"),
                        HireType = row.Field<string>("Emp_HireType"),
                        Role = row.Field<string>("Emp_role"),
                        Technology = row.Field<string>("Emp_category"),
                        MaritalStatus = row.Field<string>("Emp_marital_status"),
                        SpouseName = row.Field<string>("Emp_spouse_name"),
                        OfficeEmail = row.Field<string>("Emp_email_id1"),
                        PersonalEmail = row.Field<string>("Emp_email_id2"),
                        OfficePhone = row.Field<string>("Emp_office_phone"),
                        HomePhone = row.Field<string>("Emp_home_phone"),
                        Fax = row.Field<string>("Emp_fax_no"),
                        Mobile = row.Field<string>("Emp_mobile_no"),

                        ExtensionNumber = row.Field<string>("Emp_Extn_Number"),

                        HomeTown = row.Field<string>("Emp_hometown"),
                        PermanentAddress1 = row.Field<string>("Emp_permt_address1"),
                        PermanentAddress2 = row.Field<string>("Emp_permt_address2"),
                        PermanentCity = row.Field<string>("Emp_permt_city"),
                        PermanentState = row.Field<string>("Emp_permt_state"),
                        PermanentPostalCode = row.Field<string>("Emp_permt_zip"),
                        PermanentCountry = row.Field<string>("Emp_permt_country"),
                        CurrentAddress1 = row.Field<string>("Emp_pre_address1"),
                        CurrentAddress2 = row.Field<string>("Emp_pre_address2"),
                        CurrentCity = row.Field<string>("Emp_pre_city"),
                        CurrentState = row.Field<string>("Emp_pre_state"),
                        CurrentPostalCode = row.Field<string>("Emp_pre_zip"),
                        CurrentCountry = row.Field<string>("Emp_pre_country"),

                        Status = row.Field<string>("Emp_status"),
                        Band = row.Field<string>("Emp_Band"),
                        Company = row.Field<string>("Emp_Company"),
                        Division = row.Field<string>("Emp_Division"),
                        Department = row.Field<string>("Emp_Department"),
                        EmployeementStatus = row.Field<string>("EmploymentStatus"),
                        EmployeementTypeStatus = row.Field<string>("Emp_EmploymentTypeStatus"),
                        CareerStartDate = row.Field<DateTime?>("Emp_Career_Start_Date"),
                        PositionId = row.Field<decimal?>("LgInfo_User_Position"),
                        Position = new Position
                        {
                            Id = row.Field<decimal?>("LgInfo_User_Position"),
                            Name = row.Field<string>("Emp_position"),
                        },
                        OrganizationId = row.Field<int?>("emp_Organisation"),
                        Organization = new Organization
                        {
                            Id = row.Field<int?>("emp_Organisation")
                        },
                        OracleHcm = new OracleHcm
                        {
                            Id = row.Field<Int64>("HcmPersonId"),
                            BandGrade = row.Field<string>("Emp_HcmBandGrade"),
                            PayrollType = row.Field<string>("Emp_Payroll_Type"),
                            EmployeeType = row.Field<string>("Emp_HCM_EmployeeType"),
                            AssignmentStatusPay = row.Field<string>("Emp_AssignmentStatusPay"),
                            SalaryBasis = row.Field<string>("Emp_SalaryBasis"),
                            PayrollName = row.Field<string>("Emp_PayrollName")
                            //ManagerFlag = row.Field<string>("HCM_Manager_Flag")
                            //SupervisorId = row.Field<decimal>("HCM_Supervisor_Id"),
                        },
                        Login = new Login
                        {
                            LoginId = row.Field<string>("Login_Id"),
                            //EmployeeId = row.Field<decimal>("LgInfo_Resource_Id"),
                            ClientId = row.Field<decimal>("LgInfo_Client_Id"),
                            Status = row.Field<string>("LgInfo_Status"),
                            Position = row.Field<decimal>("LgInfo_User_Position"),
                        },
                        LocationId = row.Field<int?>("Emp_location_Id"),
                        Location = new Location
                        {
                            Id = row.Field<int?>("Emp_location_Id"),
                            Name = row.Field<string>("Emp_location")
                        },
                        Projects = projects
                    }).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "Business Access Policy"))
                    throw;
            }
            return obj;
        }
    }
}