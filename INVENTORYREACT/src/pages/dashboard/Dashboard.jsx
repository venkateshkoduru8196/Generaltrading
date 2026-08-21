import React, { useState } from "react";
import {
  LineChart,
  Line,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  ResponsiveContainer,
  PieChart,
  Pie,
  Cell,
} from "recharts";
import "./Dashboard.css";

import MainLayout
from "../../layouts/MainLayout";

// ─── Icons (inline SVG helpers) ───────────────────────────────────────────────
const Icon = ({ d, size = 20, color = "currentColor" }) => (
  <svg width={size} height={size} viewBox="0 0 24 24" fill="none" stroke={color} strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
    <path d={d} />
  </svg>
);

const icons = {
  dashboard:   "M3 9l9-7 9 7v11a2 2 0 01-2 2H5a2 2 0 01-2-2z M9 22V12h6v10",
  master:      "M12 2L2 7l10 5 10-5-10-5zM2 17l10 5 10-5M2 12l10 5 10-5",
  purchase:    "M6 2L3 6v14a2 2 0 002 2h14a2 2 0 002-2V6l-3-4z M3 6h18 M16 10a4 4 0 01-8 0",
  sales:       "M22 12h-4l-3 9L9 3l-3 9H2",
  inventory:   "M21 16V8a2 2 0 00-1-1.73l-7-4a2 2 0 00-2 0l-7 4A2 2 0 003 8v8a2 2 0 001 1.73l7 4a2 2 0 002 0l7-4A2 2 0 0021 16z",
  accounts:    "M14 2H6a2 2 0 00-2 2v16a2 2 0 002 2h12a2 2 0 002-2V8z M14 2v6h6 M16 13H8 M16 17H8 M10 9H8",
  gst:         "M9 14l6-6 M3.5 18.5l17-17 M3 12a9 9 0 1018 0 9 9 0 00-18 0",
  reports:     "M18 20V10 M12 20V4 M6 20v-6",
  tools:       "M14.7 6.3a1 1 0 010 1.4l-8 8a1 1 0 01-1.4 0l-2-2a1 1 0 010-1.4l8-8a1 1 0 011.4 0l2 2z M3 20h.01 M4 19l7.5-7.5",
  crm:         "M17 21v-2a4 4 0 00-4-4H5a4 4 0 00-4 4v2 M9 11a4 4 0 100-8 4 4 0 000 8z M23 21v-2a4 4 0 00-3-3.87 M16 3.13a4 4 0 010 7.75",
  users:       "M20 21v-2a4 4 0 00-4-4H8a4 4 0 00-4 4v2 M12 11a4 4 0 100-8 4 4 0 000 8z",
  backup:      "M21 15v4a2 2 0 01-2 2H5a2 2 0 01-2-2v-4 M7 10l5 5 5-5 M12 15V3",
  settings:    "M12 15a3 3 0 100-6 3 3 0 000 6z M19.4 15a1.65 1.65 0 00.33 1.82l.06.06a2 2 0 010 2.83 2 2 0 01-2.83 0l-.06-.06a1.65 1.65 0 00-1.82-.33 1.65 1.65 0 00-1 1.51V21a2 2 0 01-4 0v-.09A1.65 1.65 0 009 19.4a1.65 1.65 0 00-1.82.33l-.06.06a2 2 0 01-2.83 0 2 2 0 010-2.83l.06-.06A1.65 1.65 0 004.68 15a1.65 1.65 0 00-1.51-1H3a2 2 0 010-4h.09A1.65 1.65 0 004.6 9a1.65 1.65 0 00-.33-1.82l-.06-.06a2 2 0 010-2.83 2 2 0 012.83 0l.06.06A1.65 1.65 0 009 4.68a1.65 1.65 0 001-1.51V3a2 2 0 014 0v.09a1.65 1.65 0 001 1.51 1.65 1.65 0 001.82-.33l.06-.06a2 2 0 012.83 0 2 2 0 010 2.83l-.06.06A1.65 1.65 0 0019.4 9a1.65 1.65 0 001.51 1H21a2 2 0 010 4h-.09a1.65 1.65 0 00-1.51 1z",
  help:        "M12 22a10 10 0 100-20 10 10 0 000 20z M9.09 9a3 3 0 015.83 1c0 2-3 3-3 3 M12 17h.01",
  chevron:     "M9 18l6-6-6-6",
  bell:        "M18 8A6 6 0 006 8c0 7-3 9-3 9h18s-3-2-3-9 M13.73 21a2 2 0 01-3.46 0",
  menu:        "M3 12h18 M3 6h18 M3 18h18",
  close:       "M18 6L6 18 M6 6l12 12",
  rupee:       "M18 7H9.5a3.5 3.5 0 000 7H18 M9.5 14H6 M12 7v14",
  arrow:       "M5 12h14 M12 5l7 7-7 7",
  warning:     "M10.29 3.86L1.82 18a2 2 0 001.71 3h16.94a2 2 0 001.71-3L13.71 3.86a2 2 0 00-3.42 0z M12 9v4 M12 17h.01",
  error:       "M12 22a10 10 0 100-20 10 10 0 000 20z M12 8v4 M12 16h.01",
  info:        "M12 22a10 10 0 100-20 10 10 0 000 20z M12 16v-4 M12 8h.01",
  invoice:     "M14 2H6a2 2 0 00-2 2v16a2 2 0 002 2h12a2 2 0 002-2V8z M14 2v6h6",
  cart:        "M6 2L3 6v14a2 2 0 002 2h14a2 2 0 002-2V6l-3-4z",
  box:         "M21 16V8a2 2 0 00-1-1.73l-7-4a2 2 0 00-2 0l-7 4A2 2 0 003 8v8a2 2 0 001 1.73l7 4a2 2 0 002 0l7-4A2 2 0 0021 16z",
  user:        "M20 21v-2a4 4 0 00-4-4H8a4 4 0 00-4 4v2 M12 11a4 4 0 100-8 4 4 0 000 8z",
  supplier:    "M17 21v-2a4 4 0 00-4-4H5a4 4 0 00-4 4v2 M9 11a4 4 0 100-8 4 4 0 000 8z M23 21v-2a4 4 0 00-3-3.87 M16 3.13a4 4 0 010 7.75",
  chart:       "M18 20V10 M12 20V4 M6 20v-6",
  book:        "M2 3h6a4 4 0 014 4v14a3 3 0 00-3-3H2z M22 3h-6a4 4 0 00-4 4v14a3 3 0 013-3h7z",
  database:    "M12 2a9 3 0 110 6 9 3 0 010-6z M3 5v14a9 3 0 0018 0V5",
  power:       "M18.36 6.64A9 9 0 0121 12a9 9 0 11-18 0 9 9 0 012.64-6.36 M12 2v10",
};

