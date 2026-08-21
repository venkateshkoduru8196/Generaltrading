import {
    getAdminsApi,
    getAdminByIdApi,
    createAdminApi,
    updateAdminApi,
    updateAdminStatusApi,
    deleteAdminApi
} from "../api/adminApi";


//====================================================
// GET ADMINS
//====================================================

export const getAdmins = async (params) => {

    const response =
        await getAdminsApi(params);

    return response.data;
};


//====================================================
// GET ADMIN BY ID
//====================================================

export const getAdminById = async (userId) => {

    const response =
        await getAdminByIdApi(userId);

    return response.data;
};


//====================================================
// CREATE ADMIN
//====================================================

export const createAdmin = async (data) => {

    const response =
        await createAdminApi(data);

    return response.data;
};


//====================================================
// UPDATE ADMIN
//====================================================

export const updateAdmin = async (
    userId,
    data
) => {

    const response =
        await updateAdminApi(
            userId,
            data
        );

    return response.data;
};


//====================================================
// UPDATE ADMIN STATUS
//====================================================

export const updateAdminStatus = async (
    userId,
    isActive
) => {

    const response =
        await updateAdminStatusApi(
            userId,
            isActive
        );

    return response.data;
};


//====================================================
// DELETE ADMIN
//====================================================

export const deleteAdmin = async (userId) => {

    const response =
        await deleteAdminApi(userId);

    return response.data;
};