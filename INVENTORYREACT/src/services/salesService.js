import {
    getSales,
    getSaleById,
    getSaleByDocNo,
    createSale,
    updateSale,
    deleteSale,
    downloadSalePdf,
    downloadSaleWord,
    downloadSaleExcel
} from "../api/salesApi";

const salesService = {

    //=====================================================
    // GET ALL
    //=====================================================

    async getAll() {
        const response =
            await getSales();

        return response.data;
    },


    //=====================================================
    // GET BY ID
    //=====================================================

    async getById(id) {
        const response =
            await getSaleById(id);

        return response.data;
    },


    //=====================================================
    // GET BY DOCUMENT NUMBER
    //=====================================================

    async getByDocNo(docNo) {
        const response =
            await getSaleByDocNo(docNo);

        return response.data;
    },


    //=====================================================
    // CREATE
    //=====================================================

    async create(data) {
        const response =
            await createSale(data);

        return response.data;
    },


    //=====================================================
    // UPDATE
    //=====================================================

    async update(id, data) {
        await updateSale(id, data);
    },


    //=====================================================
    // DELETE
    //=====================================================

    async delete(id) {
        await deleteSale(id);
    },


    //=====================================================
    // DOWNLOAD PDF
    //=====================================================

    async downloadPdf(id) {
        const response =
            await downloadSalePdf(id);

        return response;
    },


    //=====================================================
    // DOWNLOAD WORD
    //=====================================================

    async downloadWord(id) {
        const response =
            await downloadSaleWord(id);

        return response;
    },


    //=====================================================
    // DOWNLOAD EXCEL
    //=====================================================

    async downloadExcel(id) {
        const response =
            await downloadSaleExcel(id);

        return response;
    }

};

export default salesService;