// ─── Data Arrays ─────────────────────────────────────────────────────────────
const salesData = [
  { date: "01 May", amount: 85000 },
  { date: "04 May", amount: 120000 },
  { date: "06 May", amount: 52000 },
  { date: "09 May", amount: 145000 },
  { date: "11 May", amount: 98000 },
  { date: "14 May", amount: 160000 },
  { date: "16 May", amount: 130000 },
  { date: "18 May", amount: 220000 },
  { date: "21 May", amount: 145000 },
  { date: "23 May", amount: 138000 },
  { date: "24 May", amount: 195000 },
];

const stockData = [
  { name: "In Stock", value: 1045, color: "#22c55e" },
  { name: "Low Stock", value: 120,  color: "#f59e0b" },
  { name: "Out of Stock", value: 80, color: "#ef4444" },
];

const topItems = [
  { code: "ITM001", name: "Dell Keyboard",       qty: 45, amount: "22,500" },
  { code: "ITM005", name: "HP Mouse",            qty: 38, amount: "15,960" },
  { code: "ITM002", name: "Zebronics Speaker",  qty: 32, amount: "15,360" },
  { code: "ITM010", name: "USB 32GB",            qty: 28, amount: "14,000" },
  { code: "ITM003", name: "HDMI Cable",         qty: 25, amount: "8,750"  },
];

