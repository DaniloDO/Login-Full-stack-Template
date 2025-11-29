import { useState } from 'react'

import CustomNavbar from './components/CustomNavbar'
import { DarkThemeToggle } from 'flowbite-react'

import './App.css'
import Login from './pages/Login'
import CustomFooter from './components/CustomFooter'
import { Outlet } from 'react-router-dom'
import ThemeToggle from './components/ThemeToggle'

function App() {

  return (
    <>
      <CustomNavbar />
      <div className="flex justify-end p-4 bg-white dark:bg-gray-900">
        <ThemeToggle />
      </div>
      <Outlet />
      <CustomFooter />
    </>
  )
}

export default App
