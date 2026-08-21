import axiosClient from "./axiosClient";

export const getPartiesApi = () =>
    axiosClient.get("/Receipt/parties");

export const getAccountsApi = () =>
    axiosClient.get("/Receipt/accounts");

export const saveReceiptApi = (data) =>
    axiosClient.post("/Receipt", data);

export const getNextReceiptNumberApi = () =>
    axiosClient.get("/Receipt/next-number");

export const searchReceiptsApi = (keyword) =>
    axiosClient.get("/Receipt/search", {
        params: { keyword }
    });

export const getReceiptByDocNoApi = (docNo) =>
    axiosClient.get(`/Receipt/${docNo}`);

export const deleteReceiptApi = (docNo) => {
    return axiosClient.delete(`/Receipt/${docNo}`);
};