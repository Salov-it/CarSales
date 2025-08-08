using CarSales.Application.CQRS.Queries.GetMonthlySales;
using CarSales.Application.CQRS.Queries.GetMonthlySalesExcel;
using CarSales.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarSales.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("monthly")]
        public async Task<IActionResult> GetMonthlySales([FromQuery] int year, [FromQuery] string model = null)
        {
            var result = await _mediator.Send(new GetMonthlySalesQuery(year, model));
            return Ok(result);
        }


        [HttpGet("monthly/excel")]
        public async Task<IActionResult> GetMonthlyExcel([FromQuery] int year, [FromQuery] string? model)
        {
            var fileContent = await _mediator.Send(new GetMonthlySalesExcelQuery(year, model));

            return File(fileContent,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"Отчет по продажам_{year}_{(model ?? "All")}.xlsx");
        }

    }
}
