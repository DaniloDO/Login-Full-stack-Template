import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import { BrowserRouter, Routes, Route } from 'react-router-dom'

import App from './App.tsx'
import Login from './pages/Login.tsx'
import Register from './pages/Register.tsx'
import Access from './pages/Access.tsx'
import { ThemeContextProvider } from './context/ThemeContextProvider.tsx'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <ThemeContextProvider>
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<App />}>
          <Route index element={<Login />}/>
          <Route path="login" element={<Login />}/>
          <Route path="register" element={<Register />}/>
          <Route path="access" element={<Access />} />
        </Route>
      </Routes>
    </BrowserRouter>
    </ThemeContextProvider>
  </StrictMode>,
)
