import axiosClient from "./axiosClient";

export const getPartiesApi = () =>
  axiosClient.get("/Payment/parties");

export const getAccountsApi = () =>
  axiosClient.get("/Payment/accounts");

export const savePaymentApi = (data) =>
  axiosClient.post("/Payment", data);

export const getNextPaymentNumberApi = () =>
  axiosClient.get("/Payment/next-number");

export const searchPaymentsApi = (keyword) =>
  axiosClient.get("/Payment/search", {
    params: { keyword }
  });

export const getPaymentByDocNoApi = (docNo) =>
  axiosClient.get(`/Payment/${docNo}`);

export const deletePaymentApi = (docNo) => {
  return axiosClient.delete(`/Payment/${docNo}`);
};