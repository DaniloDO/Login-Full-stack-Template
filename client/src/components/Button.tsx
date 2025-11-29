
interface ButtonText{
    text: string
}

function Button({text} : ButtonText) {
    return (
        <>
            <button
              type="submit"
              className="w-full py-2 rounded-md text-white bg-blue-500 hover:bg-blue-700 transition"
            >
                {text}
            </button>
        </>
    );
}

export default Button; 