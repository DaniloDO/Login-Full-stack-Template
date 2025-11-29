import { useState, useEffect } from "react";

import MessageDisplay from "../components/MessageDisplay";
import api from "../clients/api";

function Access(){
    const [message, setMessage] = useState("");
    const [error, setError ] = useState("");

    useEffect(() => {
        const getProtectedData = async () => {
            try {
                const token = localStorage.getItem("token");
                if (!token){
                    setError("No token found. Please login first.")
                    return; 
                }

                const response = await api.get("/auth/access", 
                    {
                        headers: {
                            Authorization: `Bearer ${token}`
                        }
                    }
                );
                console.log(response); 
                setMessage(`${response.data}`); 

            } 
            catch (err: any) {
                if(err.response?.status == 401) {
                    setError("Unauthorized. Your token is invalid or expired.");
                }

                else{
                    setError("Error loading protected data.");
                }
            }        
        }

        getProtectedData();
    }, []);

    return(
        <div className="min-h-screen flex items-center justify-center px-4 bg-white dark:bg-gray-900">
            <div className="shadow-md rounded-xl p-8 max-w-md text-center bg-gray-100 dark:bg-gray-800">
                <MessageDisplay title="User permission level" message={message} error={error} />
            </div>
        </div>
    );
};

export default Access; 