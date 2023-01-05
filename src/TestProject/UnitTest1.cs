using System;
using System.Collections.Generic;
using System.Text;

using DocumentFormat.OpenXml.ExtendedProperties;

using NUnit.Framework;

namespace TestProject
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        private string GetPerTestAttachmentsDirectory()
        {
            var path = TestContext.CurrentContext.WorkDirectory;
            path = System.IO.Path.Combine(path, TestContext.CurrentContext.Test.ID.ToString());
            System.IO.Directory.CreateDirectory(path);
            return path;
        }
        

        [Test]
        public void TestAttachFiles()
        {
            var attachmentDir = GetPerTestAttachmentsDirectory();

            var file1 = System.IO.Path.Combine(attachmentDir, "report (NOT OPENING CORRECTLY).htm");
            System.IO.File.WriteAllText(file1, "<html><body><div> SHOULD OPEN IN BROWSER! </div></body></html>");
            TestContext.AddTestAttachment(file1, "I want to open this Report in a web browser, NOT in a VS tab.html");
            
            var bytes = System.IO.File.ReadAllBytes("box.glb");
            var file2 = System.IO.Path.Combine(attachmentDir, "3D model (NOT OPENING CORRECTLY).glb");
            System.IO.File.WriteAllBytes(file2, bytes);
            TestContext.AddTestAttachment(file2, "I want to open this 3D model in Windows's AR viewer, NOT in a VS Hex tab");

            bytes = System.IO.File.ReadAllBytes("BoxVertexColors.fbx");
            var file3 = System.IO.Path.Combine(attachmentDir, "3D model (Opens in VS crappy model viewer instead of User's viewer).fbx");
            System.IO.File.WriteAllBytes(file3, bytes);
            TestContext.AddTestAttachment(file3, "I want to open this 3D model in Windows's AR viewer, NOT in a VS crappy model viewer");

            bytes = System.IO.File.ReadAllBytes("Svg.svg");
            var file4 = System.IO.Path.Combine(attachmentDir,"report (Opens in browser, but window focus is kept in VS).svg");
            System.IO.File.WriteAllBytes(file4, bytes);
            TestContext.AddTestAttachment(file4,"SVG is opened in a browser, but in some cases, the window focus doesn't jump to it");           
            
        }

        [Test]
        public void TestAttachPlotlyCharts()
        {
            var attachmentDir = GetPerTestAttachmentsDirectory();
            
            var file1 = System.IO.Path.Combine(attachmentDir, "report0 (Should open in User's brownser).html");
            System.IO.File.WriteAllText(file1, System.IO.File.ReadAllText("plotly0.html"));
            TestContext.AddTestAttachment(file1, "I want to open this Plotly report in a web browser, NOT in a VS Hex tab");

            var file2 = System.IO.Path.Combine(attachmentDir, "report1 (Should open in User's brownser).html");
            System.IO.File.WriteAllText(file2, System.IO.File.ReadAllText("plotly1.html"));
            TestContext.AddTestAttachment(file2, "I want to open this Plotly report in a web browser, NOT in a VS Hex tab");
        }

        [Test]
        public void TestAttachImages()
        {
            var attachmentDir = GetPerTestAttachmentsDirectory();
            
            var file1 = System.IO.Path.Combine(attachmentDir, "JPEG image (Should open in User's image viewer).jpg");
            System.IO.File.WriteAllBytes(file1, System.IO.File.ReadAllBytes("shannon.jpg"));
            TestContext.AddTestAttachment(file1, "I want to open this Image in my own image viewer, NOT in a SLOW VS tab");

            var file2 = System.IO.Path.Combine(attachmentDir, "DDS image (Should open in User's image viewer).dds");
            System.IO.File.WriteAllBytes(file2, System.IO.File.ReadAllBytes("shannon.dds"));
            TestContext.AddTestAttachment(file2, "I want to open this Image in my own image viewer, NOT in a SLOW VS tab");

            var file3 = System.IO.Path.Combine(attachmentDir, "WEBP image (Opens in User's image viewer, but bothered every time with an unknown binary message box).webp");
            System.IO.File.WriteAllBytes(file3, System.IO.File.ReadAllBytes("shannon.webp"));
            TestContext.AddTestAttachment(file3, "Opened in my external image viewer.");
        }

        [Test]
        public void TestAttachReports()
        {
            var table = new Dictionary<string, float>();
            var rnd = new Random();

            for(int i=0; i < 100; ++i)
            {
                table[i.ToString()] = rnd.NextSingle();
            }

            // write test report as a markdown:

            var sb = new StringBuilder();
            sb.AppendLine($"|key|value|");
            sb.AppendLine($"|-|-|");
            foreach (var row in table)
            {
                sb.AppendLine($"|{row.Key}|{row.Value}|");
            }
            System.IO.File.WriteAllText("report.md", sb.ToString());
            TestContext.AddTestAttachment("report.md");

            // write test report as excel

            var xx = new ClosedXML.Excel.XLWorkbook();
            var ss = xx.AddWorksheet();
            var col = 1;
            foreach (var rowData in table)
            {
                var row = ss.Row(col);
                row.Cell(1).SetValue(rowData.Key);
                row.Cell(2).SetValue(rowData.Value);
            }
            xx.SaveAs("report.xlsx");
            TestContext.AddTestAttachment("report.xlsx");
        }
    }
}