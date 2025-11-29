import { useState } from "react";

import Button from "../components/Button";
import InputField from "../components/InputField";
import LabelForm from "../components/labelForm";
import api from "../clients/api";



function Register(){
    const [userName, setUserName] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [error, setError] = useState("");

    const handleRegister = async (event: React.FormEvent ) : Promise<void> => {
        event.preventDefault();

        if (!userName || !email || !password || !confirmPassword){
            setError("All fields are required.");
            return;  
        }

        if (password !== confirmPassword) {
            setError("Passwords do not match.");
            return;
        }

        try {
            const response = await api.post("/auth/register", {
                userName,
                email,
                password
            });

            const token = response.data.token; 
            localStorage.setItem("token", token); 

            alert("Registration successful.")
        } 
        catch (err: any) {
            setError(err.response?.data.error || "Registration failed."); 
        }
    }; 

    return(
        <div className="min-h-screen flex items-center justify-center px-4 bg-white dark:bg-gray-900">
            <form 
              onSubmit={handleRegister}
              className="w-auto max-w-md p-8 rounded-xl shadow-md bg-gray-100 dark:bg-gray-800"
            >
                <div className="text-center text-black dark:text-white">
                    <LabelForm title="Please Register"/>
                </div>

                <InputField 
                  label="Username"
                  id="username"
                  type="username"
                  value={userName}
                  onChange={(event) => setUserName(event.target.value)}
                />
                <InputField 
                  label="Email"
                  id="email"
                  type="email"
                  value={email}
                  onChange={(event) => setEmail(event.target.value)}
                />
                <InputField 
                  label="Password"
                  id="password"
                  type="password"
                  value={password}
                  onChange={(event) => setPassword(event.target.value)}
                />
                <InputField 
                  label="Confirm password"
                  id="confirmpassword"
                  type="password"
                  value={confirmPassword}
                  onChange={(event) => setConfirmPassword(event.target.value)}
                />

                <Button text="Register"/>
            </form>
        </div>
    );
}

export default Register; 