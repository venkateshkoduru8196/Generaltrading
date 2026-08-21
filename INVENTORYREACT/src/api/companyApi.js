import axiosClient from "./axiosClient";

/*
===========================================================
COMPANY API
===========================================================

Responsible only for HTTP communication.

Backend:
GET    /api/Company
GET    /api/Company/{id}
GET    /api/Company/lookup
POST   /api/Company
PUT    /api/Company
DELETE /api/Company/{id}
===========================================================
*/

/*
===========================================================
GET ALL COMPANIES
===========================================================
*/

export const getCompaniesApi = () => {
  return axiosClient.get("/Company");
};


/*
===========================================================
GET COMPANY BY ID
===========================================================
*/

export const getCompanyByIdApi = (companyId) => {
  return axiosClient.get(
    `/Company/${companyId}`
  );
};


/*
===========================================================
COMPANY LOOKUP
===========================================================
*/

export const getCompanyLookupApi = () => {
  return axiosClient.get(
    "/Company/lookup"
  );
};


/*
===========================================================
CREATE COMPANY
===========================================================
*/

export const createCompanyApi = (data) => {
  return axiosClient.post(
    "/Company",
    data
  );
};


/*
===========================================================
UPDATE COMPANY
===========================================================
*/

export const updateCompanyApi = (data) => {
  return axiosClient.put(
    "/Company",
    data
  );
};


/*
===========================================================
DELETE COMPANY
===========================================================
*/

export const deleteCompanyApi = (companyId) => {
  return axiosClient.delete(
    `/Company/${companyId}`
  );
};