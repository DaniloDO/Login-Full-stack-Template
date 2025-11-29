import { useState } from "react";
import { Link } from "react-router-dom";

import InputField from "../components/InputField";
import LabelForm from "../components/labelForm";
import Button from "../components/Button";
import api from "../clients/api";

function Login(){

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");

    const handleLogin = async(event: React.FormEvent) : Promise<void> => {
        event.preventDefault();

        if (!email || !password){
            setError("Please send email and password.")
            return; 
        } 

        setError("");
        console.log(`Logged in with: ${email} ${password}`);

        try {
            const response = await api.post("/auth/login", {
                email,
                password
            });
            
            const token = response.data.token; 
            localStorage.setItem("token", token);

            alert("Login successful.");
        } 
        catch (err: any) {
            setError(err.response?.data.error || "Login failed");
        }

        alert("Login successfully (simulated)")
        
    };


    return(
        <>
            <div className="min-h-screen bg-white dark:bg-gray-900 flex items-center justify-center px-4">
                <form
                  onSubmit={handleLogin}
                  className="bg-gray-100 dark:bg-gray-800 shadow-md rounded-xl p-8 w-auto max-w-md"
                >
                    <div className="text-black text-center dark:text-white">
                        <LabelForm title="Welcome" />
                    </div>

                    {/* {error && <div className="p-2 mb-4 rounded text-sm bg-red-100 text-red-700">{error}</div>} */}

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

                    <p className="my-6 text-sm text-center text-black dark:text-white">
                        Don't have an account?{" "}
                        <Link 
                          to="/register"
                          className="font-medium text-blue-600 dark:text-blue-400 hover:underline"
                        >
                            Register here
                        </Link>
                    </p>

                    <Button text="Sign in"/>

                </form>
            </div>
        </>
    ); 
}

export default Login; 