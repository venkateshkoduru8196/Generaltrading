// import {

//     getBusinessReportApi,
//     viewBusinessReportPdfApi,
//     downloadBusinessReportPdfApi

// } from "../../api/reports/businessReportApi";



import {

    getBusinessReportApi,
    viewBusinessReportPdfApi,
    downloadBusinessReportPdfApi,
    downloadBusinessReportExcelApi,
    downloadBusinessReportWordApi

} from "../../api/reports/businessReportApi";

/*==========================================================
Business Report Service
==========================================================*/

/*
----------------------------------------------------------
View Report
----------------------------------------------------------
*/

export const getBusinessReport = async (request) => {

    try {

        const data = await getBusinessReportApi(request);

        return data;

    }
    catch (error) {

        console.error("Business Report Error:", error);

        throw error;

    }

};


/*
----------------------------------------------------------
View PDF
----------------------------------------------------------
*/

export const viewBusinessReportPdf = async (request) => {

    try {

        const pdf = await viewBusinessReportPdfApi(request);

        return pdf;

    }
    catch (error) {

        console.error("View PDF Error:", error);

        throw error;

    }

};


/*
----------------------------------------------------------
Download PDF
----------------------------------------------------------
*/

export const downloadBusinessReportPdf = async (request) => {

    try {

        const pdf = await downloadBusinessReportPdfApi(request);

        return pdf;

    }
    catch (error) {

        console.error("Download PDF Error:", error);

        throw error;

    }

};


/*
----------------------------------------------------------
Download Excel
----------------------------------------------------------
*/

export const downloadBusinessReportExcel = async (request) => {

    try {

        const excel =
            await downloadBusinessReportExcelApi(request);

        return excel;

    }
    catch (error) {

        console.error("Download Excel Error:", error);

        throw error;

    }

};



/*
----------------------------------------------------------
Download Word
----------------------------------------------------------
*/

export const downloadBusinessReportWord = async (request) => {

    try {

        const word =
            await downloadBusinessReportWordApi(request);

        return word;

    }
    catch (error) {

        console.error("Download Word Error:", error);

        throw error;

    }

};