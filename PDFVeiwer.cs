using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shop_Invoice_billing__system
{
    public partial class PDFVeiwer : Form
    {
        public PDFVeiwer()
        {
            InitializeComponent();
        }
        public PDFVeiwer(string ReportSourceFile, string reportFolderPath, DataTable _dataSource, DataTable _dataSource2)
        {
            InitializeComponent();
            ReportDocument rptDoc = new ReportDocument();
            var path = System.IO.Path.GetFullPath(string.Format("{0}\\{1}", reportFolderPath, ReportSourceFile));
             if (!File.Exists(path))
            {
                MessageBox.Show(string.Format("Report File Not found!\r\n{0}", path));
                return;
            }
            rptDoc.Load(path); 
                DataSet ds = new DataSet();
                DataTable dat1 = new DataTable();
                dat1 = _dataSource;
                dat1.TableName = "DataTable1";
                ds.Tables.Add(dat1);
                DataTable dat2 = new DataTable();
                dat2 = _dataSource2;
                dat2.TableName = "DataTable2";
                ds.Tables.Add(dat2);
                rptDoc.SetDataSource(ds);  
                crystalReportViewer1.ReportSource = rptDoc;
                crystalReportViewer1.Refresh();

        }

    }
}
