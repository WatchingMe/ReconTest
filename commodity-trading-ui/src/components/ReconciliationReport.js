import React, { useEffect, useState } from 'react';
import axios from 'axios';

const ReconciliationReport = () => {
    const [report, setReport] = useState([]);

    useEffect(() => {
        axios.get('http://localhost:5000/reconciliation')
            .then(response => setReport(response.data))
            .catch(error => console.error(error));
    }, []);

    return (
        <div>
            <h1>Reconciliation Report</h1>
            <table>
                <thead>
                    <tr>
                        <th>Supplier Name</th>
                        <th>Amount in CTRM</th>
                        <th>Amount in JDE</th>
                        <th>Expected Loss</th>
                        <th>Net Exposure</th>
                    </tr>
                </thead>
                <tbody>
                    {report.map((item, index) => (
                        <tr key={index}>
                            <td>{item.supplierName}</td>
                            <td>{item.amountInCtrm}</td>
                            <td>{item.amountInJde}</td>
                            <td>{item.expectedLoss}</td>
                            <td>{item.netExposure}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default ReconciliationReport;
