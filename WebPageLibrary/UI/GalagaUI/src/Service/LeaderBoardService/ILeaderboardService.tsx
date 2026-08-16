import type { IPagination } from "@/Model/Pagination/IPagination"
import type { IGridData } from "src/Model/GridData/IGridData";
import type { IRecord } from "src/Model/Record/IRecord";


export interface ILeaderBoardService {
    getLeaderboardRecords({page, limit } : IPagination): Promise<IGridData<IRecord>>;
}
