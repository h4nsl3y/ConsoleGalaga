import Grid from "@/Component/Grid";

import { useEffect, useState } from "react";
import type { IPagination } from "@/Model/Pagination/IPagination";
import type { IPlayer } from "@/Model/Player/IPlayer";
import { PlayerService } from "@/Service/PlayerService/PlayerService";

const pagination: IPagination = { page: 0, limit: 10 };

const AdminControlBoard = () => {

    const [titles, setTitles] = useState<string[]>([]);
    const [records, setRecords] = useState<IPlayer[]>([]);

    useEffect(() => {
        const fetchData = async () => {
            const playerService = new PlayerService();
            const data = await playerService.getAllPlayers(pagination);
            setTitles(data.titles ?? [] as string[]);
            setRecords(data.records ?? []);
        };
        fetchData();
    }, []);

    return (
            <div className = "w-full h-full flex rounded glass">
                <div className = "flex-2 flex-col m-4 rounded glass">
                    <Grid titles={titles} gridRecords={records} checkboxValue="PlayerId" />
                </div>    
                <div className="flex-1 m-4 rounded glass">
                    <h1>Data</h1>
                </div>
            </div>
    )
}

export default AdminControlBoard;