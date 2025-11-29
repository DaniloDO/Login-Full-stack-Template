
interface LabelFormProps{
    title: string
};

function LabelForm({title} : LabelFormProps)
{
    return(
        <>
            <h2 className="font-semibold">{title}</h2>
        </>
    );
}

export default LabelForm; 