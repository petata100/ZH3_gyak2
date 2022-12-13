using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using FormsApp.Models;

namespace FormsApp
{
    public partial class ucExcel : UserControl
    {
        Excel.Application xlApp;
        Excel.Workbook xlWb;
        Excel.Worksheet xlSheet;

        public ucExcel()
        {
            InitializeComponent();
        }

        public void CreateTable()
        {
            string[] fejlécek = new string[]{
                "MennyisegiEgysegId",
                "Egysegnev"
            };

            for (int i = 0; i < fejlécek.Count(); i++)
            {
                xlSheet.Cells[1, i + 1] = fejlécek[i];
            }

            ReceptContext context = new();
            var megysegek = context.MennyisegiEgysegek.ToList();

            object[,] adatTömb = new object[megysegek.Count(), fejlécek.Count()];

            for (int i = 0; i < megysegek.Count(); i++)
            {
                adatTömb[i, 0] = megysegek[i].MennyisegiEgysegId;
                adatTömb[i, 1] = megysegek[i].EgysegNev;
            }

            int sorokSzáma = megysegek.Count();
            int oszlopokSzáma = fejlécek.Count();

            Excel.Range adatRange = xlSheet.get_Range("A2", Type.Missing).get_Resize(sorokSzáma, oszlopokSzáma);
            adatRange.Value2 = adatTömb;


            Excel.Range fejlécRange = xlSheet.get_Range("A1", Type.Missing).get_Resize(1, oszlopokSzáma);
            fejlécRange.Font.Bold = true;
        }

        public void CreateExcel()
        {
            try
            {
                xlApp = new Excel.Application();
                xlWb = xlApp.Workbooks.Add(Missing.Value);
                xlSheet = xlWb.ActiveSheet;

                CreateTable();

                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                xlWb.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlApp = null;
                xlWb = null;
            }
        }

        private void bExport_Click(object sender, EventArgs e)
        {
            CreateExcel();
        }
    }
}
