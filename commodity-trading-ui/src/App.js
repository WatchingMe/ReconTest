import React from 'react';
import './App.css';
import ReconciliationReport from './components/ReconciliationReport';
import CalculateReconciliation from './components/CalculateReconciliation';

function App() {
    return (
        <div className="App">
            <header className="App-header">
                <h1>Commodity Trading Reconciliation</h1>
            </header>
            <CalculateReconciliation />
            <ReconciliationReport />
        </div>
    );
}

export default App;
