import { useState } from "react";

import "./BusinessReport.css";

import ReportFilter from "./components/ReportFilter";
import ReportToolbar from "./components/ReportToolbar";
import ReportTable from "./components/ReportTable";





import {

    getBusinessReport,
    viewBusinessReportPdf,
    downloadBusinessReportPdf,
    downloadBusinessReportExcel,
    downloadBusinessReportWord

} from "../../../services/reports/businessReportService";

export default function BusinessReport() {

    //---------------------------------------
    // Report Type
    //---------------------------------------

    const [reportType, setReportType] = useState("Monthly");

    //---------------------------------------
    // Daily
    //---------------------------------------

    const [reportDate, setReportDate] = useState("");

    //---------------------------------------
    // Monthly
    //---------------------------------------

    const [month, setMonth] = useState(
        new Date().getMonth() + 1
    );

    const [year, setYear] = useState(
        new Date().getFullYear()
    );

    //---------------------------------------
    // Periodical
    //---------------------------------------

    const [fromDate, setFromDate] = useState("");

    const [toDate, setToDate] = useState("");

    //---------------------------------------
    // Report Data
    //---------------------------------------

    const [report, setReport] = useState(null);

    //---------------------------------------
    // Loading
    //---------------------------------------

  
//---------------------------------------
// Loading Action
//---------------------------------------

const [loadingAction, setLoadingAction] = useState("");
    //---------------------------------------
    // Build Request
    //---------------------------------------

    const buildRequest = () => {

        return {

            reportType,

            reportDate: reportDate || null,

            month: reportType === "Monthly"
                ? month
                : null,

            year: reportType === "Monthly"
                ? year
                : null,

            fromDate: fromDate || null,

            toDate: toDate || null

        };

    };

    //---------------------------------------
    // View Report
    //---------------------------------------

    //---------------------------------------
// Preview Report
//---------------------------------------

const handleView = async () => {

    try {

        setLoadingAction("preview");

        const data = await getBusinessReport(

            buildRequest()

        );

        setReport(data);

    }

    catch (error) {

        console.error(error);

        alert("Unable to load report.");

    }

    finally {

        setLoadingAction("");

    }

};

    //---------------------------------------
    // PDF
    //---------------------------------------
          




    //---------------------------------------
// PDF Preview
//---------------------------------------

const handlePdf = async () => {

    try {

        setLoadingAction("pdfPreview");

        const pdf = await viewBusinessReportPdf(

            buildRequest()

        );

        //-------------------------------------
        // Blob
        //-------------------------------------

        const blob = new Blob(

            [pdf],

            {

                type: "application/pdf"

            }

        );

        //-------------------------------------
        // Open PDF
        //-------------------------------------

        const url = URL.createObjectURL(blob);

        window.open(

            url,

            "_blank"

        );

        //-------------------------------------
        // Release Memory
        //-------------------------------------

        setTimeout(() => {

            URL.revokeObjectURL(url);

        }, 1000);

    }

    catch (error) {

        console.error(error);

        alert("Unable to preview PDF.");

    }

    finally {

        setLoadingAction("");

    }

};
    
  
        


    //---------------------------------------
// Download PDF
//---------------------------------------

const handleDownloadPdf = async () => {

    try {

        setLoadingAction("pdfDownload");

        const pdf = await downloadBusinessReportPdf(

            buildRequest()

        );

        //-------------------------------------
        // Blob
        //-------------------------------------

        const blob = new Blob(

            [pdf],

            {

                type: "application/pdf"

            }

        );

        //-------------------------------------
        // Download
        //-------------------------------------

        const url = URL.createObjectURL(blob);

        const link = document.createElement("a");

        link.href = url;

        link.download =

            "BusinessReport.pdf";

        document.body.appendChild(link);

        link.click();

        link.remove();

        URL.revokeObjectURL(url);

    }

    catch (error) {

        console.error(error);

        alert("Unable to download PDF.");

    }

    finally {

        setLoadingAction("");

    }

};
    




         //---------------------------------------
    // Excel
    //---------------------------------------



    //---------------------------------------
// Download Excel
//---------------------------------------

const handleExcel = async () => {

    try {

        setLoadingAction("excel");

        const excel =
            await downloadBusinessReportExcel(
                buildRequest()
            );

        //-------------------------------------
        // Blob
        //-------------------------------------

        const blob = new Blob(

            [excel],

            {

                type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"

            }

        );

        //-------------------------------------
        // Download
        //-------------------------------------

        const url = URL.createObjectURL(blob);

        const link = document.createElement("a");

        link.href = url;

        link.download =

            "BusinessReport.xlsx";

        document.body.appendChild(link);

        link.click();

        link.remove();

        URL.revokeObjectURL(url);

    }

    catch (error) {

        console.error(error);

        alert("Unable to download Excel.");

    }

    finally {

        setLoadingAction("");

    }

};


     




    //---------------------------------------
    // Word
    //---------------------------------------

         //---------------------------------------
// Download Word
//---------------------------------------

const handleWord = async () => {

    try {

        setLoadingAction("word");

        const word =
            await downloadBusinessReportWord(
                buildRequest()
            );

        //-------------------------------------
        // Blob
        //-------------------------------------

        const blob = new Blob(

            [word],

            {

                type: "application/vnd.openxmlformats-officedocument.wordprocessingml.document"

            }

        );

        //-------------------------------------
        // Download
        //-------------------------------------

        const url = URL.createObjectURL(blob);

        const link = document.createElement("a");

        link.href = url;

        link.download =

            "BusinessReport.docx";

        document.body.appendChild(link);

        link.click();

        link.remove();

        URL.revokeObjectURL(url);

    }

    catch (error) {

        console.error(error);

        alert("Unable to download Word.");

    }

    finally {

        setLoadingAction("");

    }

};




    return (

        <div className="business-report-page">

            {/*================================================*/}
            {/* PAGE HEADER                                    */}
            {/*================================================*/}

            <div className="page-header">

                <div>

                    <h1>

                        Business Report

                    </h1>

                    <p>

                        Stock Movement Summary Report

                    </p>

                </div>

            </div>

            {/*================================================*/}
            {/* FILTER CARD                                    */}
            {/*================================================*/}

            <div className="card">

                <div className="card-title">

                    Report Filters

                </div>

                <ReportFilter

                    reportType={reportType}
                    setReportType={setReportType}

                    reportDate={reportDate}
                    setReportDate={setReportDate}

                    month={month}
                    setMonth={setMonth}

                    year={year}
                    setYear={setYear}

                    fromDate={fromDate}
                    setFromDate={setFromDate}

                    toDate={toDate}
                    setToDate={setToDate}

                />

            </div>

            {/*================================================*/}
            {/* ACTION CARD                                    */}
            {/*================================================*/}

            <div className="card">

                <div className="card-title">

                    Report Actions

                </div>

              <ReportToolbar

    loadingAction={loadingAction}

    onView={handleView}

    onPdf={handlePdf}

    onDownloadPdf={handleDownloadPdf}

    onExcel={handleExcel}

    onWord={handleWord}

/>
            </div>

            {/*================================================*/}
            {/* REPORT                                         */}
            {/*================================================*/}

            {

                report &&

                <div className="card">

                    <ReportTable

                        report={report}

                    />

                </div>

            }

        </div>

    );

}