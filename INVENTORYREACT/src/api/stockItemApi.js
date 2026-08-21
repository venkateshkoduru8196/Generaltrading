import axiosClient from "./axiosClient";

const STOCKITEM_BASE_URL = "/StockItem";

// ==============================
// Get All Stock Items
// ==============================
export const getStockItems = () => {
    return axiosClient.get(STOCKITEM_BASE_URL);
};

// ==============================
// Get Stock Item By Id
// ==============================
export const getStockItemById = (id) => {
    return axiosClient.get(`${STOCKITEM_BASE_URL}/${id}`);
};

// ==============================
// Lookup Stock Items
// ==============================
export const getStockItemLookup = () => {
    return axiosClient.get(`${STOCKITEM_BASE_URL}/lookup`);
};

// ==============================
// Create Stock Item
// ==============================
export const createStockItem = (data) => {
    return axiosClient.post(STOCKITEM_BASE_URL, data);
};

// ==============================
// Update Stock Item
// ==============================
export const updateStockItem = (data) => {
    return axiosClient.put(STOCKITEM_BASE_URL, data);
};


// ==============================
// Delete Stock Item
// ==============================
export const deleteStockItem = (id) => {
    return axiosClient.delete(`${STOCKITEM_BASE_URL}/${id}`);
};
