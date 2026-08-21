import {
    getStockItems,
    getStockItemById,
    getStockItemLookup,
    createStockItem,
    updateStockItem,
    deleteStockItem
} from "../api/stockItemApi";

const stockItemService = {

    async getAll() {
        const response = await getStockItems();
        return response.data;
    },

    async getById(id) {
        const response = await getStockItemById(id);
        return response.data;
    },

    async getLookup() {
        const response = await getStockItemLookup();
        return response.data;
    },

    async create(data) {
        const response = await createStockItem(data);
        return response.data;
    },

    async update(data) {
        const response = await updateStockItem(data);
        return response.data;
    },

    async delete(id) {
        const response = await deleteStockItem(id);
        return response.data;
    }

};

export default stockItemService;