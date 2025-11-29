
import { createContext, useContext } from "react";
import { useState, useEffect } from "react";
import { type ReactNode } from "react";

type Theme = "light" | "dark"; 

const ThemeContext = createContext<{
    theme: Theme,
    setTheme: (theme: Theme) => void
} | undefined>(undefined)

export function ThemeContextProvider({ children } : { children: ReactNode}){
    const [theme, setTheme] = useState<Theme>(() => {

        const savedTheme = localStorage.getItem("theme"); 
        if (savedTheme === "light" || savedTheme === "dark"){
            return savedTheme; 
        }

        return "dark"
    }); 

    useEffect(() => {
        console.log("Theme applied:", theme);
        localStorage.setItem("theme", theme);

        document.documentElement.classList.remove("light", "dark");
        document.documentElement.classList.add(theme); 

    }, [theme]);

    return(
        <ThemeContext.Provider value={{theme, setTheme}}>
            {children}
        </ThemeContext.Provider>
    ); 
}

export function useTheme(){
    const context = useContext(ThemeContext);

    if(!context){
        throw new Error('useTheme must be used within a ThemeProvider');
    }

    return context; 
};
