import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import reportWebVitals from './reportWebVitals';

// Import the main App (which already has RouterApp inside)
import App from './App';

// Redux store (optional — only if you use Redux)
import { Provider } from 'react-redux';
import store from './app/store';

// Create the root element
const root = ReactDOM.createRoot(document.getElementById('root'));

// Render
root.render(
  <React.StrictMode>
    {/* ✅ Wrap in Provider only if you are using Redux in your components */}
    <Provider store={store}>
      <App />
    </Provider>
  </React.StrictMode>
);

// Performance logging (optional)
reportWebVitals();