const recentTx = [
  { date: "24-05-2025", type: "Sales",     voucher: "INV-000123", party: "ABC Traders",        amount: "18,500" },
  { date: "24-05-2025", type: "Purchase", voucher: "PUR-000067", party: "Super Distributors", amount: "12,450" },
  { date: "24-05-2025", type: "Payment",  voucher: "PAY-000045", party: "ABC Traders",        amount: "10,000" },
  { date: "23-05-2025", type: "Receipt",  voucher: "REC-000035", party: "XYZ Retailers",      amount: "15,000" },
  { date: "23-05-2025", type: "Sales",     voucher: "INV-000122", party: "LMN Enterprises",    amount: "7,650"  },
];


const quickLinks = [
  { label: "Sales Invoice",    icon: "invoice",  color: "#3b82f6" },
  { label: "Purchase Invoice", icon: "cart",     color: "#22c55e" },
  { label: "Item Master",      icon: "box",      color: "#a855f7" },
  { label: "Customer",         icon: "user",     color: "#06b6d4" },
  { label: "Supplier",         icon: "supplier", color: "#f59e0b" },
  { label: "Stock Report",     icon: "chart",    color: "#6366f1" },
  { label: "Day Book",         icon: "book",     color: "#ec4899" },
  { label: "Backup",           icon: "database", color: "#14b8a6" },
  { label: "Exit",             icon: "power",    color: "#ef4444" },
];

const typeColors = {
  Sales:    { bg: "#dcfce7", text: "#16a34a" },
  Purchase: { bg: "#dbeafe", text: "#1d4ed8" },
  Payment:  { bg: "#fce7f3", text: "#be185d" },
  Receipt:  { bg: "#fef3c7", text: "#b45309" },
};

// ─── Custom Tooltip Component ─────────────────────────────────────────────────
const CustomTooltip = ({ active, payload }) => {
  if (active && payload?.length) {
    return (
      <div className="chart-tooltip">
        <p className="tooltip-label">{payload[0].payload.date}</p>
        <p className="tooltip-value">₹ {payload[0].value.toLocaleString("en-IN")}</p>
      </div>
    );
  }
  return null;
};

// ─── Sidebar Component ────────────────────────────────────────────────────────

// ─── Header Component ─────────────────────────────────────────────────────────


// ─── Stat Card Component ──────────────────────────────────────────────────────
function StatCard({ title, value, sub, iconPath, colorClass, trend }) {
  return (
    <div className={`stat-card ${colorClass}`}>
      <div className="stat-top">
        <div className="stat-icon-wrap">
          <Icon d={iconPath} size={22} color="white" />
        </div>
        <span className="stat-badge">{sub}</span>
      </div>
      <div className="stat-value">₹ {value}</div>
      <div className="stat-title">{title}</div>
      {trend && (
        <div className="stat-trend">
          <Icon d={icons.arrow} size={13} />
          <span>{trend}</span>
        </div>
      )}
    </div>
  );
}

