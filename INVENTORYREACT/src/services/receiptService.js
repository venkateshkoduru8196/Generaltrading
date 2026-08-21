import {
    getPartiesApi,
    getAccountsApi,
    saveReceiptApi,
    getNextReceiptNumberApi,
    searchReceiptsApi,
    getReceiptByDocNoApi,
    deleteReceiptApi
} from "../api/receiptApi";

export const getParties = async () => {
    const response = await getPartiesApi();
    return response.data;
};

export const getAccounts = async () => {
    const response = await getAccountsApi();
    return response.data;
};

export const saveReceipt = async (data) => {
    const response = await saveReceiptApi(data);
    return response.data;
};

export const getNextReceiptNumber = async () => {
    const response = await getNextReceiptNumberApi();
    return response.data;
};

export const searchReceipts = async (keyword) => {
    const response = await searchReceiptsApi(keyword);
    return response.data;
};

export const getReceiptByDocNo = async (docNo) => {
    const response = await getReceiptByDocNoApi(docNo);
    return response.data;
};

export const deleteReceipt = async (docNo) => {
    const response = await deleteReceiptApi(docNo);
    return response.data;
};