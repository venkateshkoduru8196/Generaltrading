import React, { useEffect, useState } from "react";
import axios from "axios";
import "./ItemMaster.css";

function ItemMaster() {
  const API_URL = "https://localhost:7124/api/Item";

  const emptyForm = {
    code: "",
    name: "",
    regionalName: "",
    category: "",
    manufacturer: "",
    hsnCode: "",
    cgstPer: "",
    sgstPer: "",
    igstPer: "",
    pRate: "",
    sRate: "",
    mrp: "",
    mainUnit: "",
    remarks: "",
  };

  const [items, setItems] = useState([]);
  const [formData, setFormData] = useState(emptyForm);
  const [editId, setEditId] = useState(null);

  const categories = ["Grocery", "Electronics", "Stationery"];
  const manufacturers = ["Nestle", "ITC", "Dabur"];
  const units = ["KG", "LTR", "PCS"];

  useEffect(() => {
    loadItems();
  }, []);

  const loadItems = async () => {
    try {
      const response = await axios.get(API_URL);
      setItems(response.data);
    } catch (error) {
      console.log(error);
    }
  };

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  };

  const clearForm = () => {
    setFormData(emptyForm);
    setEditId(null);
  };

  const getPayload = () => ({
    code: formData.code,
    name: formData.name,
    regionalName: formData.regionalName,
    hsnCode: formData.hsnCode,
    cgstPer: Number(formData.cgstPer || 0),
    sgstPer: Number(formData.sgstPer || 0),
    igstPer: Number(formData.igstPer || 0),
    pRate: Number(formData.pRate || 0),
    sRate: Number(formData.sRate || 0),
    mrp: Number(formData.mrp || 0),
    remarks: formData.remarks,
  });

  const handleSave = async (e) => {
    e.preventDefault();

    try {
      await axios.post(API_URL, getPayload());

      await loadItems();
      clearForm();

      alert("Item Saved Successfully");
    } catch (error) {
      console.log(error);
      alert("Save Failed");
    }
  };

  const handleEdit = (item) => {
    setEditId(item.id);

    setFormData({
      code: item.code || "",
      name: item.name || "",
      regionalName: item.regionalName || "",
      category: "",
      manufacturer: "",
      hsnCode: item.hsnCode || "",
      cgstPer: item.cgstPer || "",
      sgstPer: item.sgstPer || "",
      igstPer: item.igstPer || "",
      pRate: item.pRate || "",
      sRate: item.sRate || "",
      mrp: item.mrp || "",
      mainUnit: "",
      remarks: item.remarks || "",
    });

    window.scrollTo({
      top: 0,
      behavior: "smooth",
    });
  };

  const handleUpdate = async () => {
    if (!editId) {
      alert("Select record to update");
      return;
    }

    try {
      await axios.put(
        `${API_URL}/${editId}`,
        getPayload()
      );

      await loadItems();
      clearForm();

      alert("Updated Successfully");
    } catch (error) {
      console.log(error);
      alert("Update Failed");
    }
  };

  const handleDelete = async () => {
    if (!editId) {
      alert("Select record first");
      return;
    }

    if (!window.confirm("Delete this item ?"))
      return;

    try {
      await axios.delete(
        `${API_URL}/${editId}`
      );

      await loadItems();
      clearForm();

      alert("Deleted Successfully");
    } catch (error) {
      console.log(error);
      alert("Delete Failed");
    }
  };

  return (
    <div className="item-container">
      <div className="item-card">

        <h2>Item Master</h2>

        {editId && (
          <div className="edit-banner">
            Editing Item ID : {editId}
          </div>
        )}

        <form onSubmit={handleSave}>
          <div className="form-grid">

            <label>Code</label>
            <input
              type="text"
              name="code"
              value={formData.code}
              onChange={handleChange}
            />

            <label>Name</label>
            <input
              type="text"
              name="name"
              value={formData.name}
              onChange={handleChange}
            />

            <label>Regional Name</label>
            <input
              type="text"
              name="regionalName"
              value={formData.regionalName}
              onChange={handleChange}
            />

            <label>Category</label>
            <select
              name="category"
              value={formData.category}
              onChange={handleChange}
            >
              <option value="">Select</option>
              {categories.map((c) => (
                <option key={c}>{c}</option>
              ))}
            </select>

            <label>Manufacturer</label>
            <select
              name="manufacturer"
              value={formData.manufacturer}
              onChange={handleChange}
            >
              <option value="">Select</option>
              {manufacturers.map((m) => (
                <option key={m}>{m}</option>
              ))}
            </select>

            <label>HSN Code</label>
            <input
              type="text"
              name="hsnCode"
              value={formData.hsnCode}
              onChange={handleChange}
            />

            <label>CGST %</label>
            <input
              type="number"
              name="cgstPer"
              value={formData.cgstPer}
              onChange={handleChange}
            />

            <label>SGST %</label>
            <input
              type="number"
              name="sgstPer"
              value={formData.sgstPer}
              onChange={handleChange}
            />

            <label>IGST %</label>
            <input
              type="number"
              name="igstPer"
              value={formData.igstPer}
              onChange={handleChange}
            />

            <label>Purchase Rate</label>
            <input
              type="number"
              name="pRate"
              value={formData.pRate}
              onChange={handleChange}
            />

            <label>Sales Rate</label>
            <input
              type="number"
              name="sRate"
              value={formData.sRate}
              onChange={handleChange}
            />

            <label>MRP</label>
            <input
              type="number"
              name="mrp"
              value={formData.mrp}
              onChange={handleChange}
            />

            <label>Main Unit</label>
            <select
              name="mainUnit"
              value={formData.mainUnit}
              onChange={handleChange}
            >
              <option value="">Select</option>
              {units.map((u) => (
                <option key={u}>{u}</option>
              ))}
            </select>

            <label>Remarks</label>
            <textarea
              rows="3"
              name="remarks"
              value={formData.remarks}
              onChange={handleChange}
            />
          </div>

          <div className="button-group">

            {!editId ? (
              <button
                className="save-btn"
                type="submit"
              >
                Save
              </button>
            ) : (
              <>
                <button
                  className="update-btn"
                  type="button"
                  onClick={handleUpdate}
                >
                  Update
                </button>

                <button
                  className="delete-btn"
                  type="button"
                  onClick={handleDelete}
                >
                  Delete
                </button>

                <button
                  className="cancel-btn"
                  type="button"
                  onClick={clearForm}
                >
                  Cancel
                </button>
              </>
            )}

          </div>
        </form>

        <table className="item-table">
          <thead>
            <tr>
              <th>ID</th>
              <th>Code</th>
              <th>Name</th>
              <th>HSN</th>
              <th>Purchase</th>
              <th>Sales</th>
              <th>MRP</th>
              <th>Action</th>
            </tr>
          </thead>

          <tbody>
            {items.map((item) => (
              <tr key={item.id}>
                <td>{item.id}</td>
                <td>{item.code}</td>
                <td>{item.name}</td>
                <td>{item.hsnCode}</td>
                <td>{item.pRate}</td>
                <td>{item.sRate}</td>
                <td>{item.mrp}</td>
                <td>
                  <button
                    type="button"
                    onClick={() => handleEdit(item)}
                  >
                    Edit
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>

      </div>
    </div>
  );
}

export default ItemMaster;