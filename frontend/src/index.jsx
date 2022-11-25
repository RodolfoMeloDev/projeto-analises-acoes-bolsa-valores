import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';

import Menu from './components/menu/Menu';
import { BrowserRouter as Router } from 'react-router-dom';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <Router>
      <Menu />
      <div className='container'>
        <App/>
      </div>
    </Router>  
  </React.StrictMode>
);