// ─── Main Dashboard Component ─────────────────────────────────────────────────
export default function Dashboard() {


  const [fy, setFy] = useState("2025-26");

 
  return (

    <MainLayout>
    
      

      
        

        <main className="content">
          <div className="page-header">
            <div>
              <h1 className="page-title">Dashboard</h1>
              <p className="page-sub">Financial overview &amp; quick access</p>
            </div>
            <select className="fy-select" value={fy} onChange={e => setFy(e.target.value)}>
              <option value="2025-26">FY 2025-26</option>
              <option value="2024-25">FY 2024-25</option>
            </select>
          </div>

          <div className="stat-grid">
            <StatCard title="Total Sales"      value="12,45,320.00" sub="This Month"  iconPath={icons.rupee}    colorClass="card-blue"   trend="View Details" />
            <StatCard title="Total Purchase"    value="7,65,430.00"  sub="This Month"  iconPath={icons.cart}     colorClass="card-green"  trend="View Details" />
            <StatCard title="Total Receivable"  value="2,48,750.00"  sub="Outstanding" iconPath={icons.users}    colorClass="card-orange" trend="View Details" />
            <StatCard title="Total Payable"     value="1,35,650.00"  sub="Outstanding" iconPath={icons.supplier} colorClass="card-red"    trend="View Details" />
            <StatCard title="Total Items"       value="1,245"        sub="In Stock"    iconPath={icons.box}      colorClass="card-purple" trend="View Details" />
          </div>

          <div className="charts-row">
            <div className="card chart-card">
              <div className="card-header">
                <h2 className="card-title">Sales Summary <span className="card-period">(This Month)</span></h2>
              </div>
              <div className="chart-wrap">
                <ResponsiveContainer width="100%" height={220}>
                  <LineChart data={salesData} margin={{ top: 5, right: 10, left: 0, bottom: 0 }}>
                    <CartesianGrid strokeDasharray="3 3" stroke="var(--grid)" />
                    <XAxis dataKey="date" tick={{ fontSize: 11, fill: "var(--muted)" }} tickLine={false} axisLine={false} />
                    <YAxis tickFormatter={v => `${v/1000}K`} tick={{ fontSize: 11, fill: "var(--muted)" }} tickLine={false} axisLine={false} width={40} />
                    <Tooltip content={<CustomTooltip />} />
                    <Line
                      type="monotone"
                      dataKey="amount"
                      stroke="url(#lineGrad)"
                      strokeWidth={2.5}
                      dot={{ r: 4, fill: "#3b82f6", strokeWidth: 2, stroke: "#fff" }}
                      activeDot={{ r: 6 }}
                    />
                    <defs>
                      <linearGradient id="lineGrad" x1="0" y1="0" x2="1" y2="0">
                        <stop offset="0%" stopColor="#6366f1" />
                        <stop offset="100%" stopColor="#3b82f6" />
                      </linearGradient>
                    </defs>
                  </LineChart>
                </ResponsiveContainer>
              </div>
            </div>

            <div className="card chart-card stock-card">
              <div className="card-header">
                <h2 className="card-title">Stock Summary</h2>
              </div>
              <div className="stock-pie-wrap">
                <PieChart width={180} height={180}>
                  <Pie data={stockData} cx={85} cy={85} innerRadius={52} outerRadius={80} paddingAngle={3} dataKey="value">
                    {stockData.map((entry, i) => <Cell key={i} fill={entry.color} />)}
                  </Pie>
                </PieChart>
                <div className="stock-legend">
                  {stockData.map((d) => (
                    <div key={d.name} className="legend-item">
                      <span className="legend-dot" style={{ background: d.color }} />
                      <div>
                        <div className="legend-name">{d.name}</div>
                        <div className="legend-val">{d.value.toLocaleString()} <span className="legend-pct">({((d.value/1245)*100).toFixed(1)}%)</span></div>
                      </div>
                    </div>
                  ))}
                </div>
              </div>
            </div>
          </div>

          <div className="tables-row">
            <div className="card">
              <div className="card-header">
                <h2 className="card-title">Top Selling Items <span className="card-period">(This Month)</span></h2>
              </div>
              <div className="table-wrap">
                <table className="data-table">
                  <thead>
                    <tr>
                      <th>Code</th>
                      <th>Item Name</th>
                      <th className="text-right">Qty</th>
                      <th className="text-right">Amount (₹)</th>
                    </tr>
                  </thead>
                  <tbody>
                    {topItems.map((item, i) => (
                      <tr key={item.code}>
                        <td><span className="rank-badge">{i+1}</span> {item.code}</td>
                        <td>{item.name}</td>
                        <td className="text-right"><span className="qty-pill">{item.qty}</span></td>
                        <td className="text-right amount-cell">{item.amount}</td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            </div>

            <div className="card">
              <div className="card-header">
                <h2 className="card-title">Recent Transactions</h2>
              </div>
              <div className="table-wrap">
                <table className="data-table">
                  <thead>
                    <tr>
                      <th>Date</th>
                      <th>Type</th>
                      <th>Voucher</th>
                      <th>Party</th>
                      <th className="text-right">Amount (₹)</th>
                    </tr>
                  </thead>
                  <tbody>
                    {recentTx.map((tx, i) => (
                      <tr key={i}>
                        <td className="tx-date">{tx.date}</td>
                        <td>
                          <span className="type-badge" style={{
                            background: typeColors[tx.type]?.bg,
                            color: typeColors[tx.type]?.text,
                          }}>{tx.type}</span>
                        </td>
                        <td className="voucher-code">{tx.voucher}</td>
                        <td>{tx.party}</td>
                        <td className="text-right amount-cell">{tx.amount}</td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            </div>
          </div>

          <div className="bottom-row">
            <div className="card cash-card">
              <div className="card-header">
                <h2 className="card-title">Cash Flow <span className="card-period">(This Month)</span></h2>
              </div>
              <div className="cash-rows">
                <div className="cash-row">
                  <span>Opening Balance</span>
                  <span className="cash-val">₹ 1,25,000.00</span>
                </div>
                <div className="cash-row">
                  <span>Cash In (Sales)</span>
                  <span className="cash-val positive">₹ 12,45,320.00</span>
                </div>
                <div className="cash-row">
                  <span>Cash Out (Purchase)</span>
                  <span className="cash-val negative">₹ 7,65,430.00</span>
                </div>
                <div className="cash-divider" />
                <div className="cash-row closing">
                  <span>Closing Balance</span>
                  <span className="cash-val highlight">₹ 5,29,890.00</span>
                </div>
              </div>
            </div>

            <div className="card alerts-card">
              <div className="card-header">
                <h2 className="card-title">Alerts</h2>
              </div>
              <div className="alerts-list">
                <div className="alert-item alert-warn">
                  <div className="alert-icon"><Icon d={icons.warning} size={18} color="#b45309" /></div>
                  <div>
                    <div className="alert-title">Low Stock Alert</div>
                    <div className="alert-msg">120 items are running low in stock.</div>
                  </div>
                </div>
                <div className="alert-item alert-error">
                  <div className="alert-icon"><Icon d={icons.error} size={18} color="#dc2626" /></div>
                  <div>
                    <div className="alert-title">Out of Stock Alert</div>
                    <div className="alert-msg">80 items are out of stock.</div>
                  </div>
                </div>
                <div className="alert-item alert-info">
                  <div className="alert-icon"><Icon d={icons.info} size={18} color="#0369a1" /></div>
                  <div>
                    <div className="alert-title">Backup Status</div>
                    <div className="alert-msg">Last backup taken on 24-05-2025 10:30 AM</div>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <div className="card quick-links-card">
            <div className="card-header">
              <h2 className="card-title">Quick Access</h2>
            </div>
            <div className="quick-links-grid">
              {quickLinks.map((link) => (
                <button key={link.label} className="quick-link-btn">
                  <div className="quick-icon" style={{ background: link.color + "22", color: link.color }}>
                    <Icon d={icons[link.icon]} size={22} color={link.color} />
                  </div>
                  <span className="quick-label">{link.label}</span>
                </button>
              ))}
            </div>
          </div>
        </main>

        
      
    


</MainLayout>
  );
}