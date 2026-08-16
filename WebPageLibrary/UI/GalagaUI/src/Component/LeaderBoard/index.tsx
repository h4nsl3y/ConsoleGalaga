import Grid from "@/Component/Grid";
import { useEffect, useState } from "react";
import { LeaderBoardService } from "@/Service/LeaderBoardService/LeaderboardService";
import type { IPagination } from "@/Model/Pagination/IPagination"
import type { IRecord } from "@/Model/Record/IRecord";

const pagination: IPagination = { page: 0, limit: 10 };

const LeaderBoard = () => {
    const [title, setTitle] = useState<string[]>([]);
    const [records, setRecords] = useState<IRecord[]>([]);

    useEffect(() => {
        const fetchData = async () => {
            const leaderboardService = new LeaderBoardService();
            const data = await leaderboardService.getLeaderboardRecords(pagination);
            setTitle(data.titles ?? []);
            setRecords(data.records ?? []);
        };
        fetchData();
    }, []);

    return (
        <>
            <div className={` flex flex-1 items-center justify-center flex-col text-center`}>
                <h1>Leaderboard</h1>
                <p>Get ready to defend your galaxy from the invading forces!</p>
            </div>
            <div className={`w-full h-full flex flex-5 items-center justify-center flex-col `}>
                <Grid titles={title} gridRecords={records} />
            </div>
        </>
        )
    }

export default LeaderBoard;