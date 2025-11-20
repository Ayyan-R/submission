import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { HashRouter } from 'react-router-dom'
import './index.css'
import App from './App.jsx'
import { AuthProvider } from './context/AuthContext.jsx'
import { BankingDataProvider } from './context/BankingDataContext.jsx'

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <HashRouter>
      <AuthProvider>
        <BankingDataProvider>
          <App />
        </BankingDataProvider>
      </AuthProvider>
    </HashRouter>
  </StrictMode>
)
