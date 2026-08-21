import axiosClient from "./axiosClient";

const ACCOUNT_BASE_URL = "/Account";

// ==============================
// Get All Accounts
// ==============================
export const getAccounts = () => {
    return axiosClient.get(ACCOUNT_BASE_URL);
};

// ==============================
// Get Account By Id
// ==============================
export const getAccountById = (id) => {
    return axiosClient.get(`${ACCOUNT_BASE_URL}/${id}`);
};

// ==============================
// Lookup Accounts
// ==============================
export const getAccountLookup = () => {
    return axiosClient.get(`${ACCOUNT_BASE_URL}/lookup`);
};

// ==============================
// Create Account
// ==============================
export const createAccount = (data) => {
    return axiosClient.post(ACCOUNT_BASE_URL, data);
};

// ==============================
// Update Account
// ==============================
export const updateAccount = (id, data) => {
    return axiosClient.put(`${ACCOUNT_BASE_URL}/${id}`, data);
};

// ==============================
// Delete Account
// ==============================
export const deleteAccount = (id) => {
    return axiosClient.delete(`${ACCOUNT_BASE_URL}/${id}`);
};