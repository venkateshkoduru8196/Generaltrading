import axiosClient from "./axiosClient";


//====================================================
// GET ADMINS
//====================================================

export const getAdminsApi = (params) => {

    return axiosClient.get(
        "/User/admins",
        {
            params
        }
    );
};


//====================================================
// GET ADMIN BY ID
//====================================================

export const getAdminByIdApi = (userId) => {

    return axiosClient.get(
        `/User/admins/${userId}`
    );
};


//====================================================
// CREATE ADMIN
//====================================================

export const createAdminApi = (data) => {

    return axiosClient.post(
        "/Auth/create-admin",
        data
    );
};


//====================================================
// UPDATE ADMIN
//====================================================

export const updateAdminApi = (userId, data) => {

    return axiosClient.put(
        `/User/admins/${userId}`,
        data
    );
};


//====================================================
// UPDATE ADMIN STATUS
//====================================================

export const updateAdminStatusApi = (
    userId,
    isActive
) => {

    return axiosClient.patch(
        `/User/admins/${userId}/status`,
        {
            isActive
        }
    );
};


//====================================================
// DELETE ADMIN
//====================================================

export const deleteAdminApi = (userId) => {

    return axiosClient.delete(
        `/User/admins/${userId}`
    );
};