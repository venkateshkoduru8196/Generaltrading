import {
    getUnits,
    getUnitById,
    getUnitLookup,
    createUnit,
    updateUnit,
    deleteUnit
} from "../api/unitApi";

const unitService = {

    async getAll() {
        const response = await getUnits();
        return response.data;
    },

    async getById(id) {
        const response = await getUnitById(id);
        return response.data;
    },

    async getLookup() {
        const response = await getUnitLookup();
        return response.data;
    },

    async create(data) {
        const response = await createUnit(data);
        return response.data;
    },

    async update(data) {
        const response = await updateUnit(data);
        return response.data;
    },

    async delete(id) {
        const response = await deleteUnit(id);
        return response.data;
    }

};

export default unitService;