import { Navbar, NavbarCollapse, NavbarToggle, NavbarLink, NavbarBrand } from "flowbite-react";
import { Link } from "react-router-dom";

function CustomNavbar()
{
  return( 
    <div className="bg-gray-500">
      <Navbar fluid className="bg-gray-200">
        <NavbarBrand>
          <img src="/favicon.svg" className="mr-3 h-6 sm:h-9" />
          <span className="self-center whitespace-nowrap text-xl font-semibold dark:text-white">LoginApp</span>
        </NavbarBrand>
        <NavbarToggle />
        <NavbarCollapse>
          <NavbarLink href="/">Home</NavbarLink>
          <NavbarLink href="/login">Login</NavbarLink>
          <NavbarLink href="/register">Register</NavbarLink>
          <NavbarLink href="/access">Access</NavbarLink>
        </NavbarCollapse>
      </Navbar>
    </div>   
)
}

export default CustomNavbar; 