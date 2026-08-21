using INVENTORYAPP.Data;
//using INVENTORYAPP.DTOs.Reports;
using INVENTORYAPP.Features.Reports.BusinessReport.DTOs;
using INVENTORYAPP.Features.Reports.BusinessReport.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace INVENTORYAPP.Features.Reports.BusinessReport.Repositories;

public class BusinessReportRepository : IBusinessReportRepository
{
    private readonly AppDbContext _context;

    public BusinessReportRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<BusinessReportResponseDto> GetBusinessReportAsync(
        BusinessReportRequestDto request)
    {
        DateTime fromDate;
        DateTime toDate;

        //-------------------------------------------------------
        // REPORT TYPE
        //-------------------------------------------------------

        if (request.ReportType.Equals("Daily", StringComparison.OrdinalIgnoreCase))
        {
            if (!request.ReportDate.HasValue)
                throw new Exception("Report Date is required.");

            fromDate = request.ReportDate.Value.Date;
            toDate = request.ReportDate.Value.Date;
        }
        else if (request.ReportType.Equals("Monthly", StringComparison.OrdinalIgnoreCase))
        {
            if (!request.Month.HasValue || !request.Year.HasValue)
                throw new Exception("Month and Year are required.");

            fromDate = new DateTime(request.Year.Value, request.Month.Value, 1);
            toDate = fromDate.AddMonths(1).AddDays(-1);
        }
        else if (request.ReportType.Equals("Periodical", StringComparison.OrdinalIgnoreCase))
        {
            if (!request.FromDate.HasValue || !request.ToDate.HasValue)
                throw new Exception("From Date and To Date are required.");

            fromDate = request.FromDate.Value.Date;
            toDate = request.ToDate.Value.Date;
        }
        else
        {
            throw new Exception("Invalid Report Type.");
        }

        //-------------------------------------------------------
        // GOLD
        //-------------------------------------------------------

        var goldRows = await
        (
            from m in _context.MetalMasters
            join s in _context.StockTransactionDetails
                on m.StkCode equals s.StkCode
            where m.MtlType == "G"
            group new { m, s } by m.KaratCat into g
            orderby g.Key
            select new StockMovementRowDto
            {
                Metal = "Gold",

                AccountName = g.Key ?? "",

                Opening = g.Sum(x =>
                    x.s.DocDate < fromDate
                        ? (x.s.GrQty ?? 0) * (x.s.NFactor ?? 0)
                        : 0),

                MoveIn = g.Sum(x =>
                    x.s.DocDate >= fromDate &&
                    x.s.DocDate <= toDate &&
                    x.s.NFactor == 1
                        ? x.s.GrQty ?? 0
                        : 0),

                MoveOut = g.Sum(x =>
                    x.s.DocDate >= fromDate &&
                    x.s.DocDate <= toDate &&
                    x.s.NFactor == -1
                        ? x.s.GrQty ?? 0
                        : 0),

                Closing = g.Sum(x =>
                    x.s.DocDate <= toDate
                        ? (x.s.GrQty ?? 0) * (x.s.NFactor ?? 0)
                        : 0)
            }
        ).ToListAsync();

        goldRows.Add(new StockMovementRowDto
        {
            Metal = "Gold",
            AccountName = "Total",
            Opening = goldRows.Sum(x => x.Opening),
            MoveIn = goldRows.Sum(x => x.MoveIn),
            MoveOut = goldRows.Sum(x => x.MoveOut),
            Closing = goldRows.Sum(x => x.Closing)
        });

        //-------------------------------------------------------
        // SILVER
        //-------------------------------------------------------

        var silverRows = await
        (
            from m in _context.MetalMasters
            join s in _context.StockTransactionDetails
                on m.StkCode equals s.StkCode
            where m.MtlType == "S"
            group new { m, s } by m.KaratCat into g
            orderby g.Key
            select new StockMovementRowDto
            {
                Metal = "Silver",

                AccountName = g.Key ?? "",

                Opening = g.Sum(x =>
                    x.s.DocDate < fromDate
                        ? (x.s.GrQty ?? 0) * (x.s.NFactor ?? 0)
                        : 0),

                MoveIn = g.Sum(x =>
                    x.s.DocDate >= fromDate &&
                    x.s.DocDate <= toDate &&
                    x.s.NFactor == 1
                        ? x.s.GrQty ?? 0
                        : 0),

                MoveOut = g.Sum(x =>
                    x.s.DocDate >= fromDate &&
                    x.s.DocDate <= toDate &&
                    x.s.NFactor == -1
                        ? x.s.GrQty ?? 0
                        : 0),

                Closing = g.Sum(x =>
                    x.s.DocDate <= toDate
                        ? (x.s.GrQty ?? 0) * (x.s.NFactor ?? 0)
                        : 0)
            }
        ).ToListAsync();

        silverRows.Add(new StockMovementRowDto
        {
            Metal = "Silver",
            AccountName = "Total",
            Opening = silverRows.Sum(x => x.Opening),
            MoveIn = silverRows.Sum(x => x.MoveIn),
            MoveOut = silverRows.Sum(x => x.MoveOut),
            Closing = silverRows.Sum(x => x.Closing)
        });

        //-------------------------------------------------------
        // COMBINE
        //-------------------------------------------------------

        goldRows.AddRange(silverRows);

        //-------------------------------------------------------
        // RESPONSE
        //-------------------------------------------------------

        return new BusinessReportResponseDto
        {
            CompanyName = "ABC Company",
            CompanyAddress = "Gold Souq, Deira, Dubai",
            ReportDateTime = DateTime.Now,
            StockMovements = goldRows
        };
    }
}