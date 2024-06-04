import React from 'react';
import axios from 'axios';

const CalculateReconciliation = () => {
    const handleCalculate = () => {
        axios.post('http://localhost:5000/reconciliation')
            .then(response => {
                alert('Reconciliation calculation completed successfully.');
            })
            .catch(error => {
                console.error(error);
                alert('Failed to calculate reconciliation.');
            });
    };

    return (
        <div>
            <button onClick={handleCalculate}>Calculate Reconciliation</button>
        </div>
    );
};

export default CalculateReconciliation;
