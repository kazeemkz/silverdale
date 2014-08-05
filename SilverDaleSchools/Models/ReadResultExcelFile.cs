using SilverDaleSchools.DAL;
using SilverDaleSchools.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SilverDaleSchools.Models
{
    public class ReadResultExcelFile
    {
        public void  Read(string file, string fileExtension, string theClass, string session, string term)
        //public async Task Read(string file, string fileExtension, string theClass, string session, string term)
        {
         //   await new ReadResultExcelFile().Read(physicalPath, fileExtension, model.Class, model.Session, model.Term);
            try
            {
                UnitOfWork work = new UnitOfWork();

                List<Result> theResultList = new List<Result>();


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
                    Result theNewResult = new Result();

                   
                   
                    theNewResult.Class = theClass;
                    theNewResult.Session = session;
                    theNewResult.Term = term;
                    theNewResult.StudentNo = dr["Admission No"].ToString();

                    theNewResult.Surname = dr["Surname"].ToString();

                    theNewResult.Firstname = dr["First Name"].ToString();

                    theNewResult.Othernames = dr["Other names"].ToString();

                    theNewResult.Class = dr["Class"].ToString();

                    theNewResult.Sex = dr["Sex"].ToString();

                   // string theV = dr["No# of Times School Opened"].ToString();

                   
                 //   dr.
                    try
                    {
                        theNewResult.NoofTimesSchoolOpened = Convert.ToInt32(dr["No# of Times School Opened"].ToString());
                    }
                    catch (Exception e)
                    {
                        theNewResult.NoofTimesSchoolOpened = 0;
                    }
                    try
                    {
                        theNewResult.NoofTimesPresent = Convert.ToInt32(dr["No# of Times Present"].ToString());
                    }
                    catch(Exception e)
                    {
                        theNewResult.NoofTimesPresent = 0;
                    }

                    try
                    {
                        theNewResult.NoofTimesPunctual = Convert.ToInt32(dr["No# of Times Punctual"].ToString());
                    }
                    catch (Exception e)
                    {
                        theNewResult.NoofTimesPunctual = 0;
                    }

                    try
                    {
                        theNewResult.NoofTimesAbsent = Convert.ToInt32(dr["No# of Times Absent"].ToString());
                    }
                    catch (Exception e)
                    {
                        theNewResult.NoofTimesAbsent = 0;
                    }
                   

                  

                    theNewResult.Fluency = dr["Fluency"].ToString();

                    theNewResult.Games = dr["Games"].ToString();

                    theNewResult.Sports = dr["Sports"].ToString();

                    theNewResult.HandlingofTools = dr["Handling of Tools"].ToString();

                    theNewResult.MusicalSkills = dr["Musical Skills"].ToString();

                    theNewResult.AttentiveandWorkIndependently = dr["Attentive and Works Independently"].ToString();

                    theNewResult.DoesHomeworkRegularly = dr["Does Homework Regularly"].ToString();

                    theNewResult.IsNeatandOrderly = dr["Is Neat and Orderly"].ToString();


                    theNewResult.Respectauthority = dr["Respect authority"].ToString();

                    theNewResult.TakeCareofBooksandProperty = dr["Take Care of Books and Property"].ToString();

                    theNewResult.FormMastersComments = dr["Form Master's Comments"].ToString();


                    theNewResult.PrincipalsComment = dr["Principal's Comment"].ToString();

                    theNewResult.TotalScoreObtainable = Convert.ToDecimal(dr["Total Score Obtainable"].ToString());

                    theNewResult.TotalScoreObtained = Convert.ToDecimal(dr["Total Score Obtained"].ToString());

                    theNewResult.Percentage = Convert.ToDecimal(dr["Percentage"].ToString());


                    //English
                    if (!(string.IsNullOrEmpty(dr["English Language (Total Score)"].ToString())))
                    {
                        theNewResult.EnglishLanguage_Attendance = Convert.ToInt16(dr["English Language (Attendance)"].ToString());
                        theNewResult.EnglishLanguage_ContinuousAssessment = Convert.ToDecimal(dr["English Language (Continuous Assessment)"].ToString());
                        theNewResult.EnglishLanguage_ExaminationScore = Convert.ToDecimal(dr["English Language (Examination Score)"].ToString());
                        theNewResult.EnglishLanguage_TotalScore = Convert.ToDecimal(dr["English Language (Total Score)"].ToString());

                    }
                    //

                    //Yoruba
                    if (!(string.IsNullOrEmpty(dr["Yoruba (Total Score)"].ToString())))
                    {
                        theNewResult.Yoruba_Attendance = Convert.ToInt16(dr["Yoruba (Attendance)"].ToString());
                        theNewResult.Yoruba_ContinuousAssessment = Convert.ToDecimal(dr["Yoruba (Continuous Assessment)"].ToString());
                        theNewResult.Yoruba_ExaminationScore = Convert.ToDecimal(dr["Yoruba (Examination Score)"].ToString());
                        theNewResult.Yoruba_TotalScore = Convert.ToDecimal(dr["Yoruba (Total Score)"].ToString());
                    }

                    //French
                    if (!(string.IsNullOrEmpty(dr["French (Total Score)"].ToString())))
                    {
                        theNewResult.French_Attendance = Convert.ToInt16(dr["French (Attendance)"].ToString());
                        theNewResult.French_ContinuousAssessment = Convert.ToDecimal(dr["French (Continuous Assessment)"].ToString());
                        theNewResult.French_ExaminationScore = Convert.ToDecimal(dr["French (Examination Score)"].ToString());
                        theNewResult.French_TotalScore = Convert.ToDecimal(dr["French (Total Score)"].ToString());
                    }


                    //Literature in English

                string lie =    dr["Literature in English (Attendance)"].ToString();

                if (!(string.IsNullOrEmpty(dr["Literature in English (Total Score)"].ToString())))
                    {
                        theNewResult.LiteratureinEnglish_ContinuousAssessment = Convert.ToDecimal(dr["Literature in English (Continuous Assessment)"].ToString());
                        theNewResult.LiteratureinEnglish_ExaminationScore = Convert.ToDecimal(dr["Literature in English (Examination Score)"].ToString());
                        theNewResult.LiteratureinEnglish_TotalScore = Convert.ToDecimal(dr["Literature in English (Total Score)"].ToString());

                        theNewResult.LiteratureinEnglish_Attendance = Convert.ToInt16(dr["Literature in English (Attendance)"].ToString());
                    }
                   
                   


                    //Igbo
                if (!(string.IsNullOrEmpty(dr["Igbo (Total Score)"].ToString())))
                {
                    theNewResult.Igbo_Attendance = Convert.ToInt16(dr["Igbo (Attendance)"].ToString());
                    theNewResult.Igbo_ContinuousAssessment = Convert.ToDecimal(dr["Igbo (Continuous Assessment)"].ToString());
                    theNewResult.Igbo_ExaminationScore = Convert.ToDecimal(dr["Igbo (Examination Score)"].ToString());
                    theNewResult.Igbo_TotalScore = Convert.ToDecimal(dr["Igbo (Total Score)"].ToString());

                }
                    //Hausa
                if (!(string.IsNullOrEmpty(dr["Hausa (Total Score)"].ToString())))
                {
                    theNewResult.Hausa_Attendance = Convert.ToInt16(dr["Hausa (Attendance)"].ToString());
                    theNewResult.Hausa_ContinuousAssessment = Convert.ToDecimal(dr["Hausa (Continuous Assessment)"].ToString());
                    theNewResult.Hausa_ExaminationScore = Convert.ToDecimal(dr["Hausa (Examination Score)"].ToString());
                    theNewResult.Hausa_TotalScore = Convert.ToDecimal(dr["Hausa (Total Score)"].ToString());

                }
                    //Physics
                if (!(string.IsNullOrEmpty(dr["Physics (Total Score)"].ToString())))
                {
                    theNewResult.Physics_Attendance = Convert.ToInt16(dr["Physics (Attendance)"].ToString());
                    theNewResult.Physics_ContinuousAssessment = Convert.ToDecimal(dr["Physics (Continuous Assessment)"].ToString());
                    theNewResult.Physics_ExaminationScore = Convert.ToDecimal(dr["Physics (Examination Score)"].ToString());
                    theNewResult.Physics_TotalScore = Convert.ToDecimal(dr["Physics (Total Score)"].ToString());

                }

                    //Chemistry
                if (!(string.IsNullOrEmpty(dr["Chemistry (Total Score)"].ToString())))
                {
                    theNewResult.Chemistry_Attendance = Convert.ToInt16(dr["Chemistry (Attendance)"].ToString());
                    theNewResult.Chemistry_ContinuousAssessment = Convert.ToDecimal(dr["Chemistry (Continuous Assessment)"].ToString());
                    theNewResult.Chemistry_ExaminationScore = Convert.ToDecimal(dr["Chemistry (Examination Score)"].ToString());
                    theNewResult.Chemistry_TotalScore = Convert.ToDecimal(dr["Chemistry (Total Score)"].ToString());
                }


                    //Biology
                if (!(string.IsNullOrEmpty(dr["Biology (Total Score)"].ToString())))
                {
                    theNewResult.Biology_Attendance = Convert.ToInt16(dr["Biology (Attendance)"].ToString());
                    theNewResult.Biology_ContinuousAssessment = Convert.ToDecimal(dr["Biology (Continuous Assessment)"].ToString());
                    theNewResult.Biology_ExaminationScore = Convert.ToDecimal(dr["Biology (Examination Score)"].ToString());
                    theNewResult.Biology_TotalScore = Convert.ToDecimal(dr["Biology (Total Score)"].ToString());

                }
                    //Computer
                if (!(string.IsNullOrEmpty(dr["Computer (Total Score)"].ToString())))
                {
                    theNewResult.Computer_Attendance = Convert.ToInt16(dr["Computer (Attendance)"].ToString());
                    theNewResult.Computer_ContinuousAssessment = Convert.ToDecimal(dr["Computer (Continuous Assessment)"].ToString());
                    theNewResult.Computer_ExaminationScore = Convert.ToDecimal(dr["Computer (Examination Score)"].ToString());
                    theNewResult.Computer_TotalScore = Convert.ToDecimal(dr["Computer (Total Score)"].ToString());
                }

                    //Physical Health Education
                if (!(string.IsNullOrEmpty(dr["Physical Health Education (Total Score)"].ToString())))
                {
                    theNewResult.PhysicalHealthEducation_Attendance = Convert.ToInt16(dr["Physical Health Education (Attendance)"].ToString());
                    theNewResult.PhysicalHealthEducation_ContinuousAssessment = Convert.ToDecimal(dr["Physical Health Education (Continuous Assessment)"].ToString());
                    theNewResult.PhysicalHealthEducation_ExaminationScore = Convert.ToDecimal(dr["Physical Health Education (Total Score)"].ToString());
                    theNewResult.PhysicalHealthEducation_TotalScore = Convert.ToDecimal(dr["Physical Health Education (Total Score)"].ToString());
                }


                    //Physical Health Education
                if (!(string.IsNullOrEmpty(dr["Mathematics (Attendance)"].ToString())))
                {
                    theNewResult.Mathematics_Attendance = Convert.ToInt16(dr["Mathematics (Attendance)"].ToString());
                    theNewResult.Mathematics_ContinuousAssessment = Convert.ToDecimal(dr["Mathematics (Continuous Assessment)"].ToString());
                    theNewResult.Mathematics_ExaminationScore = Convert.ToDecimal(dr["Mathematics (Examination Score)"].ToString());
                    theNewResult.Mathematics_TotalScore = Convert.ToDecimal(dr["Mathematics (Total Score)"].ToString());

                }

                    //Further Mathematics
                if (!(string.IsNullOrEmpty(dr["Further Mathematics (Total Score)"].ToString())))
                {
                    theNewResult.FurtherMathematics_Attendance = Convert.ToInt16(dr["Further Mathematics (Attendance)"].ToString());
                    theNewResult.FurtherMathematics_ContinuousAssessment = Convert.ToDecimal(dr["FurtherMathematics (Continuous Assessment)"].ToString());
                    theNewResult.FurtherMathematics_ExaminationScore = Convert.ToDecimal(dr["FurtherMathematics (Examination Score)"].ToString());
                    theNewResult.FurtherMathematics_TotalScore = Convert.ToDecimal(dr["Further Mathematics (Total Score)"].ToString());
                }


                    //Agricultural Science
                if (!(string.IsNullOrEmpty(dr["Agricultural Science (Total Score)"].ToString())))
                {
                    theNewResult.AgriculturalScience_Attendance = Convert.ToInt16(dr["Agricultural Science (Attendance)"].ToString());
                    theNewResult.AgriculturalScience_ContinuousAssessment = Convert.ToDecimal(dr["Agricultural Science (Continuous Assessment)"].ToString());
                    theNewResult.AgriculturalScience_ExaminationScore = Convert.ToDecimal(dr["Agricultural Science (Examination Score)"].ToString());
                    theNewResult.AgriculturalScience_TotalScore = Convert.ToDecimal(dr["Agricultural Science (Total Score)"].ToString());

                }
                    //Food and Nutrition (Attendance)
                if (!(string.IsNullOrEmpty(dr["Food and Nutrition (Total Score)"].ToString())))
                {
                    theNewResult.FoodandNutrition_Attendance = Convert.ToInt16(dr["Food and Nutrition (Attendance)"].ToString());
                    theNewResult.FoodandNutrition_ContinuousAssessment = Convert.ToDecimal(dr["Food and Nutrition (Continuous Assessment)"].ToString());
                    theNewResult.FoodandNutrition_ExaminationScore = Convert.ToDecimal(dr["Food and Nutrition (Examination Score)"].ToString());
                    theNewResult.FoodandNutrition_TotalScore = Convert.ToDecimal(dr["Food and Nutrition (Total Score)"].ToString());

                }
                    //Technical Drawing (Attendance)
                if (!(string.IsNullOrEmpty(dr["Technical Drawing (Total Score)"].ToString())))
                {
                    theNewResult.TechnicalDrawing_Attendance = Convert.ToInt16(dr["Technical Drawing (Attendance)"].ToString());
                    theNewResult.TechnicalDrawing_ContinuousAssessment = Convert.ToDecimal(dr["Technical Drawing (Continuous Assessment)"].ToString());
                    theNewResult.TechnicalDrawing_ExaminationScore = Convert.ToDecimal(dr["Technical Drawing (Examination Score)"].ToString());
                    theNewResult.TechnicalDrawing_TotalScore = Convert.ToDecimal(dr["Technical Drawing (Total Score)"].ToString());

                }

                    //Visual Art  (Attendance)
                if (!(string.IsNullOrEmpty(dr["Visual Art (Total Score)"].ToString())))
                {
                    theNewResult.VisualArt_Attendance = Convert.ToInt16(dr["Visual Art (Attendance)"].ToString());
                    theNewResult.VisualArt_ContinuousAssessment = Convert.ToDecimal(dr["Visual Art (Continuous Assessment)"].ToString());
                    theNewResult.VisualArt_ExaminationScore = Convert.ToDecimal(dr["Visual Art (Examination Score)"].ToString());
                    theNewResult.VisualArt_TotalScore = Convert.ToDecimal(dr["Visual Art (Total Score)"].ToString());
                }

                    //Home Economics 
                if (!(string.IsNullOrEmpty(dr["Home Economics (Total Score)"].ToString())))
                {
                    theNewResult.HomeEconomics_Attendance = Convert.ToInt16(dr["Home Economics (Attendance)"].ToString());
                    theNewResult.HomeEconomics_ContinuousAssessment = Convert.ToDecimal(dr["Home Economics (Continuous Assessment)"].ToString());
                    theNewResult.HomeEconomics_ExaminationScore = Convert.ToDecimal(dr["Home Economics (Examination Score)"].ToString());
                    theNewResult.HomeEconomics_TotalScore = Convert.ToDecimal(dr["Home Economics (Total Score)"].ToString());

                }
                    //Music
                if (!(string.IsNullOrEmpty(dr["Music (Total Score)"].ToString())))
                {
                    theNewResult.Music_Attendance = Convert.ToInt16(dr["Music (Attendance)"].ToString());
                    theNewResult.Music_ContinuousAssessment = Convert.ToDecimal(dr["Music (Continuous Assessment)"].ToString());
                    theNewResult.Music_ExaminationScore = Convert.ToDecimal(dr["Music (Examination Score)"].ToString());
                    theNewResult.Music_TotalScore = Convert.ToDecimal(dr["Music (Total Score)"].ToString());
                }
                    //Economics  
                if (!(string.IsNullOrEmpty(dr["Economics (Total Score)"].ToString())))
                {
                    theNewResult.Economics_Attendance = Convert.ToInt16(dr["Economics (Attendance)"].ToString());
                    theNewResult.Economics_ContinuousAssessment = Convert.ToDecimal(dr["Economics (Continuous Assessment)"].ToString());
                    theNewResult.Economics_ExaminationScore = Convert.ToDecimal(dr["Economics (Examination Score)"].ToString());
                    theNewResult.Economics_TotalScore = Convert.ToDecimal(dr["Economics (Total Score)"].ToString());
                }


                    //Geography   
                if (!(string.IsNullOrEmpty(dr["Geography (Examination Score)"].ToString())))
                {
                    theNewResult.Geography_Attendance = Convert.ToInt16(dr["Geography (Attendance)"].ToString());
                    theNewResult.Geography_ContinuousAssessment = Convert.ToDecimal(dr["Geography (Continuous Assessment)"].ToString());
                    theNewResult.Geography_ExaminationScore = Convert.ToDecimal(dr["Economics (Examination Score)"].ToString());
                    theNewResult.Geography_TotalScore = Convert.ToDecimal(dr["Geography (Examination Score)"].ToString());
                }



                    //Financial Accounting   
                if (!(string.IsNullOrEmpty(dr["Financial Accounting (Attendance)"].ToString())))
                {
                    theNewResult.FinancialAccounting_Attendance = Convert.ToInt16(dr["Financial Accounting (Attendance)"].ToString());
                    theNewResult.FinancialAccounting_ContinuousAssessment = Convert.ToDecimal(dr["Financial Accounting (Continuous Assessment)"].ToString());
                    theNewResult.FinancialAccounting_ExaminationScore = Convert.ToDecimal(dr["Financial Accounting (Examination Score)"].ToString());
                    theNewResult.FinancialAccounting_TotalScore = Convert.ToDecimal(dr["Financial Accounting (Total Score)"].ToString());

                }
                    //History
                if (!(string.IsNullOrEmpty(dr["History (Total Score)"].ToString())))
                {
                    theNewResult.History_Attendance = Convert.ToInt16(dr["History (Attendance)"].ToString());
                    theNewResult.History_ContinuousAssessment = Convert.ToDecimal(dr["History (Continuous Assessment)"].ToString());
                    theNewResult.History_ExaminationScore = Convert.ToDecimal(dr["History (Examination Score))"].ToString());
                    theNewResult.History_TotalScore = Convert.ToDecimal(dr["History (Total Score)"].ToString());
                }


                    //Business Studies
                if (!(string.IsNullOrEmpty(dr["Business Studies (Total Score)"].ToString())))
                {
                    theNewResult.BusinessStudies_Attendance = Convert.ToInt16(dr["Business Studies (Attendance)"].ToString());
                    theNewResult.BusinessStudies_ContinuousAssessment = Convert.ToDecimal(dr["Business Studies (Continuous Assessment)"].ToString());
                    theNewResult.BusinessStudies_ExaminationScore = Convert.ToDecimal(dr["Business Studies (Continuous Assessment)"].ToString());
                    theNewResult.BusinessStudies_TotalScore = Convert.ToDecimal(dr["Business Studies (Total Score)"].ToString());
                }


                    //Social Studies
                if (!(string.IsNullOrEmpty(dr["Social Studies (Total Score)"].ToString())))
                {
                    theNewResult.SocialStudies_Attendance = Convert.ToInt16(dr["Social Studies (Attendance)"].ToString());
                    theNewResult.SocialStudies_ContinuousAssessment = Convert.ToDecimal(dr["Social Studies (Continuous Assessment)"].ToString());
                    theNewResult.SocialStudies_ExaminationScore = Convert.ToDecimal(dr["Social Studies (Examination Score)"].ToString());
                    theNewResult.SocialStudies_TotalScore = Convert.ToDecimal(dr["Social Studies (Total Score)"].ToString());
                }


                    //Commerce
                if (!(string.IsNullOrEmpty(dr["Commerce (Total Score)"].ToString())))
                {
                    theNewResult.Commerce_Attendance = Convert.ToInt16(dr["Commerce(Attendance)"].ToString());
                    theNewResult.Commerce_ContinuousAssessment = Convert.ToDecimal(dr["Commerce (Continuous Assessment)"].ToString());
                    theNewResult.Commerce_ExaminationScore = Convert.ToDecimal(dr["Commerce (Examination Score)"].ToString());
                    theNewResult.Commerce_TotalScore = Convert.ToDecimal(dr["Commerce (Total Score)"].ToString());

                }
                    //Government_Attendance
                if (!(string.IsNullOrEmpty(dr["Government (Total Score)"].ToString())))
                {
                    theNewResult.Government_Attendance = Convert.ToInt16(dr["Government(Attendance)"].ToString());
                    theNewResult.Government_ContinuousAssessment = Convert.ToDecimal(dr["Government (Continuous Assessment)"].ToString());
                    theNewResult.Government_ExaminationScore = Convert.ToDecimal(dr["Government (Examination Score)"].ToString());
                    theNewResult.Government_TotalScore = Convert.ToDecimal(dr["Government (Total Score)"].ToString());
                }


                    //Christian Religious Knowledge
                if (!(string.IsNullOrEmpty(dr["Christian Religious Knowledge (Total Score)"].ToString())))
                {
                    theNewResult.ChristianReligiousKnowledge_Attendance = Convert.ToInt16(dr["Christian Religious Knowledge (Attendance)"].ToString());
                    theNewResult.ChristianReligiousKnowledge_ContinuousAssessment = Convert.ToDecimal(dr["Christian Religious Knowledge (Continuous Assessment)"].ToString());
                    theNewResult.ChristianReligiousKnowledge_ExaminationScore = Convert.ToDecimal(dr["Christian Religious Knowledge (Examination Score)"].ToString());
                    theNewResult.ChristianReligiousKnowledge_TotalScore = Convert.ToDecimal(dr["Christian Religious Knowledge (Total Score)"].ToString());
                }

                    //Islamic Religious Knowledge(Attendance
                if (!(string.IsNullOrEmpty(dr["Islamic Religious Knowledge (Total Score)"].ToString())))
                {
                    theNewResult.IslamicReligiousKnowledge_Attendance = Convert.ToInt16(dr["Islamic Religious Knowledge(Attendance)"].ToString());
                    theNewResult.IslamicReligiousKnowledge_ContinuousAssessment = Convert.ToDecimal(dr["Islamic Religious Knowledge (Continuous Assessment)"].ToString());
                    theNewResult.IslamicReligiousKnowledge_ExaminationScore = Convert.ToDecimal(dr["Islamic Religious Knowledge (Examination Score)"].ToString());
                    theNewResult.IslamicReligiousKnowledge_TotalScore = Convert.ToDecimal(dr["Islamic Religious Knowledge (Total Score)"].ToString());

                }
                    //Royal English
                if (!(string.IsNullOrEmpty(dr["Royal English (Total Score)"].ToString())))
                {
                    theNewResult.RoyalEnglish_Attendance = Convert.ToInt16(dr["Royal English (Attendance)"].ToString());
                    theNewResult.RoyalEnglish_ContinuousAssessment = Convert.ToDecimal(dr["Royal English (Continuous Assessment)"].ToString());
                    theNewResult.RoyalEnglish_ExaminationScore = Convert.ToDecimal(dr["Royal English (Examination Score)"].ToString());
                    theNewResult.RoyalEnglish_TotalScore = Convert.ToDecimal(dr["Royal English (Total Score)"].ToString());

                }

                //    //Royal English
                //if (!(string.IsNullOrEmpty(dr["Royal English (Total Score)"].ToString())))
                //{
                //    theNewResult.RoyalEnglish_Attendance = Convert.ToInt16(dr["Royal English(Attendance)"].ToString());
                //    theNewResult.RoyalEnglish_ContinuousAssessment = Convert.ToDecimal(dr["Royal English (Continuous Assessment)"].ToString());
                //    theNewResult.RoyalEnglish_ExaminationScore = Convert.ToDecimal(dr["Royal English (Examination Score)"].ToString());
                //    theNewResult.RoyalEnglish_TotalScore = Convert.ToDecimal(dr["Royal English (Total Score)"].ToString());
                //}

                    //Basic Science
                if (!(string.IsNullOrEmpty(dr["Basic Science (Total Score)"].ToString())))
                {
                    theNewResult.BasicScience_Attendance = Convert.ToInt16(dr["Basic Science (Attendance)"].ToString());
                    theNewResult.BasicScience_ContinuousAssessment = Convert.ToDecimal(dr["Basic Science (Continuous Assessment)"].ToString());
                    theNewResult.BasicScience_ExaminationScore = Convert.ToDecimal(dr["Basic Science (Examination Score)"].ToString());
                    theNewResult.BasicScience_TotalScore = Convert.ToDecimal(dr["Basic Science (Total Score)"].ToString());
                }
                    //Basic Technology
                if (!(string.IsNullOrEmpty(dr["Basic Technology (Total Score)"].ToString())))
                {
                    theNewResult.BasicTechnology_Attendance = Convert.ToInt16(dr["Basic Technology (Attendance)"].ToString());
                    theNewResult.BasicTechnology_ContinuousAssessment = Convert.ToDecimal(dr["Basic Technology (Continuous Assessment)"].ToString());
                    theNewResult.BasicTechnology_ExaminationScore = Convert.ToDecimal(dr["Basic Technology (Examination Score)"].ToString());
                    theNewResult.BasicTechnology_TotalScore = Convert.ToDecimal(dr["Basic Technology (Total Score)"].ToString());
                }
                    theResultList.Add(theNewResult);
                    work.ResultRepository.Insert(theNewResult);
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