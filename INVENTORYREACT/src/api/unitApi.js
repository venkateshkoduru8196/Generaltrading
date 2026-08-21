import axiosClient from "./axiosClient";

const UNIT_BASE_URL = "/Unit";

// ==============================
// Get All Units
// ==============================
export const getUnits = () => {
    return axiosClient.get(UNIT_BASE_URL);
};

// ==============================
// Get Unit By Id
// ==============================
export const getUnitById = (id) => {
    return axiosClient.get(`${UNIT_BASE_URL}/${id}`);
};

// ==============================
// Lookup Units
// ==============================
export const getUnitLookup = () => {
    return axiosClient.get(`${UNIT_BASE_URL}/lookup`);
};

// ==============================
// Create Unit
// ==============================
export const createUnit = (data) => {
    return axiosClient.post(UNIT_BASE_URL, data);
};

// ==============================
// Update Unit
// ==============================
export const updateUnit = (data) => {
    return axiosClient.put(UNIT_BASE_URL, data);
};

// ==============================
// Delete Unit
// ==============================
export const deleteUnit = (id) => {
    return axiosClient.delete(`${UNIT_BASE_URL}/${id}`);
};