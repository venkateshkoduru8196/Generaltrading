using OfficeOpenXml;

namespace INVENTORYAPP.Shared.Excel;

public static class ExcelDocumentBuilder
{
    //====================================================
    // CREATE WORKBOOK
    //====================================================

    public static ExcelPackage Create(
        string? worksheetName = null,
        string? documentTitle = null)
    {
        //------------------------------------------------
        // EPPLUS LICENSE
        //------------------------------------------------

        ExcelPackage.License
            .SetNonCommercialPersonal(
                "Inventory ERP");


        //------------------------------------------------
        // CREATE PACKAGE
        //------------------------------------------------

        var package =
            new ExcelPackage();


        //------------------------------------------------
        // WORKBOOK PROPERTIES
        //------------------------------------------------

        package.Workbook.Properties.Author =
            ExcelTheme.CompanyName;

        package.Workbook.Properties.Company =
            ExcelTheme.CompanyName;

        package.Workbook.Properties.Title =
            string.IsNullOrWhiteSpace(documentTitle)
                ? ExcelTheme.CompanyName
                : documentTitle;


        //------------------------------------------------
        // CREATE WORKSHEET
        //------------------------------------------------

        package.Workbook.Worksheets.Add(
            string.IsNullOrWhiteSpace(worksheetName)
                ? ExcelTheme.DefaultSheetName
                : worksheetName);


        //------------------------------------------------
        // RETURN
        //------------------------------------------------

        return package;
    }
}