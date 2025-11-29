import { useTheme } from "../context/ThemeContextProvider";

function ThemeToggle(){
    const { theme, setTheme } = useTheme();

    const toggleTheme = () => {
        setTheme(theme === "dark"? "light": "dark");
    };

    return(
        <button
            onClick={toggleTheme}
             className="px-4 py-2 rounded-lg bg-gray-200 dark:bg-gray-700 text-black dark:text-white"
        >
            {theme === "dark" ? "Light Mode" : "Dark Mode"}
        </button>
    )
};

export default ThemeToggle; 