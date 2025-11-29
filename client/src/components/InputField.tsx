
interface InputFieldProps{
    label: string,
    id: string,
    type?: string,
    value: string,
    onChange: (event: React.ChangeEvent<HTMLInputElement>) => void
};

function InputField({ label, id, type, value, onChange }: InputFieldProps){

    return (
        <div className="mb-4">
            <label htmlFor={id} className="mb-1 block text-sm font-medium text-gray-700 dark:text-gray-200">
                {label}
            </label>
            <input 
                type={type}
                value={value}
                onChange={onChange}
                required
                className="w-full rounded-md border border-gray-300 px-3 py-2 text-gray-700 dark:text-gray-200"
            />
        </div>
    );
}; 

export default InputField; 