interface MessageProps{
    title: string,
    message: string,
    error: string
};

function MessageDisplay({ title, message, error } : MessageProps)
{
    return(
        <div>
            <h1 className="font-bold text-xl mb-4 text-black dark:text-white">{title}</h1>
            {message && (
                <p className="text-green-600 dark:text-green-400">{message}</p>
            )}

            {error && (
                <p className="text-red-600 dark:text-red-400">{error}</p>
            )} 
        </div>
    );
}

export default MessageDisplay; 