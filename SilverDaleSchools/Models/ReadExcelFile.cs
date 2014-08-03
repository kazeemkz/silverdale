//using LinqToExcel;
using Remotion.Data.Linq;
using SilverDaleSchools.DAL;
using SilverDaleSchools.Model;
using SilverDalesSchools.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace SilverDaleSchools.Models
{
    public class ReadExcelFile
    {

        public async Task Read(string file, string fileExtension)
        {

            try
            {
                UnitOfWork work = new UnitOfWork();

                List<Student> theStudentList = new List<Student>();


                string conString = null;

                if (fileExtension.ToLower() == ".xls")
                {
                    conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + file + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\""; ;
                }
                else if (fileExtension.ToLower() == ".xlsx")
                {
                    conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + file + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                }

                string query = "Select * from [Sheet1$]";
                OleDbConnection con = new OleDbConnection(conString);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                OleDbCommand cmd = new OleDbCommand(query, con);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);

                DataSet ds = new DataSet();
                da.Fill(ds);
                da.Dispose();
                con.Close();
                con.Dispose();

                // Import to Database
                //using (MuDatabaseEntities dc = new MuDatabaseEntities())
                //{
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    //    // Insert
                    theStudentList.Add(new Student
                    {
                        UserID =( dr["Student Number"].ToString()),
                        LastName = dr["Surname"].ToString(),
                        FirstName = dr["Firstname"].ToString(),
                         Middle = dr["Lastname"].ToString(),
                        Sex = dr["Sex"].ToString(),

                        PhoneNumber = dr["Phone_number"].ToString(),
                        EmailAddress = dr["Email_Address"].ToString(),

                        Address = dr["Address"].ToString(),
                        ParentName = dr["Parent Name"].ToString(),
                        ParentAddress = dr["Parent Address"].ToString(),
                        ParentPhoneNumber = dr["ParentPhoneNo"].ToString(),
                        LGAName = dr["LGAName"].ToString(),
                        StateName = dr["StateName"].ToString(),
                        CountryName = dr["CountryName"].ToString(),
                        LocalLanguageName = dr["LocalLanguageName"].ToString(),
                            Role = "Student",
                                             
                     
                    });
                    //}
                }

                foreach(Student s in theStudentList)
                {
                    work.StudentRepository.Insert(s);

                     if (Membership.GetUser(s.UserID.ToString()) == null)
                     {
                         if (string.IsNullOrEmpty(s.EmailAddress))
                         {
                             s.EmailAddress = "student@yahoo.com";
                         }
                         Membership.CreateUser(s.UserID.ToString(), PaddPassword.Padd(s.LastName), s.EmailAddress);
                         Roles.AddUserToRole(s.UserID.ToString(), s.Role);
                          work.StudentRepository.Insert(s);
                         //  work.Save();
                     }
                }
                work.Save();
                if (System.IO.File.Exists(file))
                {

                    System.IO.File.Delete(file);

                }


            }

            catch (Exception e)
            {

                if (System.IO.File.Exists(file))
                {

                    System.IO.File.Delete(file);

                }
            }






            //UnitOfWork work = new UnitOfWork();
            ////var excel = new ExcelQueryFactory(file);
            ////List<Student> list = from c in excel.Worksheet()
            ////                     select c as List<Student>;

            //// List<Student> theStudents = list as List<Student>;
            ////  List<Student> theStudents = new List<Student>();
            //Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            //Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@file);
            //Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            //Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;

            //int rowCount = xlRange.Rows.Count;
            //int colCount = xlRange.Columns.Count;
            //List<Student> theListOfAllStudents = new List<Student>();
            //try
            //{




            //    for (int i = 2; i <= rowCount; i++)
            //    {
            //        int counter = 1;
            //        Student theNewStudent = new Student();
            //        for (int j = 1; j <= colCount; j++)
            //        {
            //            // counter = counter + 1;
            //            if (xlRange.Cells[i, j].Value2 != null)
            //            {
            //                if (counter == 1)
            //                {

            //                    string theUserIDString = xlRange.Cells[i, j].Value2.ToString();
            //                    theNewStudent.UserID = Convert.ToInt32(theUserIDString.TrimStart().TrimEnd()); ;
            //                }

            //                if (counter == 2)
            //                {
            //                    theNewStudent.LastName = xlRange.Cells[i, j].Value2.ToString();
            //                }

            //                if (counter == 3)
            //                {
            //                    theNewStudent.FirstName = xlRange.Cells[i, j].Value2.ToString();
            //                }

            //                if (counter == 4)
            //                {
            //                    theNewStudent.Middle = xlRange.Cells[i, j].Value2.ToString();
            //                }

            //                if (counter == 5)
            //                {
            //                    theNewStudent.Sex = xlRange.Cells[i, j].Value2.ToString();
            //                }

            //                if (counter == 6)
            //                {
            //                    theNewStudent.PhoneNumber = xlRange.Cells[i, j].Value2.ToString();
            //                }

            //                if (counter == 7)
            //                {
            //                    theNewStudent.EmailAddress = xlRange.Cells[i, j].Value2.ToString();
            //                }
            //                if (counter == 8)
            //                {
            //                    theNewStudent.Address = xlRange.Cells[i, j].Value2.ToString();
            //                }

            //                if (counter == 9)
            //                {
            //                    theNewStudent.ParentName = xlRange.Cells[i, j].Value2.ToString();
            //                }
            //                if (counter == 10)
            //                {
            //                    theNewStudent.ParentAddress = xlRange.Cells[i, j].Value2.ToString();
            //                }

            //                if (counter == 11)
            //                {
            //                    theNewStudent.ParentPhoneNumber = xlRange.Cells[i, j].Value2.ToString();
            //                }
            //                if (counter == 12)
            //                {
            //                    theNewStudent.LGAName = xlRange.Cells[i, j].Value2.ToString();
            //                }

            //                if (counter == 13)
            //                {
            //                    theNewStudent.StateName = xlRange.Cells[i, j].Value2.ToString();
            //                }
            //                if (counter == 14)
            //                {
            //                    theNewStudent.CountryName = xlRange.Cells[i, j].Value2.ToString();
            //                }

            //                if (counter == 15)
            //                {
            //                    theNewStudent.LocalLanguageName = xlRange.Cells[i, j].Value2.ToString();
            //                }
            //                // Console.WriteLine(xlRange.Cells[i, j].Value2.ToString());
            //            }
            //            else
            //            {
            //                //  if (counter == 1)
            //                // {

            //                // }
            //            }

            //            counter = counter + 1;
            //        }
            //        theListOfAllStudents.Add(theNewStudent);
            //        theNewStudent.Role = "Student";
            //        work.StudentRepository.Insert(theNewStudent);

            //    }
            //    foreach(Student s in theListOfAllStudents )
            //    {
            //        if (Membership.GetUser(s.UserID.ToString()) == null)
            //        {
            //            Membership.CreateUser(s.UserID.ToString(), PaddPassword.Padd(s.LastName), s.EmailAddress);
            //            Roles.AddUserToRole(s.UserID.ToString(), s.Role);
            //           // work.StaffRepository.Insert(model);
            //          //  work.Save();
            //        }
            //    }
            //    work.Save();

            //    if (System.IO.File.Exists(file))
            //    {
            //        xlWorkbook.Close(true);
            //        xlApp.Quit();
            //        xlRange = null;
            //        xlWorksheet = null;
            //        System.IO.File.Delete(file);

            //    }


            //}

            //catch (Exception e)
            //{

            //    if (System.IO.File.Exists(file))
            //    {
            //        xlWorkbook.Close(true);
            //        xlApp.Quit();
            //        xlRange = null;
            //        xlWorksheet = null;
            //        System.IO.File.Delete(file);

            //    }
            //}





        }
    }
}