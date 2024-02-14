
// //
// DESIGN AND DEVELOPED BY ADD INNOVATION 
// A MACHINE VISION SYSTEM 
// //
using System.IO;
using System.Text;

namespace FoilInspection
{
    public class CreateFIle_with_format
    {

        private int n;
        private string temp;
        // //set name and type of file as persistance
        private string logpath = @"C:\Add_innovation";
        public int setNameandTypeOfFile(string fname, string type_of_file)
        {
            temp = fname + "." + type_of_file;
            if (File.Exists(logpath + @"\" + temp))
            {
                // MsgBox("file is already exist same name and same type ")
                return 1;
            }
            else
            {
                using (var wr = File.AppendText(logpath + @"\" + temp))
                {
                    // MsgBox("your file is created  ")
                }
            }
            return 0;
        }

        // // number of column in grid sheet
        public int no_of_field(int noofcolumn)
        {
            n = noofcolumn;
            return 0;
        }

        // // set the column name by spliting the string 
        public int setcolumnName(string name)
        {
            var builder = new StringBuilder();
            string[] strArr;
            int count;
            strArr = name.Split(' ');
            var loopTo = strArr.Length - 1;
            for (count = 0; count <= loopTo; count++)
            {
                if (count < strArr.Length - 1)
                {
                    builder.Append(strArr[count]);
                    builder.Append(",");
                }
                else
                {
                    builder.Append(strArr[count]);
                }
            }
            string s = builder.ToString();
            using (var wr = File.AppendText(logpath + @"\" + temp))
            {
                wr.WriteLine(s);
            }
            return 0;
        }
        // // end of function 


        // // set data to grid cell 
        public int setdattocolumn(ref string valstring)
        {

            var builder = new StringBuilder();
            string[] strArr;
            int count;

            strArr = valstring.Split(' ');
            var loopTo = strArr.Length - 1;
            for (count = 0; count <= loopTo; count++)
            {
                if (count < strArr.Length - 1)
                {
                    builder.Append(strArr[count]);
                    builder.Append(",");
                }
                else
                {
                    builder.Append(strArr[count]);
                }
            }
            string s = builder.ToString();
            using (var wr = File.AppendText(logpath + @"\" + temp))
            {
                wr.WriteLine(s);
            }
            return 0;
        }


    }
}