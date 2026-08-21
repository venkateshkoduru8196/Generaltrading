import axiosClient from "./axiosClient";

const SALES_BASE_URL = "/GSal";

//=====================================================
// GET ALL SALES
//=====================================================

export const getSales = () => {
    return axiosClient.get(SALES_BASE_URL);
};

//=====================================================
// GET SALE BY ID
//=====================================================

export const getSaleById = (id) => {
    return axiosClient.get(
        `${SALES_BASE_URL}/${id}`
    );
};

//=====================================================
// GET SALE BY DOCUMENT NUMBER
//=====================================================

export const getSaleByDocNo = (docNo) => {
    return axiosClient.get(
        `${SALES_BASE_URL}/doc/${docNo}`
    );
};

//=====================================================
// CREATE SALE
//=====================================================

export const createSale = (data) => {
    return axiosClient.post(
        SALES_BASE_URL,
        data
    );
};

//=====================================================
// UPDATE SALE
//=====================================================

export const updateSale = (id, data) => {
    return axiosClient.put(
        `${SALES_BASE_URL}/${id}`,
        data
    );
};

//=====================================================
// DELETE SALE
//=====================================================

export const deleteSale = (id) => {
    return axiosClient.delete(
        `${SALES_BASE_URL}/${id}`
    );
};


//=====================================================
// DOWNLOAD PDF
//=====================================================

export const downloadSalePdf = (id) => {
    return axiosClient.get(
        `${SALES_BASE_URL}/${id}/download/pdf`,
        {
            responseType: "blob",
        }
    );
};


//=====================================================
// DOWNLOAD WORD
//=====================================================

export const downloadSaleWord = (id) => {
    return axiosClient.get(
        `${SALES_BASE_URL}/${id}/download/word`,
        {
            responseType: "blob",
        }
    );
};


//=====================================================
// DOWNLOAD EXCEL
//=====================================================

export const downloadSaleExcel = (id) => {
    return axiosClient.get(
        `${SALES_BASE_URL}/${id}/download/excel`,
        {
            responseType: "blob",
        }
    );
};