    interface GridProps<T> {
        titles: string[];
        gridRecords: T[];
        checkboxValue?: string;
    }

const Grid = <T extends object,>({ titles, gridRecords, checkboxValue = "" }: GridProps<T>) => {
    console.log("Grid Rendered with titles: ", titles);
    const colIndex = titles.findIndex((title) => title === checkboxValue);
    return (
        <div className={`w-full h-full items-center relative glass rounded`}>
            <table className=" flex flex-col w-full">
                <thead>
                    <tr className="w-full flex items-center justify-space-around border-b-2 border-b-(--primary-color)">
                        {titles.map((element, index) => {
                            const isCheckboxColumn = index === colIndex;

                            return (
                                <th
                                    key={`${element}-${index}`}
                                    className={`m-3 ${isCheckboxColumn ? "flex-1" : "flex-3"}`}
                                >
                                    {!isCheckboxColumn && <p>{element}</p>}
                                </th>
                            );
                        })}
                    </tr>
                </thead>
                <tbody className="overflow-y-auto">
                    {
                        gridRecords.map((element, index) => {
                            const values = Object.values(element);
                            return(
                                <tr key={index} className="w-full flex items-center justify-center text-center">
                                   
                                    {values.map((value, valueIndex) => {
                                        const isCheckboxColumn = valueIndex === colIndex;

                                        return (
                                            <td key={valueIndex} className={`m-3 ${isCheckboxColumn ? "flex-1" : "flex-3"}`}>
                                                {
                                                    isCheckboxColumn 
                                                    ? (<input type="checkbox" value={String(value)} />) 
                                                    : ( <p>{String(value)}</p> )
                                                }
                                            </td>
                                        );
                                    })}
                                </tr>
                            )
                        })
                    }
                </tbody>
            </table>
        </div>
    )
}

export default Grid;