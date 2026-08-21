import {
  getPartiesApi,
  getAccountsApi,
  savePaymentApi,
  getNextPaymentNumberApi,
  searchPaymentsApi,
  getPaymentByDocNoApi,
  deletePaymentApi,
} from "../api/paymentApi";

export const getParties = async () => {
  const response = await getPartiesApi();
  return response.data;
};

export const getAccounts = async () => {
  const response = await getAccountsApi();
  return response.data;
};

export const savePayment = async (data) => {
  const response = await savePaymentApi(data);
  return response.data;
};

export const getNextPaymentNumber = async () => {
  const response = await getNextPaymentNumberApi();
  return response.data;
};

export const searchPayments = async (keyword) => {
  const response = await searchPaymentsApi(keyword);
  return response.data;
};

export const getPaymentByDocNo = async (docNo) => {
  const response = await getPaymentByDocNoApi(docNo);
  return response.data;
};

export const deletePayment = async (docNo) => {
  const response = await deletePaymentApi(docNo);
  return response.data;
};