import {
    getAccounts,
    getAccountById,
    getAccountLookup,
    createAccount,
    updateAccount,
    deleteAccount
} from "../api/accountApi";

const accountService = {

    async getAll() {
        const response = await getAccounts();
        return response.data;
    },

    async getById(id) {
        const response = await getAccountById(id);
        return response.data;
    },

    async getLookup() {
        const response = await getAccountLookup();
        return response.data;
    },

    async create(data) {
        const response = await createAccount(data);
        return response.data;
    },

    async update(id, data) {
        const response = await updateAccount(id, data);
        return response.data;
    },

    async delete(id) {
        const response = await deleteAccount(id);
        return response.data;
    }

};

export default accountService;