import axiosClient from "../axiosClient";

/*==========================================================
Business Report API Endpoints
==========================================================*/

const BUSINESS_REPORT_URL = "/BusinessReport";

const BUSINESS_REPORT_PDF_URL = "/BusinessReportExport";

/*==========================================================
View Report
POST : /api/BusinessReport
==========================================================*/

export const getBusinessReportApi = async (request) => {

    const response = await axiosClient.post(
        BUSINESS_REPORT_URL,
        request
    );

    return response.data;
};

/*==========================================================
View PDF
POST : /api/BusinessReportPdf/view
==========================================================*/

export const viewBusinessReportPdfApi = async (request) => {

    const response = await axiosClient.post(
        `${BUSINESS_REPORT_PDF_URL}/view`,
        request,
        {
            responseType: "blob"
        }
    );

    return response.data;
};

/*==========================================================
Download PDF
POST : /api/BusinessReportPdf/download
==========================================================*/

export const downloadBusinessReportPdfApi = async (request) => {

    const response = await axiosClient.post(
        `${BUSINESS_REPORT_PDF_URL}/download`,
        request,
        {
            responseType: "blob"
        }
    );

    return response.data;
};


/*==========================================================
Download Excel
POST : /api/BusinessReportExport/excel
==========================================================*/

export const downloadBusinessReportExcelApi = async (request) => {

    const response = await axiosClient.post(

        `${BUSINESS_REPORT_PDF_URL}/excel`,

        request,

        {

            responseType: "blob"

        }

    );

    return response.data;

};


/*==========================================================
Download Word
POST : /api/BusinessReportExport/word
==========================================================*/

export const downloadBusinessReportWordApi = async (request) => {

    const response = await axiosClient.post(

        `${BUSINESS_REPORT_PDF_URL}/word`,

        request,

        {

            responseType: "blob"

        }

    );

    return response.data;

};