using CarSales.Application.DTOs;
using CarSales.Application.Interfaces;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Infrastructure.Excel
{
    public class ExcelExportService : IExcelExportService
    {
        public byte[] ExportMonthlySalesToExcel(List<MonthlySalesReportDto> reports)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Отчёт по продажам");

            // Заголовки на русском
            var headers = new List<string>
            {
                "Марка", "Модель", "Год",
                "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь",
                "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь",
                "Итого"
             };

            for (int i = 0; i < headers.Count; i++)
            {
                var cell = worksheet.Cell(1, i + 1);
                cell.Value = headers[i];
                cell.Style.Font.Bold = true;
                cell.Style.Fill.BackgroundColor = XLColor.LightGray;
                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }

            // Данные
            int row = 2;
            foreach (var report in reports)
            {
                worksheet.Cell(row, 1).Value = report.Brand;
                worksheet.Cell(row, 2).Value = report.Model;
                worksheet.Cell(row, 3).Value = report.Year;

                int colIndex = 4;
                foreach (var monthValue in report.MonthlySales.Values)
                {
                    var monthCell = worksheet.Cell(row, colIndex);
                    monthCell.Value = monthValue;
                    monthCell.Style.NumberFormat.Format = "#,##0.00";
                    colIndex++;
                }

                var totalCell = worksheet.Cell(row, 16);
                totalCell.Value = report.Total;
                totalCell.Style.NumberFormat.Format = "#,##0.00";

                // Выделение ячейки "Итого", если > 25 млн
                if (report.Total > 25_000_000)
                {
                    totalCell.Style.Fill.BackgroundColor = XLColor.LightGreen;
                    totalCell.Style.Font.Bold = true;
                }

                row++;
            }

            // Автоширина и автофильтр
            worksheet.Columns().AdjustToContents();
            worksheet.RangeUsed().SetAutoFilter();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }


    }
}
